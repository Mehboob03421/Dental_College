using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_College.Models
{
    public class PatientNationality
    {
        [Column("Nationality_Code")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Patient Nationality Code")]
        public int Nationality_Code { get; set; }

        [Column("Nationality_Name")]
        [Required(ErrorMessage = "Patient Nationality is required")]
        [Display(Name = "Patient Nationality")]
        [StringLength(25, ErrorMessage = "Patient Nationality_Name must be less than or equal to 25 characters")]
        public string? Nationality_Name { get; set; } = string.Empty;

        [Column("Nationality_EntryDateTime")] // Define the column name
        [Display(Name = "Entry Date and Time")]
        public DateTime Nationality_EntryDateTime { get; set; } = DateTime.Now; // Initialize with the current date and time

    }
}
