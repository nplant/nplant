namespace NPlant.UI.Screens.FileViews
{
    public class ImageFileGenerationModel
    {
        public ImageFileGenerationModel(string diagramText)
        {
            SystemSettings settings = SystemEnvironment.GetSettings();

            this.JavaPath = settings.JavaPath;

            DiagramText = diagramText;
        }

        public static ImageFileGenerationModel Create(string diagramText)
        {
            return new ImageFileGenerationModel(diagramText);
        }

        public string JavaPath { get; private set; }
        public string DiagramText { get; private set; }

        public string GetJavaArguments()
        {
            return "-jar \"{0}\\plantuml.jar\" -pipe".FormatWith(SystemEnvironment.ExecutionDirectory);
        }
    }
}