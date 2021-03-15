using AutoMapper;
using Brewery.MVC.Data;
using Brewery.MVC.Dtos;
using Brewery.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Brewery.API.Controllers
{
    [Route("api/v1/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IBreweryRepository _repo;

        public AccountController(IBreweryRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("getuser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser()
        {
            return Ok(new UserDto());
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var register = await _repo.RegisterUser(userDto);
            switch (register.StatusCode) 
            {
                case 200:
                    return Created("GetUser", register.User);
                case 400:
                    return BadRequest(register.ErrorMessage);
                case 500:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"error {register.ErrorMessage}");
                default:
                    break;
            }
            return BadRequest();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var register = await _repo.LoginUser(userDto);
            switch (register.StatusCode)
            {
                case 200:
                    return Ok(new
                    {
                        user = register.User,
                        token = register.Token
                    });
                case 400:
                    return BadRequest(register.ErrorMessage);
                case 500:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"error {register.ErrorMessage}");
                default:
                    break;
            }
            return BadRequest();

        }
    }
}
