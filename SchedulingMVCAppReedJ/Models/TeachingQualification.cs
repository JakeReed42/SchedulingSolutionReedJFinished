using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models
{
    public class TeachingQualification
    {
        [Key]
        [Required]
        public int TeachingQualificationID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [ForeignKey("CourseID")]
        public Course Course { get; set; }

        [Required]
        public int InstructorID { get; set;}

        [ForeignKey("InstructorID")]
        public Instructor Instructor { get; set; }


        public TeachingQualification(int courseID, int instructorID)
        {
            this.CourseID = courseID;
            this.InstructorID = instructorID;
        }

        public TeachingQualification()
        { }

        public static List<TeachingQualification> PopulateTeachingQualifications()
        {
            List<TeachingQualification> teachingQualificationsList = new List<TeachingQualification>();

            TeachingQualification teachingQualification = new TeachingQualification(1, 1);
            teachingQualificationsList.Add(teachingQualification);

            teachingQualification = new TeachingQualification(2, 1);
            teachingQualificationsList.Add(teachingQualification);

            teachingQualification = new TeachingQualification(3, 3);
            teachingQualificationsList.Add(teachingQualification);

            teachingQualification = new TeachingQualification(6, 4);
            teachingQualificationsList.Add(teachingQualification);

            return teachingQualificationsList;
        }


    }// end of class 
}// end of namespace
