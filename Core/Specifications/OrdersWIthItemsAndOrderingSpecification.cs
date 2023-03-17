using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersWIthItemsAndOrderingSpecification : BaseSpecification<Order>
    {
        public OrdersWIthItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.Id);
        }

        public OrdersWIthItemsAndOrderingSpecification(int id, string email) :
        base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}