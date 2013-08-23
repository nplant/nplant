using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

public static class ExtensionMethods
{
    public static bool IsNPlantFile(this FileInfo file)
    {
        if (file == null)
            return false;

        return string.Equals(file.Extension, ".nplant");
    }
    
    public static bool IsAssembly(this FileInfo file)
    {
        if (file == null)
            return false;

        return string.Equals(file.Extension, ".exe") || string.Equals(file.Extension, ".dll");
    }

    public static string ReplaceIllegalPathCharacters(this string path, char replacement)
    {
        if (path.IsNullOrEmpty())
            return path;

        char[] characters = Path.GetInvalidFileNameChars();

        path = characters.Aggregate(path, (current, character) => current.Replace(character, replacement));

        return path;
    }

    public static void EnsureExists(this DirectoryInfo dir)
    {
        if (! dir.Exists)
            dir.Create();
    }

    public static KnownTypeAttribute[] GetKnownTypes(this Type type, bool inherit = false)
    {
        if(type == null)
            return new KnownTypeAttribute[0];

        return type.GetAttributesOf<KnownTypeAttribute>(inherit);
    }

    public static T[] GetAttributesOf<T>(this Type type, bool inherit = false) where T : Attribute
    {
        var customAttributes = type.GetCustomAttributes(typeof(T), inherit);
        return customAttributes.Cast<T>().ToArray();
    }

    public static T[] GetAttributesOf<T>(this PropertyInfo property, bool inherit = false) where T : Attribute
    {
        var customAttributes = property.GetCustomAttributes(typeof(T), inherit);
        return customAttributes.Cast<T>().ToArray();
    }

    public static bool HasAttribute<T>(this PropertyInfo property, bool inherit = false) where T : Attribute
    {
        var attributes = property.GetAttributesOf<T>(inherit);

        return attributes != null && attributes.Length > 0;
    }

    public static bool HasAttribute<T>(this Type type, bool inherit = false) where T : Attribute
    {
        var attributes = type.GetAttributesOf<T>(inherit);

        return attributes != null && attributes.Length > 0;
    }

    public static bool IsIndexed(this PropertyInfo property)
    {
        var parameters = property.GetIndexParameters();
        return parameters.Length > 0;
    }

    public static bool IsString(this Type type)
    {
        return type == typeof (string);
    }

    public static Queue<string> ReadAllLines(this StringReader reader)
    {
        var lines = new Queue<string>();
        var line = reader.ReadLine();

        while (line != null)
        {
            lines.Enqueue(line);
            line = reader.ReadLine();
        }

        return lines;
    }

    public static string FormatWith(this string expression, params object[] args)
    {
        if (expression == null || args == null || args.Length < 1)
            return expression;

        return string.Format(expression, args);
    }

    public static T CheckForNull<T>(this T obj, Func<Exception> exceptionProvider = null) where T : class
    {
        if (exceptionProvider == null)
            exceptionProvider = () => new NullReferenceException(string.Format("The supplied {0} instace was null when null was not expected.", typeof (T).FullName));

        if (obj == null)
            throw exceptionProvider();

        return obj;
    }

    public static string CheckForNullOrEmptyArg(this string obj, string argumentName)
    {
        if(! string.IsNullOrEmpty(obj))
            return obj;

        throw new ArgumentNullException(argumentName);
    }

    public static T CheckForNullArg<T>(this T obj, string argumentName) where T : class
    {
        return CheckForNull(obj, () => new ArgumentNullException(argumentName));
    }

    public static string IfIsNullOrEmpty(this string strg, string other)
    {
        return string.IsNullOrEmpty(strg) ? other : strg;
    }

    public static bool IsNullOrEmpty(this string strg)
    {
        return string.IsNullOrEmpty(strg);
    }

    public static bool TryGetPublicParameterlessConstructor(this Type type, out ConstructorInfo info)
    {
        if (type != null)
        {
            var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

            foreach (var ctor in ctors)
            {
                if (ctor.GetParameters().Length == 0)
                {
                    info = ctor;
                    return true;
                }
            }
        }

        info = null;
        return false;
    }

    public static bool IsWithin(this int value, int lower, int upper)
    {
        return value >= lower && value <= upper;
    }

    public static Type GetEnumeratorType(this Type type)
    {
        if(!typeof (IEnumerable).IsAssignableFrom(type))
            throw new ArgumentException("Invalid GetEnumeratorType() usage - {0} must implement interface {1}.".FormatWith(type.FullName, typeof(IEnumerable).FullName));

        if (type.IsArray)
            return type.GetElementType();

        Type currentType = type;

        while (currentType != null && currentType != typeof(object) && typeof (IEnumerable).IsAssignableFrom(currentType))
        {
            if (currentType.IsGenericType)
            {
                Type[] genericArguments = currentType.GetGenericArguments();

                if(genericArguments.Length == 1)
                    return genericArguments[0];
            }

            currentType = currentType.BaseType;
        }

        return typeof (object);
    }

    public static bool ToBool(this string value, bool @default = false)
    {
        bool result;

        return bool.TryParse(value, out result) ? result : @default;
    }
}