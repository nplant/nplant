namespace NPlant.Samples.Hiding
{
    public class SimpleHidingClassDiagram : ClassDiagram
    {
        public SimpleHidingClassDiagram()
        {
            this.AddClass<SimpleEntity>().ForMember(x => x.Bar).Hide();
            this.AddClass<SimpleEntity2>();
            this.GenerationOptions.ForType<BahEntity>().Hide();

            this.AddNote("Bar is not displayed as a member of SimpleEntity.  Bah is no where.");
        }

        public class SimpleEntity
        {
            public string Foo;
            public string Bar;
            public string Baz;
            public BahEntity Bah;
        }

        public class SimpleEntity2
        {
            public string Foo;
            public BahEntity Bah;
        }

        public class BahEntity
        {
            public string Something;
        }
    }
}
