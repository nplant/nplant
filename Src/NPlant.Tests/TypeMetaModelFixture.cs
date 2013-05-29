using System;
using System.Collections.Generic;
using NPlant.Core;
using NUnit.Framework;

namespace NPlant.Tests
{
    [TestFixture]
    public class TypeMetaModelFixture
    {
        [TestCase(typeof(int?), "Nullable<Int32>")]
        [TestCase(typeof(DateTime?), "Nullable<DateTime>")]
        [TestCase(typeof(List<int>), "List<Int32>")]
        [TestCase(typeof(List<int?>), "List<Nullable<Int32>>")]
        [TestCase(typeof(Dictionary<string, List<int?>>), "Dictionary<String, List<Nulable<Int32>>>")]
        public void Name_Formatting_Suite(Type type, string expectedName)
        {
            var model = new TypeMetaModel(type);

            Assert.That(model.Name, Is.EqualTo(expectedName));
        }
    }
}
