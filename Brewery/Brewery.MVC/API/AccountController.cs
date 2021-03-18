using AutoMapper;
using Brewery.MVC.Data;
using Brewery.MVC.Dtos;
using Brewery.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
            var email = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            var userid = User.Claims.Where(x => x.Type == "id").FirstOrDefault()?.Value;

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

        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(UserPassChangeDto userPassChangeDto)
        {
            var tokenEmail = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            if (tokenEmail != userPassChangeDto.Email)
            {
                return BadRequest("Authenticated user different from email informed");
            }
            var user = await _repo.ChangePassword(userPassChangeDto);
            switch (user.StatusCode)
            {
                case 200:
                    return Ok(new
                    {
                        Message = user.Message,
                    });
                case 400:
                case 404:
                    return BadRequest(user.Message);
                case 500:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"error {user.Message}");
                default:
                    break;
            }
            return BadRequest();

        }

        [HttpGet("GetAllUsers")]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            var tokenEmail = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            var (status, users) = await _repo.GetAllUsers(tokenEmail);
            switch (status.StatusCode)
            {
                case 200:
                    return Ok(users);
                case 400:
                case 404:
                    return BadRequest(status.Message);
                case 500:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, status.Message);
                default:
                    break;
            }
            return BadRequest();

        }







    }
}
