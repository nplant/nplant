using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagraming
{
    public interface IClassDiagramMetaModel
    {
        KeyedList<IClassDiagramClassDescriptor> Classes { get; }
        TypeMetaModelSet Types { get; }
    }
}