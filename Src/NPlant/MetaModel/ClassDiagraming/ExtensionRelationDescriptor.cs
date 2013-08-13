using NPlant.Core;
using NPlant.Generation;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ExtensionRelationDescriptor : AbstractRelationDescriptor
    {
        public ExtensionRelationDescriptor(IClassDiagramClassDescriptor source, TypeMetaModel target)
            : base("", source, target)
        {
            this.Key = "BaseClass";
        }

        public override string Noun
        {
            get { return null; }
        }

        public override string Verb
        {
            get { return "extends"; }
        }

        public override IBuilder GetBuilder()
        {
            return new ExtensionBuilder(this);
        }
    }
}