using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_College.Models
{
    public class Genders
    {
        //internal readonly object? Genders;

        [Column("Gender_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Gender Code")]
        public int Gender_ID { get; set; }

        [Column("Gender_Desc")]
        [Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender Desc")]
        [StringLength(15, ErrorMessage = "Gender must be less than or equal to 15 characters")]
        public string? Gender_Desc { get; set; } = string.Empty;

        [Column("EntryDateTime")] // Define the column name
        [Display(Name = "Entry Date and Time")]
        public DateTime EntryDateTime { get; set; } = DateTime.Now; // Initialize with the current date and time

    }
}
