namespace NPlant.Samples.AddAllSubClassesOf
{
    public class SimpleAddAllSubClassesOfClassDiagram : ClassDiagram
    {
        public SimpleAddAllSubClassesOfClassDiagram()
        {
            this.AddAllSubClassesOff<Animal>();
            this.GenerationOptions.ForType<Thing>().HideAsBase();
            this.GenerationOptions.ShowMethods();
        }
    }

    public abstract class Thing
    {
        public string Id { get; set; }
    }

    public class MiddleMan : Thing { }

    public class Animal : MiddleMan
    {
        public string Name { get; set; }
    }

    public class Gorilla : Animal
    {
        public string ChestSize { get; set; }

        public void Fight() { }
    }

    public class Elephant : Animal
    {
        public string TuskSize { get; set; }

        public void Charge() { }
    }

    public class Chair
    {
        
    }
}