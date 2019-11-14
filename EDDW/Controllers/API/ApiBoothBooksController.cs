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
    [Route("api/BoothBooks")]
    [ApiController]
    public class ApiBoothBooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiBoothBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiBoothBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoothBook>>> GetBoothBook()
        {
            return await _context.BoothBook
                .Include(s=>s.Sponsor)
                .Include(b=>b.Booth)
                .ToListAsync();
        }

        // GET: api/ApiBoothBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoothBook>> GetBoothBook(int id)
        {
            var boothBook = await _context.BoothBook.FindAsync(id);

            if (boothBook == null)
            {
                return NotFound();
            }

            return boothBook;
        }

        // PUT: api/ApiBoothBooks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoothBook(int id, BoothBook boothBook)
        {
            if (id != boothBook.Id)
            {
                return BadRequest();
            }

            _context.Entry(boothBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoothBookExists(id))
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

        // POST: api/ApiBoothBooks
        [HttpPost]
        public async Task<ActionResult<BoothBook>> PostBoothBook(BoothBook boothBook)
        {
            _context.BoothBook.Add(boothBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoothBook", new { id = boothBook.Id }, boothBook);
        }

        // DELETE: api/ApiBoothBooks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BoothBook>> DeleteBoothBook(int id)
        {
            var boothBook = await _context.BoothBook.FindAsync(id);
            if (boothBook == null)
            {
                return NotFound();
            }

            _context.BoothBook.Remove(boothBook);
            await _context.SaveChangesAsync();

            return boothBook;
        }

        private bool BoothBookExists(int id)
        {
            return _context.BoothBook.Any(e => e.Id == id);
        }
    }
}
