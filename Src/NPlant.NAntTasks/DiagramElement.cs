using System;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace NPlant.NAntTasks
{
    [Serializable]  
    public class DiagramElement : Element
    {
        [TaskAttribute("named", Required = true)]
        public string Named { get; set; }

        [TaskAttribute("output", Required = false)]
        public string Output { get; set; }
    }
}