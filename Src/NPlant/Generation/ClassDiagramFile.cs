using System.IO;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.Generation
{
    public static class ClassDiagramFile
    {
        public static string Save(string outputDirectory, ClassDiagram diagram, IRunnerRecorder recorder)
        {
            var filePath = Path.Combine(outputDirectory, "{0}.nplant".FormatWith(diagram.Name.ReplaceIllegalPathCharacters('_')));

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var file = File.CreateText(filePath))
            {
                var generator = new FileClassDiagramGenerator(diagram, file);
                generator.Generate();
                
                recorder.Log("Diagram '{0}' written...".FormatWith(diagram.GetType().FullName));
                recorder.Record(filePath);
            }

            return filePath;
        }
    }
}