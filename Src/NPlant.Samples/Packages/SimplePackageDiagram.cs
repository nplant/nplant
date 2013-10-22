using System;

namespace NPlant.Samples.Packages
{
    public class SimplePackageDiagram : ClassDiagram
    {
        public SimplePackageDiagram()
        {
            AddClass<Foo>();


            AddPackage("Package A")
                .IncludeClassesWhere(descriptor => descriptor.ReflectedType == typeof(Foo))
                .IncludeClassesWhere(descriptor => descriptor.ReflectedType == typeof(Bar));

            AddPackage("Package B")
                .IncludeClassesWhere(descriptor => descriptor.ReflectedType == typeof (Baz));

        }
    }

    public class Foo
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
