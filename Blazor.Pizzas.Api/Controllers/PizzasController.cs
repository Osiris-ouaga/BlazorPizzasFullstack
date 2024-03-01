using BlazorPizzas.API.Data;
using BlazorPizzas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorPizzas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {
        private readonly PizzasDbContext _context;

        public PizzasController(PizzasDbContext context) => _context = context;

        // GET: api/Pizzas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizza() => await _context.Pizza.ToListAsync();

        // GET: api/Pizzas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizza(int id)
        {
            var pizza = await _context.Pizza.FindAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }

        // PUT: api/Pizzas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizza(int id, Pizza pizza)
        {
            if (id != pizza.Id)
            {
                return BadRequest();
            }

            _context.Entry(pizza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(id))
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

        // POST: api/Pizzas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostPizza(Pizza pizza)
        {
            if (PizzaExists(pizza.Id))
            {
                return await PutPizza(pizza.Id, pizza);
            }
            else
            {
                _context.Pizza.Add(pizza);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPizza", new { id = pizza.Id }, pizza);
            }
        }

        // DELETE: api/Pizzas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var pizza = await _context.Pizza.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            _context.Pizza.Remove(pizza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaExists(int id) => _context.Pizza.Any(e => e.Id == id);
    }
}
