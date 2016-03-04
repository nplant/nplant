using System;

namespace NPlant.Console
{
    public static class ConsoleEnvironment
    {
        public static string ExecutionDirectory = Environment.CurrentDirectory;

        public static SystemSettings GetSettings()
        {
            string javaHome = Environment.GetEnvironmentVariable("NPLANT_JAVA_HOME", EnvironmentVariableTarget.User);

            if (javaHome.IsNullOrEmpty())
                javaHome = System.Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.User);

            return new SystemSettings()
            {
                JavaPath = javaHome
            };
        }

        public static void SetSettings(SystemSettings settings)
        {
            Environment.SetEnvironmentVariable("NPLANT_JAVA_HOME", settings.JavaPath, EnvironmentVariableTarget.User);
        }
    }
}