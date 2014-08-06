namespace NPlant.Generation
{
    public class DiscoveredDiagram
    {
        public DiscoveredDiagram(string @namespace, ClassDiagram diagram)
        {
            Namespace = @namespace;
            Diagram = diagram;
        }

        public string Namespace { get; private set; }
        public ClassDiagram Diagram { get; private set; }
    }
}