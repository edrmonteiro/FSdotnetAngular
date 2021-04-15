
using Brewery.Domain;
using Brewery.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Brewery.MVC.API
{
    [Route("api/v1/BeerType")]
    [ApiController]
    //[Authorize]
    public class BeerTypeController : ControllerBase
    {
        private readonly IBreweryRepository _repo;
        public BeerTypeController(IBreweryRepository repo)
        {
            _repo = repo;
        }
        // GET: api/<BeerTypeController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAllAsync<BeerType>());
        }

        // GET api/<BeerTypeController>/5
        [HttpGet("GetById{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetItemAsync<BeerType>(id);
            Console.WriteLine(item);
            //return Ok(new BeerType { Type = "test type" });
            return Ok(await _repo.GetItemAsync<BeerType>(id));
        }

        // POST api/<BeerTypeController>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(BeerType beerType)
        {
            _repo.Add(beerType);
            return Ok(await _repo.SaveChangesAsync());
        }

        // PUT api/<BeerTypeController>/5
        [HttpPut("Update")]
        public async Task<IActionResult> Update(BeerType beerType)
        {
            _repo.Update(beerType);
            return Ok(await _repo.SaveChangesAsync());
        }

        // DELETE api/<BeerTypeController>/5
        [HttpDelete("Remove")]
        public async Task<IActionResult> Delete(BeerType beerType)
        {
            _repo.Delete(beerType);
            return Ok(await _repo.SaveChangesAsync());
        }


    }
}
