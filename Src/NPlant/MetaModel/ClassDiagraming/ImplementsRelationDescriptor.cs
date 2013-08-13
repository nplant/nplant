using NPlant.Core;
using NPlant.Generation;
using NPlant.Generation.ClassDiagraming;

namespace NPlant.MetaModel.ClassDiagraming
{
    public class ImplementsRelationDescriptor : AbstractRelationDescriptor
    {
        public ImplementsRelationDescriptor(IClassDiagramClassDescriptor source, TypeMetaModel target)
            : base("", source, target)
        {
            this.Key = string.Format("Implementing_{0}", target.Name);
        }

        public override string Verb
        {
            get { return "implements"; }
        }

        public override IBuilder GetBuilder()
        {
            return new ExtensionBuilder(this);
        }
    }
}