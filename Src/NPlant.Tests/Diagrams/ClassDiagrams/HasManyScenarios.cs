using System.Collections.Generic;
using NPlant.MetaModel.ClassDiagraming;
using NUnit.Framework;

namespace NPlant.Tests.ClassDiagrams
{
    [TestFixture]
    public class HasManyScenarios
    {
        [Test]
        public void TestPersonHasHands()
        {
            var simulation = new SimulatedClassDiagramGenerator(new SimpleHasManyDiagram());
            simulation.Generate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(2));
            Assert.That(simulation.Classes[0].Name, Is.EqualTo("Person"));
            Assert.That(simulation.Classes[1].Name, Is.EqualTo("Hand"));
        }

        public class SimpleHasManyDiagram : ClassDiagram
        {
            public SimpleHasManyDiagram()
            {
                this.AddClass<Person>();
            }
        }

        public class Person
        {
            public IList<Hand> Hands { get; set; }
        }

        public class Hand
        {
            
        }
    }
}