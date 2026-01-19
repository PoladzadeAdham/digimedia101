using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace digimedia101.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
