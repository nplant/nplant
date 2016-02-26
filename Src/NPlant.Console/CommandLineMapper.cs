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
                    throw new ConsoleUsageException($"Arguments are expected to be in the --foo or --foo:value or --foo:\"value\" format.  Argument '{argName}' could not be parsed.");

                argName = argName.Substring(2);
                var property = properties.FirstOrDefault(x => string.Equals(x.Name, argName, StringComparison.InvariantCultureIgnoreCase));

                if (property == null)
                    throw new ConsoleException($"Argument '{argName}' is not recognized.");

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
                        throw new ConsoleUsageException($"Argument '--{argName}' has an invalid value '{argValue}'");
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
                    throw new ConsoleUsageException($"Expected argument '--{property.Name.ToLower()}', but was not received");
            }
        }
    }
}