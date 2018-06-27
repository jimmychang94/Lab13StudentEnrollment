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
        public async void CourseControllerCanCreate()
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
        public async void StudentControllerCanCreate()
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
        public async void CourseControllerCanUpdate()
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
                var y = cc.Update(course)

                var results = context.Courses.Where(a => a.Name == "DotNet");
                // Assert
                Assert.Equal(1, results.Count());
            }
        }
    }
}
