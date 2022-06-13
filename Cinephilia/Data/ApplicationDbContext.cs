using Cinephilia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinephilia.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Global DbSet For each model class that can perform CRUD operations.
        public DbSet<User> Users { get; set; }

        public DbSet<Entertainment> Entertainments { get; set; }
        public DbSet<BrowseBy> BrowseBies { get; set; }
    }
}