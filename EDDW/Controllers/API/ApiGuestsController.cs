using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EDDW.Data;
using EDDW.Models;

namespace EDDW.Controllers.API
{
    [Route("api/Guest")]
    [ApiController]
    public class ApiGuestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiGuestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiGuests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Guest>>> GetGuest()
        {
            return await _context.Guest.ToListAsync();
        }

        // GET: api/ApiGuests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Guest>> GetGuest(Guid id)
        {
            var guest = await _context.Guest.FindAsync(id);

            if (guest == null)
            {
                return NotFound();
            }

            return guest;
        }

        // PUT: api/ApiGuests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGuest(Guid id, Guest guest)
        {
            if (id != guest.Id)
            {
                return BadRequest();
            }

            _context.Entry(guest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(id))
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

        // POST: api/ApiGuests
        [HttpPost]
        public async Task<ActionResult<Guest>> PostGuest(Guest guest)
        {
            _context.Guest.Add(guest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGuest", new { id = guest.Id }, guest);
        }

        // DELETE: api/ApiGuests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Guest>> DeleteGuest(Guid id)
        {
            var guest = await _context.Guest.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }

            _context.Guest.Remove(guest);
            await _context.SaveChangesAsync();

            return guest;
        }

        private bool GuestExists(Guid id)
        {
            return _context.Guest.Any(e => e.Id == id);
        }
    }
}
