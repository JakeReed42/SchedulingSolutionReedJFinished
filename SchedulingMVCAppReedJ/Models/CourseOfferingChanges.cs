using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulingMVCAppReedJ.Models
{
    public class CourseOfferingChanges
    {

        [Key]
        public int CourseOfferingChangeID { get; set; }

        public string ChangerRole { get; set; }

        public string ChangeType { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateAndTimeOfChange { get; set; }

        public string CRN { get; set; }

        // since public and has get set its called a property
        public string Days { get; set; }

        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime EndDate { get; set; }

        public int? InstructorID { get; set; }

        public string ChangerName { get; set; }

        public int CourseID { get; set; }

        [ForeignKey("InstructorID")]
        public Instructor Instructor { get; set; }

        [ForeignKey("CourseID")]
        public Course Course { get; set; }


        public CourseOfferingChanges(string changerRole, string changeType, /*DateTime dateAndTimeOfChange,*/
            string crn, string days, DateTime startTime, DateTime endTime,
            DateTime startDate, DateTime endDate, int? instructorID ,string changerName, int courseID)
        {

            this.ChangerRole = changerRole;
            this.ChangeType = changeType;
            this.DateAndTimeOfChange = System.DateTime.Now;
            this.CRN = crn;
            this.Days = days;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.StartDate = startDate;
            this.EndDate = endTime;
            this.InstructorID = instructorID;
            this.ChangerName = changerName;
            this.CourseID = courseID;
        }

        public CourseOfferingChanges()
        { }
    }// end of class
}// end of namespace
