using System;
using System.Collections.Generic;
using System.Linq;
using NPlant.MetaModel.ClassDiagraming;

namespace NPlant
{
    public class ClassDiagramPackage
    {
        private readonly ClassDiagram _diagram;
        private readonly string _name;
        private readonly List<Func<ClassDescriptor, bool>> _matcher = new List<Func<ClassDescriptor, bool>>();

        public string Name { get { return _name; } }

        internal ClassDiagramPackage(string name, ClassDiagram diagram)
        {
            _diagram = diagram;
            _name = name;
        }

        public ClassDiagramPackage IncludeClassesWhere(Func<ClassDescriptor, bool> filter)
        {
            _matcher.Add(filter);

            return this;
        }

        public ClassDiagram IncludeAll()
        {
            IncludeClassesWhere(descriptor => true);

            return _diagram;
        }

        public ClassDiagram Diagram { get { return _diagram; } }

        public bool IsMatch(ClassDescriptor classDescriptor)
        {
            return _matcher.Any(matcher => matcher(classDescriptor));
        }
    }
}