﻿using System;
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
    [Authorize]
    public class TimelinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimelinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Timelines
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Timeline.Include(t => t.Speaker);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Timelines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeline = await _context.Timeline
                .Include(t => t.Speaker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeline == null)
            {
                return NotFound();
            }

            return View(timeline);
        }

        // GET: Timelines/Create
        public IActionResult Create()
        {
            ViewData["SpeakerId"] = new SelectList(_context.Speaker, "Id", "Fullname");
            return View();
        }

        // POST: Timelines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Information,SpeakerId,StartDate,EndDate")] Timeline timeline)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeline);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpeakerId"] = new SelectList(_context.Speaker, "Id", "Fullname", timeline.SpeakerId);
            return View(timeline);
        }

        // GET: Timelines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeline = await _context.Timeline.FindAsync(id);
            if (timeline == null)
            {
                return NotFound();
            }
            ViewData["SpeakerId"] = new SelectList(_context.Speaker, "Id", "Fullname", timeline.SpeakerId);
            return View(timeline);
        }

        // POST: Timelines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Information,SpeakerId,StartDate,EndDate")] Timeline timeline)
        {
            if (id != timeline.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeline);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimelineExists(timeline.Id))
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
            ViewData["SpeakerId"] = new SelectList(_context.Speaker, "Id", "Fullname", timeline.SpeakerId);
            return View(timeline);
        }

        // GET: Timelines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeline = await _context.Timeline
                .Include(t => t.Speaker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeline == null)
            {
                return NotFound();
            }

            return View(timeline);
        }

        // POST: Timelines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeline = await _context.Timeline.FindAsync(id);
            _context.Timeline.Remove(timeline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimelineExists(int id)
        {
            return _context.Timeline.Any(e => e.Id == id);
        }
    }
}
