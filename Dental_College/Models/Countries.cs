using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Dental_College.Models
{
    public class Countries
    {
        [Column("CountryCode")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Country Code is required")]
        [Display(Name = "Country Code")]
        public int CountryCode { get; set; }

        //[Column("CountyName")]
        [Required(ErrorMessage = "Country Name is required")]
        [Display(Name = "Country Name")]
        [StringLength(25, ErrorMessage = "Country Name must be less than or equal to 25 characters")]
        public string? CountryName { get; set; } = string.Empty;


        //[Column("NationalLanguage")]
        [Required(ErrorMessage = "Please mention the Nation Language of this country")]
        [Display(Name = "National Language")]
        [StringLength(25, ErrorMessage = "Country Language must be less than or equal to 25 characters")]
        public string? NationalLanguage { get; set; }

        //[Column("CurrencyName")]
        [Required(ErrorMessage = "Please mention the Currency of this country")]
        [Display(Name = "Currency Name")]
        [StringLength(25, ErrorMessage = "Currency must be less or equal to than 25 characters")]
        public string? CurrencyName { get; set; }

        [Column("Dialing_Code")]
        [Display(Name = "Dialing Code")]
        [Required(ErrorMessage = "Please Enter the Dialing Code")]
        public int Dialing_Code { get; set; }

    }
}
