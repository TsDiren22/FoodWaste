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

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                bool emailExistsInStudents = _studentRepository.FindByCondition(student => student.Email == registerModel.Email) != null;
                bool emailExistsInEmployees = _employeeRepository.FindByCondition(employee => employee.Email == registerModel.Email) != null;

                if (!emailExistsInStudents && !emailExistsInEmployees)
                {
                    var account = new IdentityUser
                    {
                        UserName = registerModel.Username,
                        Email = registerModel.Email
                    };

                    var result = await _userManager.CreateAsync(account, registerModel.Password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddClaimAsync(account, new Claim("Student", "true"));

                        var signInResult = await _signInManager.PasswordSignInAsync(account, registerModel.Password, false, false);

                        if (signInResult.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This email is already in use. Please use a different email.");
                }
            }

            return View();
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}