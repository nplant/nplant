using System.Collections.Generic;

namespace NPlant
{
    public interface IDiagramFactory
    {
        IEnumerable<IDiagram> GetDiagrams();
    }
}