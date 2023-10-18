using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_College.Models
{
    public class PatientLanguage
    {
        [Column("Language_Code")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Patient Language Code")]
        public int Language_Code { get; set; }

        [Column("Language_Name")]
        [Required(ErrorMessage = "Patient Language is required")]
        [Display(Name = "Patient Language")]
        [StringLength(25, ErrorMessage = "Patient Language Name must be less than or equal to 25 characters")]
        public string? Language_Name { get; set; } = string.Empty;

        [Column("Language_EntryDateTime")] // Define the column name
        [Display(Name = "Entry Date and Time")]
        public DateTime Language_EntryDateTime { get; set; } = DateTime.Now; // Initialize with the current date and time


    }
}
