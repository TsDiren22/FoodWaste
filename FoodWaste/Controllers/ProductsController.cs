using Microsoft.AspNetCore.Mvc;
using FoodWaste.Domain;
using FoodWaste.DomainServices.IRepositories;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace FoodWaste.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        // GET: ProductsController
        readonly IRepository<Product> _productRepository;

        public ProductsController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
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
            ViewBag.Image = string.Format("data:image/png;base64,{0}", picture);
            return View(product);
        }

        // GET: ProductsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

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
            ViewBag.Image = string.Format("data:image/png;base64,{0}", picture);
            return View(product);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
