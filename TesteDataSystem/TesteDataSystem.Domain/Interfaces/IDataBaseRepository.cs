using System.Threading.Tasks;
using TesteDataSystem.Domain.Entities;
using TesteDataSystem.Domain.Enums;
using TesteDataSystem.Domain.Pagination;

namespace TesteDataSystem.Domain.Interfaces
{
    public interface IDataBaseRepository
    {
        Task<DataBase> Create(DataBase dataBase);
        Task<DataBase> Update(DataBase dataBase);
        Task<DataBase> Delete(int id);
        Task<DataBase> Select(int id);
        Task<PagedList<DataBase>> SelectAll(int pageNumber, int pageSize);
        Task<PagedList<DataBase>> SelectAllByStatus(int pageNumber, int pageSize, StatusEnum status);
    }
}
