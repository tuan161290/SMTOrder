using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WiSolSMTRepo;
using WiSolSMTRepo.Model;

namespace WiSolSMTRepo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly SMTDbContext _context;

        public OrdersController(SMTDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetUnconfirmOrder()
        {
            return await _context.Orders.Where(x => x.IsConfirmed == false).ToListAsync();
        }
        [HttpGet("{LineID}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetUnconfirmOrders(int LineID)
        {
            return await _context.Orders.Where(x => x.IsConfirmed == false && x.LineInfoID == LineID).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetUnconfirmOrders()
        {
            //var UnconfirmOrders = await _context.Orders.Where(o => o.IsConfirmed == false).Include(x => x.Product).Include(x => x.LineInfo).ToListAsync();
            var UnconfirmOrders = await _context.Orders.Where(o => o.IsConfirmed == false).Include(x => x.LineInfo).Include(x => x.Product).ToListAsync();
            return Ok(UnconfirmOrders);
        }

        [HttpPut]
        public async Task<IActionResult> DiscardRemainOrder(PlanInfo plan)
        {
            try
            {
                var Plan = _context.Plans.Single(x => x.PlanInfoID == plan.PlanInfoID);
                var RemainOrders = _context.Entry(Plan).Collection(b => b.Orders).Query().Where(o => o.IsConfirmed == false);
                await RemainOrders.ForEachAsync(o =>
                {
                    o.IsConfirmed = true;
                    _context.Entry(o).State = EntityState.Modified;
                });
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ConfirmOrder(int id, Order order)
        {
            if (id != order.OrderID)
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

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderID)
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
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetOrder", new { id = order.OrderID }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }
}
