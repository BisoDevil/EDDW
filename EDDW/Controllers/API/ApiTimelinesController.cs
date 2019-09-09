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
    [Route("api/Timeline")]
    [ApiController]
    public class ApiTimelinesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiTimelinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiTimelines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Timeline>>> GetTimeline()
        {
            return await _context.Timeline.ToListAsync();
        }

        // GET: api/ApiTimelines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Timeline>>> GetTimeline(Guid id)
        {
            var timelines = await _context.Timeline.Where(t => t.SpeakerId == id).ToListAsync();

            if (timelines == null)
            {
                return NotFound();
            }

            return timelines;
        }




        // PUT: api/ApiTimelines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeline(int id, Timeline timeline)
        {
            if (id != timeline.Id)
            {
                return BadRequest();
            }

            _context.Entry(timeline).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimelineExists(id))
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

        // POST: api/ApiTimelines
        [HttpPost]
        public async Task<ActionResult<Timeline>> PostTimeline(Timeline timeline)
        {
            _context.Timeline.Add(timeline);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimeline", new { id = timeline.Id }, timeline);
        }

        // DELETE: api/ApiTimelines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Timeline>> DeleteTimeline(int id)
        {
            var timeline = await _context.Timeline.FindAsync(id);
            if (timeline == null)
            {
                return NotFound();
            }

            _context.Timeline.Remove(timeline);
            await _context.SaveChangesAsync();

            return timeline;
        }

        private bool TimelineExists(int id)
        {
            return _context.Timeline.Any(e => e.Id == id);
        }
    }
}
