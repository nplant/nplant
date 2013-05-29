using System.Collections;
using System.Collections.Generic;
using NPlant.Core;

namespace NPlant
{
    internal interface IDiagramFactory
    {
        IEnumerable InternalGetDiagrams();
    }

    public abstract class DiagramFactory<T> : IDiagramFactory where T : IDiagram
    {
        public abstract IEnumerable<T> GetDiagrams();

        IEnumerable IDiagramFactory.InternalGetDiagrams()
        {
            return this.GetDiagrams();
        }
    }
}