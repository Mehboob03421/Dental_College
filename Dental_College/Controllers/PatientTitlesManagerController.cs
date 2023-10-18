using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Dental_College.Controllers
{
    public class PatientTitlesManagerController : Controller
    {
        private readonly AppDbContext _context;

        public PatientTitlesManagerController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: PatientTitles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient_Titles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientTitle_ID,PatientTitle_Desc,PatientTitle_EntryDateTime.DateTime.Now")] PatientTitles patienttitleModel)
        {
            if (ModelState.IsValid)
            {

                _context.Add(patienttitleModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(patienttitleModel);
        }

        //List: PatientTitles
        public async Task<IActionResult> Index()
        {
            var patienttitles = await _context.Patient_Titles.ToListAsync();
            return View(patienttitles);
        }

        // GET: PatientTitles/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patient_Titles == null)
            {
                return NotFound();
            }

            var patientTitle = await _context.Patient_Titles.FindAsync(id);
            if (patientTitle == null)
            {
                return NotFound();
            }

            return View(patientTitle);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientTitle_ID,PatientTitle_Desc,PatientTitle_EntryDateTime.DateTime.Now")] PatientTitles patientTitle)
        {
            if (id != patientTitle.PatientTitle_ID) // Check against PatientTitle_ID instead of PatientTitle object
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Patient_Types.Any(s => s.PatientType_ID == patientTitle.PatientTitle_ID))
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

            return View(patientTitle);
        }

        // GET: PatientTitle/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patient_Titles == null)
            {
                return NotFound();
            }

            var patienttitle = await _context.Patient_Titles
                .FirstOrDefaultAsync(m => m.PatientTitle_ID == id);
            if (patienttitle == null)
            {
                return NotFound();
            }

            return View(patienttitle);
        }

        // POST: PatientTitles/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patient_Titles == null)
            {
                return Problem("Entity set 'AppDbContext.Patient_Titles'  is null.");
            }
            var patienttitle = await _context.Patient_Titles.FindAsync(id);
            if (patienttitle != null)
            {
                _context.Patient_Titles.Remove(patienttitle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
