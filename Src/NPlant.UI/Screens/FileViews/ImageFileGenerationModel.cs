using System;
using System.IO;
using NPlant.UI.Screens.Controls;

namespace NPlant.UI.Screens.FileViews
{
    public class ImageFileGenerationModel
    {
        private ImageFileWatcher _watcher;

        public ImageFileGenerationModel(string nplantFileName, string extension, string directoryPath, string finalFinalNameBase)
        {
            SystemSettings settings = SystemEnvironment.GetSettings();

            this.JavaPath = settings.JavaPath;

            NPlantFilePath = nplantFileName;
            Extension = extension;
            DirectoryPath = directoryPath;
            FinalFileName = finalFinalNameBase + Extension;
            FinalFilePath = Path.Combine(DirectoryPath, FinalFileName);
            TempFileName = Guid.NewGuid().ToString() + Extension;
            TempFilePath = Path.Combine(DirectoryPath, TempFileName);

            _watcher = new ImageFileWatcher(this.TempFilePath, () => File.Copy(this.TempFilePath, this.FinalFilePath, true));

            _watcher.Watch();
        }

        public static ImageFileGenerationModel Create(string nplantFilePath, ImageFileGenerationFormat format = ImageFileGenerationFormat.PNG)
        {
            string directory = Path.GetDirectoryName(nplantFilePath);
            string nameBase = Path.GetFileNameWithoutExtension(nplantFilePath);

            string extension;

            switch (format)
            {
                case ImageFileGenerationFormat.JPEG:
                    extension = ".jpeg";
                    break;
                default:
                    extension = ".png";
                    break;
            }

            return new ImageFileGenerationModel(nplantFilePath, extension, directory, nameBase);
        }

        public string JavaPath { get; private set; }
        public string Extension { get; private set; }
        public string DirectoryPath { get; private set; }
        public string NPlantFilePath { get; private set; }
        public string FinalFileName { get; private set; }
        public string FinalFilePath { get; private set; }
        public string TempFileName { get; private set; }
        public string TempFilePath { get; private set; }

        public string GetJavaArguments()
        {
            return "-jar \"{0}\\plantuml.jar\" -pipe".FormatWith(SystemEnvironment.ExecutionDirectory);
            //return "\"{0}\" | \"{1}\" -jar \"{2}\\plantuml.jar\" -pipe".FormatWith(this.NPlantFilePath, this.JavaPath, SystemEnvironment.ExecutionDirectory);
        }
    }

    public enum ImageFileGenerationFormat
    {
        PNG,
        JPEG
    }
}