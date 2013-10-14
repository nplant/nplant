namespace NPlant.MetaModel.ClassDiagraming
{
    public class ClassDiagramRelationship
    {
        public ClassDiagramRelationship(string name, AbstractClassDescriptor party1, AbstractClassDescriptor party2, ClassDiagramRelationshipTypes relationshipType)
        {
            Name = name;
            Party1 = party1;
            Party2 = party2;
            RelationshipType = relationshipType;
        }

        public string Name { get; private set; }

        public AbstractClassDescriptor Party1 { get; private set; }

        public AbstractClassDescriptor Party2 { get; private set; }

        public ClassDiagramRelationshipTypes RelationshipType { get; private set; }

        public override bool Equals(object obj)
        {
            ClassDiagramRelationship relationship = obj as ClassDiagramRelationship;

            if (relationship == null)
                return false;

            return object.Equals(relationship.Party1, this.Party1) &&
                   object.Equals(relationship.Party2, this.Party2) &&
                   string.Equals(relationship.Name, this.Name);
        }

        public override int GetHashCode()
        {
            return this.Party1.GetHashCode() + this.Party2.GetHashCode() + this.Name.GetHashCode();
        }
    }

    public enum ClassDiagramRelationshipTypes
    {
        HasA,
        HasMany,
        Base,
        Dependency
    }
}