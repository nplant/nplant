using NPlant.Generation;

namespace NPlant.Core
{
    public interface IDiagram
    {
        IDiagramGenerator CreateGenerator();
        string GetName();
    }
}