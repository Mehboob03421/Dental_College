using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Microsoft.AspNetCore.HostFiltering;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;
using System.Data;

namespace Dental_College.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
        { }

        public DbSet<Countries> Countries { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Dental_College.Models.Cities>? Cities { get; set; }
        public DbSet<Genders> Genders { get; set; }
        public DbSet<Relations> Relations { get; set; }
        public DbSet<PatientTypes> Patient_Types { get; set; }
        public DbSet<PatientTitles> Patient_Titles{ get; set; }
        public DbSet<PatientNationality> Patient_Nationality { get; set; }
        public DbSet<PatientLanguage> Patient_Language { get; set; }
        public DbSet<PatientOccupation> Patient_Occupation { get; set; }
        public DbSet<PatientReference> Patient_Reference { get; set; }
        public DbSet<Patient_Identification> PatientId_Type { get; set; }
        public DbSet<PatientPrefixes> PatientPrefixes { get; set; }
        public DbSet<Patient> Patient { get; set; }

    }
}
