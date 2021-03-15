using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brewery.MVC.Data
{
    public class BreweryContext : IdentityDbContext
    {
        public BreweryContext(DbContextOptions<BreweryContext> options)
            : base(options)
        {
        }
    }
}
