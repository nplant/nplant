using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using NPlant.Console.Exceptions;

namespace NPlant.Console
{
    public static class CommandLineMapper
    {
        public static void Map<T>(T instance, string[] args)
        {
            var properties = new HashSet<PropertyInfo>(typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public));

            foreach (var arg in args)
            {
                var argParts = arg.Split(':');

                string argName = argParts.First();
                string argValue = null;

                if(argParts.Length > 1)
                    argValue = string.Join(":", argParts.Where((argPart, index) => index > 0));

                if (!argName.StartsWith("--"))
                    throw new ConsoleUsageException(string.Format("Arguments are expected to be in the --foo or --foo:value or --foo:\"value\" format.  Argument '{0}' could not be parsed.", argName));

                argName = argName.Substring(2);
                var property = properties.FirstOrDefault(x => string.Equals(x.Name, argName, StringComparison.InvariantCultureIgnoreCase));

                if (property == null)
                    throw new ConsoleException(string.Format("Argument '{0}' is not recognized.", argName));

                if (argValue != null)
                {
                    var converter = TypeDescriptor.GetConverter(property.PropertyType);

                    try
                    {
                        if(argValue.StartsWith("\"") && argValue.EndsWith("\""))
                            argValue = argValue.Substring(1, argValue.Length - 2);

                        property.SetValue(instance, converter.ConvertFromInvariantString(argValue), null);
                    }
                    catch
                    {
                        throw new ConsoleUsageException(string.Format("Argument '--{0}' has an invalid value '{1}'", argName, argValue));
                    }

                    properties.Remove(property);
                }
                else
                {
                    if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                        property.SetValue(instance, true, null);
                }
            }

            foreach (var property in properties)
            {
                if (property.HasAttribute<RequiredArgumentAttribute>())
                    throw new ConsoleUsageException(string.Format("Expected argument '--{0}', but was not received", property.Name.ToLower()));
            }
        }
    }
}