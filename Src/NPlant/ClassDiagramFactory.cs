using System.Collections.Generic;

namespace NPlant
{
    internal abstract class DiagramFactory<T> : IDiagramFactory where T : ClassDiagram
    {
        public abstract IEnumerable<ClassDiagram> GetDiagrams();
    }
}