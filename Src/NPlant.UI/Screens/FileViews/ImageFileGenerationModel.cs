using NPlant.Generation;

namespace NPlant.UI.Screens.FileViews
{
    public class ImageFileGenerationModel
    {
        private readonly PlantUmlInvocation _invocation = new PlantUmlInvocation(SystemEnvironment.ExecutionDirectory);

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

        public PlantUmlInvocation Invocation { get { return _invocation; } }
    }
}