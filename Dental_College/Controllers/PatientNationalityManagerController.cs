using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dental_College.Controllers
{
    public class PatientNationalityManagerController : Controller
    {
        private readonly AppDbContext _context;

        public PatientNationalityManagerController(AppDbContext context)
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
        public async Task<IActionResult> Create([Bind("Nationality_Code,Nationality_Name,Nationality_EntryDateTime.DateTime.Now")] PatientNationality patientnationalityModel)
        {
            if (ModelState.IsValid)
            {

                _context.Add(patientnationalityModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(patientnationalityModel);
        }

        //List: PatientNationality
        public async Task<IActionResult> Index()
        {
            var patientnationality = await _context.Patient_Nationality.ToListAsync();
            return View(patientnationality);
        }
        // GET: PatientNationality/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patient_Nationality == null)
            {
                return NotFound();
            }

            var patientnationality = await _context.Patient_Nationality.FindAsync(id);
            if (patientnationality == null)
            {
                return NotFound();
            }

            return View(patientnationality);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nationality_Code,Nationality_Name,Nationality_EntryDateTime.DateTime.Now")] PatientNationality patientnationality)
        {
            if (id != patientnationality.Nationality_Code) // Check against PatientTitle_ID instead of PatientTitle object
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientnationality);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Patient_Nationality.Any(s => s.Nationality_Code == patientnationality.Nationality_Code))
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

            return View(patientnationality);
        }

        // GET: PatientNationality/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patient_Nationality == null)
            {
                return NotFound();
            }

            var patientnationality = await _context.Patient_Nationality
                .FirstOrDefaultAsync(m => m.Nationality_Code== id);
            if (patientnationality == null)
            {
                return NotFound();
            }

            return View(patientnationality);
        }

        // POST: PatientNationality/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patient_Nationality == null)
            {
                return Problem("Entity set 'AppDbContext.Patient_Nationality'  is null.");
            }
            var patientnationality = await _context.Patient_Nationality.FindAsync(id);
            if (patientnationality != null)
            {
                _context.Patient_Nationality.Remove(patientnationality);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
