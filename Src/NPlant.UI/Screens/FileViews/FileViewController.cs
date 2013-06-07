using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NPlant.UI.Screens.FileViews
{
    public abstract class FileViewController
    {
        private readonly IFileView _view;

        protected FileViewController(IFileView view)
        {
            _view = view;
        }

        public void Generate(bool async = true)
        {
            var filePath = GetFilePath();
            var model = ImageFileGenerationModel.Create(filePath);

            if (!File.Exists(filePath))
                return;

            if (async)
            {
                var backgroundWorker = new BackgroundWorker {WorkerReportsProgress = true};
                backgroundWorker.DoWork += DoGeneration;
                backgroundWorker.ProgressChanged += OnGenerationProgressChanged;
                backgroundWorker.RunWorkerAsync(model);
            }
            else
            {
                DoGeneration(this, new DoWorkEventArgs(model));
            }
        }

        protected abstract string GetFilePath();

        private void DoGeneration(object sender, DoWorkEventArgs e)
        {
            ImageFileGenerationModel model = (ImageFileGenerationModel)e.Argument;

            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgressSafely(25);

            Process process = new Process {
                StartInfo = {
                    FileName = model.JavaPath,
                    Arguments = model.GetJavaArguments(),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };

            worker.ReportProgressSafely(50);

            bool started = process.Start();

            if (!started)
            {
                EventDispatcher.Raise(new UserNotificationEvent("Failed to start plantuml.jar"));
            }
            else
            {
                worker.ReportProgressSafely(75);

                process.WaitForExit(2000);

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("PlantUML.jar Invoked:");
                builder.AppendLine("    {0} {1}".FormatWith(process.StartInfo.FileName, process.StartInfo.Arguments));

                var exitCode = process.HasExited ? process.ExitCode.ToString() : "?";
                
                builder.AppendLine("Exit Code: {0}".FormatWith(exitCode));

                builder.AppendLine("NPlant File (exists: {0}): {1}".FormatWith(File.Exists(model.NPlantFilePath), model.NPlantFilePath));

                builder.AppendLine("Image File (exists: {0}): {1}".FormatWith(File.Exists(model.FinalFilePath), model.FinalFilePath));

                worker.ReportProgressSafely(90, new UserNotificationEvent(builder.ToString()));
            }
        }

        private void OnGenerationProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _view.Progress = e.ProgressPercentage;

            UserNotificationEvent notification = e.UserState as UserNotificationEvent;

            if (notification != null)
            {
                EventDispatcher.Raise(notification);
            }
        }
    }
}