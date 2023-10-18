using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_College.Models
{
    public class PatientOccupation
    {
        [Column("Occupation_Code")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Patient Occupation Code")]
        public int Occupation_Code { get; set; }

        [Column("Occupation_Desc")]
        [Required(ErrorMessage = "Patient Occupation is required")]
        [Display(Name = "Patient Occupation")]
        [StringLength(50, ErrorMessage = "Patient Occupation Description must be less than or equal to 50 characters")]
        public string? Occupation_Desc { get; set; } = string.Empty;

        [Column("Occupation_EntryDateTime")] // Define the column name
        [Display(Name = "Entry Date and Time")]
        public DateTime Occupation_EntryDateTime { get; set; } = DateTime.Now; // Initialize with the current date and time

    }
}
