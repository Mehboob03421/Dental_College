using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dental_College.Models
{
    public class Relations
    {
        [Column("Relation_ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Relation Code")]
        public int Relation_ID { get; set; }

        [Column("Relation_Desc")]
        [Required(ErrorMessage = "Relation Description is required")]
        [Display(Name = "Relation Desc")]
        [StringLength(10, ErrorMessage = "Relation must be less than or equal to 10 characters")]
        public string? Relation_Desc { get; set; } = string.Empty;

        [Column("Relation_EntryDateTime")] // Define the column name
        [Display(Name = "Entry Date and Time")]
        public DateTime Telation_EntryDateTime { get; set; } = DateTime.Now; // Initialize with the current date and time
    }
}
