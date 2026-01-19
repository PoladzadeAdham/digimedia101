using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace digimedia101.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
