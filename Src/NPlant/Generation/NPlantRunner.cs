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

        public NPlantRunner(NPlantRunnerOptions options) : this(options, () => NullRecorder.Instance) { }

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

                Assembly assembly = RunAssemblyLoadStage(recorder);

                IEnumerable<IDiagram> diagrams = RunLoadDiagramsStage(assembly, recorder);

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
                var generator = diagram.CreateGenerator();

                var filePath = Path.Combine(outputDirectory.FullName,
                                            "{0}.nplant".FormatWith(diagram.GetName().ReplaceIllegalPathCharacters('_')));

                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (var file = File.CreateText(filePath))
                {
                    file.Write(generator.Generate());
                    recorder.Log("Diagram '{0}' written...".FormatWith(diagram.GetType().FullName));
                    recorder.Record(filePath);
                }
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

        private IEnumerable<IDiagram> RunLoadDiagramsStage(Assembly assembly, IRunnerRecorder recorder)
        {
            recorder.Log("Starting Stage: Diagram Instantiation...");

            IDiagram[] diagrams = DiscoverDiagrams.InAssembly(assembly, recorder.Log);

            recorder.Log("Finished Stage: Diagram Instantiation (diagrams instantiated={0})...".FormatWith(diagrams.Length));
            return diagrams;
        }

        private Assembly RunAssemblyLoadStage(IRunnerRecorder recorder)
        {
            recorder.Log("Starting Stage: Assembly Load (assembly={0})...".FormatWith(_options.AssemblyToScan));

            _options.AssemblyToScan.CheckForNull(() => new NPlantException("An 'assembly' attribute is required."));

            string loadMessage;

            Assembly assembly = LoadAssembly(out loadMessage);

            assembly.CheckForNull(
                () =>
                new NPlantException(
                    "Failed to load assembly '{0}'.  Exception message detected:  {1}".FormatWith(_options.AssemblyToScan, loadMessage)));

            recorder.Log("Finished Stage: Assembly Load...");
            return assembly;
        }

        private Assembly LoadAssembly(out string message)
        {
            Assembly assembly;

            try
            {
                if (Path.IsPathRooted(_options.AssemblyToScan))
                    assembly = Assembly.LoadFrom(_options.AssemblyToScan);
                else
                    assembly = Assembly.Load(_options.AssemblyToScan);
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return null;
            }

            message = null;
            return assembly;
        }

        private bool ShouldClean()
        {
            if (_options.Clean.IsNullOrEmpty())
                return false;

            return _options.Clean.ToBool(false);
        }
    }
}
