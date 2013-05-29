using NPlant.Tests.Readers;
using NUnit.Framework;

namespace NPlant.Tests.ClassDiagrams
{
    [TestFixture]
    public class SimpleScenarios
    {
        [Test]
        public void GIVEN_A_Simple_Single_Entity_Diagram_WHEN_Rendered_With_Defaults_THEN_Diagram_Has_One_Class_With_Members()
        {
            var reader = ClassDiagramReader.Read(new DefaultBehaviorDiagram());

            Assert.That(reader.Classes.Length, Is.EqualTo(1));
            Assert.That(reader.Classes[0].Name, Is.EqualTo("SimpleEntity"));
            Assert.That(reader.Classes[0].Members.Length, Is.EqualTo(3));
            Assert.That(reader.Classes[0].Members[0].Name, Is.EqualTo("Foo"));
            Assert.That(reader.Classes[0].Members[1].Name, Is.EqualTo("Bar"));
            Assert.That(reader.Classes[0].Members[2].Name, Is.EqualTo("Baz"));
        }

        [Test]
        public void GIVEN_A_Simple_Single_Entity_Diagram_WHEN_Rendered_With_Hidden_Fields_THEN_Diagram_Has_One_Class_With_Members_Selectively_Hidden()
        {
            var reader = ClassDiagramReader.Read(new HiddenFieldBarDiagram());

            Assert.That(reader.Classes.Length, Is.EqualTo(1));
            Assert.That(reader.Classes[0].Name, Is.EqualTo("SimpleEntity"));
            Assert.That(reader.Classes[0].Members.Length, Is.EqualTo(2));
            Assert.That(reader.Classes[0].Members[0].Name, Is.EqualTo("Foo"));
            Assert.That(reader.Classes[0].Members[1].Name, Is.EqualTo("Baz"));
        }

        public class DefaultBehaviorDiagram : ClassDiagramDefinition
        {
            public DefaultBehaviorDiagram()
            {
                this.AddClass<SimpleEntity>();
            }
        }
        public class HiddenFieldBarDiagram : ClassDiagramDefinition
        {
            public HiddenFieldBarDiagram()
            {
                this.AddClass<SimpleEntity>().HideMember(x => x.Bar);
            }
        }

        public class SimpleEntity
        {
            public string Foo;
            public string Bar;
            public string Baz;
        }
    }
}
