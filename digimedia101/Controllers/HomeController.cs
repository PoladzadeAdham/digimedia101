using System.Diagnostics;
using System.Threading.Tasks;
using digimedia101.Context;
using digimedia101.ViewModel.CategoryViewModel;
using digimedia101.ViewModel.ProjectViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace digimedia101.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                                                    .Select(x=> new ProjectGetVm
                                                    {
                                                        Id = x.Id,
                                                        Name = x.Name,
                                                        CategoryName =x.Category.Name,
                                                        ImagePath = x.ImagePath
                                                    })
                                                    .ToListAsync();

            return View(projects);
        }


    }
}
