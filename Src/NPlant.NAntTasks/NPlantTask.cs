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
            var runner = new NPlantRunner(this, () => new NAntRunnerRecorder(this, this.Property, this.Delimiter));
            runner.Run();
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
