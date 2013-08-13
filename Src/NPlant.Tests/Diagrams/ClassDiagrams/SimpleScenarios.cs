using NPlant.MetaModel.ClassDiagraming;
using NUnit.Framework;

namespace NPlant.Tests.Diagrams.ClassDiagrams
{
    [TestFixture]
    public class SimpleScenarios
    {
        [Test]
        public void GIVEN_A_Simple_Single_Entity_Diagram_WHEN_Rendered_With_Defaults_THEN_Diagram_Has_One_Class_With_Members()
        {
            var simulation = new ClassDiagramSimulation(new ClassDiagram(typeof(SimpleEntity)));
            simulation.Simulate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(1));
            Assert.That(simulation.Classes[0].Name, Is.EqualTo("SimpleEntity"));
            Assert.That(simulation.Classes[0].Members.Count, Is.EqualTo(3));
            Assert.That(simulation.Classes[0].Members[0].Key, Is.EqualTo("Foo"));
            Assert.That(simulation.Classes[0].Members[1].Key, Is.EqualTo("Bar"));
            Assert.That(simulation.Classes[0].Members[2].Key, Is.EqualTo("Baz"));
        }

        [Test]
        public void GIVEN_A_Simple_Single_Entity_Diagram_WHEN_Rendered_With_Hidden_Fields_THEN_Diagram_Has_One_Class_With_Members_Selectively_Hidden()
        {
            var simulation = new ClassDiagramSimulation(new HiddenFieldBarDiagram());
            simulation.Simulate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(1));
            Assert.That(simulation.Classes[0].Name, Is.EqualTo("SimpleEntity"));
            Assert.That(simulation.Classes[0].Members.Count, Is.EqualTo(3));
            Assert.That(simulation.Classes[0].Members[0].Key, Is.EqualTo("Foo"));
            Assert.That(simulation.Classes[0].Members[1].Key, Is.EqualTo("Bar"));
            Assert.That(simulation.Classes[0].Members[2].Key, Is.EqualTo("Baz"));
        }

        public class HiddenFieldBarDiagram : ClassDiagram
        {
            public HiddenFieldBarDiagram()
            {
                this.AddClass<SimpleEntity>().ForMember(x => x.Bar);
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
