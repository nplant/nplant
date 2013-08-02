using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NPlant.Core;

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
                IEnumerable<IDiagram> diagrams = diagramLoader.Load(assembly);

                DirectoryInfo outputDirectory = RunInitializeOutputDirectoryStage();

                RunGenerateDiagramFilesStage(outputDirectory, diagrams, recorder);

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

        private void RunGenerateDiagramFilesStage(FileSystemInfo outputDirectory, IEnumerable<IDiagram> diagrams, IRunnerRecorder recorder)
        {
            recorder.Log("Starting Stage: Diagram Rendering (output={0})...".FormatWith(outputDirectory.FullName));

            foreach (var diagram in diagrams)
            {
                ClassDiagramFile.Save(outputDirectory.FullName, diagram, recorder);
            }

            recorder.Log("Finished Stage: Diagram Rendering...");
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
