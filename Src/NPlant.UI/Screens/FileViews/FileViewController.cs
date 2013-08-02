using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
            var filePath = GetFilePath();
            var model = ImageFileGenerationModel.Create(filePath);

            if (!File.Exists(filePath))
                return;

            var image = DoGeneration(model);
            _view.Image = image;
        }

        protected abstract string GetFilePath();

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
                //read in the file.
                using (var fileStream = new StreamReader(model.NPlantFilePath))
                {
                    fileStream.BaseStream.CopyTo(process.StandardInput.BaseStream);
                    process.StandardInput.Close();
                }

                return Image.FromStream(process.StandardOutput.BaseStream);
            }

            return null;
        }
    }
}