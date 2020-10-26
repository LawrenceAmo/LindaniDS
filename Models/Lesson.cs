using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LindaniDrivingSchool.Models
{
    public class Lesson
    {
        [Key]
        public int LessonID { get; set; }
        public string Name { get; set; }
        public string LessonDisc { get; set; }
        public string Vid { get; set; }
        public int MyProperty { get; set; }
        public int CourseID { get; set; }
        public int lessonType { get; set; }
        public virtual Course Course { get; set; }

    }
}