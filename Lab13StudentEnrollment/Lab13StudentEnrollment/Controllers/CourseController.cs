using Lab13StudentEnrollment.Data;
using Lab13StudentEnrollment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab13StudentEnrollment.Controllers
{
    public class CourseController : Controller
    {
        private Lab13StudentEnrollmentDbContext _context;

        /// <summary>
        /// This makes sure that controller has a database
        /// </summary>
        /// <param name="context">The database</param>
        public CourseController(Lab13StudentEnrollmentDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This is the index page of the courses, it also serves as the list of courses
        /// It also serves as a filter for the courses
        /// </summary>
        /// <param name="searchString">A way to filter what courses are shown</param>
        /// <returns>The view of the index page</returns>
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            
            if (!String.IsNullOrEmpty(searchString))
            {
                var courses = await _context.Courses
                                            .Where(c => c.Name.Contains(searchString))
                                            .ToListAsync();
                return View(courses);
            }
            else
            {
                var courses = await _context.Courses.ToListAsync();
                return View(courses);
            }
        }

        /// <summary>
        /// This is the details page of a specific course.
        /// It shows not only the basic information of the course but also all the students in the course
        /// </summary>
        /// <param name="id">This makes sure that this is a valid course</param>
        /// <returns>The view of the specified course</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id.HasValue)
            {
                CourseStudentViewModel courseStudent = new CourseStudentViewModel();
                courseStudent.Course = await _context.Courses.FirstOrDefaultAsync(s => s.ID == id);
                courseStudent.students = await _context.Students
                                                       .Where(x => x.CourseID == id)
                                                       .ToListAsync();

                return View(courseStudent);
            }
            // If the user put in an invalid course id, then they are sent back to the home page.
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// This allows for a new course to be made.
        /// </summary>
        /// <returns>This returns the create page</returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This takes in the new course information and then adds it to the database
        /// </summary>
        /// <param name="course">This is the course to be added</param>
        /// <returns>This returns the user back to the index page of the courses</returns>
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ID, Name, Description")]Course course)
        {
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// This allows for a course to be updated.
        /// </summary>
        /// <param name="id">This makes sure that the id is valid</param>
        /// <returns>The update view page</returns>
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id.HasValue)
            {
                Course course = await _context.Courses.FirstOrDefaultAsync(s => s.ID == id);
                return View(course);
            }
            // THis sends the user back to the home page if they put in an invalid course ID.
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// This returns the coruse that is to be updated in the database.
        /// </summary>
        /// <param name="course">The course to be updated</param>
        /// <returns>This redirects the user back to the index page of the courses</returns>
        [HttpPost]
        public async Task<IActionResult> Update([Bind("ID, Name, Description")]Course course)
        {
            _context.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// This deletes a course from the database
        /// </summary>
        /// <param name="id">This is the id of the course to be deleted</param>
        /// <returns>This redirects the user back to the index page of the courses</returns>
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            CourseStudentViewModel courseStudent = new CourseStudentViewModel();
            courseStudent.Course = await _context.Courses.FindAsync(id);

            if (courseStudent.Course == null)
            {
                // If there is no course at the ID provided, the user is sent to the NotFound error page.
                return NotFound();
            }

            courseStudent.Student = await _context.Students.FirstOrDefaultAsync(s => s.CourseID == id);
            if (courseStudent.Student != null)
            {
                // If the course still has students in it, the user will be sent to this error page
                return RedirectToAction("Error");
            }

            _context.Courses.Remove(courseStudent.Course);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This is an error page for when the user tries to delete a course with a student still enrolled.
        /// </summary>
        /// <returns>The error page</returns>
        public IActionResult Error()
        {
            return View();
        }
    }
}
