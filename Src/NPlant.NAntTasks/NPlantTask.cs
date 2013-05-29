using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using NPlant.Core;
using NPlant.Generation;

namespace NPlant.NAntTasks
{
    [TaskName("nplant")]
    public class NPlantTask : Task
    {
        private DiagramsElement _diagramsElement = new DiagramsElement();

        protected override void ExecuteTask()
        {
            using(var recorder = new TaskRecorder(this.Delimiter))
            {
                recorder.OnDispose(recording =>
                    {
                        this.Properties[this.Property] = recording;
                        base.Log(Level.Debug, "Recording: {0}".FormatWith(recording));
                    });

                base.Log(Level.Debug, "{0}.ExecuteTask Started...".FormatWith(typeof (NPlantTask).Name));
                base.Log(Level.Debug, SummarizeConfiguration());

                Assembly assembly = RunAssemblyLoadStage();

                IEnumerable<IDiagram> diagrams = RunLoadDiagramsStage(assembly);

                DirectoryInfo outputDirectory = RunInitializeOutputDirectoryStage();

                RunGenerateDiagramFilesStage(outputDirectory, diagrams, recorder);

                base.Log(Level.Debug, "{0}.ExecuteTask Finished...".FormatWith(typeof (NPlantTask).Name));
            }
        }

        private string SummarizeConfiguration()
        {
            var summary = new StringBuilder();

            summary.AppendLine("Task Attributes:");

            IEnumerable<PropertyInfo> properties = this.GetType().GetProperties();
            
            foreach (var property in properties)
            {
                if (! property.IsIndexed())
                {
                    TaskAttributeAttribute[] taskAttributes = property.GetAttributesOf<TaskAttributeAttribute>();

                    foreach (var taskAttribute in taskAttributes)
                    {
                        summary.AppendLine("    [{0}]: {1}".FormatWith(taskAttribute.Name, property.GetValue(this, null)));
                    }
                }
            }

            summary.AppendLine();

            if (this.DiagramsElement != null)
            {
                summary.AppendLine("    Diagrams Element Found");
                summary.AppendLine("    [in]: {0}".FormatWith(this.DiagramsElement.In));

                if (this.DiagramsElement.Diagrams != null)
                {
                    summary.AppendLine("    Diagrams Element has a collection of Diagram Elements (Count: {0}):".FormatWith(this.DiagramsElement.Diagrams.Count));

                    for (int i = 0; i < this.DiagramsElement.Diagrams.Count; i++)
                    {
                        var diag = this.DiagramsElement.Diagrams[i];
                        summary.AppendLine("    {0}) named: {1}, output: {2}".FormatWith(i, diag.Name, diag.Output));
                    }
                }
            }
            else
            {
                summary.AppendLine("    Diagrams Element Not Found");
            }

            return summary.ToString();
        }

        private void RunGenerateDiagramFilesStage(FileSystemInfo outputDirectory, IEnumerable<IDiagram> diagrams, TaskRecorder recorder)
        {
            base.Log(Level.Debug, "Starting Stage: Diagram Rendering (output={0})...".FormatWith(outputDirectory.FullName));

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
                    base.Log(Level.Debug, "Diagram '{0}' writen...".FormatWith(diagram.GetType().FullName));
                    recorder.Record(filePath);
                }
            }

            base.Log(Level.Debug, "Finished Stage: Diagram Rendering...");
        }

        private DirectoryInfo RunInitializeOutputDirectoryStage()
        {
            var outputDirectory = new DirectoryInfo(this.OutputDirectory.IfIsNullOrEmpty("."));

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

        private IEnumerable<IDiagram> RunLoadDiagramsStage(Assembly assembly)
        {
            base.Log(Level.Debug, "Starting Stage: Diagram Instantiation...");

            IDiagram[] diagrams = DiscoverDiagrams.InAssembly(assembly, msg => base.Log(Level.Debug, msg));

            base.Log(Level.Debug,
                     "Finished Stage: Diagram Instantiation (diagrams instantiated={0})...".FormatWith(diagrams.Length));
            return diagrams;
        }

        private Assembly RunAssemblyLoadStage()
        {
            base.Log(Level.Debug, "Starting Stage: Assembly Load (assembly={0})...".FormatWith(this.AssemblyToScan));

            this.AssemblyToScan.CheckForNull(
                () =>
                new BuildException(
                    "The {0} requires an 'assembly' attribute to be specified.".FormatWith(typeof (NPlantTask).Name)));

            string loadMessage;

            Assembly assembly = LoadAssembly(out loadMessage);

            assembly.CheckForNull(
                () =>
                new BuildException(
                    "Failed to load assembly '{0}'.  Exception message detected:  {1}".FormatWith(this.AssemblyToScan,
                                                                                                  loadMessage)));

            base.Log(Level.Debug, "Finished Stage: Assembly Load...");
            return assembly;
        }

        private Assembly LoadAssembly(out string message)
        {
            Assembly assembly;

            try
            {
                if (Path.IsPathRooted(this.AssemblyToScan))
                    assembly = Assembly.LoadFrom(this.AssemblyToScan);
                else
                    assembly = Assembly.Load(this.AssemblyToScan);
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
            if (this.Clean.IsNullOrEmpty())
                return false;

            return this.Clean.ToBool(false);
        }

        [TaskAttribute("property", Required = false)]
        public string Property { get; set; }

        [TaskAttribute("clean", Required = false)]
        public string Clean { get; set; }

        [TaskAttribute("delim", Required = false)]
        public string Delimiter { get; set; }

        [TaskAttribute("assembly", Required = true)]
        public string AssemblyToScan { get; set; }

        [TaskAttribute("dir", Required = false)]
        public string OutputDirectory { get; set; }

        [BuildElement("diagrams", Required = false)]
        public DiagramsElement DiagramsElement
        {
            get { return _diagramsElement; }
            set { _diagramsElement = value; }
        }
    }
}
