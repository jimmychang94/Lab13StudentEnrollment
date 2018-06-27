using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab13StudentEnrollment.Models
{
    public class CourseStudentViewModel
    {
        public List<Student> students;
        public List<Course> courses;
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
