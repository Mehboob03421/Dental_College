using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Dental_College.Controllers
{
    public class CountriesManagerController : Controller
    {
        private AppDbContext? db = null;

        public CountriesManagerController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        
        [HttpPost]
        public IActionResult Create(Countries country)
        {
            if (ModelState.IsValid)
            {
                if (db.Countries.Any(c => c.Dialing_Code == country.Dialing_Code))
                {
                    ModelState.AddModelError("Dialing_Code", "Dialing code is already in use.");
                    return View(country);   
                }
                if (country != null)
                {
                    db.Countries.Add(country);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(country);
        }
        
        public IActionResult Index()
        {
            List<Countries> model = (from c in db.Countries
                                     orderby c.CountryCode
                                     select c).ToList();
            return View(model);
        }
        public ActionResult Details(int id) 
        {
            Countries model = db.Countries.Find(id);
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Countries model = db.Countries.Find(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Countries model)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    db.Countries.Update(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                    //return RedirectToAction(nameof("Index"));
                }
                else
                return View(model);
            }
            catch 
            {
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Countries model = db.Countries.Find(id);
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Delete")]
        //public ActionResult ConfirmDelete(int CountryCode)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            Countries model = db.Countries.Find(CountryCode);
        //            db.Countries.Remove(model);
        //            db.SaveChanges();
        //            TempData["Message"] = "Country Deleted Successfully";
        //            return RedirectToAction(nameof(Index));
        //        }
        //        return View();

        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int CountryCode)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Countries model = db.Countries.Find(CountryCode);

                    // Check if there are associated child records in the States table
                    bool hasChildRecords = db.States.Any(s => s.CountryCode == model.CountryCode);

                    if (hasChildRecords)
                    {
                        TempData["ErrorMessage"] = "Cannot delete the country because it has associated states.";
                    }
                    else
                    {
                        db.Countries.Remove(model);
                        db.SaveChanges();
                        TempData["Message"] = "Country Deleted Successfully";
                    }

                    //return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

    }
}
