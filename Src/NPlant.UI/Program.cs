using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace NPlant.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += (sender, args) =>
                {
                    if (args.Exception != null)
                    {
                        var @event = new UserNotificationEvent(args.Exception.Message, UserNotificationType.Error);
                        EventDispatcher.Raise(@event);
                    }
                };

            UnpackPlantUML();

            Application.Run(new MainScreen());
        }

        private static void UnpackPlantUML()
        {
            try
            {
                string jarPath = Path.Combine(SystemEnvironment.ExecutionDirectory, "plantuml.jar");

                if (! File.Exists(jarPath))
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();

                    using (Stream input = assembly.GetManifestResourceStream("NPlant.UI.plantuml.jar"))
                    using (Stream output = File.Create(jarPath))
                    {
                        CopyStream(input, output);
                    }
                }
            }
            catch (Exception exception)
            {
                EventDispatcher.Raise(new UserNotificationEvent("Error occurred while trying to unpack the plantuml.jar file", exception));
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8192];

            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }
    }
}
