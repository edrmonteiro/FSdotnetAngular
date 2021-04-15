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
    [Route("api/v1/BeerStyle")]
    [ApiController]
    //[Authorize]
    public class BeerStyleController : ControllerBase
    {
        private readonly IBreweryRepository _repo;
        public BeerStyleController(IBreweryRepository repo)
        {
            _repo = repo;
        }
        // GET: api/<BeerStyleController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAllAsync<BeerStyle>());
        }

        // GET api/<BeerStyleController>/5
        [HttpGet("GetById{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _repo.GetItemAsync<BeerStyle>(id));
        }

        // POST api/<BeerStyleController>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(BeerStyle beerStyle)
        {
            _repo.Add(beerStyle);
            return Ok(await _repo.SaveChangesAsync());
        }

        // PUT api/<BeerStyleController>/5
        [HttpPut("Update")]
        public async Task<IActionResult> Update(BeerStyle beerStyle)
        {
            _repo.Update(beerStyle);
            return Ok(await _repo.SaveChangesAsync());
        }

        // DELETE api/<BeerStyleController>/5
        [HttpDelete("Remove")]
        public async Task<IActionResult> Delete(BeerType beerStyle)
        {
            _repo.Delete(beerStyle);
            return Ok(await _repo.SaveChangesAsync());
        }


    }
}
