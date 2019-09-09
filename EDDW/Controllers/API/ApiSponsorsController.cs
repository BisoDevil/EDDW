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
    [Route("api/Sponsor")]
    [ApiController]
    public class ApiSponsorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiSponsorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiSponsors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sponsor>>> GetSponsor()
        {
            return await _context.Sponsor.ToListAsync();
        }

        // GET: api/ApiSponsors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sponsor>> GetSponsor(int id)
        {
            var sponsor = await _context.Sponsor.FindAsync(id);

            if (sponsor == null)
            {
                return NotFound();
            }

            return sponsor;
        }

        // PUT: api/ApiSponsors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSponsor(int id, Sponsor sponsor)
        {
            if (id != sponsor.Id)
            {
                return BadRequest();
            }

            _context.Entry(sponsor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SponsorExists(id))
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

        // POST: api/ApiSponsors
        [HttpPost]
        public async Task<ActionResult<Sponsor>> PostSponsor(Sponsor sponsor)
        {
            _context.Sponsor.Add(sponsor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSponsor", new { id = sponsor.Id }, sponsor);
        }

        // DELETE: api/ApiSponsors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sponsor>> DeleteSponsor(int id)
        {
            var sponsor = await _context.Sponsor.FindAsync(id);
            if (sponsor == null)
            {
                return NotFound();
            }

            _context.Sponsor.Remove(sponsor);
            await _context.SaveChangesAsync();

            return sponsor;
        }

        private bool SponsorExists(int id)
        {
            return _context.Sponsor.Any(e => e.Id == id);
        }
    }
}
