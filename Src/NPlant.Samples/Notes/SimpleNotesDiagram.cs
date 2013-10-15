namespace NPlant.Samples.Notes
{
    public class SimpleNotesDiagram : ClassDiagram
    {
        public SimpleNotesDiagram()
        {
            this.AddClass<Foo>();
            this.AddClass<Bar>();
            this.AddNote("this is a note");
            this.AddNote("this is another note")
                    .AddLine("with another line");
            this.AddNote("this is connected note")
                    .AddLine("with another line")
                    .ConnectedToClass<Foo>()
                    .ConnectedToClass<Bar>();
        }
    }
    public class Foo { }
    public class Bar { }
}
