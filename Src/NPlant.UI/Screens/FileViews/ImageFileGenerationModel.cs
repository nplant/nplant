using NPlant.Generation;

namespace NPlant.UI.Screens.FileViews
{
    public class ImageFileGenerationModel
    {
        private readonly PlantUmlInvocation _invocation = new PlantUmlInvocation(SystemEnvironment.ExecutionDirectory);

        private ImageFileGenerationModel(string diagramText, string diagramName)
        {
            SystemSettings settings = SystemEnvironment.GetSettings();

            this.JavaPath = settings.JavaPath;

            DiagramText = diagramText;
            DiagramName = diagramName;
        }

        public static ImageFileGenerationModel Create(string diagramText, string diagramName)
        {
            return new ImageFileGenerationModel(diagramText, diagramName);
        }

        public string JavaPath { get; private set; }
        public string DiagramText { get; private set; }
        public string DiagramName { get; private set; }

        public PlantUmlInvocation Invocation { get { return _invocation; } }
    }
}