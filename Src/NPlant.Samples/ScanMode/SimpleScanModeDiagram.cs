using System;
using System.Runtime.Serialization;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.Samples.ScanMode
{
    public class SimpleScanModeDiagram : ClassDiagram
    {
        public SimpleScanModeDiagram()
        {
            base.GenerationOptions.ScanModeOf(ClassDiagramScanModes.AllMembers);
            AddClass<Foo>();
        }
    }

    public class Foo
    {
        public Bar TheBar;
        public Baz TheBaz;
        private int ThePrivateCount;
        internal bool IsValid;
        protected string Name;
    }

    public class Bar : Foo
    {
        public DateTime? SomeDate;

    }

    public class Baz
    {
        public Foo TheFoo;
    }
}
