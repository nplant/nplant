using System;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace NPlant.NAntTasks
{
    [Serializable]
    public class DiagramsElement : Element
    {
        private DiagramElementCollection _diagrams = new DiagramElementCollection();

        [TaskAttribute("in", Required = false)]
        public string In { get; set; }

        [BuildElementArray("diagram")]
        public virtual DiagramElementCollection Diagrams
        {
            get { return _diagrams; }
            set { _diagrams = value; }
        }
    }
}