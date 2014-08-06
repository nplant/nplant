using System;

namespace NPlant.Generation.ClassDiagraming
{
    public class ClassDiagramGeneration : IDisposable
    {
        private readonly ClassDiagramVisitorContext _previousVisitorContext;

        public ClassDiagramGeneration(ClassDiagram diagram)
        {
            _previousVisitorContext = ClassDiagramVisitorContext.Current;
            ClassDiagramVisitorContext.Current = new ClassDiagramVisitorContext(diagram);
        }

        public void Dispose()
        {
            ClassDiagramVisitorContext.Current = _previousVisitorContext;
        }
    }
}