namespace GoodHamburger.Models;

public class Sandwich
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
}