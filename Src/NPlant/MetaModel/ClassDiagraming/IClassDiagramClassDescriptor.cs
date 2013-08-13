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
        KeyedList<IRelationDescriptor> Relations { get; }
        void AddRelation(IRelationDescriptor descriptor);
        TypeMetaModel MetaModel { get; }
        bool RenderInheritance { get; set; }
    }
}