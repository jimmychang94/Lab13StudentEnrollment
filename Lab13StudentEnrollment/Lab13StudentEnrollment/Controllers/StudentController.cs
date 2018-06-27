using Lab13StudentEnrollment.Data;
using Lab13StudentEnrollment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab13StudentEnrollment.Controllers
{
    public class StudentController : Controller
    {
        /// <summary>
        /// The private version of the database
        /// </summary>
        private Lab13StudentEnrollmentDbContext _context;

        /// <summary>
        /// This constructor makes it so that the controller always has a database to connect to
        /// </summary>
        /// <param name="context">The database it connects to</param>
        public StudentController(Lab13StudentEnrollmentDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This is the index page for the students. It is also where we can see all the students.
        /// </summary>
        /// <param name="searchString">This will filter the students based off of the string</param>
        /// <returns>This view of the index page</returns>
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<Course> courseQuery = _context.Courses;

            //This joins the table of courses and students to fill out the students to include the course name
            var students = from c in _context.Courses
                                 join s in _context.Students on c.ID equals s.CourseID
                                 select new Student {
                                     Name = s.Name,
                                     CourseName = c.Name,
                                     ID = s.ID,
                                     CourseID = c.ID
                                 };

            // This if statement filters the students more if there is a search string
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString));
            }
            
            List<Student> courseStudent = await students.ToListAsync();
            return View(courseStudent);
        }
        
        /// <summary>
        /// This sets up the create page and then returns the view for the page
        /// </summary>
        /// <returns>The view of the create page</returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CourseStudentViewModel courseStudent = new CourseStudentViewModel();
            courseStudent.courses = await _context.Courses.ToListAsync();
            return View(courseStudent);
        }

        /// <summary>
        /// This is what is returned from the Create page. It holds the information for a new student
        /// </summary>
        /// <param name="student">The new student to be added</param>
        /// <returns>This sends the user back to the index page for students</returns>
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ID, Name, CourseID")]Student student)
        {

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// This updates the student with the specified id.
        /// </summary>
        /// <param name="id">This makes sure that the id specified is a valid one</param>
        /// <returns>The Update view with the correct information</returns>
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if(id.HasValue)
            {
                CourseStudentViewModel courseStudent = new CourseStudentViewModel();
                courseStudent.courses = await _context.Courses.ToListAsync();
                courseStudent.Student = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
                return View(courseStudent);
            }
            // If the user gave the wrong id, this sends the user back to the home page
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// This updtaes the student specified with the new information received
        /// </summary>
        /// <param name="id">The id of the student to be updated</param>
        /// <param name="student">The updated student information</param>
        /// <returns>This sends the user back to the index page for students</returns>
        [HttpPost]
        public async Task<IActionResult> Update( int id, [Bind("ID, Name, CourseID")]Student student)
        {
            student.ID = id;
            _context.Update(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This deletes a student from the database
        /// </summary>
        /// <param name="id">This makes sure that there is a valid student ID</param>
        /// <returns>This sends the user back to the index page for students</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
