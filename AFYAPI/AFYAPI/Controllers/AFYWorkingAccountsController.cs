using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AFYAPI.Data;
using AFYAPI.Models;

namespace AFYAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AFYWorkingAccountsController : ControllerBase
    {
        private readonly AFYContext _context;

        public AFYWorkingAccountsController(AFYContext context)
        {
            _context = context;
        }

        // GET: api/AFYWorkingAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AFYWorkingAccount>>> GetAFYWorkingAccount()
        {
            return await _context.AFYWorkingAccount.ToListAsync();
        }

        // GET: api/AFYWorkingAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AFYWorkingAccount>> GetAFYWorkingAccount(int id)
        {
            var aFYWorkingAccount = await _context.AFYWorkingAccount.FindAsync(id);

            if (aFYWorkingAccount == null)
            {
                return NotFound();
            }

            return aFYWorkingAccount;
        }

        // PUT: api/AFYWorkingAccounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAFYWorkingAccount(int id, AFYWorkingAccount aFYWorkingAccount)
        {
            if (id != aFYWorkingAccount.AFYWorkingAccountId)
            {
                return BadRequest();
            }

            _context.Entry(aFYWorkingAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AFYWorkingAccountExists(id))
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

        // POST: api/AFYWorkingAccounts
        [HttpPost]
        public async Task<ActionResult<AFYWorkingAccount>> PostAFYWorkingAccount(AFYWorkingAccount aFYWorkingAccount)
        {
            _context.AFYWorkingAccount.Add(aFYWorkingAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAFYWorkingAccount", new { id = aFYWorkingAccount.AFYWorkingAccountId }, aFYWorkingAccount);
        }

        // DELETE: api/AFYWorkingAccounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AFYWorkingAccount>> DeleteAFYWorkingAccount(int id)
        {
            var aFYWorkingAccount = await _context.AFYWorkingAccount.FindAsync(id);
            if (aFYWorkingAccount == null)
            {
                return NotFound();
            }

            _context.AFYWorkingAccount.Remove(aFYWorkingAccount);
            await _context.SaveChangesAsync();

            return aFYWorkingAccount;
        }

        private bool AFYWorkingAccountExists(int id)
        {
            return _context.AFYWorkingAccount.Any(e => e.AFYWorkingAccountId == id);
        }
    }
}
