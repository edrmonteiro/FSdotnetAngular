using Brewery.API.Controllers;
using Brewery.Repository.Data;
using Brewery.Domain.Dtos;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Brewery.Repository.Contracts;

namespace BeweryMVC.Tests
{
    public class AccountControllerTest
    {
        private readonly Mock<DbSet<UserDto>> _mockSet;
        private readonly Mock<IBreweryRepository> _mockContext;
        private readonly UserDto _userDto;
        public AccountControllerTest()
        {
            _mockSet = new Mock<DbSet<UserDto>>();
            _mockContext = new Mock<IBreweryRepository>();
            _userDto = new UserDto { Email = "a@a", Password = "123456" };

            //_mockContext.Setup(m => m.GetAllUsers).Returns(_mockSet.Object);

            //_mockContext.Setup(m => m.Categorias.FindAsync(1))
            //    .ReturnsAsync(_categoria);


            //_mockContext.Setup(m => m.SetModified(_categoria));

            //_mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(1);
        }

        [Fact]
        public async Task Login()
        {
            var service = new AccountController(_mockContext.Object);

            var loginTest = await service.Login(_userDto);
            Console.WriteLine(loginTest);

            _mockSet.Verify(m => m.FindAsync(1),
                Times.Once());
        }

    }
}
