using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace lindaniDS.Models
{
    public class VehicleHire
    {
        [Key]
        public int vehicleID { get; set; }

        [ForeignKey("VehicleModel")]
        [Display(Name = "Car Model")]
        public int modelID { get; set; }
        public virtual VehicleModel VehicleModel { get; set; }

     
        [StringLength(50, ErrorMessage = "Please provide with valid E-mail!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

       
        [Display(Name = "Car Color")]
        public string color { get; set; }

   
        [Display(Name = "Registration Number")]
        public string regNo { get; set; }

      
        [Display(Name = "Car Number Plate")]
        public string noPlate { get; set; }

        
        [Display(Name = "Car Hire Cost")]
        public decimal cost { get; set; }

        [Required]
        [Display(Name = "Condition of the Car")]
        public bool condition { get; set; }

     
        [Display(Name = "Is This Car Hired?")]
        public bool availability { get; set; }

        public byte[] Picture { get; set; }

        public string Photo { get; set; }

        [Display(Name = "Email")]
        public DateTime? PickDate { get; set; }

       
        [Display(Name = "Email")]
        public DateTime? DropDate { get; set; }

        [ForeignKey("Address")]
        [Display(Name = "Email")]
        public int AddreessID { get; set; }
        public ICollection<Address> Address { get; set; }


        public bool car()
        {
            if (condition == true)
            {
                availability = false;
            }
            else
            {
                availability = true;
            }
            return availability;
        }
    }
}