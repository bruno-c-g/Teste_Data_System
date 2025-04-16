using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TesteDataSystem.Domain.Entities;
using TesteDataSystem.Domain.Enums;
using TesteDataSystem.Domain.Interfaces;
using TesteDataSystem.Domain.Pagination;
using TesteDataSystem.Infrastructure.Context;
using TesteDataSystem.Infrastructure.Helpers;

namespace TesteDataSystem.Infrastructure.Repositories
{
    public class DataBaseRepository : IDataBaseRepository
    {
        private readonly ApplicationDbContext _context;

        public DataBaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataBase> Create(DataBase dataBase)
        {
            _context.DataBase.Add(dataBase);
            await _context.SaveChangesAsync();
            return dataBase;
        }

        public async Task<DataBase> Delete(int id)
        {
            DataBase dataBase = await _context.DataBase.FindAsync(id);

            if (dataBase != null)
            {
                _context.DataBase.Remove(dataBase);
                await _context.SaveChangesAsync();
                return dataBase;
            }
            return null;
        }

        public async Task<DataBase> Select(int id)
        {
            return await _context.DataBase.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DataBase> Update(DataBase dataBase)
        {
            _context.DataBase.Update(dataBase);
            await _context.SaveChangesAsync();
            return dataBase;
        }

        public async Task<PagedList<DataBase>> SelectAll(int pageNumber, int pageSize)
        {
            IQueryable<DataBase> query = _context.DataBase.AsQueryable();
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<PagedList<DataBase>> SelectAllByStatus(int pageNumber, int pageSize, StatusEnum status)
        {
            IQueryable<DataBase> query = _context.DataBase.AsQueryable().Where(x => x.Status == status);
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }
    }
}
