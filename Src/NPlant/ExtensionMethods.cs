using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using NPlant.Core;
using NPlant.MetaModel.ClassDiagraming;

public static class ExtensionMethods
{
    public static string GetFriendlyGenericName(this Type type)
    {
        if (type.IsGenericType)
        {
            StringBuilder buffer = new StringBuilder();

            buffer.Append(type.Name.SubstringTo("`"));

            buffer.Append("<");
            
            Type[] arguments = type.GetGenericArguments();

            for (int i = 0; i < arguments.Length; i++)
            {
                if (i != 0)
                    buffer.Append(",");

                var argument = arguments[i];
                buffer.Append(argument.Name);
            }

            buffer.Append(">");

            return buffer.ToString();
        }

        return type.Name;
    }

    public static string SubstringTo(this string source, string to)
    {
        if (source.IsNullOrEmpty() || to.IsNullOrEmpty())
            return source;

        int indexOf = source.IndexOf(to);

        if (indexOf == 0)
            return string.Empty;

        if (indexOf < 0)
            return source;

        return source.Substring(0, indexOf);
    }

    public static ReflectedClassDescriptor GetReflected(this Type type)
    {
        if (type.IsEnum)
            return new ReflectedEnumDescriptor(type);

        return new ReflectedClassDescriptor(type);
    }

    public static Type[] GetTypesExtending<T>(this Assembly assembly)
    {
        List<Type> result = new List<Type>();

        if (assembly == null)
            return result.ToArray();

        var types = assembly.GetTypes();

        result.AddRange(types.Where(type => typeof (T).IsAssignableFrom(type)));

        return result.ToArray();
    }

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

    public static T GetAttributeOf<T>(this Type type, bool inherit = false) where T : Attribute
    {
        var customAttributes = type.GetCustomAttributes(typeof(T), inherit);
        var array = customAttributes.Cast<T>().ToArray();

        if (array.Length < 1)
            return null;

        return array[0];
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
    
    public static T[] GetAttributesOf<T>(this FieldInfo field, bool inherit = false) where T : Attribute
    {
        var customAttributes = field.GetCustomAttributes(typeof(T), inherit);
        return customAttributes.Cast<T>().ToArray();
    }

    public static bool HasAttribute<T>(this PropertyInfo property, bool inherit = false) where T : Attribute
    {
        var attributes = property.GetAttributesOf<T>(inherit);

        return attributes != null && attributes.Length > 0;
    }
    
    public static bool HasAttribute<T>(this FieldInfo field, bool inherit = false) where T : Attribute
    {
        var attributes = field.GetAttributesOf<T>(inherit);

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

    private static readonly Assembly mscorelib = typeof(string).Assembly;

    public static bool IsMsCoreLibType(this Type type)
    {
        return type.Assembly == mscorelib;
    }

    public static bool IsEnumerable(this Type type)
    {
        return (typeof (IEnumerable).IsAssignableFrom(type));
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

    public static bool IsPublic(this MemberInfo info)
    {
        return AccessorEqualuation(info, field => field.IsPublic, method => method != null && method.IsPublic);
    }

    public static bool IsPrivate(this MemberInfo info)
    {
        return AccessorEqualuation(info, field => field.IsPrivate, method => method == null ||  method.IsPrivate);
    }

    public static bool IsInternal(this MemberInfo info)
    {
        return AccessorEqualuation(info, field => field.IsAssembly, method => method == null || method.IsAssembly);
    }

    public static bool IsProperty(this MethodInfo info)
    {
        if (info == null)
            return false;

        if (info.IsDefined(typeof(CompilerGeneratedAttribute), false))
        {
            if (info.Name.StartsWith("get_") || info.Name.StartsWith("set_"))
                return true;
        }

        return false;
    }
    private static bool AccessorEqualuation(MemberInfo info, Func<FieldInfo, bool> fieldProcessor, Func<MethodInfo, bool> propertyProcessor)
    {
        FieldInfo field = info as FieldInfo;

        if (field != null)
            return fieldProcessor(field);

        PropertyInfo property = info as PropertyInfo;

        if (property != null)
        {
            var getMethod = property.GetGetMethod();

            if (propertyProcessor(getMethod))
                return true;

            var setMethod = property.GetSetMethod();

            if (propertyProcessor(setMethod))
                return true;

            return false;
        }

        throw new NPlantException("Couldn't interpret MemberInfo instance - expected it to be a property or a field, but it was neither.  Declaring Type: {0}, Member: {1}".FormatWith(info.DeclaringType.FullName, info.Name));
    }

    public static bool StartsWithAVowel(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        return "aeiou".IndexOf(value[0].ToString(), StringComparison.InvariantCultureIgnoreCase) >= 0;
    }
}

public static class ImageExtensionMethods
{
    public static string SaveNPlantImage(this Image image, string dir, string file)
    {
        if (!Path.HasExtension(file) || Path.GetExtension(file) != ".png")
            file = Path.ChangeExtension(file, ".png");

        DirectoryInfo directory = new DirectoryInfo(dir);
        
        if (!directory.Exists)
            directory.Create();

        string path = Path.Combine(dir, file);

        if (File.Exists(path))
            File.Delete(path);

        image.Save(path);

        return path;
    }
}

public static class ExceptionExtensionMethods
{
    public static bool IsDontMessWithMeException(this Exception exception)
    {
        while (exception != null)
        {
            if (exception as OutOfMemoryException != null && exception as InsufficientMemoryException == null || exception as ThreadAbortException != null ||
                exception as AccessViolationException != null || exception as SEHException != null || exception as StackOverflowException != null)
            {
                return true;
            }
            
            if (exception as TypeInitializationException == null && exception as TargetInvocationException == null)
            {
                break;
            }

            exception = exception.InnerException;
        }

        return false;
    }
}
