using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Dental_College.Controllers
{
    public class PatientTypesManagerController : Controller
    {
        private readonly AppDbContext _context;

        public PatientTypesManagerController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: PatientTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient_Types/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientType_ID,PatientType_Desc,PatientType_EntryDateTime.DateTime.Now")] PatientTypes patienttypeModel)
        {
            if (ModelState.IsValid)
            {

                _context.Add(patienttypeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(patienttypeModel);
        }

        //List: PatientTypes
        public async Task<IActionResult> Index()
        {
            var patienttypes = await _context.Patient_Types.ToListAsync();
            return View(patienttypes);
        }

        // GET: PatientTypes/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patient_Types == null)
            {
                return NotFound();
            }

            var patientType = await _context.Patient_Types.FindAsync(id);
            if (patientType == null)
            {
                return NotFound();
            }

            return View(patientType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientType_ID,PatientType_Desc,PatientType_EntryDateTime.DateTime.Now")] PatientTypes patientType)
        {
            if (id != patientType.PatientType_ID) // Check against PatientType_ID instead of PatientType object
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Patient_Types.Any(s => s.PatientType_ID == patientType.PatientType_ID))
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

            return View(patientType);
        }

        // GET: PatientType/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patient_Types == null)
            {
                return NotFound();
            }

            var patienttype = await _context.Patient_Types
                .FirstOrDefaultAsync(m => m.PatientType_ID== id);
            if (patienttype == null)
            {
                return NotFound();
            }

            return View(patienttype);
        }

        // POST: PatientTypes/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patient_Types == null)
            {
                return Problem("Entity set 'AppDbContext.Patient_Types'  is null.");
            }
            var patienttype = await _context.Patient_Types.FindAsync(id);
            if (patienttype != null)
            {
                _context.Patient_Types.Remove(patienttype);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
