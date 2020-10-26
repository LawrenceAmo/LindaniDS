using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lindaniDS.Models
{
    public class VehicleModel
    {
        [Key]
        public int modelID { get; set; }

        [Required]
        [Display(Name = "Car Name")]
        public string vehicleName { get; set; }

        [Required]
        [Display(Name = "Car Make")]
        public string vehicleMake { get; set; }

        [Required]
        [Display(Name = "Car Body Type")]
        public string vehicleBodyType { get; set; }

        [Required]
        [Display(Name = "Car Model")]
        public string modelName { get; set; }
    }
}