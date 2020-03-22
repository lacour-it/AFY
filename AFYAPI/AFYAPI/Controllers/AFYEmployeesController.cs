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
    public class AFYEmployeesController : ControllerBase
    {
        private readonly AFYContext _context;

        public AFYEmployeesController(AFYContext context)
        {
            _context = context;
        }

        // GET: api/AFYEmployees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AFYEmployee>>> GetAFYEmployee()
        {
            return await _context.AFYEmployee.ToListAsync();
        }

        // GET: api/AFYEmployees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AFYEmployee>> GetAFYEmployee(int id)
        {
            var aFYEmployee = await _context.AFYEmployee.FindAsync(id);

            if (aFYEmployee == null)
            {
                return NotFound();
            }

            return aFYEmployee;
        }

        // PUT: api/AFYEmployees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAFYEmployee(int id, AFYEmployee aFYEmployee)
        {
            if (id != aFYEmployee.AFYEmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(aFYEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AFYEmployeeExists(id))
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

        // POST: api/AFYEmployees
        [HttpPost]
        public async Task<ActionResult<AFYEmployee>> PostAFYEmployee(AFYEmployee aFYEmployee)
        {
            _context.AFYEmployee.Add(aFYEmployee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAFYEmployee", new { id = aFYEmployee.AFYEmployeeId }, aFYEmployee);
        }

        // DELETE: api/AFYEmployees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AFYEmployee>> DeleteAFYEmployee(int id)
        {
            var aFYEmployee = await _context.AFYEmployee.FindAsync(id);
            if (aFYEmployee == null)
            {
                return NotFound();
            }

            _context.AFYEmployee.Remove(aFYEmployee);
            await _context.SaveChangesAsync();

            return aFYEmployee;
        }

        private bool AFYEmployeeExists(int id)
        {
            return _context.AFYEmployee.Any(e => e.AFYEmployeeId == id);
        }
    }
}
