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
       public DbSet<Contract> Contracts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<HouseItem> HouseItems { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<ApartmentWeb> ApartmentWebs { get; set; }
    }
}