using NPlant.Core;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.Tests.Diagraming
{
    public class StubClassDiagramVisitorContext : ClassDiagramVisitorContext
    {
        public StubClassDiagramVisitorContext(ClassDiagramScanModes scanMode)
        {
            this.TypeMetaModelSet = new TypeMetaModelSet();
            this.ScanMode = scanMode;
        }
    }
}