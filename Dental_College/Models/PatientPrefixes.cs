using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_College.Models
{
    public class PatientPrefixes
    {
        [Column("Prefix_Code")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Patient Prefix Code")]
        public int Prefix_Code { get; set; }

        [Column("Prefix_Desc")]
        [Required(ErrorMessage = "Patient Prefix is required")]
        [Display(Name = "Patient Prefix")]
        [StringLength(25, ErrorMessage = "Patient Prefix must be less than or equal to 25 characters")]
        public string? Prefix_Desc { get; set; } = string.Empty;

        [Column("Prefix_EntryDateTime")] // Define the column name
        [Display(Name = "Entry Date and Time")]
        public DateTime Prefix_EntryDateTime { get; set; } = DateTime.Now; // Initialize with the current date and time

    }
}
