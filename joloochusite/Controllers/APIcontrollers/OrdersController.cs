using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using joloochusite.Models;
using joloochusite.Model;
using joloochusite.Data;

namespace joolochu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders
                .Include(e => e.Car)
                .Include(e => e.Car.Mark)
                .Include(e => e.Car.Mark.TypeCar)
                .Include(e => e.Car.User)
                .ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Where(p => p.Id == id)
                .Include(e => e.Car)
                .Include(e => e.Car.Mark)
                .Include(e => e.Car.Mark.TypeCar)
                .Include(e => e.Car.User)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

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

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders
                .Where(p => p.Id == id)
                .Include(e => e.Car)
                .Include(e => e.Car.Mark)
                .Include(e => e.Car.Mark.TypeCar)
                .Include(e => e.Car.User)
                .FirstOrDefaultAsync();
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        [HttpGet("{start}/{end}")]
        public async Task<ActionResult<List<Order>>> FindOrder(int start, int end)
        {
            var order = await _context.Orders
                .Include(e => e.Car)
                .Include(e => e.Car.Mark)
                .Include(e => e.Car.Mark.TypeCar)
                .Include(e => e.Car.User)
                .Where(e => e.StartPointId == start && e.EndPointId == end).ToListAsync() ?? null;
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}