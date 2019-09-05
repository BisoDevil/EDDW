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
    public class PackageBooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PackageBooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PackageBooks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PackageBook.Include(p => p.Company).Include(p => p.Package);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PackageBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packageBook = await _context.PackageBook
                .Include(p => p.Company)
                .Include(p => p.Package)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (packageBook == null)
            {
                return NotFound();
            }

            return View(packageBook);
        }

        // GET: PackageBooks/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name");
            ViewData["PackageId"] = new SelectList(_context.Package, "Id", "Name");
            return View();
        }

        // POST: PackageBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PackageId,CompanyId")] PackageBook packageBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(packageBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name", packageBook.CompanyId);
            ViewData["PackageId"] = new SelectList(_context.Package, "Id", "Name", packageBook.PackageId);
            return View(packageBook);
        }

        // GET: PackageBooks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packageBook = await _context.PackageBook.FindAsync(id);
            if (packageBook == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name", packageBook.CompanyId);
            ViewData["PackageId"] = new SelectList(_context.Package, "Id", "Name", packageBook.PackageId);
            return View(packageBook);
        }

        // POST: PackageBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PackageId,CompanyId")] PackageBook packageBook)
        {
            if (id != packageBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(packageBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageBookExists(packageBook.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name", packageBook.CompanyId);
            ViewData["PackageId"] = new SelectList(_context.Package, "Id", "Name", packageBook.PackageId);
            return View(packageBook);
        }

        // GET: PackageBooks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packageBook = await _context.PackageBook
                .Include(p => p.Company)
                .Include(p => p.Package)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (packageBook == null)
            {
                return NotFound();
            }

            return View(packageBook);
        }

        // POST: PackageBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var packageBook = await _context.PackageBook.FindAsync(id);
            _context.PackageBook.Remove(packageBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackageBookExists(int id)
        {
            return _context.PackageBook.Any(e => e.Id == id);
        }
    }
}
