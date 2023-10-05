using LogsAllianceApi.Data;
using LogsAllianceApi.Models;
using LogsAllianceApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogsAllianceApi.Services.Services
{
    public class ServiceRepositoryHistorico : IRepositoryHistorico
    {
        private readonly ApiContext _ctx;

        public ServiceRepositoryHistorico(ApiContext context)
        {
            _ctx = context;
        }

        public async Task<Historico> GetHistoricoById(Guid id)
        {
            Historico historicos = await _ctx.Historicos.FindAsync(id);
            return historicos;
        }

        public async Task<Historico> CreateHistorico(Historico historico)
        {
            _ctx.Historicos.Add(historico);
            await _ctx.SaveChangesAsync();
            return historico;
        }

        public async Task UpdateHistorico(Historico historico)
        {
            _ctx.Entry(historico).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteHistorico(Guid id)
        {
            Historico historico = await _ctx.Historicos.FindAsync(id);
            if (historico == null)
            {
                throw new ArgumentException("Historico not found");
            }

            _ctx.Historicos.Remove(historico);
            await _ctx.SaveChangesAsync();
        }
    }
}
