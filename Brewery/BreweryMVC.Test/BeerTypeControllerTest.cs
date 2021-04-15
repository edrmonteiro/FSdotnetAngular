using Brewery.Domain;
using Brewery.MVC.API;
using Brewery.Repository.Contracts;
using Brewery.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BreweryMVC.Test
{
    public class BeerTypeControllerTest
    {
        private readonly Mock<IBreweryRepository> _mockRepo;
        private readonly BeerType _beerType;
        private List<BeerType> beerTypes;
        public BeerTypeControllerTest()
        {
            //https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.1
            try
            {
                seedBeerTypes();
                _mockRepo = new Mock<IBreweryRepository>();
                _mockRepo.Setup(m => m.GetAllAsync<BeerType>()).ReturnsAsync(getBeerTypes);
            }
            catch(Exception ex)
            { }
        }
        private void seedBeerTypes()
        {
            beerTypes = new List<BeerType>
                {
                    new BeerType { Id = 1, Type = "test type1"},
                    new BeerType { Id = 2, Type = "test type2"},
                    new BeerType { Id = 3, Type = "test type3"},
                };
        }
        private List<BeerType> getBeerTypes()
        {            
            return beerTypes.ToList();
        }
        private BeerType getBeerType(int id)
        {
            return beerTypes.Where(c => c.Id == id).FirstOrDefault();
        }
        [Fact]
        public async Task GetAll_BeerType()
        {
            var controller = new BeerTypeController(_mockRepo.Object);
            var result = await controller.GetAll();
            var okResult = result as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }


    }
}
