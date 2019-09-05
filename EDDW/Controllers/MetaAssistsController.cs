using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDDW.Data;
using EDDW.Models;

namespace EDDW.Controllers
{
    public class MetaAssistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MetaAssistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MetaAssists
        public async Task<IActionResult> Index()
        {
            return View(await _context.MetaAssist.ToListAsync());
        }

        // GET: MetaAssists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metaAssist = await _context.MetaAssist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metaAssist == null)
            {
                return NotFound();
            }

            return View(metaAssist);
        }

        // GET: MetaAssists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MetaAssists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User,AccoStartDate,AccoEndDate,IsTransportation,Room")] MetaAssist metaAssist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metaAssist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(metaAssist);
        }

        // GET: MetaAssists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metaAssist = await _context.MetaAssist.FindAsync(id);
            if (metaAssist == null)
            {
                return NotFound();
            }
            return View(metaAssist);
        }

        // POST: MetaAssists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,User,AccoStartDate,AccoEndDate,IsTransportation,Room")] MetaAssist metaAssist)
        {
            if (id != metaAssist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metaAssist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetaAssistExists(metaAssist.Id))
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
            return View(metaAssist);
        }

        // GET: MetaAssists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metaAssist = await _context.MetaAssist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metaAssist == null)
            {
                return NotFound();
            }

            return View(metaAssist);
        }

        // POST: MetaAssists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metaAssist = await _context.MetaAssist.FindAsync(id);
            _context.MetaAssist.Remove(metaAssist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetaAssistExists(int id)
        {
            return _context.MetaAssist.Any(e => e.Id == id);
        }
    }
}
