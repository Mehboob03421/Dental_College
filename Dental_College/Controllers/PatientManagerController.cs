using System;
using System.IO;
using Microsoft.AspNetCore.HostFiltering;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Dental_College.Controllers
{
    public class PatientManagerController : Controller
    {
        private readonly AppDbContext _context;

        public PatientManagerController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // GET: Patient Prefixes /Create
        public IActionResult Create()
        {
            //var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //optionsBuilder.UseSqlServer("Server=G7-MIS-AHMED-LT\\SQLEXPRESS; Database=BasicDataInfo;uid=sa;Password=pakistan@1234;TrustServerCertificate=true;");

            //using (var dbContext = new AppDbContext(optionsBuilder.Options))
            //{
            //    var prefixes = dbContext.PatientPrefixes.ToList();
            //    ViewBag.Prefixes = prefixes;
            //}

            //using (var dbContext = new AppDbContext(optionsBuilder.Options))
            //{
            //    var patienttypes = dbContext.Patient_Types.ToList();
            //    ViewBag.patienttypes = patienttypes;
            //}

            //using (var dbContext = new AppDbContext(optionsBuilder.Options))
            //{
            //    var patientidtypes = dbContext.PatientId_Type.ToList();
            //    ViewBag.patientidtypes = patientidtypes;
            //}

            //// Make sure ViewBag.patienttypes is initialized
            //if (ViewBag.patienttypes == null)
            //{
            //    ViewBag.patienttypes = new List<PatientTypes>(); // Replace YourPatientTypeModel with the actual type you're using
            //}

            //return View();
            // Load data from the database and pass it to the view
            ViewBag.Prefixes = _context.PatientPrefixes.ToList();
            ViewBag.PatientTypes = _context.Patient_Types.ToList();
            ViewBag.PatientIdTypes = _context.PatientId_Type.ToList();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicalRecord_No,PatientType_ID,PatientIdType_Code,Patient_ID,Prefix_Code,PFName,PMName,PLName,RFName,RMName,RLName,PatientPicture_Path,RegEntry_DateTime")] Patient patientModel,IFormFile Patient_Pictures)

        {
            if (ModelState.IsValid)
            {

                if (Patient_Pictures != null && Patient_Pictures.Length > 0)
                {
                    // Get the file name and unique path
                    var fileName = Path.GetFileName(Patient_Pictures.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                    // Construct the path to save the file
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", "Patient_Pictures", uniqueFileName);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Patient_Pictures.CopyToAsync(fileStream);
                    }

                    // Update the database record with the file path
                    patientModel.PatientPicture_Path = "/Pictures/Patient_Pictures/" + uniqueFileName;
                }

                _context.Add(patientModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(patientModel);
        }

        //List: Patient
        public async Task<IActionResult> Index()
        {
            var  patient_v= await _context.Patient.ToListAsync();

            var prefixes = _context.PatientPrefixes.ToList();
            // Pass the prefixes to the view
            ViewData["Prefixes"] = prefixes;
            return View(patient_v);
        }

        

        // GET: Patient/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patient == null)
            {
                return NotFound();
            }

            var Patient_Var = await _context.Patient.FindAsync(id);
            if (Patient_Var == null)
            {
                return NotFound();
            }


            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=G7-MIS-AHMED-LT\\SQLEXPRESS; Database=BasicDataInfo;uid=sa;Password=pakistan@1234;TrustServerCertificate=true;");

            using (var dbContext = new AppDbContext(optionsBuilder.Options))
            {
                var prefixes = dbContext.PatientPrefixes.ToList();
                ViewBag.Prefixes = prefixes;

                var patienttypes = dbContext.Patient_Types.ToList();
                ViewBag.patienttypes = patienttypes;

                var patientidtypes = dbContext.PatientId_Type.ToList();
                ViewBag.patientidtypes = patientidtypes;

                // Fetch the patient you want to edit by ID and pass it to the view
                var patient = dbContext.Patient.FirstOrDefault(p => p.MedicalRecord_No == id);

                if (patient == null)
                {
                    return NotFound(); // Handle the case when the patient is not found
                }

                return View(patient);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicalRecord_No,PatientType_ID,PatientIdType_Code,Patient_ID,Prefix_Code,PFName,PMName,PLName,RFName,RMName,RLName,PatientPicture_Path,RegEntry_DateTime")] Patient patientModel, IFormFile Patient_Pictures)
        //public async Task<IActionResult> Edit(int id, [Bind("MedicalRecord_No,PatientType_ID,PatientIdType_Code,Patient_ID,Prefix_Code,PFName,PMName,PLName,RFName,RMName,RLName,PatientPicture_Path,RegEntry_DateTime")] Patient patientModel)
        {

            if (id != patientModel.MedicalRecord_No)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                //Check if a new picture is uploaded
                if (Patient_Pictures != null && Patient_Pictures.Length > 0)
                {
                    // If a new picture is uploaded, handle the update
                    if (!string.IsNullOrEmpty(patientModel.PatientPicture_Path))
                    {
                        // Delete the existing picture file
                        var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", patientModel.PatientPicture_Path.TrimStart('/'));
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }

                        // Get the file name and unique path for the new picture
                        var fileName = Path.GetFileName(Patient_Pictures.FileName);
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

                        // Construct the path to save the new file
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures", "Patient_Pictures", uniqueFileName);

                        // Save the new file to the server
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await Patient_Pictures.CopyToAsync(fileStream);
                        }

                        // Update the database record with the new file path
                        patientModel.PatientPicture_Path = "/Pictures/Patient_Pictures/" + uniqueFileName;
                    }
                }

                // Update the patient's information in the database
                _context.Update(patientModel);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Update the patient's information in the database
            _context.Update(patientModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            //return View(patientModel);

        }
        // GET: Patient/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patient == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }


            // Fetch the corresponding PatientType_Desc
            var patientTypeDesc = _context.Patient_Types
                .Where(pt => pt.PatientType_ID == patient.PatientType_ID)
                .Select(pt => pt.PatientType_Desc)
                .FirstOrDefault();

            // Pass the patient and patientTypeDesc to the view
            ViewData["PatientTypeDesc"] = patientTypeDesc;

            // Fetch the corresponding PatientIdType_Desc
            var patientidTypeDesc = _context.PatientId_Type
                .Where(pt => pt.PatientIdType_Code == patient.PatientIdType_Code)
                .Select(pt => pt.PatientIdType_Desc)
                .FirstOrDefault();

            // Pass the patient and PatientIdType_Desc to the view
            ViewData["PatientIdTypeDesc"] = patientidTypeDesc;

            // Fetch the corresponding PatientIdType_Desc
            var patientprefixeDesc = _context.PatientPrefixes
                .Where(pt => pt.Prefix_Code == patient.Prefix_Code)
                .Select(pt => pt.Prefix_Desc)
                .FirstOrDefault();

            // Pass the patient and patientprefixeDesc to the view
            ViewData["PatientprefixeDesc"] = patientprefixeDesc;

            return View(patient);


        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patient == null)
            {
                return Problem("Entity set 'AppDbContext.Patient' is null.");
            }

            var patient_v = await _context.Patient.FindAsync(id);

            if (patient_v != null)
            {
                // Store the picture path before removing the patient
                var patientPicturePath = patient_v.PatientPicture_Path;

                _context.Patient.Remove(patient_v);

                await _context.SaveChangesAsync();

                if (!string.IsNullOrEmpty(patientPicturePath))
                {
                    // Delete the existing picture file
                    var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", patientPicturePath.TrimStart('/'));

                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath);
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
