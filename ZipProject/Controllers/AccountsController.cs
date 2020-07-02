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
    public class AccountsController : ControllerBase
    {
        private readonly zip_dbContext _context;

        public AccountsController(zip_dbContext context)
        {
            _context = context;
        }

        // GET: accounts/ListAccounts
        [HttpGet("listaccounts")]
        public async Task<ActionResult<IEnumerable<Account>>> ListAccounts()
        {
            return await _context.Account.ToListAsync();
        }


        // POST: accounts/createaccount
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("createaccount")]
        public async Task<ActionResult<Account>> CreateAccount(User user)
        {
            var myUser =  _context.User.FirstOrDefault(x=>x.EmailAddress.Equals(user.EmailAddress));
            if (myUser == null) return BadRequest("No user with that email address!");
            if (myUser.Salary - myUser.Expenses < 1000) return BadRequest("Not enough cash, sorry!");

            //Do we allow multiple accounts for a user? Assuming yes...
            var newAccnt = new Account()
            {
                Amount = 1000,
                User = myUser,
                UserId = myUser.Id,

            };


            await _context.Account.AddAsync(newAccnt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = newAccnt.AccountId });
        }

        // GET: accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var aacnt = await _context.Account.FindAsync(id);

            if (aacnt == null)
            {
                return NotFound("No account with that primary key!");
            }

            return aacnt;
        }
    }
}
