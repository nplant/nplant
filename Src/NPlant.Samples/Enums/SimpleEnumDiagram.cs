using System;

namespace NPlant.Samples.Enums
{
    public class SimpleEnumDiagram : ClassDiagram
    {
        public SimpleEnumDiagram()
        {
            AddClass<Foo>();
            AddEnum<RandomEnum>();
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
        public BarTypes Type;
    }

    public enum RandomEnum
    {
        Member1,
        Member2,
        Member3
    }
    public enum BarTypes
    {
        HighBar,
        LowBar
    }

    public class Baz
    {
        public Foo TheFoo;
    }
}
