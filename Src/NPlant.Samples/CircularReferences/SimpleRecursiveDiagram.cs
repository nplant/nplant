using System;

namespace NPlant.Samples.CircularReferences
{
    [Sample]
    public class SimpleRecursiveDiagram : ClassDiagram
    {
        public SimpleRecursiveDiagram()
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
