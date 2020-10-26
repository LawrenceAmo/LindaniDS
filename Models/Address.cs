using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lindaniDS.Models
{
    public class Address
    {
        [Key]
        public int AddreessID { get; set; }

        [StringLength(50, ErrorMessage = "Please provide with valid E-mail!")]
        [Display(Name = "Email")]
        public string Country { get; set; }

        [StringLength(50, ErrorMessage = "Please provide with valid E-mail!")]
        [Display(Name = "Email")]
        public string Province { get; set; }

        [StringLength(50, ErrorMessage = "Please provide with valid E-mail!")]
        [Display(Name = "Email")]
        public string City { get; set; }

        [StringLength(50, ErrorMessage = "Please provide with valid E-mail!")]
        [Display(Name = "Email")]
        public string Surbub { get; set; }

        [StringLength(50, ErrorMessage = "Please provide with valid E-mail!")]
        [Display(Name = "Email")]
        public string Street { get; set; }
       
        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }
    }
}