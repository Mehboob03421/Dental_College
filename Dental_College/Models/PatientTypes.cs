using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_College.Models
{
    public class PatientTypes
    {
        [Column("PatientType_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "PatientType Code")]
        public int PatientType_ID { get; set; }

        [Column("PatientType_Desc")]
        [Required(ErrorMessage = "PatientType is required")]
        [Display(Name = "PatientType Desc")]
        [StringLength(15, ErrorMessage = "PatientType Description must be less than or equal to 15 characters")]
        public string? PatientType_Desc { get; set; } = string.Empty;

        [Column("PatientType_EntryDateTime")] // Define the column name
        [Display(Name = "Entry Date and Time")]
        public DateTime PatientType_EntryDateTime { get; set; } = DateTime.Now; // Initialize with the current date and time

    }
}
