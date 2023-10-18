using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dental_College.Controllers
{
    public class RelationManagerController : Controller
    {
        private readonly AppDbContext _context;

        public RelationManagerController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        // GET: Relation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Relations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Relation_ID,Relation_Desc,Relation_EntryDateTime.DateTime.Now")] Relations genderModel)
        {
            if (ModelState.IsValid)
            {

                _context.Add(genderModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(genderModel);
        }

        //List: Relations
        public async Task<IActionResult> Index()
        {
            var relations = await _context.Relations.ToListAsync();
            return View(relations);
        }

        // GET: Relations/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Relations == null)
            {
                return NotFound();
            }

            var relation = await _context.Relations.FindAsync(id);
            if (relation == null)
            {
                return NotFound();
            }

            return View(relation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Relation_ID,Relation_Desc,Relation_EntryDateTime")] Relations relation)
        {
            if (id != relation.Relation_ID) // Check against relation_ID instead of relation object
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Relations.Any(s => s.Relation_ID == relation.Relation_ID))
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

            return View(relation);
        }

        // GET: Gender/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Relations == null)
            {
                return NotFound();
            }

            var relation = await _context.Relations
                .FirstOrDefaultAsync(m => m.Relation_ID == id);
            if (relation == null)
            {
                return NotFound();
            }

            return View(relation);
        }

        // POST: Relation/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Relations == null)
            {
                return Problem("Entity set 'AppDbContext.Relations'  is null.");
            }
            var relation = await _context.Relations.FindAsync(id);
            if (relation != null)
            {
                _context.Relations.Remove(relation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
