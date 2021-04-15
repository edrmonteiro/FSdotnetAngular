using AutoMapper;
using Brewery.Domain;
using Brewery.Domain.Dtos;
using Brewery.Repository.Contracts;
using Brewery.Repository.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brewery.Repository.Data
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
        public static async Task CreateInitialBeers(IServiceProvider serviceProvider)
        {
            try
            {
                var _repo = serviceProvider.GetRequiredService<IBreweryRepository>();
                var types = await _repo.GetAllAsync<BeerType>();
                if (types.Count == 0)
                {
                    var beerType = new List<BeerType>
                    {
                        new BeerType
                        {
                             Type = "PALE ALE"
                        },
                    };
                    beerType.ForEach(s => _repo.Add(s));
                    await _repo.SaveChangesAsync();

                    var beerStyle = new List<BeerStyle>
                    {
                        new BeerStyle
                        {
                            Type = beerType[0],
                            Style = "Pale Ale",
                            ABV = "4.5-5.5",
                            IBU = "20-40",
                            InitialDensity = "1.043-1.056",
                            FinalDensity = "1.008-1.016",
                            SRM = "4-11"
                        },
                        new BeerStyle
                        {
                            Type = beerType[0],
                            Style = "American Pale Ale",
                            ABV = "4.5-5.7",
                            IBU = "20-40",
                            InitialDensity = "1.045-1.056",
                            FinalDensity = "1.010-1.015",
                            SRM = "4-11"
                        },
                        new BeerStyle
                        {
                            Type = beerType[0],
                            Style = "India Pale Ale",
                            ABV = "5.1-7.6",
                            IBU = "40-60",
                            InitialDensity = "1.050-1.075",
                            FinalDensity = "1.012-1.018",
                            SRM = "8-14"
                        },
                        new BeerStyle
                        {
                            Type = beerType[0],
                            Style = "American Amber Ale",
                            ABV = "4.5-5.7",
                            IBU = "20-40",
                            InitialDensity = "1.043-1.056",
                            FinalDensity = "1.008-1.016",
                            SRM = "11-18"
                        },
                    };
                    beerStyle.ForEach(s => _repo.Add(s));
                    await _repo.SaveChangesAsync();
                }

            }
            catch (System.Exception ex)
            {

            }
        }




    }
}