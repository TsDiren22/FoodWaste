using Microsoft.AspNetCore.Mvc;
using FoodWaste.Domain;
using FoodWaste.DomainServices.IRepositories;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using FoodWaste.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FoodWaste.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        // GET: ProductsController
        readonly IRepository<Product> _productRepository;
        readonly UserManager<IdentityUser> _userManager;
        readonly IRepository<Employee> _employeeRepository;

        public ProductsController(IRepository<Product> productRepository, UserManager<IdentityUser> userManager, IRepository<Employee> employeeRepository)
        {
            _productRepository = productRepository;
            _userManager = userManager;
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            Employee? employee = null;
            if (user != null)
            {
                employee = _employeeRepository.FindByCondition(e => e.Email == user.Email) ?? null;
            }
            ViewBag.IsEmployee = employee != null ? true : false;
            return View(_productRepository.GetAll());

        }

        // GET: ProductsController/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _productRepository.GetAll() == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            string picture = Convert.ToBase64String(product.Picture);
            ViewBag.Image = string.Format("data:product.Picture/png;base64,{0}", picture);
            return View(product);
        }

        [Authorize(Policy = "EmployeeOnly")]
        // GET: ProductsController/Create
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "EmployeeOnly")]
        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(ProductCreateViewModel product)
        {
            if (ModelState.IsValid)
            {
                var newProduct = new Product();
                
                using (var memoryStream = new MemoryStream())
                {
                    await product.PictureFile.CopyToAsync(memoryStream);
                    newProduct.Picture = memoryStream.ToArray();
                    newProduct.PictureType = product.PictureFile.ContentType;
                }

                newProduct.Name = product.Name;
                newProduct.IsAlcoholic = product.IsAlcoholic;

                _productRepository.Add(newProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        
        [Authorize(Policy = "EmployeeOnly")]
        // GET: ProductsController/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _productRepository.GetAll() == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _productRepository.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ProductsController/Delete/5
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult Delete(int? id)
        {
            if (id == null || _productRepository.GetAll() == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            string picture = Convert.ToBase64String(product.Picture);
            ViewBag.Image = string.Format("data:product.Picture/png;base64,{0}", picture);
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult Delete(int id)
        {
            if (_productRepository.GetAll() == null)
            {
                return Problem("Entity set 'WebAppContext.Package'  is null.");
            }
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Delete(product);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(int id)
        {
            return _productRepository.GetById(id) != null;
        }
    }
}
