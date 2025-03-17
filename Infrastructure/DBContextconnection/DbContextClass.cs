using Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingMovieCore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
namespace Infrastructure.DBContextconnection
{
    public class DbContextClass:IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public DbContextClass(DbContextOptions<DbContextClass>option):base(option)
        {
            
        }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movies>().HasOne(x => x.Category).WithMany(x => x.Movies).
                HasForeignKey(d => d.CategoryId)
                .HasConstraintName("fk_movie_catId");
            builder.Entity<IdentityUserLogin<int>>()
       .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            builder.Entity<IdentityUserRole<int>>()
                .HasKey(r => new { r.UserId, r.RoleId });

            builder.Entity<IdentityUserToken<int>>()
                .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        }
    }
}
