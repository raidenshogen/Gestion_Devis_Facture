using Gestion_Devis_Facture.Model;

namespace Gestion_Devis_Facture.Services
{
   
        public interface IInvoiceService
        {
        Task<List<Agent>> GetAgentsAsync();
        Task<Agent> GetAgentByIdAsync(int id); // For Edit functionality
        Task AddAgentAsync(Agent agent);       // Add functionality
        Task UpdateAgentAsync(Agent agent);
        Task<Invoice> CreateInvoiceAsync(Invoice invoice);
    }
    }

