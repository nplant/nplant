using System;
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

            Application.Run(new MainScreen());
        }
    }
}
