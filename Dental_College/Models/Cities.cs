using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Dental_College.Models
{
    public class Cities
    {
        public int? CountryCode { get; set; }
        [ForeignKey("CountryCode")]
        public virtual Countries? Countries { get; set; }
        public int? StateCode { get; set; }
        [ForeignKey("StateCode")]
        public virtual States? States { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "City Code is required")]
        [Display(Name = "City Code")]
        public int CityCode { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        [Display(Name = "City Name")]
        [StringLength(30, ErrorMessage = "City Name must be less than or equal to 30 characters")]
        public string? CityName { get; set; }

        

    }
}
