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
    public class AFYWorkingTimesController : ControllerBase
    {
        private readonly AFYContext _context;

        public AFYWorkingTimesController(AFYContext context)
        {
            _context = context;
        }

        // GET: api/AFYWorkingTimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AFYWorkingTime>>> GetAFYWorkingTime()
        {
            return await _context.AFYWorkingTime.ToListAsync();
        }

        // GET: api/AFYWorkingTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AFYWorkingTime>> GetAFYWorkingTime(int id)
        {
            var aFYWorkingTime = await _context.AFYWorkingTime.FindAsync(id);

            if (aFYWorkingTime == null)
            {
                return NotFound();
            }

            return aFYWorkingTime;
        }

        // PUT: api/AFYWorkingTimes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAFYWorkingTime(int id, AFYWorkingTime aFYWorkingTime)
        {
            if (id != aFYWorkingTime.AFYWorkingTimeId)
            {
                return BadRequest();
            }

            _context.Entry(aFYWorkingTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AFYWorkingTimeExists(id))
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

        // POST: api/AFYWorkingTimes
        [HttpPost]
        public async Task<ActionResult<AFYWorkingTime>> PostAFYWorkingTime(AFYWorkingTime aFYWorkingTime)
        {
            _context.AFYWorkingTime.Add(aFYWorkingTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAFYWorkingTime", new { id = aFYWorkingTime.AFYWorkingTimeId }, aFYWorkingTime);
        }

        // DELETE: api/AFYWorkingTimes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AFYWorkingTime>> DeleteAFYWorkingTime(int id)
        {
            var aFYWorkingTime = await _context.AFYWorkingTime.FindAsync(id);
            if (aFYWorkingTime == null)
            {
                return NotFound();
            }

            _context.AFYWorkingTime.Remove(aFYWorkingTime);
            await _context.SaveChangesAsync();

            return aFYWorkingTime;
        }

        private bool AFYWorkingTimeExists(int id)
        {
            return _context.AFYWorkingTime.Any(e => e.AFYWorkingTimeId == id);
        }
    }
}
