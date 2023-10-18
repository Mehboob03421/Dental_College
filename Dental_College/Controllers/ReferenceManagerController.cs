using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dental_College.Controllers
{
    public class ReferenceManagerController : Controller
    {
        private readonly AppDbContext _context;

        public ReferenceManagerController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET: Patient Reference /Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient_Reference/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Reference_Code,Reference_Desc,Reference_EntryDateTime.DateTime.Now")] PatientReference patientreferenceModel)
        {
            if (ModelState.IsValid)
            {

                _context.Add(patientreferenceModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(patientreferenceModel);
        }

        //List: Patient Reference
        public async Task<IActionResult> Index()
        {
            var patientreference = await _context.Patient_Reference.ToListAsync();
            return View(patientreference);
        }

        // GET: Patient Reference/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patient_Reference == null)
            {
                return NotFound();
            }

            var patientreference = await _context.Patient_Reference.FindAsync(id);
            if (patientreference == null)
            {
                return NotFound();
            }

            return View(patientreference);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Reference_Code,Reference_Desc,Reference_EntryDateTime.DateTime.Now")] PatientReference patientreference)
        {
            if (id != patientreference.Reference_Code) // Check against PatientReference_Code instead of PatientReference object
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientreference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Patient_Reference.Any(s => s.Reference_Code == patientreference.Reference_Code))
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

            return View(patientreference);
        }

        // GET: Patient Reference/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patient_Reference == null)
            {
                return NotFound();
            }

            var patientreference = await _context.Patient_Reference
                .FirstOrDefaultAsync(m => m.Reference_Code == id);
            if (patientreference == null)
            {
                return NotFound();
            }

            return View(patientreference);
        }

        // POST: PatientOccupation/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patient_Reference == null)
            {
                return Problem("Entity set 'AppDbContext.Patient_Reference'  is null.");
            }
            var patientreference = await _context.Patient_Reference.FindAsync(id);
            if (patientreference != null)
            {
                _context.Patient_Reference.Remove(patientreference);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
