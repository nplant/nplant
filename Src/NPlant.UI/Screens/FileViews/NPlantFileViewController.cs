using System.Diagnostics;
using System.IO;
using System.Text;

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
            Process process = new Process();
            process.StartInfo.FileName = "\"C:\\Program Files\\Oxygen XML Developer 14\\jre\\bin\\java.exe\"";
            process.StartInfo.Arguments = "-jar \"C:\\Github\\nplant\\Tools\\PlantUML\\plantuml.jar\" \"{0}\" -o C:\\Github\\nplant\\Tools\\PlantUML\\".FormatWith(_filePath);
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            bool started = process.Start();

            if (! started)
            {
                EventDispatcher.Raise(new UserNotificationEvent("Failed to start plantuml.jar"));
            }
            else
            {
                process.WaitForExit(1000);

                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("PlantUML.jar Invoked...");

                builder.AppendLine("Standard Output: ");
                builder.AppendLine(output);
                builder.AppendLine("Standard Error Out: ");
                builder.AppendLine(error);

                EventDispatcher.Raise(new UserNotificationEvent(builder.ToString()));
            }
        }
    }
}