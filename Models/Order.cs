namespace GoodHamburger.Models;


public class Order
{
    public int Id { get; private set; }
    public int? SandwichId { get; set; }
    public int? FriesId { get; set; }
    public int? DrinkId { get; set; }
    public decimal TotalPrice { get; set; }
}
