using Houzing.Data.Houses;
using Houzing.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Houzing.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
       public DbSet<Owner> Owners { get; set; }
       public DbSet<HouseItem> HouseItems { get; set; }
       public DbSet<Apartment> Apartments { get; set; }
       public DbSet<CreateAparmentVM> CreateAparmentVM { get; set; } = default!;
       //public DbSet<Image> Images { get; set; }
    }
}