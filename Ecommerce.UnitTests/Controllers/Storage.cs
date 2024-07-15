using Ecommerce.Areas.Admin.Controllers;
using Ecommerce.DataAccess;
using Ecommerce.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Ecommerce.UnitTests.Controllers
{
    public class Storage
    {
        [Fact]
        public async Task When_GettingAllStorages_Expected_ReturnStorages()
        {
            SetupOptionsBuilder();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(u => u.Storage.GetAll(
                It.IsAny<Expression<Func<Models.Storage, bool>>>(),
                It.IsAny<Func<IQueryable<Models.Storage>, IOrderedQueryable<Models.Storage>>>(),
                It.IsAny<string>(),
                It.IsAny<bool>()
                )).ReturnsAsync(new List<Models.Storage>
            {
                new() { Id = 1, Name = "Storage 1", Description = "Test storage 1", Status = false },
                new() { Id = 2, Name = "Storage 2", Description = "Test storage 2", Status = false },
            });

            var sut = new StorageController(mockUnitOfWork.Object);
            var result = await sut.GetAll();
            var jsonResult = result as JsonResult;
            var json = JsonConvert.SerializeObject(jsonResult?.Value);

            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.Equal(
                JsonConvert.SerializeObject(new
                {
                    data = new List<Models.Storage>
                {
                    new() { Id = 1, Name = "Storage 1", Description = "Test storage 1", Status = false },
                    new() { Id = 2, Name = "Storage 2", Description = "Test storage 2", Status = false },
                }
                }), json
                );
        }

        [Fact]
        public async Task When_ValidStorageIdForDeletion_Expected_SuccessfulResponse()
        {
            SetupOptionsBuilder();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(
                u => u.Storage.Get(It.IsAny<int>())
                ).ReturnsAsync(new Models.Storage
                {
                    Id = 1,
                    Name = "Storage 1",
                    Description = "Test storage 1",
                    Status = false
                });

            var expectedResponse = new { success = true, message = "Delete successful" };

            var sut = new StorageController(mockUnitOfWork.Object);

            var result = await sut.Delete(1);
            var jsonResult = result as JsonResult;
            var json = JsonConvert.SerializeObject(jsonResult?.Value);

            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.Equal(JsonConvert.SerializeObject(expectedResponse), json);
        }

        [Fact]
        public async Task When_InvalidStorageIdForDeletion_Expected_ErrorResponse()
        {
            SetupOptionsBuilder();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(
                u => u.Storage.Get(It.IsAny<int>())
                ).ReturnsAsync((Models.Storage)null);

            var expectedResponse = new { success = false, message = "Error while deleting" };

            var sut = new StorageController(mockUnitOfWork.Object);

            var result = await sut.Delete(1);
            var jsonResult = result as JsonResult;
            var json = JsonConvert.SerializeObject(jsonResult?.Value);

            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.Equal(JsonConvert.SerializeObject(expectedResponse), json);
        }

        public DbContextOptionsBuilder<ApplicationDbContext> SetupOptionsBuilder()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
        }
    }
}