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
    [Authorize]
    public class BoothBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoothBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BoothBooks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BoothBook.Include(b => b.Booth).Include(b => b.Sponsor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BoothBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boothBook = await _context.BoothBook
                .Include(b => b.Booth)
                .Include(b => b.Sponsor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boothBook == null)
            {
                return NotFound();
            }

            return View(boothBook);
        }

        // GET: BoothBooks/Create
        public IActionResult Create()
        {
            ViewData["BoothId"] = new SelectList(_context.Booth, "Id", "Location");
            ViewData["SponsorId"] = new SelectList(_context.Sponsor, "Id", "Name");
            return View();
        }

        // POST: BoothBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SponsorId,BoothId")] BoothBook boothBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boothBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BoothId"] = new SelectList(_context.Booth, "Id", "Location", boothBook.BoothId);
            ViewData["SponsorId"] = new SelectList(_context.Sponsor, "Id", "Name", boothBook.SponsorId);
            return View(boothBook);
        }

        // GET: BoothBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boothBook = await _context.BoothBook.FindAsync(id);
            if (boothBook == null)
            {
                return NotFound();
            }
            ViewData["BoothId"] = new SelectList(_context.Booth, "Id", "Location", boothBook.BoothId);
            ViewData["SponsorId"] = new SelectList(_context.Sponsor, "Id", "Name", boothBook.SponsorId);
            return View(boothBook);
        }

        // POST: BoothBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SponsorId,BoothId")] BoothBook boothBook)
        {
            if (id != boothBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boothBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoothBookExists(boothBook.Id))
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
            ViewData["BoothId"] = new SelectList(_context.Booth, "Id", "Location", boothBook.BoothId);
            ViewData["SponsorId"] = new SelectList(_context.Sponsor, "Id", "Name", boothBook.SponsorId);
            return View(boothBook);
        }

        // GET: BoothBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boothBook = await _context.BoothBook
                .Include(b => b.Booth)
                .Include(b => b.Sponsor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boothBook == null)
            {
                return NotFound();
            }

            return View(boothBook);
        }

        // POST: BoothBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boothBook = await _context.BoothBook.FindAsync(id);
            _context.BoothBook.Remove(boothBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoothBookExists(int id)
        {
            return _context.BoothBook.Any(e => e.Id == id);
        }
    }
}
