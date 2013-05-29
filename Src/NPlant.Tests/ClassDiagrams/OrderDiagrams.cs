using CSG.ACPx.Core.Service.Contract;
using OrMSUserInfo=CSG.ACPx.OrderManagement.Contract.CommonModel.Data.UserInfo;
using CSG.ACPx.OrderPresentation.Contract.Common.Context;
using NPlant.Tests.Readers;
using NUnit.Framework;
using CIMUserInfo = CSG.ACPx.CIM.Contract.Common.Data.UserInfo;

namespace NPlant.Tests.ClassDiagrams
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestIt()
        {
            var reader = ClassDiagramReader.Read(new CustomerProcessContractsClassDiagram());

            Assert.That(reader.Classes.Length, Is.GreaterThan(0));
        }
    }

    public class CustomerProcessContractsClassDiagram : ClassDiagramDefinition
    {
        public CustomerProcessContractsClassDiagram()
        {
            base.AddClass<CustomerProcessResponseContext>().HideMember(x => x.Order).HideMember(x => x.ShoppingCart);

            base.TypeOf<CodeData>().AsPrimitives();
            base.TypeOf<CodeDataCollection>().AsPrimitives();
            base.TypeOf<PropertyAccessCollection>().AsPrimitives();
            base.TypeOf<OrMSUserInfo>().AsPrimitives();
            base.TypeOf<CIMUserInfo>().AsPrimitives();
        }

        public override string Name
        {
            get
            {
                return "CustomerProcessContractsClassDiagram";
            }
        }
    }
}
