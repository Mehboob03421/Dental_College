using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dental_College.Controllers
{
    public class OccupationManagerController : Controller
    {
        private readonly AppDbContext _context;

        public OccupationManagerController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET: PatientNationality/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient_Occupation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Occupation_Code,Occupation_Desc,Occupation_EntryDateTime.DateTime.Now")] PatientOccupation patientoccupationModel)
        {
            if (ModelState.IsValid)
            {

                _context.Add(patientoccupationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(patientoccupationModel);
        }

        //List: PatientNationality
        public async Task<IActionResult> Index()
        {
            var patientoccupation = await _context.Patient_Occupation.ToListAsync();
            return View(patientoccupation);
        }

        // GET: Patient Occupation/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patient_Occupation == null)
            {
                return NotFound();
            }

            var patientoccupation = await _context.Patient_Occupation.FindAsync(id);
            if (patientoccupation == null)
            {
                return NotFound();
            }

            return View(patientoccupation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Occupation_Code,Occupation_Desc,Occupation_EntryDateTime.DateTime.Now")] PatientOccupation patientoccupation)
        {
            if (id != patientoccupation.Occupation_Code) // Check against PatientOccupation_Code instead of PatientOccupation object
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientoccupation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Patient_Occupation.Any(s => s.Occupation_Code == patientoccupation.Occupation_Code))
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

            return View(patientoccupation);
        }

        // GET: Patient Occupation/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patient_Occupation == null)
            {
                return NotFound();
            }

            var patientoccupation = await _context.Patient_Occupation
                .FirstOrDefaultAsync(m => m.Occupation_Code == id);
            if (patientoccupation == null)
            {
                return NotFound();
            }

            return View(patientoccupation);
        }

        // POST: PatientOccupation/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patient_Occupation == null)
            {
                return Problem("Entity set 'AppDbContext.Patient_Occupation'  is null.");
            }
            var patientoccupation = await _context.Patient_Occupation.FindAsync(id);
            if (patientoccupation != null)
            {
                _context.Patient_Occupation.Remove(patientoccupation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
