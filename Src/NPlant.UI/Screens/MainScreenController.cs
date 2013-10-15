using System;
using NPlant.UI.Screens;

namespace NPlant.UI
{
    public class MainScreenController
    {
        private readonly IMainScreen _screen;
        private readonly CommandLineArguments _args;

        public MainScreenController(IMainScreen screen, CommandLineArguments args)
        {
            _screen = screen;
            _args = args;
        }

        public void Start()
        {
            if (_args.HasFilePath)
                OpenFile(_args.FilePath);
            else
                _screen.Title = "NPlant UI";

            EventDispatcher.Register<UserNotificationEvent>(LogUserNotificationToConsole);
        }

        private void LogUserNotificationToConsole(UserNotificationEvent @event)
        {
            if (@event != null)
                _screen.DisplayUserNotification(@event);
        }

        public void Stop(Action action)
        {
            action();
        }

        public void OpenFile(FileDialogResult result)
        {
            if (result.UserApproved)
            {
                this.OpenFile(result.FilePath);
            }
        }

        private void OpenFile(string filePath)
        {
            _screen.Title = "NPlant UI - {0}".FormatWith(filePath);

            if (filePath.IsNPlantFilePath())
                _screen.AddFileView(FileViewType.NPlantFile, filePath);
            else if (filePath.IsAssemblyFilePath())
                _screen.AddFileView(FileViewType.AssemblyFile, filePath);
        }
    }
}