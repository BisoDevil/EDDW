using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDDW.Data;
using EDDW.Models;
using Microsoft.AspNetCore.Authorization;

namespace EDDW.Controllers
{
   
    public class SpeakersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpeakersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Speakers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Speaker.ToListAsync());
        }

        // GET: Speakers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // GET: Speakers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Speakers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fullname,Username,Email,Phone,Title,Speciality,Country")] Speaker speaker)
        {
            if (ModelState.IsValid)
            {
                speaker.Id = Guid.NewGuid();
                speaker.Username = GetUserName(speaker);
                _context.Add(speaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(speaker);
        }

        // GET: Speakers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker.FindAsync(id);
            if (speaker == null)
            {
                return NotFound();
            }
            return View(speaker);
        }

        // POST: Speakers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Fullname,Username,Email,Phone,Title,Speciality,Country")] Speaker speaker)
        {
            if (id != speaker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerExists(speaker.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(speaker);
        }

        // GET: Speakers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var speaker = await _context.Speaker.FindAsync(id);
            _context.Speaker.Remove(speaker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(Guid id)
        {
            return _context.Speaker.Any(e => e.Id == id);
        }
        private string GetUserName(Speaker speaker)
        {
            string iso = speaker.Country.Split("-")[1];
            int end = new Random().Next(1000, 9999);
            string cap = speaker.Title.ToString().ToUpper();
            return $"{cap}{iso}{end}";
        }
    }
}
