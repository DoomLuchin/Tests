using LogsAllianceApi.Data;
using LogsAllianceApi.Models;
using LogsAllianceApi.Models.ViewModel;
using LogsAllianceApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogsAllianceApi.Services.Services
{
    public class ServiceSearchFilterHistorico : ISearchFilterHistorico
    {
        private readonly ApiContext _ctx;

        public ServiceSearchFilterHistorico(ApiContext context)
        {
            _ctx = context;
        }

        public async Task<List<Historico>> SearchByGuidIdRelacionAndDate(Guid idRelacion, DateTime fecha)
        {
            List<Historico> historicos = new List<Historico>();
            historicos = await _ctx.Historicos
            .Where(h => h.FechaCreacion.Date == fecha.Date && h.IdRelacionGuid == idRelacion)
            .ToListAsync();
            return historicos;
        }

        public async Task<List<Historico>> SearchByStringIdRelacionAndDate(string idRelacion, DateTime fecha)
        {
            List<Historico> historicos = new List<Historico>();
            historicos = await _ctx.Historicos
            .Where(h => h.FechaCreacion.Date == fecha.Date && h.IdRelacionVarchar == idRelacion)
            .ToListAsync();
            return historicos;
        }

        public async Task<List<Historico>> SearchByTypeAndDate(FiltersHistorico filters)
        {
            List<Historico> historicos = new List<Historico>();
            historicos = await _ctx.Historicos
            .Where(h => h.FechaCreacion.Date >= filters.DateStart.Date && h.FechaCreacion.Date <= filters.DateEnd.Date && h.Tipo == filters.Tipo && h.Descripcion == filters.Descripcion && h.Evento == filters.Evento)
            .ToListAsync();
            return historicos;
        }
    }
}
