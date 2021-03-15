using AutoMapper;
using Brewery.MVC.Dtos;
using Brewery.MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brewery.MVC.Data
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly BreweryContext _context;
        public readonly UserManager<IdentityUser> _userManager;
        public readonly SignInManager<IdentityUser> _signInManager;
        public readonly IMapper _mapper;
        public BreweryRepository(BreweryContext context,
                                UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        //General

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //Account

        public async Task<LoginDto> RegisterUser(UserDto userDto)
        {
            try
            {
                var user = new IdentityUser { UserName = userDto.Email, Email = userDto.Email };
                var result = await _userManager.CreateAsync(user, userDto.Password);
                
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var result1 = await _userManager.ConfirmEmailAsync(user, code);

                    var userToReturn = _mapper.Map<UserDto>(user);
                    return new LoginDto { StatusCode = 200, User = userToReturn };
                }
                return new LoginDto { StatusCode = 400, ErrorMessage = Newtonsoft.Json.JsonConvert.SerializeObject(result.Errors)};
            }
            catch (System.Exception ex)
            {
                return new LoginDto { StatusCode = 500, ErrorMessage = ex.Message };
            }

        }
        public async Task<LoginDto> LoginUser(UserDto userDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userDto.Email);
                var result = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users
                                    .FirstOrDefaultAsync(u => u.NormalizedUserName == userDto.Email.ToUpper());
                    var userToReturn = _mapper.Map<UserDto>(appUser);
                    return new LoginDto { StatusCode = 200, User = userToReturn, Token = TokenService.GenerateToken(appUser) };
                }
                return new LoginDto { StatusCode = 401} ;
            }
            catch (System.Exception ex)
            {
                return new LoginDto { StatusCode = 500, ErrorMessage = ex.Message };
            }
        }


    }
}
