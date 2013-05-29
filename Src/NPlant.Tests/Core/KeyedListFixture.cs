using NPlant.Core;
using NUnit.Framework;

namespace NPlant.Tests.Core
{
    [TestFixture]
    public class KeyedListFixture
    {
        [Test]
        public void Can_Do_Simple_Adds_Like_A_Regular_List()
        {
            var list = new KeyedList<Subject>();

            list.Add(new Subject("a"));
            list.Add(new Subject("b"));
            list.Add(new Subject("c"));
            list.Add(new Subject("d"));

            Assert.That(list.Count, Is.EqualTo(4));
        }

        [Test]
        public void Duplicates_Are_Treated_As_Last_One_Wins()
        {
            var list = new KeyedList<Subject>();

            list.Add(new Subject("a"));
            list.Add(new Subject("b"));
            list.Add(new Subject("b"));
            var b = new Subject("b");
            list.Add(b);
            list.Add(new Subject("c"));
            list.Add(new Subject("d"));

            Assert.That(list.Count, Is.EqualTo(4));
            Assert.That(list["b"], Is.EqualTo(b));
        }

        [Test]
        public void Indexers_Are_Treated_As_Add()
        {
            var list = new KeyedList<Subject>();

            list["a"] = new Subject("a");
            list["b"] = new Subject("b");
            list["b"] = new Subject("b");
            var b = new Subject("b");
            list["b"] = b;
            list["c"] = new Subject("c");
            list["d"] = new Subject("d");

            Assert.That(list.Count, Is.EqualTo(4));
            Assert.That(list["b"], Is.EqualTo(b));
        }

        [Test]
        public void AddRange_Is_Treated_As_Many_Adds()
        {
            var list = new KeyedList<Subject>();

            var a = new Subject("a");
            var b = new Subject("b");
            var c = new Subject("c");
            var d = new Subject("d");

            list.AddRange(a, b, c, d);

            Assert.That(list.Count, Is.EqualTo(4));
            Assert.That(list["b"], Is.EqualTo(b));
        }

        public class Subject : IKeyedItem
        {
            public Subject(string key)
            {
                Key = key;
            }

            public string Key { get; private set; }
        }
    }
}
