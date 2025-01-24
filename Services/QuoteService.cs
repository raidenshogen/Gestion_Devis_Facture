using System.Threading.Tasks;
using Gestion_Devis_Facture.Data;
using Gestion_Devis_Facture.Model;
using Microsoft.EntityFrameworkCore;

namespace Gestion_Devis_Facture.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly QuoteDbContext _context;
        private readonly InvoiceDbContext _invoiceContext; // Add this line

        public QuoteService(QuoteDbContext context, InvoiceDbContext invoiceContext)
        {
            _context = context;
            _invoiceContext = invoiceContext; // Fix the error here
        }

        public async Task<List<Agent>> GetAgentsAsync()
        {
            return await _invoiceContext.Ags.ToListAsync(); // Fix the error here
        }

        public async Task<Agent> GetAgentByIdAsync(int id)
        {
            return await _invoiceContext.Ags.FindAsync(id);
        }

        public async Task AddAgentAsync(Agent agent)
        {
            _invoiceContext.Ags.Add(agent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAgentAsync(Agent agent)
        {
            _invoiceContext.Ags.Update(agent);
            await _context.SaveChangesAsync();
        }

        public async Task<Quote> CreateQuoteAsync(Quote quote)
        {
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();
            return quote;
        }
    }
}