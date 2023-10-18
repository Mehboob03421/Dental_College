using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Dental_College.Controllers
{
    public class StatesManagerController : Controller
    {
        private AppDbContext? db = null;
        private readonly AppDbContext _context;

        public StatesManagerController(AppDbContext context)
        {
            _context = context;
            this.db = db;
        }

        //For data list
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var countries = await _context.Countries.ToListAsync(); // Retrieve records from the database asynchronously

            if (countries.Any())
            {
                ViewData["CountryCode"] = new SelectList(countries, "CountryCode", "CountryName");
            }
            else
            {
                ViewData["CountryCode"] = new SelectList(new List<Countries>(), "CountryCode", "CountryName");
                // You can also set an error message or perform other actions as needed.
            }

            return View();
        }

        //For Data Entry
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateCode,StateName,CountryCode")] States state)
        {
            // Fetch the list of countries first
            var countries = await _context.Countries.ToListAsync();
            

            if (ModelState.IsValid)
            {
                // Check if the selected CountryCode exists in the Countries table
                var country = await _context.Countries.FindAsync(state.CountryCode);

                if (country == null)
                {
                    // Add an error message to ModelState if the selected country doesn't exist
                    ModelState.AddModelError("CountryCode", "Selected country does not exist.");
                    ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryCode", "CountryName", state.CountryCode);
                    return View(state);
                }

                _context.Add(state);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Check if there are no countries in the list
            if (countries.Count == 0)
            {
                // Add an error message if there are no countries in the list
                ModelState.AddModelError("CountryCode", "No countries available. Please add countries first.");
            }
            ViewData["CountryCode"] = new SelectList(countries, "CountryCode", "CountryName", state.CountryCode);
            return View(state);



        }

        
        //For list
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.States.Include(e => e.Countries);
            return View(await appDbContext.ToListAsync());
        }

        // GET: States/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .Include(e => e.Countries)
                .FirstOrDefaultAsync(m => m.StateCode == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }
        // GET: States/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryCode", "CountryName", state.CountryCode);
            return View(state);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateCode,StateName,CountryCode")] States state)
        {
            if (id != state.StateCode) // Check against StateCode instead of state object
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.States.Any(s => s.StateCode == state.StateCode))
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
            ViewData["CountryCode"] = new SelectList(_context.Countries, "CountryCode", "CountryCode", state.CountryCode);
            return View(state);
        }
        // GET: Employees/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.States == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .Include(e => e.Countries)
                .FirstOrDefaultAsync(m => m.StateCode == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // POST: States/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.States == null)
            {
                return Problem("Entity set 'AppDbContext.States'  is null.");
            }
            var state = await _context.States.FindAsync(id);
            if (state != null)
            {
                _context.States.Remove(state);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
    



}

