using Houzing.Data.Houses;
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
       public DbSet<HouseImg> HouseImgs { get; set; }
    }
}