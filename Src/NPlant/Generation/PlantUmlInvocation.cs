using System.IO;

namespace NPlant.Generation
{
    public class PlantUmlInvocation
    {
        private readonly string _jarPath;
        const string PlantUmlJarName = "plantuml.jar";

        public PlantUmlInvocation(string jarPath)
        {
            _jarPath = jarPath.CheckForNullOrEmptyArg("jarPath");

            if (!_jarPath.EndsWith(PlantUmlJarName))
                _jarPath = Path.Combine(_jarPath, PlantUmlJarName);
        }

        public override string ToString()
        {
            return "-jar \"{0}\" -pipe".FormatWith(_jarPath);
        }
    }
}