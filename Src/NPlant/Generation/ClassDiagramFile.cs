using System.IO;

namespace NPlant.Generation
{
    public static class ClassDiagramFile
    {
        public static string Save(string outputDirectory, IDiagram diagram, IRunnerRecorder recorder)
        {
            var generator = diagram.CreateGenerator();

            var filePath = Path.Combine(outputDirectory, "{0}.nplant".FormatWith(diagram.Name.ReplaceIllegalPathCharacters('_')));

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var file = File.CreateText(filePath))
            {
                file.Write(generator.Generate());
                recorder.Log("Diagram '{0}' written...".FormatWith(diagram.GetType().FullName));
                recorder.Record(filePath);
            }

            return filePath;
        }
    }
}