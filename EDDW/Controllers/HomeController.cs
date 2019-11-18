using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EDDW.Models;
using EDDW.Data;
using Microsoft.EntityFrameworkCore;

namespace EDDW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            ViewData["speaker"] = _context.Speaker.ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
