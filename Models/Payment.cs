using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace lindaniDS.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public string PayType { get; set; }

       // [ForeignKey("User")]
        public string UserID { get; set; }
       // public virtual User User { get; set; }

        [ForeignKey("VehicleHire")]
        public int? vehicleID { get; set; }
        public virtual VehicleHire VehicleHire { get; set; }

        [ForeignKey("Learners")]
        public int? LearnerID { get; set; }
        public virtual Learners Learners { get; set; }

        [ForeignKey("BookingPackages")]
        public int? PackageID { get; set; }
        public virtual BookingPackages BookingPackages { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(50, ErrorMessage = "Please provide with valid E-mail!")]
        [Display(Name = "BankName")]
        public string BankName { get; set; }


        [Display(Name = "BranchCode")]
        public int BranchCode { get; set; }

        [Display(Name = "CardNumber")]
        public int CardNumber { get; set; }


        [Display(Name = "Expire")]
        public DateTime Expire { get; set; }


        [Display(Name = "CVV")]
        public int CVV { get; set; }

    }
}