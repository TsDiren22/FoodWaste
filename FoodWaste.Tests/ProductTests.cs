using FoodWaste.Domain;
using FoodWaste.Domain.DataAnnotations;
using FoodWaste.DomainServices.IRepositories;
using FoodWaste.WebApp.Controllers;
using FoodWaste.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace FoodWaste.Tests
{
    public class ProductTests
    {       
        Mock<IRepository<Package>> _packageRepositoryMock = new();
        Mock<IRepository<Product>> _productRepositoryMock = new();
        Mock<IRepository<Student>> _studentRepositoryMock = new();
        Mock<IRepository<Cafeteria>> _cafeteriaRepositoryMock = new();
        Mock<IRepository<Employee>> _employeeRepositoryMock = new();
        Mock<AgeDataAnnotations> ageLogic = new();
        Mock<MaxDaysInFutureAnnotation> daysInFutreLogic = new();
        readonly Mock<IUserStore<IdentityUser>> store = new();

        readonly Cafeteria cafeteria = new()
        {
            Id = 1,
            City = City.Breda,
            HasWarmMeals = true,
            Location = "Test location",
            Name = "Test"
        };

        readonly Product product = new()
        {
            Id = 1,
            Name = "Test",
            IsAlcoholic = false,
            Packages = new List<Package>(),
            Picture = new byte[0],
            PictureType = "Test"
        };

        readonly ProductCreateViewModel productCreateViewModel = new()
        {
            Name = "Test",
            IsAlcoholic = false,
            PictureFile = new Mock<IFormFile>().Object,
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

        private ProductsController PrepareController()
        {

            Mock<UserManager<IdentityUser>> userManagerMock = new(store.Object, null, null, null, null, null, null, null, null);
            var controller = new ProductsController(
                _productRepositoryMock.Object,
                userManagerMock.Object,
                _employeeRepositoryMock.Object
                );

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
        public void Index_GetAction_ShouldReturnView()
        {
            // Arrange
            var productRepositoryMock = new Mock<IRepository<Product>>();
            productRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Product>());
            var controller = PrepareController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Details_GetAction_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var productRepositoryMock = new Mock<IRepository<Product>>();
            productRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Product)null);
            var controller = PrepareController();

            // Act
            var result = controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_PostAction_WithValidModel_ShouldRedirectToIndex()
        {
            // Arrange
            var controller = PrepareController();

            // Act
            var result = await controller.CreateAsync(productCreateViewModel);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Edit_GetAction_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            var productRepositoryMock = new Mock<IRepository<Product>>();
            productRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Product)null);
            var controller = PrepareController();

            // Act
            var result = controller.Edit(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_PostAction_WithValidModel_ShouldRedirectToIndex()
        {
            // Arrange
            var productRepositoryMock = new Mock<IRepository<Product>>();
            productRepositoryMock.Setup(repo => repo.Update(It.IsAny<Product>()));
            var controller = PrepareController();

            var product = new Product
            {
                Id = 1,
                Name = "TestProduct",
                IsAlcoholic = false,
                Picture = new byte[] { 1, 2, 3 },
                PictureType = "image/png"
            };

            // Act
            var result = controller.Edit(1, product);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_PostAction_WithValidId_ShouldRedirectToIndex()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Product());
            _productRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Product>()));
            var controller = PrepareController();

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
            var redirectToActionResult = result as RedirectToActionResult;
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_PostAction_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Product)null);
            var controller = PrepareController();

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
