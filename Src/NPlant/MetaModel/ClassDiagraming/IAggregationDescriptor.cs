using NPlant.Core;
using NPlant.Generation;

namespace NPlant.MetaModel.ClassDiagraming
{
    public interface IAggregationDescriptor : IKeyedItem
    {
        string LeftName { get; }
        string RightName { get; }
        string Verb { get; }
        string Noun { get; }
    }
}