using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZipProject.Model;

namespace ZipProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly zip_dbContext _context;

        public UsersController(zip_dbContext context)
        {
            _context = context;
        }

        // POST: users/CreateUser
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            //Validation todo move to seperate class
            if (string.IsNullOrWhiteSpace(user.EmailAddress)) return BadRequest("Please enter your email address!");
            if (string.IsNullOrWhiteSpace(user.Name)) return BadRequest("Please enter your name!");
            if (user.Expenses < 0) return BadRequest("Expenses must be larger than or equal to zero!");
            if (user.Salary < 0) return BadRequest("Salary must be larger than or equal to zero!");

            if (_context.User.Any(x => x.EmailAddress.Equals(user.EmailAddress))) return BadRequest("Email Address Already Exists!");


            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }


        // GET: users/listusers
        [HttpGet("listusers")]
        public async Task<ActionResult<IEnumerable<User>>> ListUsers()
        {
            
            return await _context.User.ToListAsync();
        }

        // GET: users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound("No user with that primary key!");
            }

            return user;
        }

        // GET: users/fred@email.com
        [HttpGet("{EmailAddress}")]
        public async Task<ActionResult<User>> GetUser(string email)
        {
            var user = await _context.User.FirstAsync(x => x.EmailAddress.Equals(email));

            if (user == null)
            {
                return NotFound("No user with that email!");
            }

            return user;
        }

    }
}
