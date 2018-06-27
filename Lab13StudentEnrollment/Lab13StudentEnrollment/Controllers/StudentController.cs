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
        private Lab13StudentEnrollmentDbContext _context;

        public StudentController(Lab13StudentEnrollmentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<Course> courseQuery = _context.Courses;

            var students = from c in _context.Courses
                                 join s in _context.Students on c.ID equals s.CourseID
                                 select new Student {
                                     Name = s.Name,
                                     CourseName = c.Name,
                                     ID = s.ID,
                                     CourseID = c.ID
                                 };

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString));
            }
            
            List<Student> courseStudent = await students.ToListAsync();
            return View(courseStudent);
        }

        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CourseStudentViewModel courseStudent = new CourseStudentViewModel();
            courseStudent.courses = await _context.Courses.ToListAsync();
            return View(courseStudent);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ID, Name, CourseID")]Student student)
        {

            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Update( int id, [Bind("ID, Name, CourseID")]Student student)
        {
            student.ID = id;
            _context.Update(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


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
