using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.ApplicationDb
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

        public DbSet<Person> Person { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Address>()
               .HasOne(a => a.Person)  
               .WithOne(p => p.Address)  
               .HasForeignKey<Person>(p => p.AddressId);

        }
    }
}
