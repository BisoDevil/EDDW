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
    [Route("api/Programme")]
    [ApiController]
    public class ApiProgrammesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiProgrammesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiProgrammes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Programme>>> GetProgramme()
        {
            return await _context.Programme
                .Include(s=>s.Speaker)
                .Include(r=>r.Room)
                .ToListAsync();
        }

        // GET: api/ApiProgrammes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Programme>> GetProgramme(int id)
        {
            var programme = await _context.Programme.FindAsync(id);

            if (programme == null)
            {
                return NotFound();
            }

            return programme;
        }

        // PUT: api/ApiProgrammes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgramme(int id, Programme programme)
        {
            if (id != programme.Id)
            {
                return BadRequest();
            }

            _context.Entry(programme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgrammeExists(id))
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

        // POST: api/ApiProgrammes
        [HttpPost]
        public async Task<ActionResult<Programme>> PostProgramme(Programme programme)
        {
            _context.Programme.Add(programme);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgramme", new { id = programme.Id }, programme);
        }

        // DELETE: api/ApiProgrammes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Programme>> DeleteProgramme(int id)
        {
            var programme = await _context.Programme.FindAsync(id);
            if (programme == null)
            {
                return NotFound();
            }

            _context.Programme.Remove(programme);
            await _context.SaveChangesAsync();

            return programme;
        }

        private bool ProgrammeExists(int id)
        {
            return _context.Programme.Any(e => e.Id == id);
        }
    }
}
