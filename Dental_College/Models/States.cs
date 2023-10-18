using System;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_College.Models
{
    public class States
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "State Code is required")]
        [Display(Name = "State Code")]
        public int StateCode { get; set; }

        //[Column("CountyName")]
        [Required(ErrorMessage = "State Name is required")]
        [Display(Name = "State Name")]
        [StringLength(40, ErrorMessage = "State Name must be less than or equal to 30 characters")]
        public string? StateName { get; set; }
        public int? CountryCode { get; set; }
        [ForeignKey("CountryCode")]
        public virtual Countries? Countries { get; set; }



    }
}
