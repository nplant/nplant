using System;
using System.IO;
using NPlant.Core;

public static class ExtensionMethods
{
    public static bool IsNPlantFilePath(this string file)
    {
        var ext = Path.GetExtension(file);

        if (string.Equals(ext, ".nplant", StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }

        return false;
    } 

    public static bool IsAssemblyFilePath(this string file)
    {
        var ext = Path.GetExtension(file);

        if (string.Equals(ext, ".dll", StringComparison.InvariantCultureIgnoreCase)
            || string.Equals(ext, ".exe", StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }

        return false;
    }

    public static bool IsVSHostExe(this string s)
    {
        if (s == null)
            return false;

        return s.EndsWith("NPlant.UI.vshost.exe", StringComparison.InvariantCultureIgnoreCase);
    }
}