using System;

namespace NPlant.Samples.FullMonty
{
    public class FullMontyClassDiagram : ClassDiagram
    {
        public FullMontyClassDiagram()
        {
            AddClass<Foo>();
        }
    }

    public class Foo
    {
        public string SomeString;
        public Bar TheBar;
        public Baz<Arg1, Arg2> TheBaz;
        public Baz2<Arg1, Arg2> TheBaz2;
    }

    public class Bar
    {
        public DateTime? SomeDate;

    }

    public class Baz<T1, T2>
    {
        public Foo TheFoo;

        public T1 Arg1;

        public T2 Arg2;
    }

    public class Baz2<T1, T2>
    {
        public Baz2()
        {
            this.Whatever = typeof (T1).Name + typeof (T2).Name;
        }

        public string Whatever;
    }

    public class Arg1 { }
    public class Arg2{}
}
