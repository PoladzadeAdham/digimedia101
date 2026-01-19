using digimedia101.Context;
using digimedia101.Helpers;
using digimedia101.Models;
using digimedia101.ViewModel.ProjectViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Threading.Tasks;

namespace digimedia101.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _folderPath;

        public ProjectController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");
        }


        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                                            .Select(x=> new ProjectGetVm
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                                CategoryName = x.Category.Name,
                                                ImagePath = x.ImagePath
                                            })
                                            .ToListAsync();

            return View(projects);
        }


        public async Task<IActionResult> Create()
        {
            await SendCategoriesWithViewBag();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateVm vm)
        {
            await SendCategoriesWithViewBag();

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);

            if (!isExistCategory)
            {
                ModelState.AddModelError("", "Bele bir category movcud deil.");
            }

            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("", "Seklin olcusu 2 mbdan cox olmamalidir.");
                return View(vm);
            }

            if (!vm.Image.CheckType())
            {
                ModelState.AddModelError("", "Seklin image tipinde olmalidir.");
                return View(vm);
            }

            string uniqueFileName = await vm.Image.SaveFileAsync(_folderPath);

            Project project = new()
            {
                CategoryId = vm.CategoryId,
                Name = vm.Name,
                ImagePath = uniqueFileName
            };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project is null)
                return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            string path = Path.Combine(_folderPath, project.ImagePath);

            ExtensionMethod.DeleteFile(path);

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Update(int id)
        {
            await SendCategoriesWithViewBag();

            var project = await _context.Projects
                                                    .Select(x=> new ProjectUpdateVm
                                                    {
                                                        Id = x.Id,
                                                        Name = x.Name,
                                                        CategoryId = x.CategoryId
                                                    })
                                                    .FirstOrDefaultAsync(x => x.Id == id);

            return View(project);

        }

        [HttpPost]
        public async Task<IActionResult> Update(ProjectUpdateVm vm)
        {
            await SendCategoriesWithViewBag();

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);

            if (!isExistCategory)
            {
                ModelState.AddModelError("", "Bele bir category movcud deil.");
            }

            if (!vm.Image?.CheckSize(2) ?? false)
            {
                ModelState.AddModelError("", "Seklin olcusu 2 mbdan cox olmamalidir.");
                return View(vm);
            }

            if (!vm.Image?.CheckType() ?? false)
            {
                ModelState.AddModelError("", "Seklin image tipinde olmalidir.");
                return View(vm);
            }


            var existProject = await _context.Projects.FindAsync(vm.Id);

            if (existProject is null)
                return NotFound();


            if(vm.Image is { })
            {
                string path = Path.Combine(_folderPath, existProject.ImagePath);

                ExtensionMethod.DeleteFile(path);

                string uniqueFileName = await vm.Image.SaveFileAsync(_folderPath);

                existProject.ImagePath = uniqueFileName;

            }

            existProject.Name = vm.Name;
            existProject.CategoryId = vm.CategoryId;

            _context.Projects.Update(existProject);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }


        private async Task SendCategoriesWithViewBag()
        {
            var categories = await _context.Categories.ToListAsync();

            ViewBag.Categories = categories;
        }
    }
}
