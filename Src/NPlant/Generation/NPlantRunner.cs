using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace NPlant.Generation
{
    public class NPlantRunner
    {
        private readonly Func<IRunnerRecorder> _recorder;
        private readonly INPlantRunnerOptions _options;

        public NPlantRunner(INPlantRunnerOptions options) : this(options, () => NullRecorder.Instance) { }

        public NPlantRunner(INPlantRunnerOptions options, Func<IRunnerRecorder> recorder)
        {
            _recorder = recorder;
            _options = options;
        }

        public void Run()
        {
            using (var recorder = _recorder())
            {
                recorder.Log("NPlantRunner Started...");
                recorder.Log(SummarizeConfiguration());

                var loader = new NPlantAssemblyLoader(recorder);
                Assembly assembly = loader.Load(_options.AssemblyToScan);

                var diagramLoader = new NPlantDiagramLoader(recorder);
                var diagrams = diagramLoader.Load(assembly);

                DirectoryInfo outputDirectory = RunInitializeOutputDirectoryStage();

                RunGenerateDiagramImagesStage(outputDirectory, diagrams, recorder);

                recorder.Log("NPlantRunner Finished...");
            }
        }

        private string SummarizeConfiguration()
        {
            var summary = new StringBuilder();

            summary.AppendLine("Task Attributes:");

            IEnumerable<PropertyInfo> properties = _options.GetType().GetProperties();

            foreach (var property in properties)
            {
                summary.AppendLine("    [{0}]: {1}".FormatWith(property.Name, property.GetValue(_options, null)));
            }

            summary.AppendLine();

            return summary.ToString();
        }

        private void RunGenerateDiagramImagesStage(FileSystemInfo outputDirectory, IEnumerable<DiscoveredDiagram> diagrams, IRunnerRecorder recorder)
        {
            recorder.Log("Starting Stage: Diagram Rendering (output={0})...".FormatWith(outputDirectory.FullName));

            foreach (var diagram in diagrams)
            {
                var text = diagram.Diagram.CreateGenerator().Generate();
                var javaPath = _options.JavaPath ?? "java.exe";
                var plantUml = _options.PlantUml ?? Assembly.GetExecutingAssembly().Location;

                var npImage = new NPlantImage(javaPath, new PlantUmlInvocation(plantUml))
                    {
                        Logger = recorder.Log
                    };

                var image = npImage.Create(text, diagram.Diagram.Name);

                if (image != null)
                {
                    string dir = outputDirectory.FullName;

                    dir = Categorize(diagram, dir);

                    var fileName = diagram.Diagram.Name.ReplaceIllegalPathCharacters('_');

                    image.SaveNPlantImage(dir, fileName);
                }
            }

            recorder.Log("Finished Stage: Diagram Rendering...");
        }

        private string Categorize(DiscoveredDiagram diagram, string dir)
        {
            if (_options.ParsedCategorized == NPlantCategorizations.ByNamespace)
            {
                var ns = diagram.Namespace;

                if (! string.IsNullOrEmpty(ns))
                {
                    dir = Path.Combine(dir, ns);
                }
            }

            return dir;
        }

        private DirectoryInfo RunInitializeOutputDirectoryStage()
        {
            var outputDirectory = new DirectoryInfo(_options.OutputDirectory.IfIsNullOrEmpty("."));

            outputDirectory.EnsureExists();

            if (this.ShouldClean())
            {
                var files = outputDirectory.GetFiles("*.*", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    file.Delete();
                }
            }

            return outputDirectory;
        }

        private bool ShouldClean()
        {
            if (_options.Clean.IsNullOrEmpty())
                return false;

            return _options.Clean.ToBool(false);
        }
    }
}
