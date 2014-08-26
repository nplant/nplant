using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using NPlant.Generation.ClassDiagraming;
using NPlant.MetaModel.ClassDiagraming;
using NUnit.Framework;

namespace NPlant.Tests.Diagraming
{
    [TestFixture]
    public class ClassDescriptor_MemberScan_Fixture
    {
        [TestCase(typeof(PublicMembersOnly), ClassDiagramScanModes.PublicMembersOnly, new string[] { "Foo" })]
        [TestCase(typeof(AllMembers), ClassDiagramScanModes.AllMembers, new[] { "Foo", "Moo", "Bar", "Baz" })]
        [TestCase(typeof(DataContractSubject), ClassDiagramScanModes.SystemServiceModelMember, new[] { "Foo", "Baz" })]
        [TestCase(typeof(MessageContractSubject), ClassDiagramScanModes.SystemServiceModelMember, new[] { "Foo", "Baz" })]
        [TestCase(typeof(FieldDataContractSubject), ClassDiagramScanModes.SystemServiceModelMember, new[] { "Foo", "Baz" })]
        [TestCase(typeof(FieldMessageContractSubject), ClassDiagramScanModes.SystemServiceModelMember, new[] { "Foo", "Baz" })]
        [TestCase(typeof(FieldPropertyHybridDataContractSubject), ClassDiagramScanModes.SystemServiceModelMember, new[] { "Foo", "Baz" })]
        [TestCase(typeof(FieldPropertyHybridMessageContractSubject), ClassDiagramScanModes.SystemServiceModelMember, new[] { "Foo", "Baz" })]
        public void Scan_Suite(Type subjectType, ClassDiagramScanModes scanMode, string[] expectations)
        {
            using (new ClassDiagramGeneration(new StubClassDiagramVisitorContext(scanMode)))
            {
                ClassDescriptor descriptor = new ReflectedClassDescriptor(subjectType);
                descriptor.Visit();
                Assert.That(descriptor.Members.Count, Is.EqualTo(expectations.Length));

                for (int index = 0; index < expectations.Length; index++)
                {
                    Assert.That(descriptor.Members[index].Name, Is.EqualTo(expectations[index]));
                }
            }
        }

        public class PublicMembersOnly
        {
            public string Foo;

            // should not be scanned in
            protected string Moo;

            // should not be scanned in
            private string Bar = "";

            // should not be scanned in
            internal string Baz = "";

            public override string ToString()
            {
                // use Bar to get rid of "Warning as Error ... is never used" error. 
                return Bar;
            }
        }


        public class AllMembers
        {
            public string Foo;

            protected string Moo;

            private string Bar = "";

            internal string Baz = "";

            public override string ToString()
            {
                // use Bar to get rid of "Warning as Error ... is never used" errors. 
                return Bar;
            }
        }

        [DataContract]
        public class DataContractSubject
        {
            [DataMember]
            public string Foo { get; set; }

            // should not be scanned in
            public string Bar { get; set; }

            [DataMember]
            public string Baz { get; set; }
        }

        [MessageContract]
        public class MessageContractSubject
        {
            [MessageBodyMember]
            public string Foo { get; set; }

            // should not be scanned in
            public string Bar { get; set; }

            [MessageBodyMember]
            public string Baz { get; set; }
        }

        [DataContract]
        public class FieldDataContractSubject
        {
            [DataMember]
            public string Foo;

            // should not be scanned in
            public string Bar;

            [DataMember]
            public string Baz;
        }

        [MessageContract]
        public class FieldMessageContractSubject
        {
            [MessageBodyMember]
            public string Foo;

            // should not be scanned in
            public string Bar;

            [MessageBodyMember]
            public string Baz;
        }

        [DataContract]
        public class FieldPropertyHybridDataContractSubject
        {
            [DataMember]
            public string Foo;

            // should not be scanned in
            public string Bar;

            [DataMember]
            public string Baz { get; set; }
        }

        [MessageContract]
        public class FieldPropertyHybridMessageContractSubject
        {
            [MessageBodyMember]
            public string Foo;

            // should not be scanned in
            public string Bar;

            [MessageBodyMember]
            public string Baz { get; set; }
        }
    }
}
