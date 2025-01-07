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
                .HasKey(e => e.Id);

            modelBuilder.Entity<Address>()
                .HasOne(e => e.Person)
                .WithOne(e=>e.Address)
                .HasForeignKey<Person>(e => e.AddressId);
        }
    }
}
