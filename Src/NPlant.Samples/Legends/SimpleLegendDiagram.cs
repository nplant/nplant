namespace NPlant.Samples.Legends
{
    public class SimpleLegendDiagram : ClassDiagram
    {
        public SimpleLegendDiagram()
        {
            this.AddClass<Foo>();
            this.AddClass<Bar>();
            this.LegendOf("This is my legend")
                .DisplayLeft();
        }
    }
    public class Foo { }
    public class Bar { }
}
