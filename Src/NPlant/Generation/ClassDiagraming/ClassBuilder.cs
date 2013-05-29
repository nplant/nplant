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

        void IBuilder.Build(ClassDiagramGenerationContext context)
        {
            TypeMetaModel typeMetaModel = _descriptor.MetaModel;

            if (! typeMetaModel.Hidden)
            {
                context.WriteLine(String.Format("class \"{0}\" {1}", _descriptor.Name, "{"));

                foreach (ClassMemberDescriptor member in _descriptor.Members.InnerList)
                {
                    TypeMetaModel metaModel = member.MetaModel;

                    if (! metaModel.Hidden)
                    {
                        if (metaModel.IsComplexType)
                        {
                            IAggregationDescriptor descriptor = this.CreateAssociation(context, member);
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
            foreach (var association in _descriptor.Associations.InnerList)
            {
                IBuilder builder = new AggregationBuilder(association);
                builder.Build(context);
            }
        }

        internal IAggregationDescriptor CreateAssociation(ClassDiagramGenerationContext context, ClassMemberDescriptor member)
        {
            if (_descriptor.GetMemberVisibility(member.Key))
            {
                IAggregationDescriptor descriptor;

                var nextLevel = _descriptor.Level + 1;

                if (typeof(IEnumerable).IsAssignableFrom(member.MemberType))
                {
                    var enumeratorType = member.MemberType.GetEnumeratorType();
                    var enumeratorTypeMetaModel = context.Diagram.MetaModel.Types[enumeratorType];

                    descriptor = new HasManyAggregationDescriptor(context.Diagram, _descriptor, new ClassMemberDescriptor(enumeratorTypeMetaModel.Name, enumeratorType, context.Diagram.MetaModel.Types[enumeratorType]));

                    if (enumeratorTypeMetaModel.IsComplexType)
                    {
                        context.Diagram.AddReflectedClass(nextLevel, enumeratorType);
                    }

// what was thsi code doing in the second block?  hoping the above code replaces it
/*
                    if (enumeratorTypeMetaModel.IsComplexType)
                    {
                        descriptor = new HasManyAggregationDescriptor(context.Diagram, _descriptor, new ClassMemberDescriptor(enumeratorTypeMetaModel.Name, enumeratorType));
                        context.Diagram.AddReflectedClass(nextLevel, new ReflectedTypeClassDescriptor(context.Diagram, enumeratorType));
                    }
                    else
                    {
                        descriptor = new HasOneAggregationDescriptor(context.Diagram, _descriptor, member);
                        context.Diagram.AddReflectedClass(nextLevel, new ReflectedTypeClassDescriptor(context.Diagram, member.MemberType));
                    }
*/
                }
                else
                {
                    descriptor = new HasOneAggregationDescriptor(context.Diagram, _descriptor, member);
                    context.Diagram.AddReflectedClass(nextLevel, member.MemberType);
                }

                _descriptor.AddAssociation(descriptor);

                return descriptor;
            }

            return new NullIAggregationDescriptor();
        }    
    }
}