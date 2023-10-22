using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using FoodWaste.Domain;
using FoodWaste.DomainServices.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FoodWaste.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("packages")]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private IRepository<Package> _packageRepository;
        readonly UserManager<IdentityUser> _userManager;
        private IRepository<Student> _studentRepository;

        public ReservationController(ILogger<ReservationController> logger, IRepository<Package> packageRepository, UserManager<IdentityUser> userManager, IRepository<Student> studentRepository)
        {
            _logger = logger;
            _packageRepository = packageRepository;
            _userManager = userManager;
            _studentRepository = studentRepository;
        }
        

        [HttpPost("{packageId}/reserve")]
        public IActionResult Reserve(int packageId, [FromBody] DateTime pickupDate)
        {
            if (ModelState.IsValid)
            {
                var package = _packageRepository.GetById(packageId);
                
                if (package == null) return NotFound();

                var user = _userManager.GetUserAsync(HttpContext.User).Result;
                var student = _studentRepository.FindByCondition(x => x.Email == user.Email);
                if (student == null) return NotFound();

                if (student.DateOfBirth.AddYears(18) > pickupDate && package.ContainsAdultProducts)
                {
                    ModelState.AddModelError("", "You have to be older than 18 to reserve this package.");
                    return BadRequest(ModelState);
                }

                if (pickupDate > package.PickupExpiry || pickupDate < DateTime.Now)
                {
                    ModelState.AddModelError("", "The package has to be reserved between now and " + package.PickupExpiry.ToShortDateString());
                    return BadRequest(ModelState);
                }

                package.StudentId = student.Id;
                package.PickupDate = pickupDate;

                _packageRepository.Update(package);
                return Ok("Package reserved successfully");
            }

            return BadRequest(ModelState);
        }
    }
}
