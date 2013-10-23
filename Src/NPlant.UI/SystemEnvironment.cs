using System;
using System.IO;
using System.Windows.Forms;

namespace NPlant.UI
{
    public static class SystemEnvironment
    {
        public static string ExecutionDirectory
        {
            get { return Path.GetDirectoryName(Application.ExecutablePath); }
        }

        public static SystemSettings GetSettings()
        {
            string javaHome = Environment.GetEnvironmentVariable("NPLANT_JAVA_HOME", EnvironmentVariableTarget.User);

            if(javaHome.IsNullOrEmpty())
                javaHome = System.Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.User);

            return new SystemSettings()
                {
                    JavaPath  = javaHome
                };
        }

        public static void SetSettings(SystemSettings settings)
        {
            Environment.SetEnvironmentVariable("NPLANT_JAVA_HOME", settings.JavaPath, EnvironmentVariableTarget.User);
        }
    }

    public class SystemSettings
    {
        public string JavaPath { get; set; }
    }
}
