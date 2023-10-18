using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Threading.Tasks;

namespace Dental_College.Controllers
{
    public class CitiesManagerController : Controller
    {
        private AppDbContext? db = null;
        private IEnumerable states;
        private readonly AppDbContext _context;

        public CitiesManagerController(AppDbContext context)
        {
            _context = context;
            this.db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var countries = await _context.Countries.ToListAsync();
            ViewData["Countries"] = new SelectList(countries, "CountryCode", "CountryName");

            return View();
        }


        [HttpGet]
        public IActionResult GetStatesByCountry(int countryCode)
        {
            var states = _context.States.Where(s => s.CountryCode == countryCode).ToList();
            var stateList = new SelectList(states, "StateCode", "StateName");

            return Json(stateList);
        }

        //For Data Entry
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityCode,CityName,CountryCode,StateCode")] Cities city)
        {

            if (ModelState.IsValid)
            {

                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(city);

        }
        public IActionResult Index()
        {
            List<Cities> model = _context.Cities
            .Include(c => c.Countries)
            .Include(c => c.States)
            .OrderBy(c => c.CityCode)
            .ToList();
            return View(model);
        }
    }
}
