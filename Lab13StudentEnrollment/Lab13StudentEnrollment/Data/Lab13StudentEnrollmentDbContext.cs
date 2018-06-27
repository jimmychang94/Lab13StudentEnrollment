using Lab13StudentEnrollment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab13StudentEnrollment.Data
{
    public class Lab13StudentEnrollmentDbContext : DbContext
    {
        public Lab13StudentEnrollmentDbContext (DbContextOptions<Lab13StudentEnrollmentDbContext> options) : base (options)
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
