using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab13StudentEnrollment.Models
{
    public class Course
    {
        /// <summary>
        /// This is the ID of the course that is determined when it is added to the database
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// This is the name of the course
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This is a description of the course
        /// </summary>
        public string Description { get; set; }


    }
}
