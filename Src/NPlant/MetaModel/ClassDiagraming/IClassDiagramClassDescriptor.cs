using System;
using NPlant.Core;

namespace NPlant.MetaModel.ClassDiagraming
{
    public interface IClassDiagramClassDescriptor : IKeyedItem
    {
        string Name { get; }
        Type ReflectedType { get; }
        int Level { get; }
        KeyedList<ClassMemberDescriptor> Members { get; }
        bool GetMemberVisibility(string name);
        KeyedList<IAggregationDescriptor> Associations { get; }
        void AddAssociation(IAggregationDescriptor descriptor);
        TypeMetaModel MetaModel { get; }
    }
}