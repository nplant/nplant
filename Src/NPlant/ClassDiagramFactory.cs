using System.Collections.Generic;

namespace NPlant
{
    internal abstract class DiagramFactory<T> : IDiagramFactory where T : IDiagram
    {
        public abstract IEnumerable<IDiagram> GetDiagrams();
    }
}