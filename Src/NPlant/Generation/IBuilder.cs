using NPlant.Generation.ClassDiagraming;

namespace NPlant.Generation
{
    public interface IBuilder
    {
        void Build(ClassDiagramVisitorContext context);
    }
}