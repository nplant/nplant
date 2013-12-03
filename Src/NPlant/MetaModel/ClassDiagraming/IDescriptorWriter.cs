using NPlant.Generation.ClassDiagraming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public interface IDescriptorWriter
    {
        string Write(ClassDiagramVisitorContext context);
    }
}