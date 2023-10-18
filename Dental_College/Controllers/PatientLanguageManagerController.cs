using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dental_College.Controllers
{
    public class PatientLanguageManagerController : Controller
    {
        private readonly AppDbContext _context;

        public PatientLanguageManagerController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET: PatientNationality/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient_Nationality/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Language_Code,Language_Name,Language_EntryDateTime.DateTime.Now")] PatientLanguage patientlanguageModel)
        {
            if (ModelState.IsValid)
            {

                _context.Add(patientlanguageModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(patientlanguageModel);
        }

        //List: PatientNationality
        public async Task<IActionResult> Index()
        {
            var patientlanguage = await _context.Patient_Language.ToListAsync();
            return View(patientlanguage);
        }

        // GET: PatientLanguage/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patient_Language == null)
            {
                return NotFound();
            }

            var patientlanguage = await _context.Patient_Language.FindAsync(id);
            if (patientlanguage == null)
            {
                return NotFound();
            }

            return View(patientlanguage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Language_Code,Language_Name,Language_EntryDateTime.DateTime.Now")] PatientLanguage patientlanguage)
        {
            if (id != patientlanguage.Language_Code) // Check against PatientLanguage_Code instead of PatientLanguage object
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientlanguage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Patient_Language.Any(s => s.Language_Code == patientlanguage.Language_Code))
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

            return View(patientlanguage);
        }

        // GET: PatientLanguage/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patient_Language == null)
            {
                return NotFound();
            }

            var patientlanguage = await _context.Patient_Language
                .FirstOrDefaultAsync(m => m.Language_Code == id);
            if (patientlanguage == null)
            {
                return NotFound();
            }

            return View(patientlanguage);
        }

        // POST: PatientLanguage/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patient_Language == null)
            {
                return Problem("Entity set 'AppDbContext.Patient_Language'  is null.");
            }
            var patientlanguage = await _context.Patient_Language.FindAsync(id);
            if (patientlanguage != null)
            {
                _context.Patient_Language.Remove(patientlanguage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
