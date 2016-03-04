using NPlant.Generation;

namespace NPlant.Console
{
    public class ImageFileGenerationModel
    {
        public ImageFileGenerationModel(string diagramText, string diagramName, string javaPath, string jarPath)
        {
            if(javaPath.IsNullOrEmpty())
                javaPath = ConsoleEnvironment.GetSettings().JavaPath;

            this.JavaPath = javaPath;
            this.DiagramText = diagramText;
            this.DiagramName = diagramName;

            this.Invocation = new PlantUmlInvocation(jarPath);
        }

        public string JavaPath { get; private set; }
        public string DiagramText { get; private set; }
        public string DiagramName { get; private set; }

        public PlantUmlInvocation Invocation { get; set; }
    }
}