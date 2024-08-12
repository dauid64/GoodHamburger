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
    var sandwich = order.SandwichId.HasValue ? _context.Sandwiches.Find((long)order.SandwichId) : null;
    var extrasIds = order.ExtrasIds;

    decimal total = 0.0m;

    if (sandwich != null) total += sandwich.Price;

    List<Extra> extras = new List<Extra>();

    if (order.ExtrasIds != null) {
      foreach (var extraId in order.ExtrasIds)
      {
          var extra = _context.Extras.Find((long)extraId);
          if (extra != null)
          {
            total += extra.Price;
            extras.Add(extra);
          }
      }
    }

    if (sandwich != null && extras.Any())
    {
      string[] extraNames = extras.Select(e => e.Name).ToArray();
      if (extraNames.Contains("Fries") && extraNames.Contains("Soft Drink"))
        total *= 0.8m;
      else if (extraNames.Contains("Fries"))
        total *= 0.85m;
      else if (extraNames.Contains("Soft Drink"))
        total *= 0.9m;
    }
    
    return total;
  }

  public (bool, string) ValidateOrder(Order order)
  {
    var (isValid, err) = ValidateExtra(order);
    if (!isValid)
      return (false, err);

    (isValid, err) = ValidateExtra(order);
    if (!isValid)
      return (false, err);
    
    return (true, "");
  }

  public (bool, string) ValidateSandwich(Order order) {
    if (order.SandwichId.HasValue && _context.Sandwiches.Find((long)order.SandwichId) == null)
      return (false, "Sandwich not found");
    return (true, "");
  }

  public (bool, string) ValidateExtra(Order order) {
    if (order.ExtrasIds != null && order.ExtrasIds.Any())
    {
      List<int> AddedExtrasIds = new List<int>();
      foreach (var extraId in order.ExtrasIds)
      {
        if (AddedExtrasIds.Contains(extraId))
          return (false, "Duplicate extra");
        if (_context.Extras.Find((long)extraId) == null)
          return (false, "Extra not found");
        AddedExtrasIds.Add(extraId);
      }
    }
    if (order.ExtrasIds != null && !order.ExtrasIds.Any()) {
      return (false, "ExtrasIds cannot be empty. For set empty extras, please set null.");
    }

    return (true, "");
  }
}
}
