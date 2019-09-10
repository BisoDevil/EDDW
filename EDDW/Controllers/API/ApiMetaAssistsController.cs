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
    [Route("api/MetaAssist")]
    [ApiController]
    public class ApiMetaAssistsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiMetaAssistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiMetaAssists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetaAssist>>> GetMetaAssist()
        {
            return await _context.MetaAssist.ToListAsync();
        }

        // GET: api/ApiMetaAssists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MetaAssist>> GetMetaAssist(int id)
        {
            var metaAssist = await _context.MetaAssist.FindAsync(id);

            if (metaAssist == null)
            {
                return NotFound();
            }

            return metaAssist;
        }

        // PUT: api/ApiMetaAssists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetaAssist(int id, MetaAssist metaAssist)
        {
            if (id != metaAssist.Id)
            {
                return BadRequest();
            }

            _context.Entry(metaAssist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetaAssistExists(id))
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

        // POST: api/ApiMetaAssists
        [HttpPost]
        public async Task<ActionResult<MetaAssist>> PostMetaAssist(MetaAssist metaAssist)
        {
            _context.MetaAssist.Add(metaAssist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMetaAssist", new { id = metaAssist.Id }, metaAssist);
        }

        // DELETE: api/ApiMetaAssists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MetaAssist>> DeleteMetaAssist(int id)
        {
            var metaAssist = await _context.MetaAssist.FindAsync(id);
            if (metaAssist == null)
            {
                return NotFound();
            }

            _context.MetaAssist.Remove(metaAssist);
            await _context.SaveChangesAsync();

            return metaAssist;
        }

        private bool MetaAssistExists(int id)
        {
            return _context.MetaAssist.Any(e => e.Id == id);
        }
    }
}
