using Brewery.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brewery.Repository.Data
{
    public class BreweryContext : IdentityDbContext
    {
        public BreweryContext(DbContextOptions<BreweryContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
        public DbSet<BeerType> BeerTypes { get; set; }
        public DbSet<BeerStyle> BeerStyles { get; set; }

    }
}
