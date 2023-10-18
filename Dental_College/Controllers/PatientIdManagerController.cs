using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dental_College.Controllers
{
    public class PatientIdManagerController : Controller
    {
        private readonly AppDbContext _context;

        public PatientIdManagerController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET: Patient Reference /Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient_Identification Type/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientIdType_Code,PatientIdType_Desc,PatientIdType_EntryDateTime.DateTime.Now")] Patient_Identification patientidentificationModel)
        {
            if (ModelState.IsValid)
            {

                _context.Add(patientidentificationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(patientidentificationModel);
        }

        //List: Patient Reference
        public async Task<IActionResult> Index()
        {
            var patientidentification = await _context.PatientId_Type.ToListAsync();
            return View(patientidentification);
        }

        // GET: Patient identification/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PatientId_Type == null)
            {
                return NotFound();
            }

            var patientidentification = await _context.PatientId_Type.FindAsync(id);
            if (patientidentification == null)
            {
                return NotFound();
            }

            return View(patientidentification);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientIdType_Code,PatientIdType_Desc,PatientIdType_EntryDateTime.DateTime.Now")] Patient_Identification patientidentification)
        {
            if (id != patientidentification.PatientIdType_Code) // Check against Patientidentification_Code instead of Patientidentification object
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientidentification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.PatientId_Type.Any(s => s.PatientIdType_Code == patientidentification.PatientIdType_Code))
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

            return View(patientidentification);
        }
        // GET: Patient Identification/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PatientId_Type == null)
            {
                return NotFound();
            }

            var patientidentification = await _context.PatientId_Type
                .FirstOrDefaultAsync(m => m.PatientIdType_Code == id);
            if (patientidentification == null)
            {
                return NotFound();
            }

            return View(patientidentification);
        }

        // POST: PatientIdentification/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PatientId_Type == null)
            {
                return Problem("Entity set 'AppDbContext.PatientId_Type'  is null.");
            }
            var patientidentification = await _context.PatientId_Type.FindAsync(id);
            if (patientidentification != null)
            {
                _context.PatientId_Type.Remove(patientidentification);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
