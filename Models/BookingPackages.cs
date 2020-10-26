using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lindaniDS.Models
{
    public class BookingPackages
    {
        [Key]
        public int PackageID { get; set; }

        [StringLength(50, ErrorMessage = "Please provide with valid E-mail!")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Cost")]
        public double Cost { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "AvailableFrom")]
        public DateTime? AvailableFrom { get; set; }       

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Package Availability")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime? AvailableTo { get; set; }

    }
}