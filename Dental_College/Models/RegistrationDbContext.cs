using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dental_College.Models;


namespace Dental_College.Models
{
   
        public class RegistrationDbContext : DbContext
        {
            public RegistrationDbContext(DbContextOptions<RegistrationDbContext> options) :base(options)
            { }

            public DbSet<Genders> Genders { get; set; }

        }
    
}
