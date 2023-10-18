using System;
using System.IO;
using Microsoft.AspNetCore.HostFiltering;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dental_College.Models
{
    public class Patient
    {
        
        [Column("MedicalRecord_No")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "MR.NO:")]
        public int MedicalRecord_No { get; set; }

        [Column("PatientType_ID")]
        [Display(Name = "Patient Type")]
        public int PatientType_ID { get; set; }

        [Column("PatientIdType_Code")]
        [Display(Name = "Patient.ID Type")]
        public int PatientIdType_Code { get; set; }

        [Column("Patient_ID")]
        [Required(ErrorMessage = "Patient.ID required")]
        [Display(Name = "Patient.ID:")]
        [StringLength(30, ErrorMessage = "Patient.ID must be less than or equal to 30 characters")]
        public string? Patient_ID { get; set; } = string.Empty;

        [Display(Name = "Patient Prefix Code:")]
        public int Prefix_Code { get; set; }

        [Column("PFName")]
        [Required(ErrorMessage = "Patient First Name is required")]
        [Display(Name = "Patient First Name:")]
        [StringLength(25, ErrorMessage = "Patient First Name must be less than or equal to 25 characters")]
        public string? PFName { get; set; } = string.Empty;

        [Column("PMName")]
        [Display(Name = "Patient Middle Name:")]
        [StringLength(25, ErrorMessage = "Patient Middle Name must be less than or equal to 25 characters")]
        public string? PMName { get; set; } = string.Empty;

        [Column("PLName")]
        [Display(Name = "Patient Last Name:")]
        [StringLength(25, ErrorMessage = "Patient Last Name must be less than or equal to 25 characters")]
        public string? PLName { get; set; } = string.Empty;

        [Column("RFName")]
        [Required(ErrorMessage = "Patient Relation First Name is required")]
        [Display(Name = "Patient Relation First Name:")]
        [StringLength(25, ErrorMessage = "Patient Relation First Name must be less than or equal to 25 characters")]
        public string? RFName { get; set; } = string.Empty;

        [Column("RMName")]
        [Display(Name = "Patient Relation Middle Name:")]
        [StringLength(25, ErrorMessage = "Patient Relation Middle Name must be less than or equal to 25 characters")]
        public string? RMName { get; set; } = string.Empty;

        [Column("RLName")]
        [Display(Name = "Patient Relation Last Name:")]
        [StringLength(25, ErrorMessage = "Patient Relation Last Name must be less than or equal to 25 characters")]
        public string? RLName { get; set; } = string.Empty;

        [Column("PatientPicture_Path")]
        [Display(Name = "Current Patient Picture Path")]
        public string? PatientPicture_Path { get; set; }
        

        [Column("RegEntry_DateTime")] // Define the column name
        [Display(Name = "Entry Date and Time")]
        public DateTime RegEntry_DateTime { get; set; } = DateTime.Now; // Initialize with the current date and time
    }
}
