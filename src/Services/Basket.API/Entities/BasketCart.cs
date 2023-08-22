namespace Basket.API.Entities;

public class BasketCart
{
    public string Username { get; set; }

    public List<BasketCartItem> Items { get; } = new();

    public BasketCart()
    {
        
    }

    public BasketCart(string username)
    {
        Username = username;
    }

    public decimal TotalPrice
        => Items.Sum(e => e.Price * e.Quantity);
}