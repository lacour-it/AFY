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
    public class AFYRolesController : ControllerBase
    {
        private readonly AFYContext _context;

        public AFYRolesController(AFYContext context)
        {
            _context = context;
        }

        // GET: api/AFYRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AFYRole>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET: api/AFYRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AFYRole>> GetAFYRole(int id)
        {
            var aFYRole = await _context.Roles.FindAsync(id);

            if (aFYRole == null)
            {
                return NotFound();
            }

            return aFYRole;
        }

        // PUT: api/AFYRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAFYRole(int id, AFYRole aFYRole)
        {
            if (id != aFYRole.AFYRoleId)
            {
                return BadRequest();
            }

            _context.Entry(aFYRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AFYRoleExists(id))
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

        // POST: api/AFYRoles
        [HttpPost]
        public async Task<ActionResult<AFYRole>> PostAFYRole(AFYRole aFYRole)
        {
            _context.Roles.Add(aFYRole);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAFYRole", new { id = aFYRole.AFYRoleId }, aFYRole);
        }

        // DELETE: api/AFYRoles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AFYRole>> DeleteAFYRole(int id)
        {
            var aFYRole = await _context.Roles.FindAsync(id);
            if (aFYRole == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(aFYRole);
            await _context.SaveChangesAsync();

            return aFYRole;
        }

        private bool AFYRoleExists(int id)
        {
            return _context.Roles.Any(e => e.AFYRoleId == id);
        }
    }
}
