using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab13StudentEnrollment.Models
{
    public class CourseStudentViewModel
    {
        /// <summary>
        /// This holds a list of students for the detailed view of a course
        /// </summary>
        public List<Student> students;

        /// <summary>
        /// This holds a list of courses for the create and update for students
        /// </summary>
        public List<Course> courses;

        /// <summary>
        /// This holds a specified course for use when a list is needed as well as a course.
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// This holds a specified student for use when a list is needed as well as a student.
        /// </summary>
        public Student Student { get; set; }
    }
}
