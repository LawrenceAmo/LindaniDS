using lindaniDS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class Course
    {        
        [Key]
        public int CourseID { get; set; }
        public string CourseDisc { get; set; }
        public string Name { get; set; }
        public int LearnerID { get; set; }
        public byte[] Picture { get; set; }

        public string Photo { get; set; }
        public int Code { get; set; }

        public virtual Learners Learners { get; set; }
    }
}