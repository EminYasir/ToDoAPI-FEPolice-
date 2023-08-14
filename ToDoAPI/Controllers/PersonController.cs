using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoAPI.Models;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PoliceAppDbContext _appDbContext;

        public PersonController(PoliceAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //GET :api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            if (_appDbContext.Person == null)
            {
                return NotFound();
            }
            return await _appDbContext.Person.ToListAsync();

        }

        //GET :api/Person/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            if (_appDbContext.Person == null)
            {
                return NotFound();
            }
            var user = await _appDbContext.Person.FirstOrDefaultAsync(x => x.PersonId == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }


        //Post :api/Person
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person Person)
        {
            _appDbContext.Person.Add(Person);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerson), new { id = Person.PersonId }, Person);
        }

        //PUT : api/Person/2
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (person.PersonId != id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(person).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!PersonExists(id))
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

        private bool PersonExists(long id)
        {
            return (_appDbContext.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            if (_appDbContext.Person == null)
            {
                return NotFound();
            }

            var user = await _appDbContext.Person.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _appDbContext.Person.Remove(user);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            Person user = _appDbContext.Person.SingleOrDefault(u => u.Adi + "" + u.Soyadi == model.KullaniciAdi && u.Password == model.Password);

            if (user != null)
            {
                // Kullanıcı doğrulandı
                var token = GenerateJwtToken(user); // Token üretimi için bir metot çağırılmalı
                return Ok(new { message = "Giriş başarılı.", token,user });
            }

            // Kullanıcı doğrulanamadı
            return BadRequest(new { message = "Geçersiz kullanıcı adı veya şifre." });
        }


        private string GenerateJwtToken(Person user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("a9d1j8s2kf73dl0g"); // Gizli anahtarınızı buraya yerleştirin
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.Adi + " " + user.Soyadi)
            // Diğer isteğe bağlı idari yetkileri burada ekleyebilirsiniz.
        }),
                Expires = DateTime.UtcNow.AddHours(1), // Token geçerlilik süresi
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
