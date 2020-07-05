using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

        // POST: users/createuser
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("createuser")]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel user)
        {
            //Validation todo move to seperate class
            if (string.IsNullOrWhiteSpace(user.EmailAddress)) return BadRequest("Please enter your email address!");
            if (string.IsNullOrWhiteSpace(user.Name)) return BadRequest("Please enter your name!");
            if (user.Expenses < 0) return BadRequest("Expenses must be larger than or equal to zero!");
            if (user.Salary < 0) return BadRequest("Salary must be larger than or equal to zero!");

            if (_context.UserModel.Find(user.EmailAddress) != null) return BadRequest("Email Address Already Exists!");


            _context.UserModel.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { EmailAddress = user.EmailAddress }, user);
        }


        // GET: users/listusers
        [HttpGet("listusers")]
        public async Task<ActionResult<IEnumerable<UserModel>>> ListUsers()
        {
            
            return await _context.UserModel.Include(x=>x.AccountModel).ToListAsync();
        }


        // GET: users/fred@email.com
        [HttpGet("getuser/{EmailAddress}")]
        public async Task<ActionResult<UserModel>> GetUser(string EmailAddress)
        {
            var user = await _context.UserModel.FindAsync(EmailAddress);

            if (user == null)
            {
                return NotFound("No user with that email!");
            }

            return user;
        }

    }
}
