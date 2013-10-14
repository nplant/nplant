using System;

namespace NPlant.Samples.Inheritance
{
    public class SimpleInheritanceDiagram : ClassDiagram
    {
        public SimpleInheritanceDiagram()
        {
            this.GenerationOptions.ForType<AbstractBase>().HideAsBase();
            AddClass<Foo>();
        }
    }

    public abstract class AbstractBase
    {
        public string SomeString;
    }

    public class Foo : AbstractBase
    {
        public Bar TheBar;
        public Baz TheBaz;
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