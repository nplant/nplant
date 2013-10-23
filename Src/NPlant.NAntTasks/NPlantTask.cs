using NAnt.Core;
using NAnt.Core.Attributes;
using NPlant.Generation;

namespace NPlant.NAntTasks
{
    [TaskName("nplant")]
    public class NPlantTask : Task, INPlantRunnerOptions
    {
        private DiagramsElement _diagramsElement = new DiagramsElement();

        protected override void ExecuteTask()
        {
            Level old = Project.Threshold;
            AssignLogLevel(Level.Debug);

            var runner = new NPlantRunner(this, () => new NAntRunnerRecorder(this, this.Property, this.Delimiter));
            runner.Run();

            AssignLogLevel(old);
        }

        private void AssignLogLevel(Level newLevel)
        {
            foreach (IBuildListener listener in Project.BuildListeners)
            {
                IBuildLogger logger = listener as IBuildLogger;

                if (logger != null)
                    logger.Threshold = newLevel;
            }
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

        [TaskAttribute("categorize", Required = false)]
        public string Categorize { get; set; }

        [TaskAttribute("java", Required = false)]
        public string JavaPath { get; set; }

        [TaskAttribute("plantuml", Required = false)]
        public string PlantUml { get; set; }

        [BuildElement("diagrams", Required = false)]
        public DiagramsElement DiagramsElement
        {
            get { return _diagramsElement; }
            set { _diagramsElement = value; }
        }
    }
}
