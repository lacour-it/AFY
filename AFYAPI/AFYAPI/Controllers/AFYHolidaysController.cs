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
    public class AFYHolidaysController : ControllerBase
    {
        private readonly AFYContext _context;

        public AFYHolidaysController(AFYContext context)
        {
            _context = context;
        }

        // GET: api/AFYHolidays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AFYHoliday>>> GetAFYHoliday()
        {
            return await _context.AFYHoliday.ToListAsync();
        }

        // GET: api/AFYHolidays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AFYHoliday>> GetAFYHoliday(int id)
        {
            var aFYHoliday = await _context.AFYHoliday.FindAsync(id);

            if (aFYHoliday == null)
            {
                return NotFound();
            }

            return aFYHoliday;
        }

        // PUT: api/AFYHolidays/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAFYHoliday(int id, AFYHoliday aFYHoliday)
        {
            if (id != aFYHoliday.AFYHolidayId)
            {
                return BadRequest();
            }

            _context.Entry(aFYHoliday).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AFYHolidayExists(id))
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

        // POST: api/AFYHolidays
        [HttpPost]
        public async Task<ActionResult<AFYHoliday>> PostAFYHoliday(AFYHoliday aFYHoliday)
        {
            _context.AFYHoliday.Add(aFYHoliday);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAFYHoliday", new { id = aFYHoliday.AFYHolidayId }, aFYHoliday);
        }

        // DELETE: api/AFYHolidays/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AFYHoliday>> DeleteAFYHoliday(int id)
        {
            var aFYHoliday = await _context.AFYHoliday.FindAsync(id);
            if (aFYHoliday == null)
            {
                return NotFound();
            }

            _context.AFYHoliday.Remove(aFYHoliday);
            await _context.SaveChangesAsync();

            return aFYHoliday;
        }

        private bool AFYHolidayExists(int id)
        {
            return _context.AFYHoliday.Any(e => e.AFYHolidayId == id);
        }
    }
}
