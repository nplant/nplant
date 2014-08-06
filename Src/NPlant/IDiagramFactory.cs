using System.Collections.Generic;

namespace NPlant
{
    public interface IDiagramFactory
    {
        IEnumerable<ClassDiagram> GetDiagrams();
    }
}