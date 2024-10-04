using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementApp.Models;
using ProductManagementApp.Data;
using System.Diagnostics;

namespace ProductManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Json(products);
        }

        [HttpPost("api/products")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("api/products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("api/products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
