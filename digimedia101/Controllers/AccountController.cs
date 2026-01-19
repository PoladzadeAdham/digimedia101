using digimedia101.Models;
using digimedia101.ViewModel.UserViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace digimedia101.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await _userManager.FindByEmailAsync(vm.Email);

            if(user is null)
            {
                ModelState.AddModelError("", "Username or password is wrong.");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is wrong.");
                return View(vm);
            }

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index","Home");

        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var existUser = await _userManager.FindByEmailAsync(vm.Email);

            if(existUser is not null)
            {
                ModelState.AddModelError("", "Bu email de user register olub.");
                return View(vm);
            }

            AppUser user = new()
            {
                UserName = vm.Username,
                Fullname = vm.Fullname,
                Email = vm.Email
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            await _userManager.AddToRoleAsync(user,"Member");

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = "Member"
            });

            return Ok("role created");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");

        }


    }
}
