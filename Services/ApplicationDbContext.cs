
using Gestion_Devis_Facture.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Devis_Facture.Services
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
		{ 
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var admin = new IdentityRole("admin");
			admin.NormalizedName = "admin";

			var client = new IdentityRole("client");
			client.NormalizedName = "client";


			builder.Entity<IdentityRole>().HasData(admin,client);

		}
        public DbSet<Agent> Ags { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceItem> InvoiceItem { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<InvoiceItem> QuoteItems { get; set; }

    }

}
