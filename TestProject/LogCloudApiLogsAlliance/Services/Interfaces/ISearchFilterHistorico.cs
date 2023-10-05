using LogsAllianceApi.Models;
using LogsAllianceApi.Models.ViewModel;

namespace LogsAllianceApi.Services.Interfaces
{
    public interface ISearchFilterHistorico
    {
        Task<List<Historico>> SearchByGuidIdRelacionAndDate(Guid idRelacion, DateTime fecha);
        Task<List<Historico>> SearchByStringIdRelacionAndDate(string idRelacion, DateTime fecha);
        Task<List<Historico>> SearchByTypeAndDate(FiltersHistorico filters);
    }
}
