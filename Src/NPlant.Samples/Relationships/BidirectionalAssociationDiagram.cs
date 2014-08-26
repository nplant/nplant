using System.Collections.Generic;

namespace NPlant.Samples.Relationships
{
    public class BidirectionalAssociationDiagram : ClassDiagram
    {
        public BidirectionalAssociationDiagram()
        {
            base.AddClass<Order>();
        }
    }

    public class Order
    {
        public long Id;
        public IList<OrderItem> Items;
    }

    public class OrderItem
    {
        public long Id;
        public Order Order;
        public Product Product;
        public Price Price;
    }

    public class Product
    {
        public long Id;
        public string Name;
        public string Description;
    }

    public class Price
    {
        public long Id;
        public string Name;
        public string Description;
        public decimal Amount;
    }
}
