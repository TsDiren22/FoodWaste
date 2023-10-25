using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodWaste.Domain;
using FoodWaste.Infrastructure.Data;
using FoodWaste.Infrastructure.Repositories;
using FoodWaste.DomainServices.IRepositories;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using FoodWaste.WebApp.Models;

namespace FoodWaste.WebApp.Controllers
{
    public class PackagesController : Controller
    {
        readonly IRepository<Package> _packageRepository;
        readonly IRepository<Student> _studentRepository;
        readonly IRepository<Cafeteria> _cafeteriaRepository;
        readonly IRepository<Product> _productRepository;
        readonly IRepository<Employee> _employeeRepository;
        private static Dictionary<string, Package> SharedStorage = new Dictionary<string, Package>();


        readonly UserManager<IdentityUser> _userManager;

        public PackagesController(IRepository<Package> packageRepository, IRepository<Student> studentRepository, IRepository<Cafeteria> cafeteriaRepository, IRepository<Product> productRepository, UserManager<IdentityUser> userManager, IRepository<Employee> employeeRepository)
        {
            _packageRepository = packageRepository;
            _studentRepository = studentRepository;
            _cafeteriaRepository = cafeteriaRepository;
            _productRepository = productRepository;
            _employeeRepository = employeeRepository;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var student = _studentRepository.FindByCondition(x => x.Email == user.Email);
            
            if (student != null)
            {
                ViewBag.StudentPackages = _packageRepository.GetAll().Where(x => x.StudentId == student.Id).OrderBy(x => x.PickupExpiry);
                ViewBag.EmployeePackages = null;
                ViewBag.Cafeterias = _cafeteriaRepository.GetAll();
                ViewBag.StudentCity = student.CityOfStudy;
                return View(_packageRepository.GetAll().Where(x => x.StudentId == null).OrderBy(x => x.PickupExpiry));
            }
            else
            {
                var employee = _employeeRepository.FindByCondition(x => x.Email == user.Email);
                ViewBag.StudentPackages = null;
                ViewBag.EmployeePackages = _packageRepository.GetAll().Where(x => x.CafeteriaId != employee!.CafeteriaId).OrderBy(x => x.PickupDate);
                return View(_packageRepository.GetAll().Where(x => x.CafeteriaId == employee!.CafeteriaId).OrderBy(x => x.PickupDate));
            }
        }
        
        // GET: Packages/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _packageRepository.GetAll() == null) return NotFound();

            var package = _packageRepository.GetById(id.Value);
            if (package == null) return NotFound();

            return View(package);
        }

        // GET: Packages/Create
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: Packages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult Create(Package package)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(HttpContext.User).Result;
                var employee = _employeeRepository.FindByCondition(x => x.Email == user.Email);

                package.CafeteriaId = employee!.CafeteriaId;

                TempData["Package"] = JsonConvert.SerializeObject(package);
                return RedirectToAction(nameof(ProductSelect));
            }
            return View(package);
        }

        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult ProductSelect(string key = null)
        {
            if (key != null)
            {
                if (SharedStorage.TryGetValue(key, out Package package))
                {
                    SharedStorage.Remove(key);

                    ViewBag.SelectedProducts = package!.Products;
                    package.Products = new List<Product>();
                    TempData["Package"] = JsonConvert.SerializeObject(package);

                    return View(_productRepository.GetAll());
                }
                else return NotFound();
            }
            else
            {
                TempData.Keep("Package");
                var package = JsonConvert.DeserializeObject<Package>(TempData["Package"].ToString());
                //TODO: Products are empty???? Look into it!!!!!!
                ViewBag.SelectedProducts = package!.Products;
                return View(_productRepository.GetAll());
            }

        }

        // POST: Packages/ProductSelect
        [Authorize(Policy = "EmployeeOnly")]
        [HttpPost]
        public IActionResult ProductSelect(List<int> products)
        {
            if (products.Count() < 1)
            {
                TempData["Error"] = "Please select at least one product";
                TempData.Keep("Package");
                return RedirectToAction(nameof(ProductSelect));
            }
            var package = JsonConvert.DeserializeObject<Package>(TempData["Package"].ToString());
            package!.Products = products.Select(x => _productRepository.GetById(x)).ToList();

            products.ForEach(x => {
                if (_productRepository.FindByCondition(y => y.Id == x).IsAlcoholic) package.ContainsAdultProducts = true;
            });

            Package existing = _packageRepository.GetById(package.Id);
            if (existing != null)
            {
                _packageRepository.Update(package);
            }
            else _packageRepository.Add(package);
                
            return RedirectToAction(nameof(Index));
        }

        // GET: Packages/Edit/5
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult Edit(int? id)
        {
            if (id == null || _packageRepository.GetAll() == null) return NotFound();

            var package = _packageRepository.GetById(id.Value);
            if (package == null) return NotFound();

            return View(package);

        }

        // POST: Packages/Edit/5
        [Authorize(Policy = "EmployeeOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Package package)
        {
            if (id != package.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(HttpContext.User).Result;
                var employee = _employeeRepository.FindByCondition(x => x.Email == user.Email);

                package.CafeteriaId = employee!.CafeteriaId;
                Package x = _packageRepository.GetById(id);
                package.Products = x.Products;
                string key = Guid.NewGuid().ToString();
                SharedStorage[key] = package;
                
                return RedirectToAction(nameof(ProductSelect), new { key });
            }
            return View(package);
        }

        // GET: Packages/Delete/5
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult Delete(int? id)
        {
            if (id == null || _packageRepository.GetAll() == null)
            {
                return NotFound();
            }

            var package = _packageRepository.GetById(id.Value);
            if (package == null) return NotFound();

            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult Delete(int id)
        {
            if (_packageRepository.GetAll() == null) return Problem("Entity set 'WebAppContext.Package'  is null.");
            
            var package = _packageRepository.GetById(id);
            
            if (package != null) _packageRepository.Delete(package);
            
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = "StudentOnly")]
        public IActionResult Reserve(int? id)
        {
            if (id == null || _packageRepository.GetAll() == null) return NotFound();
            return View(new ReserveModel(){ PackageId = (int) id });
        }

        // POST: Packages/Reserve/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "StudentOnly")]
        public IActionResult Reserve(ReserveModel reserveModel)
        {
            if (ModelState.IsValid)
            {
                var package = _packageRepository.GetById(reserveModel.PackageId);
                
                if (package == null) return NotFound();
                
                var user = _userManager.GetUserAsync(HttpContext.User).Result;
                var student = _studentRepository.FindByCondition(x => x.Email == user.Email);
                
                if (student == null) return NotFound();

                if (student.DateOfBirth.AddYears(18) > reserveModel.PickupDate && package.ContainsAdultProducts)
                {
                    ModelState.AddModelError("", "You have to be older than 18 to reserve this package.");
                    return View(reserveModel);
                }

                if (reserveModel.PickupDate > package.PickupExpiry || reserveModel.PickupDate < DateTime.Now)
                {
                    ModelState.AddModelError("", "The package has to be reserved between now and " + package.PickupExpiry.ToShortDateString());
                    return View(reserveModel);
                }

                package.StudentId = student.Id;
                package.PickupDate = reserveModel.PickupDate;

                _packageRepository.Update(package);
                return RedirectToAction(nameof(Index));
            }
            return View(reserveModel);
        }
        
        [HttpPost]
        [Authorize(Policy = "StudentOnly")]
        public IActionResult Cancel(int id)
        {
            var package = _packageRepository.GetById(id);

            if (package == null) return NotFound();

            package.StudentId = null;
            package.PickupDate = null;

            _packageRepository.Update(package);

            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(int id)
        {
            return _packageRepository.GetById(id) != null;
        }
    }
}
