using Microsoft.EntityFrameworkCore;

namespace ToDoAPI.Models
{

    public class PoliceAppDbContext : DbContext
    {

        public PoliceAppDbContext(DbContextOptions<PoliceAppDbContext> options) : base(options)
        {

        }

        public DbSet<Person> Person { get; set; } = null!;
        public DbSet<Casco> Casco { get; set; } = null!;
        public DbSet<Health> Health { get; set; } = null!;
        public DbSet<Dask> Dask { get; set; } = null!;
        public DbSet<Policys> Policys { get; set; } = null!;
        public DbSet<Traffic> Traffic { get; set; } = null!;
        public DbSet<Product> Product { get; set; } = null!;


    }
}
