using System;
using NPlant.Core;
using NPlant.Generation;
using NPlant.Generation.ClassDiagraming;
using NPlant.MetaModel.ClassDiagraming;

namespace NPlant
{
    public class ClassDiagram : IDiagram, IClassDiagramMetaModel
    {
        private readonly TypeMetaModelSet _types = new TypeMetaModelSet();
        private string _name;
        private readonly KeyedList<IClassDiagramClassDescriptor> _classDescriptors = new KeyedList<IClassDiagramClassDescriptor>();
        private readonly ClassDiagramOptions _generationOptions;

        public ClassDiagram(Type type, params Type[] types): this()
        {
            type.CheckForNullArg("type");

            this.AddClass(new ReflectedTypeClassDescriptor(this, type));

            if (types != null)
            {
                foreach (var t in types)
                {
                    this.AddClass(new ReflectedTypeClassDescriptor(this, t));
                }
            }
        }

        public ClassDiagram()
        {
            _generationOptions = new ClassDiagramOptions(this);
            _name = this.GetType().Name;
        }

        public TypeMetaModelSet Types { get { return _types; } }

        internal IClassDiagramMetaModel MetaModel
        {
            get { return this; }
        }

        protected ClassDescriptor<T> AddClass<T>()
        {
            var classDescriptor = new ClassDescriptor<T>(this);

            this.AddClass(classDescriptor);
            _classDescriptors.Add(classDescriptor);

            return classDescriptor;
        }

        internal void AddReflectedClass(int level, Type type)
        {
            var descriptor = new ReflectedTypeClassDescriptor(this, type);

            descriptor.SetLevel(level);
            this.AddClass(descriptor);
        }

        public void AddClass(IClassDiagramClassDescriptor descriptor)
        {
            if (! _classDescriptors.ContainsKey(descriptor.Key))
            {
                _classDescriptors.Add(descriptor.CheckForNullArg("descriptor"));
            }
        }

        KeyedList<IClassDiagramClassDescriptor> IClassDiagramMetaModel.Classes { get { return _classDescriptors; } }

        public IDiagramGenerator CreateGenerator()
        {
            return new ClassDiagramGenerator(this);
        }

        string IDiagram.GetName() { return _name; }

        public ClassDiagram Named(string name)
        {
            _name = name;

            return this;
        }

        public ClassDiagramOptions GenerationOptions
        {
            get { return _generationOptions; }
        }

        public int? DepthLimit { get; internal set; }
    }
}
