using System;
using System.Collections;
using NPlant.Core;
using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class ClassBuilder : IBuilder
    {
        private readonly IClassDiagramClassDescriptor _descriptor;

        public ClassBuilder(IClassDiagramClassDescriptor descriptor)
        {
            _descriptor = descriptor.CheckForNullArg("descriptor");
        }

        public void Build(ClassDiagramGenerationContext context)
        {
            TypeMetaModel typeMetaModel = _descriptor.MetaModel;

            bool showInheritance = _descriptor.RenderInheritance && _descriptor.ReflectedType.BaseType != null;
            TypeMetaModel baseTypeMetaModel = null;
            
            if (showInheritance)
            {
                baseTypeMetaModel = context.Diagram.Types[_descriptor.ReflectedType.BaseType];

                showInheritance = !baseTypeMetaModel.HiddenForExtension && !baseTypeMetaModel.Hidden;
            }

            if (! typeMetaModel.Hidden)
            {
                context.WriteLine(String.Format("class \"{0}\" {1}", _descriptor.Name, "{")); // need to insert the { to not screw up Format processing

                foreach (ClassMemberDescriptor member in _descriptor.Members.InnerList)
                {
                    TypeMetaModel metaModel = member.MetaModel;

                    if (! metaModel.Hidden)
                    {
                        if (metaModel.IsComplexType)
                        {
                            IRelationDescriptor descriptor = this.CreateAssociation(context, member);
                            IBuilder associationBuilder = new AggregationMemberBuilder(descriptor);
                            associationBuilder.Build(context);
                        }
                        else if (metaModel.IsPrimitive)
                        {
                            IBuilder memberBuilder = new SimpleMemberBuilder(member, _descriptor);
                            memberBuilder.Build(context);
                        }
                        else
                        {
                            IBuilder memberBuilder = new SimpleMemberBuilder(member, _descriptor);
                            memberBuilder.Build(context);
                        }
                    }
                }

                context.WriteLine("}");

                typeMetaModel.Note.Write(context);

            }

            if (showInheritance)
            {
                context.Diagram.AddReflectedClass(_descriptor.Level - 1, _descriptor.ReflectedType.BaseType);
                var extensionDescriptor = new ExtensionRelationDescriptor(_descriptor, baseTypeMetaModel);

                _descriptor.AddRelation(extensionDescriptor);
            }

            foreach (var relation in _descriptor.Relations.InnerList)
            {
                IBuilder builder = relation.GetBuilder();
                builder.Build(context);
            }
        }

        internal IRelationDescriptor CreateAssociation(ClassDiagramGenerationContext context, ClassMemberDescriptor member)
        {
            if (_descriptor.GetMemberVisibility(member.Key))
            {
                IRelationDescriptor descriptor;

                var nextLevel = _descriptor.Level + 1;

                if (typeof(IEnumerable).IsAssignableFrom(member.MemberType))
                {
                    var enumeratorType = member.MemberType.GetEnumeratorType();
                    var enumeratorTypeMetaModel = context.Diagram.MetaModel.Types[enumeratorType];

                    descriptor = new HasManyRelationDescriptor(member.Name, _descriptor, context.Diagram.MetaModel.Types[enumeratorType]);

                    if (enumeratorTypeMetaModel.IsComplexType)
                    {
                        context.Diagram.AddReflectedClass(nextLevel, enumeratorType);
                    }
                }
                else
                {
                    descriptor = new HasOneRelationDescriptor(member.Name, _descriptor, context.Diagram.MetaModel.Types[member.MemberType]);
                    context.Diagram.AddReflectedClass(nextLevel, member.MemberType);
                }

                _descriptor.AddRelation(descriptor);

                return descriptor;
            }

            return new NullIAggregationDescriptor();
        }    
    }
}