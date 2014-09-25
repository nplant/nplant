using System;
using System.Collections.Generic;
using NPlant.Core;
using NUnit.Framework;

namespace NPlant.Tests.Core
{
    [TestFixture]
    public class TypeMetaModelFixture
    {
        [Test]
        public void TypeMetaModel_Should_Be_Properly_Defaulted()
        {
            var model = new TypeMetaModel(typeof (Subject));

            Assert.That(model.Hidden, Is.False);
            Assert.That(model.HideAsBaseClass, Is.False);
            Assert.That(model.IsComplexType, Is.True);
            Assert.That(model.IsPrimitive, Is.False);
            Assert.That(model.Name, Is.EqualTo("Subject"));
            Assert.That(model.Note.ToString(), Is.EqualTo(string.Empty));
            Assert.That(model.TreatAllMembersAsPrimitives, Is.False);
        }

        [TestCase(typeof(int?), "Nullable<Int32>")]
        [TestCase(typeof(DateTime?), "Nullable<DateTime>")]
        [TestCase(typeof(List<int>), "List<Int32>")]
        [TestCase(typeof(List<int?>), "List<Nullable<Int32>>")]
        [TestCase(typeof(Dictionary<string, List<int?>>), "Dictionary<String, List<Nullable<Int32>>>")]
        public void Name_Formatting_Suite(Type type, string expectedName)
        {
            var model = new TypeMetaModel(type);

            Assert.That(model.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void TypeMetaModelSet_Should_Serve_Up_Singular_Instances()
        {
            TypeMetaModelSet set = new TypeMetaModelSet();
            var instance = set[typeof (Subject)];

            Assert.That(instance, Is.SameAs(set[typeof(Subject)]));
        }

        public class Subject { }
    }
}
