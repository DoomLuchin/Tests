using LogsAllianceApi.Models;
using LogsAllianceApi.Models.ViewModel;
using LogsAllianceApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogsAllianceApi.Controllers
{
    //[Authorize(Policy = "AuthorizationToken")]
    [ApiController]
    public class HistoricoController : BaseApiController
    {
        private readonly IRepositoryHistorico _repositoryService;
        private readonly ISearchFilterHistorico _searchFilterService;

        public HistoricoController(IRepositoryHistorico historicoService, ISearchFilterHistorico searchFilterService)
        {
            _repositoryService = historicoService;
            _searchFilterService = searchFilterService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Historico>> GetHistoricoById(Guid id)
        {
            Historico historico = await _repositoryService.GetHistoricoById(id);

            if (historico == null)
            {
                return NotFound();
            }

            return Ok(historico);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Historico>> CreateHistorico(Historico historico)
        {
            Historico Historico = await _repositoryService.CreateHistorico(historico);
            return CreatedAtAction(nameof(GetHistoricoById), new { id = Historico.Id }, Historico);
        }

        #region Searching by filters
        [HttpGet]
        [Route("search/idRelacionGuid/{idRelacion}/date/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Historico>> SearchByGuidIdRelacionAndDate(Guid idRelacion, DateTime date)
        {
            List<Historico> historicos = await _searchFilterService.SearchByGuidIdRelacionAndDate(idRelacion, date);
            return Ok(historicos);
        }


        [HttpGet]
        [Route("search/idRelacionString/{idRelacion}/date/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Historico>> SearchByStringIdRelacionAndDate(string idRelacion, DateTime date)
        {
            List<Historico> historicos = await _searchFilterService.SearchByStringIdRelacionAndDate(idRelacion, date);
            return Ok(historicos);
        }


        [HttpGet]
        [Route("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Historico>> SearchByTypeAndDate([FromBody] FiltersHistorico filters)
        {
            List<Historico> historicos = await _searchFilterService.SearchByTypeAndDate(filters);
            return Ok(historicos);
        }

        #endregion
    }
}
