using digimedia101.Context;
using digimedia101.Models;
using digimedia101.ViewModel.CategoryViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace digimedia101.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles ="Admin")]
    public class CategoryController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                                                    .Select(x => new CategoryGetVm
                                                    {
                                                        Id = x.Id,
                                                        Name = x.Name
                                                    })
                                                    .ToListAsync();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVm vm)
        {

            if (!ModelState.IsValid)
                return View(vm);

            Category category = new()
            {
                Name = vm.Name
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
                return NotFound();

            _context.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _context.Categories
                                                    .Select(x => new CategoryUpdateVm
                                                    {
                                                        Id = x.Id,
                                                        Name = x.Name
                                                    })
                                                    .FirstOrDefaultAsync(x => x.Id == id);

            return View(category);

        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var existingCategory = await _context.Categories.FindAsync(vm.Id);

            if (existingCategory is null)
                return NotFound();

            existingCategory.Name = vm.Name;

            _context.Categories.Update(existingCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }


    }
}
