using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Programming_Project.Models;

namespace Web_Programming_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? c)
        {
            ViewResult result;
            if(c is null)
            {
                result = View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            else
            {
                ViewData["errorCode"] = c;
                string message = "";
                string image = "";
                switch (c)
                {
                    case 400:
                    case 404:
                        message = "Exploring this page does not lead anywhere.";
                        image = "error_not_found.png";
                        break;
                    case 401:
                    case 403:
                        message = "Access to this page requires you to have additional rights.";
                        image = "error_access_denied.png";
                        break;
                    default:
                        message = "Exploring this page does not lead anywhere.";
                        image = "error_default.png";
                        break;
                }
                ViewData["errorMessage"] = message;
                ViewData["errorImage"] = image;
                result = View("Error");
            }
            return result;
        }
    }
}