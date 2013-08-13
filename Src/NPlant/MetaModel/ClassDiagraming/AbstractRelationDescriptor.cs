using NPlant.Core;
using NPlant.Generation;

namespace NPlant.MetaModel.ClassDiagraming
{
    public abstract class AbstractRelationDescriptor : IRelationDescriptor
    {
        protected readonly TypeMetaModel Target;
        protected readonly IClassDiagramClassDescriptor Source;

        protected AbstractRelationDescriptor(string key, IClassDiagramClassDescriptor source, TypeMetaModel target)
        {
            this.Key = key;
            this.Source = source;
            this.Target = target;

            this.RightName = this.Target.Name;
        }

        public string Key { get; set; }
        
        public string LeftName { get { return Source.Name; } }

        public string RightName { get; set; }

        public abstract string Verb { get; }

        public virtual string Noun
        {
            get { return this.Key; }
        }

        public abstract IBuilder GetBuilder();
    }
}