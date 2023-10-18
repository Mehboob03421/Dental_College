using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_College.Models
{
    public class PatientTitles
    {
        [Column("PatientTitle_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "PatientTitle Code")]
        public int PatientTitle_ID { get; set; }

        [Column("PatientTitle_Desc")]
        [Required(ErrorMessage = "Patient Title is required")]
        [Display(Name = "Patient Title")]
        [StringLength(15, ErrorMessage = "Patient Title Description must be less than or equal to 15 characters")]
        public string? PatientTitle_Desc { get; set; } = string.Empty;

        [Column("PatientTitle_EntryDateTime")] // Define the column name
        [Display(Name = "Entry Date and Time")]
        public DateTime PatientTitle_EntryDateTime { get; set; } = DateTime.Now; // Initialize with the current date and time
    }
}
