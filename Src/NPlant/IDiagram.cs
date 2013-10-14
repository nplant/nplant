namespace NPlant
{
    public interface IDiagram
    {
        string Name { get; }
        IDiagramGenerator CreateGenerator();
    }
}