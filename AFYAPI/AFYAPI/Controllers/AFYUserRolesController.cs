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
    public class AFYUserRolesController : ControllerBase
    {
        private readonly AFYContext _context;

        public AFYUserRolesController(AFYContext context)
        {
            _context = context;
        }

        // GET: api/AFYUserRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AFYUserRole>>> GetUserRoles()
        {
            return await _context.UserRoles.ToListAsync();
        }

        // GET: api/AFYUserRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AFYUserRole>> GetAFYUserRole(int id)
        {
            var aFYUserRole = await _context.UserRoles.FindAsync(id);

            if (aFYUserRole == null)
            {
                return NotFound();
            }

            return aFYUserRole;
        }

        // PUT: api/AFYUserRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAFYUserRole(int id, AFYUserRole aFYUserRole)
        {
            if (id != aFYUserRole.AFYUserRoleId)
            {
                return BadRequest();
            }

            _context.Entry(aFYUserRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AFYUserRoleExists(id))
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

        // POST: api/AFYUserRoles
        [HttpPost]
        public async Task<ActionResult<AFYUserRole>> PostAFYUserRole(AFYUserRole aFYUserRole)
        {
            _context.UserRoles.Add(aFYUserRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAFYUserRole", new { id = aFYUserRole.AFYUserRoleId }, aFYUserRole);
        }

        // DELETE: api/AFYUserRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AFYUserRole>> DeleteAFYUserRole(int id)
        {
            var aFYUserRole = await _context.UserRoles.FindAsync(id);
            if (aFYUserRole == null)
            {
                return NotFound();
            }

            _context.UserRoles.Remove(aFYUserRole);
            await _context.SaveChangesAsync();

            return aFYUserRole;
        }

        private bool AFYUserRoleExists(int id)
        {
            return _context.UserRoles.Any(e => e.AFYUserRoleId == id);
        }
    }
}
