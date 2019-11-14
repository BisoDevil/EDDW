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
    [Route("api/Booth")]
    [ApiController]
    public class ApiBoothsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiBoothsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiBooths
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booth>>> GetBooth()
        {
            return await _context.Booth.ToListAsync();
        }

        // GET: api/ApiBooths/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booth>> GetBooth(int id)
        {
            var booth = await _context.Booth.FindAsync(id);

            if (booth == null)
            {
                return NotFound();
            }

            return booth;
        }

        // PUT: api/ApiBooths/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooth(int id, Booth booth)
        {
            if (id != booth.Id)
            {
                return BadRequest();
            }

            _context.Entry(booth).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoothExists(id))
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

        // POST: api/ApiBooths
        [HttpPost]
        public async Task<ActionResult<Booth>> PostBooth(Booth booth)
        {
            _context.Booth.Add(booth);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBooth", new { id = booth.Id }, booth);
        }

        // DELETE: api/ApiBooths/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Booth>> DeleteBooth(int id)
        {
            var booth = await _context.Booth.FindAsync(id);
            if (booth == null)
            {
                return NotFound();
            }

            _context.Booth.Remove(booth);
            await _context.SaveChangesAsync();

            return booth;
        }

        private bool BoothExists(int id)
        {
            return _context.Booth.Any(e => e.Id == id);
        }
    }
}
