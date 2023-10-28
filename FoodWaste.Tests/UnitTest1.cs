using FoodWaste.Domain;
using FoodWaste.Domain.DataAnnotations;
using FoodWaste.DomainServices.IRepositories;
using FoodWaste.WebApp.Controllers;
using FoodWaste.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System.Linq.Expressions;
using System.Security.Claims;

namespace FoodWaste.Tests
{
    public class UnitTest1
    {
        
        Mock<IRepository<Package>> _packageRepositoryMock = new ();
        Mock<IRepository<Product>> _productRepositoryMock = new ();
        Mock<IRepository<Student>> _studentRepositoryMock = new ();
        Mock<IRepository<Cafeteria>> _cafeteriaRepositoryMock = new ();
        Mock<IRepository<Employee>> _employeeRepositoryMock = new ();
        Mock<AgeDataAnnotations> ageLogic = new ();
        Mock<MaxDaysInFutureAnnotation> daysInFutreLogic = new ();
        readonly Mock<IUserStore<IdentityUser>> store = new();

        readonly Cafeteria cafeteria = new()
        {
            Id = 1,
            City = City.Breda,
            HasWarmMeals = true,
            Location = "Test location",
            Name = "Test"
        };

        readonly Package package = new()
        {
            Id = 1,
            Name = "Test",
            PickupExpiry = DateTime.Now.AddDays(2),
            Price = 10,
            MealType = "Lunch",
            ContainsAdultProducts = true,
            Products = new List<Product>()
        };
        readonly Package package2 = new()
        {
            Id = 1,
            Name = "Test",
            PickupExpiry = DateTime.Now.AddDays(2),
            Price = 10,
            MealType = "Lunch",
            ContainsAdultProducts = false,
            Products = new List<Product>()
        };

        readonly Employee employee = new()
        {
            Id = 1,
            Email = "abc@abc.com",
            EmployeeNumber = 1,
            Name = "Test Employee",
            CafeteriaId = 1,
            
        };

        // Under 18
        readonly Student student = new()
        {
            Id = 1,
            Email = "testuser@test.com",
            DateOfBirth = new(2009, 8, 4),
            StudentNumber = 1,
            CityOfStudy = City.Breda,
            PhoneNumber = "0612345678"
        };

        // Over 18
        readonly Student student2 = new()
        {
            Id = 1,
            Email = "testuser@test.com",
            DateOfBirth = new(2000, 8, 4),
            StudentNumber = 1,
            CityOfStudy = City.Breda,
            PhoneNumber = "0612345678"
        };

        readonly ReserveModel reserveModelOld = new()
        {
            PickupDate = DateTime.Now.AddDays(3),
            PackageId = 1
        };

        readonly ReserveModel reserveModel = new()
        {
            PickupDate = DateTime.Now.AddDays(1),
            PackageId = 1
        };

        private PackagesController PrepareController()
        {
            Mock<UserManager<IdentityUser>> userManagerMock = new(store.Object, null, null, null, null, null, null, null, null);
            var controller = new PackagesController(
                _packageRepositoryMock.Object,
                _studentRepositoryMock.Object,
                _cafeteriaRepositoryMock.Object,
                _productRepositoryMock.Object,
                userManagerMock.Object,
                _employeeRepositoryMock.Object);

            // Create a user and configure userManagerMock to return this user.
            IdentityUser user = new IdentityUser { UserName = employee.Email, Email = employee.Email };
            userManagerMock.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(user);

            // Configure _employeeRepositoryMock to return the employee object.
            _employeeRepositoryMock.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Employee, bool>>>()))
                .Returns(employee);

            var context = new DefaultHttpContext();
            context.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, student.Email),
                new Claim(ClaimTypes.Role, "StudentOnly"),
            }));
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = context
            };

            return controller;
        }

        [Fact]
        public async Task Should_Redirect_To_ProductSelect_Action_On_Create()
        {
            // Arrange
            PackagesController controller = PrepareController();
            var package = new Package { /* Initialize package properties */ };

            // Mock TempData
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            // Act
            IActionResult result = controller.Create(package);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);

            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Equal("ProductSelect", redirectToActionResult.ActionName);
        }


        [Fact]
        public async Task Should_Return_ModelError_When_Outside_Pickup_Range()
        {
            // Arrange
            PackagesController controller = PrepareController();

            // Configure the package and student
            _packageRepositoryMock.Setup(x => x.GetById(package.Id)).Returns(package);
            _studentRepositoryMock.Setup(x => x.FindByCondition(It.IsAny <Expression<Func<Student, bool>>>()))
                .Returns(student2);

            // Act
            ViewResult result = controller.Reserve(reserveModelOld) as ViewResult;

            // Assert
            Assert.NotNull(result);

            // Check ModelState for the expected error
            Assert.True(controller.ModelState.ContainsKey(string.Empty)); // Check if there is an error at the root level
            var error = controller.ModelState[string.Empty].Errors[0];
            Assert.Contains("The package has to be reserved between now and", error.ErrorMessage);
        }

        [Fact]
        public async Task Should_Return_Error_When_Student_Too_Young_When_Reserve_Alcoholic()
        {
            // Arrange
            PackagesController controller = PrepareController();

            // Configure the package and student
            _packageRepositoryMock.Setup(x => x.GetById(package.Id)).Returns(package);
            _studentRepositoryMock.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Student, bool>>>()))
                .Returns(student);

            // Act
            ViewResult result = controller.Reserve(reserveModel) as ViewResult;

            // Assert
            Assert.NotNull(result);

            // Check ModelState for the expected error
            Assert.True(controller.ModelState.ContainsKey(string.Empty)); // Check if there is an error at the root level
            var error = controller.ModelState[string.Empty].Errors[0];
            Assert.Contains("You have to be older than 18 to reserve this package.", error.ErrorMessage);
        }

        [Fact]
        public async Task Should_Reserve_When_Underaged_Student_Reserves_Non_Alcoholic()
        {
            // Arrange
            PackagesController controller = PrepareController();

            // Configure the package and student
            _packageRepositoryMock.Setup(x => x.GetById(package.Id)).Returns(package2);
            _studentRepositoryMock.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Student, bool>>>()))
                .Returns(student);

            // Act
            IActionResult result = controller.Reserve(reserveModel);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToAction = (RedirectToActionResult)result;
            Assert.Equal("Index", redirectToAction.ActionName);
        }

        [Fact]
        public async Task Should_Reserve_When_Adult_Student_Reserves_Alcoholic()
        {
            // Arrange
            PackagesController controller = PrepareController();

            // Configure the package and student
            _packageRepositoryMock.Setup(x => x.GetById(package.Id)).Returns(package);
            _studentRepositoryMock.Setup(x => x.FindByCondition(It.IsAny<Expression<Func<Student, bool>>>()))
                .Returns(student2);

            // Act
            IActionResult result = controller.Reserve(reserveModel);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToAction = (RedirectToActionResult)result;
            Assert.Equal("Index", redirectToAction.ActionName);
        }

        [Fact]
        public void Edit_GetAction_WithValidId_ShouldReturnView()
        {
            // Arrange
            int packageId = 1;
            var package = new Package { Id = packageId, /* Initialize package properties */ };
            _packageRepositoryMock.Setup(x => x.GetById(packageId)).Returns(package);
            var controller = PrepareController();

            // Act
            IActionResult result = controller.Edit(packageId);

            // Assert
            Assert.IsType<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.IsType<Package>(viewResult.Model);
            var model = viewResult.Model as Package;
            Assert.Equal(packageId, model.Id);
        }

        [Fact]
        public void Edit_GetAction_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            int invalidPackageId = 999; // An ID that does not exist
            _packageRepositoryMock.Setup(x => x.GetById(invalidPackageId)).Returns((Package)null);
            var controller = PrepareController();

            // Act
            IActionResult result = controller.Edit(invalidPackageId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_PostAction_WithValidModel_ShouldRedirectToProductSelect()
        {
            // Arrange
            int packageId = 1;
            var updatedPackage = package;
            updatedPackage.Name = "Updated";
            var controller = PrepareController();
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            _packageRepositoryMock.Setup(x => x.GetById(packageId)).Returns(package);

            // Act
            IActionResult result = controller.Edit(packageId, updatedPackage);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Equal("ProductSelect", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_PostAction_WithValidId_ShouldRedirectToIndex()
        {
            // Arrange
            int packageId = 1;
            var controller = PrepareController();
            _packageRepositoryMock.Setup(x => x.GetById(packageId)).Returns(package);
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
            controller.TempData = tempData;

            // Act
            IActionResult result = controller.Delete(packageId);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_PostAction_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            int packageId = 1; // An ID that does not exist
            var controller = PrepareController();
            _packageRepositoryMock.Setup(x => x.GetById(packageId)).Returns((Package)null);

            // Act
            IActionResult result = controller.Delete(packageId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Cancel_PostAction_WithValidId_ShouldRedirectToIndex()
        {
            // Arrange
            int packageId = 1; // An existing package ID
            var controller = PrepareController();
            var package = new Package { Id = packageId, StudentId = 1, PickupDate = DateTime.Now };

            _packageRepositoryMock.Setup(x => x.GetById(packageId)).Returns(package);

            // Act
            IActionResult result = controller.Cancel(packageId);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectToActionResult.ActionName);

            // Verify that the package was updated
            _packageRepositoryMock.Verify(x => x.Update(It.IsAny<Package>()), Times.Once);
        }

        [Fact]
        public void Cancel_PostAction_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            int packageId = 1; // A non-existent package ID
            var controller = PrepareController();
            _packageRepositoryMock.Setup(x => x.GetById(packageId)).Returns((Package)null);

            // Act
            IActionResult result = controller.Cancel(packageId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

    }
}
