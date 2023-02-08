using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Improve.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Tutorials()
        {
            ViewData["Message"] = "Your Tutorials page.";

            return View();
        }

        public IActionResult Downloads()
        {
            ViewData["Message"] = "Your Downloads page.";

            return View();
        }

        public IActionResult WPF()
        {
            ViewData["Message"] = "Your WPF page.";

            return View();
        }

        public IActionResult MVC()
        {
            ViewData["Message"] = "Your MVC page.";

            return View();
        }

        public IActionResult SPA()
        {
            ViewData["Message"] = "Your SPA page.";

            return View();
        }

        public IActionResult SharePoint()
        {
            ViewData["Message"] = "Your SharePoint page.";

            return View();
        }

        public IActionResult XCode()
        {
            ViewData["Message"] = "Your XCode page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
