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

        public CourseController(Lab13StudentEnrollmentDbContext context)
        {
            _context = context;
        }

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
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ID, Name, Description")]Course course)
        {
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id.HasValue)
            {
                Course course = await _context.Courses.FirstOrDefaultAsync(s => s.ID == id);
                return View(course);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Update([Bind("ID, Name, Description")]Course course)
        {
            _context.Update(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            CourseStudentViewModel courseStudent = new CourseStudentViewModel();
            courseStudent.Course = await _context.Courses.FindAsync(id);

            if (courseStudent.Course == null)
            {
                return NotFound();
            }

            courseStudent.Student = await _context.Students.FirstOrDefaultAsync(s => s.CourseID == id);
            if (courseStudent.Student != null)
            {
                return RedirectToAction("Error");
            }

            _context.Courses.Remove(courseStudent.Course);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
