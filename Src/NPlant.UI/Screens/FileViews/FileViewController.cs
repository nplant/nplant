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

            if (!File.Exists(filePath))
                return;

            if (async)
            {
                var backgroundWorker = new BackgroundWorker {WorkerReportsProgress = true};
                backgroundWorker.DoWork += DoGeneration;
                backgroundWorker.ProgressChanged += OnGenerationProgressChanged;
                backgroundWorker.RunWorkerAsync(filePath);
            }
            else
            {
                DoGeneration(this, new DoWorkEventArgs(filePath));
            }
        }


        protected abstract string GetFilePath();

        private void DoGeneration(object sender, DoWorkEventArgs e)
        {
            string filePath = (string) e.Argument;

            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgressSafely(10);

            Process process = new Process {
                StartInfo = {
                    FileName = "java.exe",
                    Arguments = "-jar \"{0}\\plantuml.jar\" \"{1}\" -o {2}".FormatWith(SystemEnvironment.ExecutionDirectory, filePath, Path.GetDirectoryName(filePath)),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                }
            };

            worker.ReportProgressSafely(20);

            bool started = process.Start();

            if (!started)
            {
                EventDispatcher.Raise(new UserNotificationEvent("Failed to start plantuml.jar"));
            }
            else
            {
                worker.ReportProgressSafely(50);
                process.WaitForExit(1000);

                worker.ReportProgressSafely(70);

                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();

                worker.ReportProgressSafely(80);

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("PlantUML.jar Invoked...");

                builder.AppendLine("Standard Output: ");
                builder.AppendLine(output);
                builder.AppendLine("Standard Error Out: ");
                builder.AppendLine(error);

                worker.ReportProgressSafely(90);

                worker.ReportProgressSafely(100, new UserNotificationEvent(builder.ToString()));
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