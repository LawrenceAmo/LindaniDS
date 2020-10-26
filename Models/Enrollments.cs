using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace lindaniDS.Models
{
    public class Enrollments
    {
        [Key]
        public int EnrollID { get; set; }
        public string UserID { get; set; }
       
        public int LearnerID { get; set; }
        public virtual Learners Learners { get; set; }


    }
}