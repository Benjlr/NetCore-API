using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        public async Task<ActionResult<IEnumerable<AccountModel>>> ListAccounts()
        {
            return await _context.AccountModel.Include(x => x.AccountOwnerNavigation).ToListAsync();
        }


        // POST: accounts/createaccount
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("createaccount")]
        public async Task<ActionResult<AccountModel>> CreateAccount(UserModel user)
        {
            var myUser = await _context.UserModel.FindAsync(user.EmailAddress);
            if (myUser == null) return BadRequest("No user with that email address!");
            if (myUser.AccountModel != null) return BadRequest("User already has account!");
            if (myUser.Salary - myUser.Expenses < 1000) return BadRequest("Not enough cash, sorry!");

            //Do we allow multiple accounts for a user? Assuming no, hence primary key for accounts table
            // is email which is also a foreign key back to users table...
            var account = new AccountModel
            {
                Amount = 1000,
                AccountOwner = myUser.EmailAddress
            };
            _context.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { EmailAddress = user.EmailAddress }, account);
        }

        // GET: accounts/fred@email.com
        [HttpGet("getaccount/{EmailAddress}")]
        public async Task<ActionResult<AccountModel>> GetAccount(string EmailAddress)
        {
            var aacnt =  await _context.AccountModel.FindAsync(EmailAddress);

            if (aacnt == null)
            {
                return NotFound("No account with that owner!");
            }

            return aacnt;

        }
    }
}
