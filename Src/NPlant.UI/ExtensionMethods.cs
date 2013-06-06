using System;
using System.ComponentModel;
using System.IO;

public static class ExtensionMethods
{
    public static void ReportProgressSafely(this BackgroundWorker worker, int progress, object userState = null)
    {
        if (worker != null)
        {
            // this probably isn't necessary, but i don't have time to test it... go w/ the fear route for now
            if (userState == null)
                worker.ReportProgress(progress);
            else
                worker.ReportProgress(progress, userState);
        }
    }

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