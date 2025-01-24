using Gestion_Devis_Facture.Model;

namespace Gestion_Devis_Facture.Services
{
    public interface IAgentService
    {
        Task<IEnumerable<Agent>> GetAllAgentsAsync();
        Task<Agent> GetAgentByIdAsync(Guid id);
        Task<Agent> CreateAgentAsync(Agent agent);
        Task<Agent> UpdateAgentAsync(Agent agent);
        Task DeleteAgentAsync(Guid id);
    }
}
