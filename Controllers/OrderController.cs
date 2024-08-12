using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodHamburger.Data;
using GoodHamburger.Models;
using GoodHamburger.Services;

namespace GoodHamburger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly Context _context;

        public OrderController(OrderService orderService,Context context)
        {
            _orderService = orderService;
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // Patch: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchOrder(int id, Order updatedOrder)
        {
            var order = await _context.Orders.FindAsync(id);

            if (updatedOrder == null || order == null)
            {
                return BadRequest();
            }

            var needUpdateSandwichId = updatedOrder.GetType().GetProperty("SandwichId") != null;
            var needUpdateExtrasIds = updatedOrder.GetType().GetProperty("ExtrasIds") != null;


            if (needUpdateSandwichId) {
                var (isValid, err) = _orderService.ValidateSandwich(updatedOrder);
                if (!isValid) {
                    return BadRequest(err);
                }
                order.SandwichId = updatedOrder.SandwichId;
            }

            if (needUpdateExtrasIds) {
                var (isValid, err) = _orderService.ValidateExtra(updatedOrder);
                if (!isValid) {
                    return BadRequest(err);
                }
                order.ExtrasIds = updatedOrder.ExtrasIds;
            }

            var totalPrice = _orderService.CalculateOrderTotal(order);
            order.setTotalPrice(totalPrice);

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dictionary<string, Decimal>>> PostOrder(Order order)
        {
            var (isValid, err) = _orderService.ValidateOrder(order);
            if (!isValid) {
                return BadRequest(err);
            }

            var totalPrice = _orderService.CalculateOrderTotal(order);
            order.setTotalPrice(totalPrice);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var response = new Dictionary<string, Decimal>
            {
                { "totalPrice", totalPrice }
            };

            return Ok(response);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
