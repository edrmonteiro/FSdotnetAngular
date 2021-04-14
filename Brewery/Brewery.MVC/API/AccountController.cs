using AutoMapper;
using Brewery.Repository.Data;
using Brewery.Domain.Dtos;
using Brewery.Repository.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Brewery.Repository.Contracts;

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
            var adminToken = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            if (adminToken != userPassChangeDto.Email)
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
            var adminToken = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            var (status, users) = await _repo.GetAllUsers(adminToken);
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

        [HttpPost("AddUser")]
        [Authorize]
        public async Task<IActionResult> AddUser(UserDto userDto)
        {
            var adminToken = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            var (status, user) = await _repo.AddUser(userDto, adminToken);
            switch (status.StatusCode) 
            {
                case 200:
                    return Created("GetUser", user);
                case 400:
                    return BadRequest(status.Message);
                case 500:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"error {status.Message}");
                default:
                    break;
            }
            return BadRequest();
        }

        [HttpPost("RemoveUser")]
        [Authorize]
        public async Task<IActionResult> RemoveUser(UserDto userDto)
        {
            var adminToken = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            var (status, user) = await _repo.RemoveUser(userDto, adminToken);
            switch (status.StatusCode) 
            {
                case 200:
                    return Ok(user);
                case 400:
                    return BadRequest(status.Message);
                case 500:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"error {status.Message}");
                default:
                    break;
            }
            return BadRequest();
        }

        [HttpPost("AddAdminStatus2User")]
        [Authorize]
        public async Task<IActionResult> AddAdminStatus2User(UserDto userDto)
        {
            var adminToken = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            var (status, user) = await _repo.AddAdminStatus2User(userDto, adminToken);
            switch (status.StatusCode) 
            {
                case 200:
                    return Ok(user);
                case 400:
                    return BadRequest(status.Message);
                case 500:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"error {status.Message}");
                default:
                    break;
            }
            return BadRequest();
        }

        [HttpPost("RemoveAdminStatusFromUser")]
        [Authorize]
        public async Task<IActionResult> RemoveAdminStatusFromUser(UserDto userDto)
        {
            var adminToken = User.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            var (status, user) = await _repo.RemoveAdminStatusFromUser(userDto, adminToken);
            switch (status.StatusCode) 
            {
                case 200:
                    return Ok(user);
                case 400:
                    return BadRequest(status.Message);
                case 500:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"error {status.Message}");
                default:
                    break;
            }
            return BadRequest();
        }


    }
}
