using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab13StudentEnrollment.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// The home page of the entire application
        /// </summary>
        /// <returns>The home page</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
