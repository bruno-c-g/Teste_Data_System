using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TesteDataSystem.Application.DTOs;
using TesteDataSystem.Application.Interfaces;
using TesteDataSystem.Domain.Enums;
using TesteDataSystem.Domain.Pagination;
using TesteDataSystem.Presentation.Extensions;
using TesteDataSystem.Presentation.Models;

namespace TesteDataSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataBaseController : Controller
    {
        private readonly IDataBaseService _dataBaseService;

        public DataBaseController(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(DataBaseDTO dataBaseDTO)
        {
            DataBaseDTO dataBaseDTOCreated = await _dataBaseService.Create(dataBaseDTO);

            if (dataBaseDTOCreated == null)
                return BadRequest("Ocorreu um erro ao incluir os dados.");

            return Ok("Dados incluídos com sucesso!");
        }

        [HttpPut]
        public async Task<ActionResult> Update(DataBaseDTO dataBaseDTO)
        {
            DataBaseDTO dataBaseDTOModify = await _dataBaseService.Update(dataBaseDTO);

            if (dataBaseDTOModify == null)
                return BadRequest("Ocorreu um erro ao alterar os dados.");

            return Ok("Dados alterados com sucesso!");
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            DataBaseDTO dataBaseDTODeleted = await _dataBaseService.Delete(id);

            if (dataBaseDTODeleted == null)
                return BadRequest("Ocorreu um erro ao excluir os dados.");

            return Ok("Dados excluídos com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Select(int id)
        {
            DataBaseDTO dataBaseDTOSelected = await _dataBaseService.Select(id);

            if (dataBaseDTOSelected == null)
                return NotFound("Dados não encontrados.");

            return Ok(dataBaseDTOSelected);
        }

        [HttpGet("all")]
        public async Task<ActionResult> SelectAll([FromQuery]PaginationParams paginationParams)
        {
            PagedList<DataBaseDTO> dataBasesDTO = await _dataBaseService.SelectAll(paginationParams.PageNumber, paginationParams.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(dataBasesDTO.CurrentPage,
                                                              dataBasesDTO.PageSize,
                                                              dataBasesDTO.TotalCount,
                                                              dataBasesDTO.TotalPages));

            return Ok(dataBasesDTO);
        }

        [HttpGet("by-status")]
        public async Task<ActionResult> SelectAllByStatus([FromQuery] PaginationParams paginationParams, int status)
        {
            StatusEnum _status = (StatusEnum)status;
            PagedList<DataBaseDTO> dataBasesDTO = await _dataBaseService.SelectAllByStatus(paginationParams.PageNumber, paginationParams.PageSize, _status);

            Response.AddPaginationHeader(new PaginationHeader(dataBasesDTO.CurrentPage,
                                                              dataBasesDTO.PageSize,
                                                              dataBasesDTO.TotalCount,
                                                              dataBasesDTO.TotalPages));

            return Ok(dataBasesDTO);
        }
    }
}
