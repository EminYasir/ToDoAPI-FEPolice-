using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly PoliceAppDbContext _appDbContext;

        public ProductController(PoliceAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //GET :api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            var persons = _appDbContext.Person.ToList();
            var products = _appDbContext.Product.ToList();
            var policys = _appDbContext.Policys.ToList();

            if (_appDbContext.Product == null)
            {
                return NotFound();
            }
            return products;

        }

        //GET :api/Product/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_appDbContext.Product == null)
            {
                return NotFound();
            }
            var user = await _appDbContext.Product.FirstOrDefaultAsync(x => x.ProductId == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        //Post :api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _appDbContext.Product.Add(product);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        //PUT : api/Product/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (product.ProductId != id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ProductExists(id))
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

        private bool ProductExists(long id)
        {
            return (_appDbContext.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_appDbContext.Product == null)
            {
                return NotFound();
            }

            var user = await _appDbContext.Product.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _appDbContext.Product.Remove(user);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}