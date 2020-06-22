using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryWebApplication;

namespace LibraryWebApplication.Controllers
{
    public class TownsController : Controller
    {
        private readonly HotelsContext _context;

        public TownsController(HotelsContext context)
        {
            _context = context;
        }

        // GET: Towns
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "Countries");
            ViewBag.CountryId = id;
            ViewBag.CountryName = name;
            var townsByCountry = _context.Towns.Where(b => b.CountryId == id).Include(b => b.Country);
            return View(await townsByCountry.ToListAsync());
            //  var hotelsContext = _context.Towns.Include(t => t.Country);
            //  return View(await hotelsContext.ToListAsync());
        }

        // GET: Towns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var town = await _context.Towns
                .Include(t => t.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (town == null)
            {
                return NotFound();
            }

            return View(town);
        }

        // GET: Towns/Create
        public IActionResult Create(int? countryId)
        {
            // ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name");
            ViewBag.CountryId = countryId;
            ViewBag.CountryName = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault().Name;
            return View();
        }

        // POST: Towns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int countryId, [Bind("Id,Name,CountryId")] Town town)
        {
            var existingTown = _context.Towns.Where(t => t.Name == town.Name).FirstOrDefault();
            if(existingTown != null)
            {
                ModelState.AddModelError(String.Empty, "This employee already exists.");
                return RedirectToAction("Index", "Towns", new {Id = countryId});    
            }
            town.Country = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault();
            town.CountryId = countryId;
            if (ModelState.IsValid)
            {
                _context.Add(town);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Towns", new { id = countryId, name = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault().Name });
            }
           // ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", town.CountryId);
           // return View(town);
            return RedirectToAction("Index", "Towns", new { id = countryId, name = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault().Name }); ;
        }

        // GET: Towns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var town = await _context.Towns.FindAsync(id);
            if (town == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", town.CountryId);
            return View(town);
        }

        // POST: Towns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CountryId")] Town town)
        {
            if (id != town.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(town);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TownExists(town.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Name", town.CountryId);
            return View(town);
        }

        // GET: Towns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var town = await _context.Towns
                .Include(t => t.Country)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (town == null)
            {
                return NotFound();
            }

            return View(town);
        }

        // POST: Towns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var town = await _context.Towns.FindAsync(id);
            _context.Towns.Remove(town);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TownExists(int id)
        {
            return _context.Towns.Any(e => e.Id == id);
        }
    }
}
