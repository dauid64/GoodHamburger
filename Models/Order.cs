namespace GoodHamburger.Models;


public class Order
{
    public int Id { get; private set; }
    public int? SandwichId { get; set; }
    public int[]? ExtrasIds {get; set; }
    public decimal TotalPrice { get; private set; }

    public void setTotalPrice(decimal totalPrice)
    {
        TotalPrice = totalPrice;
    }
}
