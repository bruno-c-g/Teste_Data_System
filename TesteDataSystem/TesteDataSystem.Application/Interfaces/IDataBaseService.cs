using System.Threading.Tasks;
using TesteDataSystem.Application.DTOs;
using TesteDataSystem.Domain.Enums;
using TesteDataSystem.Domain.Pagination;

namespace TesteDataSystem.Application.Interfaces
{
    public interface IDataBaseService
    {
        Task<DataBaseDTO> Create(DataBaseDTO dataBaseDTO);
        Task<DataBaseDTO> Update(DataBaseDTO dataBaseDTO);
        Task<DataBaseDTO> Delete(int id);
        Task<DataBaseDTO> Select(int id);
        Task<PagedList<DataBaseDTO>> SelectAll(int pageNumber, int pageSize);
        Task<PagedList<DataBaseDTO>> SelectAllByStatus(int pageNumber, int pageSize, StatusEnum status);
    }
}
