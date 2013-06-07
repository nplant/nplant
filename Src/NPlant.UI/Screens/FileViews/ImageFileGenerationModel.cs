using System;
using System.IO;

namespace NPlant.UI.Screens.FileViews
{
    public class ImageFileGenerationModel
    {
        public ImageFileGenerationModel(string nplantFileName, string extension, string directoryPath, string finalFinalNameBase)
        {
            NPlantFilePath = nplantFileName;
            Extension = extension;
            DirectoryPath = directoryPath;
            FinalFileName = finalFinalNameBase + Extension;
            FinalFilePath = Path.Combine(DirectoryPath, FinalFileName);
            TempFileName = Guid.NewGuid().ToString() + Extension;
            TempFilePath = Path.Combine(DirectoryPath, TempFileName);
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

        public string JavaPath { get { return "java.exe"; } }
        public string Extension { get; private set; }
        public string DirectoryPath { get; private set; }
        public string NPlantFilePath { get; private set; }
        public string FinalFileName { get; private set; }
        public string FinalFilePath { get; private set; }
        public string TempFileName { get; private set; }
        public string TempFilePath { get; private set; }

        public string GetJavaArguments()
        {
            return "-jar \"{0}\\plantuml.jar\" \"{1}\" -o \"{2}\"".FormatWith(SystemEnvironment.ExecutionDirectory, this.NPlantFilePath, this.DirectoryPath);
        }
    }

    public enum ImageFileGenerationFormat
    {
        PNG,
        JPEG
    }
}