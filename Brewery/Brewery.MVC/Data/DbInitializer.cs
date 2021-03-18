using AutoMapper;
using Brewery.MVC.Dtos;
using Brewery.MVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brewery.MVC.Data
{
    public static class DataInitializer 
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            try
            {
                var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var _userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var roleExist = await _roleManager.RoleExistsAsync("Admin");
                if (!roleExist)
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                var userExists = await _userManager.FindByEmailAsync("a@a");
                if (userExists == null)
                {
                    var user = new IdentityUser { UserName = "a@a", Email = "a@a" };
                    var result = await _userManager.CreateAsync(user, "123456");
                    
                    if (result.Succeeded)
                    {
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var result1 = await _userManager.ConfirmEmailAsync(user, code);
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                }

            }
            catch (System.Exception ex)
            {
                
            }
        }
    }
}