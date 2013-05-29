using NPlant.MetaModel.ClassDiagraming;
using NUnit.Framework;

namespace NPlant.Tests.Diagrams.ClassDiagrams
{
    [TestFixture]
    public class SuppressionFixture
    {
        [Test]
        public void Can_Completely_Hide_A_Type_From_The_Diagram()
        {
            var simulation = new ClassDiagramSimulation(new Diagram());
            simulation.Simulate();

            Assert.That(simulation.Classes.Count, Is.EqualTo(3));

            Assert.That(simulation.Classes["Subject"], Is.Not.Null);
            Assert.That(simulation.Classes["Child1"], Is.Not.Null);
            Assert.That(simulation.Classes["Child2"], Is.Not.Null);
            Assert.That(simulation.Classes["Child3"], Is.Null);

            Assert.That(simulation.Classes["Subject"].Members["Child"].MetaModel.Hidden, Is.False);
            Assert.That(simulation.Classes["Child1"].Members["Child"].MetaModel.Hidden, Is.False);
            Assert.That(simulation.Classes["Child2"].Members["Child"].MetaModel.Hidden, Is.True);
        }

        internal class Diagram : ClassDiagram
        {
            public Diagram()
            {
                AddClass<Subject>();
                GenerationOptions.ForType<Child3>().Hide();
            }
        }

        public class Subject
        {
            public Child1 Child;
        }

        public class Child1
        {
            public Child2 Child;
        }

        public class Child2
        {
            public Child3 Child;
        }

        public class Child3
        {
            
        }
    }
}
