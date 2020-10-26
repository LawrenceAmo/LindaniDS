using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lindaniDS.Models
{
    public class Learners
    {
        [Key]
        public int LearnerID { get; set; }

        public string Name { get; set; }
        public string LearnerDisc { get; set; }
        public byte[] Picture { get; set; }

        public string Photo { get; set; }
        public int Code { get; set; }
        public decimal Cost { get; set; }





        //public string Email { get; set; }

        //[StringLength(50, ErrorMessage = "Please provide with valid E-mail!")]
        //[Display(Name = "Province")]
        //public string Province { get; set; }

        //[StringLength(50, ErrorMessage = "Please provide with valid E-mail!")]
        //[Display(Name = "City")]
        //public string City { get; set; }

        //[StringLength(50, ErrorMessage = "Please provide with valid E-mail!")]
        //[Display(Name = "Surbub")]
        //public string Surbub { get; set; }

        //[StringLength(50, ErrorMessage = "Please provide with valid E-mail!")]
        //[Display(Name = "Street")]
        //public string Street { get; set; }

        //[Display(Name = "Zip Code")]
        //public int ZipCode { get; set; }
        //public int Phone { get; set; }
        //public int IDNum { get; set; }
        //public string Location { get; set; }
        //public DateTime BookingDate { get; set; }
        //public string Session { get; set; }
    }
}