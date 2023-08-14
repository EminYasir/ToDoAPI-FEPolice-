using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicysController : ControllerBase
    {
        private readonly PoliceAppDbContext _appDbContext;

        public PolicysController(PoliceAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        //GET :api/Policys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Policys>>> GetPolicy()
        {

            if (_appDbContext.Policys == null)
            {
                return NotFound();
            }

            var persons = _appDbContext.Person.ToList();
            var products = _appDbContext.Product.ToList();
            var policys = _appDbContext.Policys.ToList();

            //var policysAsync = _appDbContext.Policys.ToListAsync();

            //policys.ForEach(op => op.Person = persons.FirstOrDefault(op1 => op1.PersonId == op.PersonId));
            //policys.ForEach(op => op.Product = products.FirstOrDefault(op1 => op1.ProductId == op.ProductId));

            return policys;

        }

        //GET :api/Policys/1
        [HttpGet("{number}")]
        public async Task<ActionResult<Policys>> GetPolicy(int number)
        {
            if (_appDbContext.Policys == null)
            {
                return NotFound();
            }
            var persons = _appDbContext.Person.ToList();
            var products = _appDbContext.Product.ToList();
            var policys = _appDbContext.Policys.ToList();

            var policy = await _appDbContext.Policys.FirstOrDefaultAsync(x => x.PolicyNumber == number);
            if (policy == null)
            {
                return NotFound();
            }
            return policy;
        }

        //Post :api/Policys
        [HttpPost]
        public async Task<ActionResult<Policys>> PostPolicy(Policys policy)
        {
            
            

            _appDbContext.Policys.Add(policy);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPolicy), new { id = policy.PolicyNumber }, policy);
        }

        //PUT : api/Policys/2
        [HttpPut("{number}")]
        public async Task<IActionResult> PutPolicy(int number, Policys policy)
        {
            var persons = _appDbContext.Person.ToList();
            var products = _appDbContext.Product.ToList();
            var policys = _appDbContext.Policys.ToList();

            if (policy.PolicyNumber != number)
            {
                return BadRequest();
            }

            _appDbContext.Entry(policy).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!UserExists(number))
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

        private bool UserExists(long number)
        {
            var persons = _appDbContext.Person.ToList();
            var products = _appDbContext.Product.ToList();
            var policys = _appDbContext.Policys.ToList();

            return (_appDbContext.Policys?.Any(e => e.PolicyNumber == number)).GetValueOrDefault();
        }

        [HttpDelete("{number}")]
        public async Task<IActionResult> DeletePolicy(int number)
        {
            

            if (_appDbContext.Policys == null)
            {
                return NotFound();
            }

            var policy = await _appDbContext.Policys.FindAsync(number);
            if (policy == null)
            {
                return NotFound();
            }

            _appDbContext.Policys.Remove(policy);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
