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
    [Route("api/Question")]
    [ApiController]
    public class ApiQuestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiQuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiQuestions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        // GET: api/ApiQuestions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Questions>> GetQuestions(int id)
        {
            var questions = await _context.Questions.FindAsync(id);

            if (questions == null)
            {
                return NotFound();
            }

            return questions;
        }

        // PUT: api/ApiQuestions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestions(int id, Questions questions)
        {
            if (id != questions.Id)
            {
                return BadRequest();
            }

            _context.Entry(questions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsExists(id))
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

        // POST: api/ApiQuestions
        [HttpPost]
        public async Task<ActionResult<Questions>> PostQuestions(Questions questions)
        {
            _context.Questions.Add(questions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestions", new { id = questions.Id }, questions);
        }

        // DELETE: api/ApiQuestions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Questions>> DeleteQuestions(int id)
        {
            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();

            return questions;
        }

        private bool QuestionsExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
