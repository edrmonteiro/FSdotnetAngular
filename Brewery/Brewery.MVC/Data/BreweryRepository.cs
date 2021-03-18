﻿using AutoMapper;
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

        public async Task<UserPassChangeDto> ChangePassword(UserPassChangeDto userPassChangeDto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userPassChangeDto.Email);
                if (user == null)
                {
                    return new UserPassChangeDto { StatusCode = 404, Message = $"Unable to load user with ID ."} ;
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, userPassChangeDto.OldPassword, userPassChangeDto.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    var errorMessage = ""; 
                    foreach (var error in changePasswordResult.Errors)
                    {
                        
                        errorMessage = errorMessage + " / " +  error.Description;
                    }
                    return new UserPassChangeDto { StatusCode = 400, Message = errorMessage } ;
                }
                return new UserPassChangeDto { StatusCode = 200, Message = "Your password has been changed." } ;
            }
            catch (System.Exception ex)
            {
                return new UserPassChangeDto { StatusCode = 500, Message = ex.Message };
            }
        }
        public async Task<(StatusDto, List<UserDto>)> GetAllUsers(string userAdmin)
        {
            try
            {
                var admin = await _userManager.FindByEmailAsync(userAdmin);
                var adminRoles = await _userManager.GetRolesAsync(admin);
                var adminRole = adminRoles.Where(r => r == "Admin").FirstOrDefault();
                if (adminRole != "Admin")
                {
                    return (new StatusDto { StatusCode = 404, Message = "Only Admins can get the useres list"} , null);
                }
                var users = _userManager.Users.ToList();
                var usersDto = new List<UserDto>();
                foreach(var user in users)
                {   
                    var roles = await _userManager.GetRolesAsync(user);
                    var isAdmin = false;
                    if (roles.Count > 0)
                        isAdmin = true;
                    usersDto.Add(new UserDto{ Email = user.Email, Admin = isAdmin});
                }
                return ( new StatusDto { StatusCode = 200} , usersDto );
            }
            catch (System.Exception ex)
            {
                return ( new StatusDto { StatusCode = 500, Message = ex.Message } , null );
            }
            return ( new StatusDto { StatusCode = 500} , null );
        }

    }
}
