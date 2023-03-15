using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyEmail, int deliveryMethod, string basketId, Address shippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyEmail);
        Task<Order> GetOrderByIdAsync(int id, string buyEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}