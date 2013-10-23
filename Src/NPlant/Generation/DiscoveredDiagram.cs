namespace NPlant.Generation
{
    public class DiscoveredDiagram
    {
        public DiscoveredDiagram(string @namespace, IDiagram diagram)
        {
            Namespace = @namespace;
            Diagram = diagram;
        }

        public string Namespace { get; private set; }
        public IDiagram Diagram { get; private set; }
    }
}