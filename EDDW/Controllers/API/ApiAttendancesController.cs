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
    [Route("api/Attendance")]
    [ApiController]
    public class ApiAttendancesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiAttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiAttendances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendance()
        {
            return await _context.Attendance.ToListAsync();
        }

        // GET: api/ApiAttendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id)
        {
            var attendance = await _context.Attendance.FindAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return attendance;
        }

        // PUT: api/ApiAttendances/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(int id, Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return BadRequest();
            }

            _context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
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

        // POST: api/ApiAttendances
        [HttpPost("{code}")]
        public async Task<ActionResult<Attendance>> PostAttendance(string code, Attendance attendance)
        {
            var att = await _context.Programme.FindAsync(attendance.ProgrammeId);

            if (code != att.AttendanceCode)
            {
                return BadRequest();
            }

            _context.Attendance.Add(attendance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendance", new { id = attendance.Id }, attendance);
        }

        // DELETE: api/ApiAttendances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Attendance>> DeleteAttendance(int id)
        {
            var attendance = await _context.Attendance.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendance.Remove(attendance);
            await _context.SaveChangesAsync();

            return attendance;
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendance.Any(e => e.Id == id);
        }
    }
}
