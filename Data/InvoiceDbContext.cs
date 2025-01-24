using Gestion_Devis_Facture.Model;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Devis_Facture.Data
{


    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Agent> Ags { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceItem> InvoiceItem { get; set; }

      

         
              
            


        
    }
}

