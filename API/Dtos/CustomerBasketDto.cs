using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace api.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public decimal ShippingPrice { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}