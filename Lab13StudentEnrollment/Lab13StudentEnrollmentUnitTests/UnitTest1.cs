using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Lab13StudentEnrollment.Data;
using Lab13StudentEnrollment.Models;
using System.Linq;
using Lab13StudentEnrollment.Controllers;

namespace Lab13StudentEnrollmentUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async void DbCanSaveTest()
        {
            DbContextOptions<Lab13StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<Lab13StudentEnrollmentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (Lab13StudentEnrollmentDbContext context = new Lab13StudentEnrollmentDbContext(options))
            {
                // Arrange
                Course course = new Course();
                course.Name = "DotNet";
                course.Description = "Embrace the flood";

                Student student = new Student();
                student.Name = "Someone";
                student.CourseID = 1;

                // Act
                await context.Courses.AddAsync(course);
                await context.Students.AddAsync(student);
                await context.SaveChangesAsync();

                var results = context.Courses.Where(a => a.Name == "DotNet");
                var results2 = context.Students.Where(a => a.Name == "Someone");
                // Assert
                Assert.Equal(1, results.Count());
                Assert.Equal(1, results2.Count());
            }
        }

        [Fact]
        public void CourseControllerCanCreate()
        {
            DbContextOptions<Lab13StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<Lab13StudentEnrollmentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (Lab13StudentEnrollmentDbContext context = new Lab13StudentEnrollmentDbContext(options))
            {
                // Arrange
                Course course = new Course();
                course.Name = "DotNet";
                course.Description = "Embrace the flood";

                // Act
                CourseController cc = new CourseController(context);

                var x = cc.Create(course);

                var results = context.Courses.Where(a => a.Name == "DotNet");
                // Assert
                Assert.Equal(1, results.Count());
            }
        }

        [Fact]
        public void StudentControllerCanCreate()
        {
            DbContextOptions<Lab13StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<Lab13StudentEnrollmentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (Lab13StudentEnrollmentDbContext context = new Lab13StudentEnrollmentDbContext(options))
            {
                // Arrange
                Course course = new Course();
                course.Name = "DotNet";
                course.Description = "Embrace the flood";

                CourseController cc = new CourseController(context);

                var x = cc.Create(course);

                Student student = new Student();
                student.Name = "Someone";
                student.CourseID = 1;
                // Act
                StudentController sc = new StudentController(context);

                var y = sc.Create(student);

                var results = context.Students.Where(a => a.Name == "Someone");
                // Assert
                Assert.Equal(1, results.Count());
            }
        }

        [Fact]
        public void CourseControllerCanUpdate()
        {
            DbContextOptions<Lab13StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<Lab13StudentEnrollmentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (Lab13StudentEnrollmentDbContext context = new Lab13StudentEnrollmentDbContext(options))
            {
                // Arrange
                Course course = new Course();
                course.Name = "DotNet";
                course.Description = "Embrace the flood";

                CourseController cc = new CourseController(context);
                var x = cc.Create(course);

                // Act
                course.Name = "JavaScript";
                var y = cc.Update(course);

                var results = context.Courses.Where(a => a.Name == "JavaScript");
                // Assert
                Assert.Equal(1, results.Count());
            }
        }

        [Fact]
        public async void StudentControllerCanUpdate()
        {
            DbContextOptions<Lab13StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<Lab13StudentEnrollmentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (Lab13StudentEnrollmentDbContext context = new Lab13StudentEnrollmentDbContext(options))
            {
                // Arrange
                Course course = new Course();
                course.Name = "DotNet";
                course.Description = "Embrace the flood";

                CourseController cc = new CourseController(context);

                var x = cc.Create(course);

                Student student = new Student();
                student.Name = "Someone";
                student.CourseID = 1;
                StudentController sc = new StudentController(context);

                var y = sc.Create(student);
                // Act

                student.Name = "Brian";
                var z = sc.Update(1, student);

                var results = await context.Students.Where(a => a.Name == "Brian").ToListAsync();
                // Assert
                Assert.Equal("Brian", results[0].Name);
            }
        }

        [Fact]
        public void CourseControllerCanDelete()
        {
            DbContextOptions<Lab13StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<Lab13StudentEnrollmentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (Lab13StudentEnrollmentDbContext context = new Lab13StudentEnrollmentDbContext(options))
            {
                // Arrange
                Course course = new Course();
                course.Name = "DotNet";
                course.Description = "Embrace the flood";

                CourseController cc = new CourseController(context);

                var x = cc.Create(course);
                // Act

                var y = cc.Delete(course.ID);

                var results = context.Courses.Where(a => a.Name == "DotNet");
                // Assert
                Assert.Equal(0, results.Count());
            }
        }

        [Fact]
        public void StudentControllerCanDelete()
        {
            DbContextOptions<Lab13StudentEnrollmentDbContext> options =
                new DbContextOptionsBuilder<Lab13StudentEnrollmentDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (Lab13StudentEnrollmentDbContext context = new Lab13StudentEnrollmentDbContext(options))
            {
                // Arrange
                Course course = new Course();
                course.Name = "DotNet";
                course.Description = "Embrace the flood";

                CourseController cc = new CourseController(context);

                var x = cc.Create(course);

                Student student = new Student();
                student.Name = "Someone";
                student.CourseID = 1;
                StudentController sc = new StudentController(context);

                var y = sc.Create(student);
                // Act
                var z = sc.Delete(student.ID);

                var results = context.Students.Where(a => a.Name == "Someone");
                // Assert
                Assert.Equal(0, results.Count());
            }
        }

        [Fact]
        public void CanGetAndSetForStudentsTest()
        {
            // Arrange
            Student student = new Student();

            // Act
            student.ID = 1;
            student.Name = "Jay";
            student.CourseID = 1;
            student.CourseName = "Python";

            // Assert
            Assert.Equal(1, student.ID);
            Assert.Equal("Jay", student.Name);
            Assert.Equal(1, student.CourseID);
            Assert.Equal("Python", student.CourseName);
        }

        [Fact]
        public void CanGetAndSetForCoursesTest()
        {
            // Arrange
            Course course = new Course();

            // Act
            course.ID = 1;
            course.Name = "DotNet";
            course.Description = "Savor the flood";

            // Assert
            Assert.Equal(1, course.ID);
            Assert.Equal("DotNet", course.Name);
            Assert.Equal("Savor the flood", course.Description);
        }

        [Fact]
        public void CanGetAndSetForCourseStudentViewModelTest()
        {
            // Arrange
            Course course = new Course();
            course.ID = 1;
            course.Name = "DotNet";
            course.Description = "Savor the flood";

            Student student = new Student();
            student.ID = 1;
            student.Name = "Jimmy";
            student.CourseID = 1;
            student.CourseName = "DotNet";

            CourseStudentViewModel courseStudent = new CourseStudentViewModel();

            // Act
            courseStudent.Course = course;
            courseStudent.Student = student;

            // Assert
            Assert.Equal(course.Name, courseStudent.Course.Name);
            Assert.Equal(course.ID, courseStudent.Course.ID);
            Assert.Equal(course.Description, courseStudent.Course.Description);
            Assert.Equal(student.ID, courseStudent.Student.ID);
            Assert.Equal(student.Name, courseStudent.Student.Name);
            Assert.Equal(student.CourseID, courseStudent.Course.ID);
            Assert.Equal(student.CourseID, courseStudent.Student.CourseID);
            Assert.Equal(student.CourseName, courseStudent.Course.Name);
            Assert.Equal(student.CourseName, courseStudent.Student.CourseName);
        }
    }
}
