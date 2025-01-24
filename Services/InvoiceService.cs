using Gestion_Devis_Facture.Data;
using Gestion_Devis_Facture.Model;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Devis_Facture.Services
{
    // Services/InvoiceService.cs


    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceDbContext _context;

        public InvoiceService(InvoiceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Agent>> GetAgentsAsync()
        {
            return await _context.Ags.ToListAsync();
        }
        
        public async Task<Agent> GetAgentByIdAsync(int id)
        {
            return await _context.Ags.FindAsync(id);
        }

        public async Task AddAgentAsync(Agent agent)
        {
            _context.Ags.Add(agent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAgentAsync(Agent agent)
        {
            _context.Ags.Update(agent);
            await _context.SaveChangesAsync();
        }

        public Task<Invoice> CreateInvoiceAsync(Invoice invoice)
        {
            throw new NotImplementedException();
        }
    }
}
