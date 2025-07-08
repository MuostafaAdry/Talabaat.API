using Domain.Models.OrderModule;
using Domain.Models;

public class Order : BaseEntity<Guid>
{
    public Order() { }

    public Order(string buyerEmail, OrderAddress shipToAddress, DelivaryMethod delivaryMethod, ICollection<OrderItem> items,
        decimal subTotal, string paymentIntentId )
    {
        BuyerEmail = buyerEmail;
        ShipToAddress = shipToAddress;
        DelivaryMethod = delivaryMethod;
        Items = items;
        SubTotal = subTotal;
        PaymentIntentId = paymentIntentId;
    }

    public string BuyerEmail { get; set; }
    public OrderAddress ShipToAddress { get; set; }
    public string DelivaryMethodId { get; set; }
    public DelivaryMethod DelivaryMethod { get; set; }
    public ICollection<OrderItem> Items { get; set; }
    public decimal SubTotal { get; set; }
    public OrderStatus Status { get; set; }
    public decimal GetTotal() => SubTotal + DelivaryMethod.Price;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public string PaymentIntentId { get; set; }
}
