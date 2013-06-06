using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NPlant.UI
{
    public class NPlantFileViewController
    {
        private readonly INPlantFileView _view;
        private readonly string _filePath;

        public NPlantFileViewController(INPlantFileView view, string filePath)
        {
            _view = view;
            _filePath = filePath;
        }

        public void Start()
        {
            _view.DiagramTextChanged(OnDiagramTextChanged);

            string text = null;

            if (File.Exists(_filePath))
                text = File.ReadAllText(_filePath);

            _view.DiagramText = text;
        }

        protected void OnDiagramTextChanged(string text)
        {
            this.Save();
            this.Generate();
        }

        public void Save()
        {
            File.WriteAllText(_filePath, _view.DiagramText);
        }

        public void Copy()
        {
            Clipboard.SetText(_view.DiagramText);
        }

        public void Generate(bool async = true)
        {
            if (async)
            {
                BackgroundWorker backgroundWorker = new BackgroundWorker();
                backgroundWorker.WorkerReportsProgress = true;
                backgroundWorker.DoWork += DoGeneration;
                backgroundWorker.ProgressChanged += OnGenerationProgressChanged;
                backgroundWorker.RunWorkerAsync();
            }
            else
            {
                DoGeneration(this, new DoWorkEventArgs(null));
            }
        }

        private void DoGeneration(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgressSafely(10);

            Process process = new Process();
            process.StartInfo.FileName = "java.exe";
            process.StartInfo.Arguments = "-jar \"{0}\\plantuml.jar\" \"{1}\" -o {2}".FormatWith(SystemEnvironment.ExecutionDirectory, _filePath, Path.GetDirectoryName(_filePath));
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

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

                EventDispatcher.Raise(new UserNotificationEvent(builder.ToString()));

                worker.ReportProgressSafely(100);
            }
        }

        private void OnGenerationProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _view.Progress = e.ProgressPercentage;
        }
    }
}