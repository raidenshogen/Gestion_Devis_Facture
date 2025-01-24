using Gestion_Devis_Facture.Model;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Devis_Facture.Data
{
    public class QuoteDbContext : DbContext
    {
        public QuoteDbContext(DbContextOptions<QuoteDbContext> options) : base(options) { }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<QuoteItem> QuoteItems { get; set; }

        // Reference the existing Ags table
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map the Agents table to the existing Ags table in the databasea
            modelBuilder.Entity<Agent>().ToTable("Ags");

            // Configure decimal properties for Quote
            modelBuilder.Entity<Quote>()
                .Property(q => q.TotalAmount)
                .HasPrecision(18, 2);

            // Configure decimal properties for QuoteItem
            modelBuilder.Entity<QuoteItem>()
                .Property(qi => qi.UnitPrice)
                .HasPrecision(18, 2);
        }
    }
}