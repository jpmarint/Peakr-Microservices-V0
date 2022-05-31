using Microsoft.EntityFrameworkCore;
using SolicitudesAPI.Models;

namespace SolicitudesAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)

        {
            //configuraciones a traves de Entity Framework
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<QuoteRequest>()
           //creo un nuevo objeto que representa la llave primaria de la tabla AutorLibro
           .HasKey(al => new { al.QuoteId, al.RequestId });

            base.OnModelCreating(modelBuilder);

        }


        public DbSet<Address> Address { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Quote> Quotes { get; set; }

        public DbSet<QuoteRequest> QuoteRequest { get; set; }

    }
    

}
