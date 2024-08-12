using GoodHamburger.Data;
using GoodHamburger.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Services

{
public class OrderService
{
  private readonly Context _context;

  public OrderService(Context context)
  {
    _context = context;
  }

  public decimal CalculateOrderTotal(Order order)
  {
    var sandwich = order.SandwichId.HasValue ? _context.Sandwiches.Find(order.SandwichId) : null;
    var fries = order.FriesId.HasValue ? _context.Extras.Find(order.FriesId.Value) : null;
    var drink = order.DrinkId.HasValue ? _context.Extras.Find(order.DrinkId.Value) : null;

    decimal total = 0.0m;

    if (sandwich != null) total += sandwich.Price;
    if (fries != null) total += fries.Price;
    if (drink != null) total += drink.Price;

    if (sandwich != null && fries != null && drink != null)
        total *= 0.8m; // 20% desconto
    else if (sandwich != null && drink != null)
        total *= 0.85m; // 15% desconto
    else if (sandwich != null && fries != null)
        total *= 0.9m; // 10% desconto
    
    return total;
  }

  public bool ValidateOrder(Order order)
  {
    if (order.SandwichId.HasValue && _context.Sandwiches.Find(order.SandwichId) == null)
      return false;
    if (order.FriesId.HasValue && _context.Extras.Find(order.FriesId.Value) == null)
      return false;
    if (order.DrinkId.HasValue && _context.Extras.Find(order.DrinkId.Value) == null)
      return false;
    
    return true;
  }
}
}
