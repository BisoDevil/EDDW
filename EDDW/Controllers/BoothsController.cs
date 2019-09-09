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
 
    public class BoothsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoothsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booths
        public async Task<IActionResult> Index()
        {
            return View(await _context.Booth.ToListAsync());
        }

        // GET: Booths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booth = await _context.Booth
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booth == null)
            {
                return NotFound();
            }

            return View(booth);
        }

        // GET: Booths/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Booths/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location")] Booth booth)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booth);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booth);
        }

        // GET: Booths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booth = await _context.Booth.FindAsync(id);
            if (booth == null)
            {
                return NotFound();
            }
            return View(booth);
        }

        // POST: Booths/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location")] Booth booth)
        {
            if (id != booth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoothExists(booth.Id))
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
            return View(booth);
        }

        // GET: Booths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booth = await _context.Booth
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booth == null)
            {
                return NotFound();
            }

            return View(booth);
        }

        // POST: Booths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booth = await _context.Booth.FindAsync(id);
            _context.Booth.Remove(booth);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoothExists(int id)
        {
            return _context.Booth.Any(e => e.Id == id);
        }
    }
}
