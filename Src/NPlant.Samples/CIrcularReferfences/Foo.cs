using System;

namespace NPlant.Samples.CIrcularReferfences
{
    public class FooDiagram : ClassDiagram
    {
        public FooDiagram()
        {
            AddClass<Foo>();
        }
    }

    public class Foo
    {
        public string SomeString;
        public Bar TheBar;
        public Baz TheBaz;
    }

    public class Bar
    {
        public DateTime? SomeDate;

    }

    public class Baz
    {
        public Foo TheFoo;
    }
}
