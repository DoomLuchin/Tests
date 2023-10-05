using LogsAllianceApi.Models;

namespace LogsAllianceApi.Services.Interfaces
{
    public interface IRepositoryHistorico
    {
        Task<Historico> GetHistoricoById(Guid id);
        Task<Historico> CreateHistorico(Historico historico);
        Task UpdateHistorico(Historico historico);
        Task DeleteHistorico(Guid id);
    }
}
