using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_College.Models
{
    public class Patient_Identification
    {
        [Column("PatientIdType_Code")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Patient Identification Code")]
        public int PatientIdType_Code { get; set; }

        [Column("PatientIdType_Desc")]
        [Required(ErrorMessage = "Patient Reference is required")]
        [Display(Name = "Patient Identification Type")]
        [StringLength(20, ErrorMessage = "Patient Identification must be less than or equal to 20 characters")]
        public string? PatientIdType_Desc { get; set; } = string.Empty;

        [Column("PatientIdType_EntryDateTime")] // Define the column name
        [Display(Name = "Entry Date and Time")]
        public DateTime PatientIdType_EntryDateTime { get; set; } = DateTime.Now; // Initialize with the current date and time

    }
}
