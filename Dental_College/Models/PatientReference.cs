using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_College.Models
{
    public class PatientReference
    {
        [Column("Reference_Code")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Patient Reference Code")]
        public int Reference_Code { get; set; }

        [Column("Reference_Desc")]
        [Required(ErrorMessage = "Patient Reference is required")]
        [Display(Name = "Patient Reference")]
        [StringLength(30, ErrorMessage = "Patient Reference Name must be less than or equal to 30 characters")]
        public string? Reference_Desc { get; set; } = string.Empty;

        [Column("Reference_EntryDateTime")] // Define the column name
        [Display(Name = "Entry Date and Time")]
        public DateTime Reference_EntryDateTime { get; set; } = DateTime.Now; // Initialize with the current date and time

    }
}
