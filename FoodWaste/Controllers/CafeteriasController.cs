using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodWaste.Domain;
using FoodWaste.Infrastructure.Data;

namespace FoodWaste.WebApp.Controllers
{
    public class CafeteriasController : Controller
    {
        private readonly WebAppContext _context;

        public CafeteriasController(WebAppContext context)
        {
            _context = context;
        }

        // GET: Cafeteriass
        public async Task<IActionResult> Index()
        {
              return _context.Cafeterias != null ? 
                          View(await _context.Cafeterias.ToListAsync()) :
                          Problem("Entity set 'FoodWasteWebAppContext.Cafeterias'  is null.");
        }

        // GET: Cafeteriass/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cafeterias == null)
            {
                return NotFound();
            }

            var cafeteria = await _context.Cafeterias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cafeteria == null)
            {
                return NotFound();
            }

            return View(cafeteria);
        }

        // GET: Cafeteriass/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cafeteriass/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,City")] Cafeteria cafeteria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cafeteria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cafeteria);
        }

        // GET: Cafeteriass/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cafeterias == null)
            {
                return NotFound();
            }

            var cafeteria = await _context.Cafeterias.FindAsync(id);
            if (cafeteria == null)
            {
                return NotFound();
            }
            return View(cafeteria);
        }

        // POST: Cafeteriass/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,City")] Cafeteria cafeteria)
        {
            if (id != cafeteria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cafeteria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CafeteriasExists(cafeteria.Id))
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
            return View(cafeteria);
        }

        // GET: Cafeteriass/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cafeterias == null)
            {
                return NotFound();
            }

            var cafeteria = await _context.Cafeterias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cafeteria == null)
            {
                return NotFound();
            }

            return View(cafeteria);
        }

        // POST: Cafeteriass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cafeterias == null)
            {
                return Problem("Entity set 'FoodWasteWebAppContext.Cafeterias'  is null.");
            }
            var cafeteria = await _context.Cafeterias.FindAsync(id);
            if (cafeteria != null)
            {
                _context.Cafeterias.Remove(cafeteria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CafeteriasExists(int id)
        {
          return (_context.Cafeterias?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
