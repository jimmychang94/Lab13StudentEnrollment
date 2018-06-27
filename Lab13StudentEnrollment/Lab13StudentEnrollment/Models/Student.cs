using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab13StudentEnrollment.Models
{
    public class Student
    {
        /// <summary>
        /// This is the id that will be made when the student is put into the database
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// This is the name of the student
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// This is the Course ID of the course that the student is enrolled in.
        /// </summary>
        [Required]
        public int CourseID { get; set; }

        /// <summary>
        /// This is the matching course name of the course that the student is enrolled in.
        /// </summary>
        public string CourseName { get; set; }
    }
}
