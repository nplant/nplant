using System.Diagnostics;
using System.Drawing;
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

        public void Generate()
        {
            var filePath = GetDiagramText();
            var model = ImageFileGenerationModel.Create(filePath);

            var image = DoGeneration(model);
            _view.Image = image;
        }

        protected abstract string GetDiagramText();

        private Image DoGeneration(ImageFileGenerationModel model)
        {
            Process process = new Process {
                StartInfo = {
                    FileName = model.JavaPath,
                    Arguments = model.GetJavaArguments(),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                },
                EnableRaisingEvents = true
            };

            bool started = process.Start();

            if (!started)
            {
                EventDispatcher.Raise(new UserNotificationEvent("Failed to start plantuml.jar"));
            }
            else
            {
//                byte[] bytes = Encoding.ASCII.GetBytes(model.DiagramText);
//
                //                process.StandardInput.BaseStream.Write(bytes,0, bytes.Length);
                process.StandardInput.Write(model.DiagramText);
                process.StandardInput.Close();

                return Image.FromStream(process.StandardOutput.BaseStream);
            }

            return null;
        }
    }
}