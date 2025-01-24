using System.Collections.Generic;
using System.Threading.Tasks;
using Gestion_Devis_Facture.Model;

namespace Gestion_Devis_Facture.Services
{
    public interface IQuoteService
    {
        Task<List<Agent>> GetAgentsAsync();
        Task<Agent> GetAgentByIdAsync(int id);
        Task AddAgentAsync(Agent agent);
        Task UpdateAgentAsync(Agent agent);
        Task<Quote> CreateQuoteAsync(Quote quote);
    }
}
