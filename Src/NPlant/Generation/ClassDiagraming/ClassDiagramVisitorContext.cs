using System;
using System.Collections.Generic;
using NPlant.Core;
using NPlant.MetaModel.ClassDiagraming;

namespace NPlant.Generation.ClassDiagraming
{
    public class ClassDiagramVisitorContext
    {
        private readonly Queue<AbstractClassDescriptor> _unvisitedRelatedClasses = new Queue<AbstractClassDescriptor>();
        private readonly List<ClassDiagramRelationship> _relationships = new List<ClassDiagramRelationship>();
        private readonly List<AbstractClassDescriptor> _visitedRelatedClasses = new List<AbstractClassDescriptor>();

        public ClassDiagramVisitorContext(ClassDiagram diagram, TypeMetaModelSet metaModelSet)
        {
            this.TypeMetaModelSet = metaModelSet;
            this.Diagram = diagram;
        }

        protected TypeMetaModelSet TypeMetaModelSet { get; private set; }

        protected ClassDiagram Diagram { get; set; }

        public void AddRelatedClass(AbstractClassDescriptor parent, AbstractClassDescriptor child, ClassDiagramRelationshipTypes relationshipType, int level, string name = null)
        {
            //todo:  wrap w/ a depth check?

            if (!_visitedRelatedClasses.Contains(child) && !_unvisitedRelatedClasses.Contains(child))
                _unvisitedRelatedClasses.Enqueue(child);

            var relationship = new ClassDiagramRelationship(name, parent, child, relationshipType);

            if (!_relationships.Contains(relationship))
                _relationships.Add(relationship);
        }

        public IEnumerable<AbstractClassDescriptor> VisitedRelatedClasses { get { return _visitedRelatedClasses; } }

        public IEnumerable<ClassDiagramRelationship> Relationships { get { return _relationships; } } 

        public TypeMetaModel GetTypeMetaModel(Type type)
        {
            return this.TypeMetaModelSet[type];
        }

        public void VisitAllRelatedClasses()
        {
            while (_unvisitedRelatedClasses.Count > 0)
            {
                var descriptor = _unvisitedRelatedClasses.Dequeue();

                descriptor.Visit(this);

                _visitedRelatedClasses.Add(descriptor);
            }
        }
    }
}