using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using FoodWaste.Domain;
using FoodWaste.DomainServices.IRepositories;
using FoodWaste.Models;
using FoodWaste.Infrastructure.Seed;

namespace FoodWaste.Infrastructure.Repositories
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private IRepository<Student> _studentRepository;
        private IRepository<Employee> _employeeRepository;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IRepository<Student> repository, IRepository<Employee> employeeRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _studentRepository = repository;
            _employeeRepository = employeeRepository;

            IdentityData.EnsurePopulated(userManager).Wait();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user =
                    await _userManager.FindByNameAsync(loginModel.Username);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user,
                        loginModel.Password, false, false)).Succeeded)
                    {
                        return Redirect("/Home/Index");
                    }
                }

            }
            ModelState.AddModelError("", "Invalid username or password!");
            return View(loginModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}