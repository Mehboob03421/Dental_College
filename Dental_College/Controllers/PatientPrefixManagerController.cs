using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dental_College.Controllers
{
    public class PatientPrefixManagerController : Controller

    {
        private readonly AppDbContext _context;

        public PatientPrefixManagerController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET: Patient Reference /Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient_Prefixes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Prefix_Code,Prefix_Desc,Prefix_EntryDateTime.DateTime.Now")] PatientPrefixes patientprefixModel)
        {
            if (ModelState.IsValid)
            {

                _context.Add(patientprefixModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(patientprefixModel);
        }

        //List: Patient Prefixes
        public async Task<IActionResult> Index()
        {
            var patientprefix = await _context.PatientPrefixes.ToListAsync();
            return View(patientprefix);
        }

        // GET: Patient Prefix/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PatientPrefixes == null)
            {
                return NotFound();
            }

            var patientprefixes = await _context.PatientPrefixes.FindAsync(id);
            if (patientprefixes == null)
            {
                return NotFound();
            }

            return View(patientprefixes);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Prefix_Code,Prefix_Desc,Prefix_EntryDateTime.DateTime.Now")] PatientPrefixes patientprefix)
        {
            if (id != patientprefix.Prefix_Code) // Check against PatientPrefixes_Code instead of PatientPrefix object
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientprefix);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.PatientPrefixes.Any(s => s.Prefix_Code == patientprefix.Prefix_Code))
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

            return View(patientprefix);
        }

        // GET: Patient Prefix/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PatientPrefixes == null)
            {
                return NotFound();
            }

            var patientprefix = await _context.PatientPrefixes
                .FirstOrDefaultAsync(m => m.Prefix_Code == id);
            if (patientprefix == null)
            {
                return NotFound();
            }

            return View(patientprefix);
        }

        // POST: PatientPrefixes/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PatientPrefixes == null)
            {
                return Problem("Entity set 'AppDbContext.PatientPrefixes'  is null.");
            }
            var patientprefix = await _context.PatientPrefixes.FindAsync(id);
            if (patientprefix != null)
            {
                _context.PatientPrefixes.Remove(patientprefix);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
