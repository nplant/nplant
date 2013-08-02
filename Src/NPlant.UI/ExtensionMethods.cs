using System;
using System.ComponentModel;
using System.IO;

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

    public static bool IsSelf(this string s)
    {
        if (s == null)
            return false;

        return s.EndsWith("NPlant.UI.exe", StringComparison.InvariantCultureIgnoreCase);
    }
}