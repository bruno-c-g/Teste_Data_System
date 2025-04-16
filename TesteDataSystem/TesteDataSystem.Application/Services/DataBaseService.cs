using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteDataSystem.Application.DTOs;
using TesteDataSystem.Application.Interfaces;
using TesteDataSystem.Domain.Entities;
using TesteDataSystem.Domain.Enums;
using TesteDataSystem.Domain.Interfaces;
using TesteDataSystem.Domain.Pagination;

namespace TesteDataSystem.Application.Services
{
    public class DataBaseService : IDataBaseService
    {
        private readonly IDataBaseRepository _repository;
        private readonly IMapper _mapper;

        public DataBaseService(IDataBaseRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task <DataBaseDTO> Create(DataBaseDTO dataBaseDTO)
        {
            DataBase dataBase = _mapper.Map<DataBase>(dataBaseDTO);
            DataBase dataBaseCreated = await _repository.Create(dataBase);
            return _mapper.Map<DataBaseDTO>(dataBaseCreated);
        }

        public async Task<DataBaseDTO> Delete(int id)
        {
            DataBase dataBaseDeleted= await _repository.Delete(id);
            return _mapper.Map<DataBaseDTO>(dataBaseDeleted);
        }

        public async Task<DataBaseDTO> Select(int id)
        {
            DataBase dataBase = await _repository.Select(id);
            return _mapper.Map<DataBaseDTO>(dataBase);
        }

        public async Task<DataBaseDTO> Update(DataBaseDTO dataBaseDTO)
        {
            DataBase dataBase = _mapper.Map<DataBase>(dataBaseDTO);
            DataBase dataBaseModify = await _repository.Update(dataBase);
            return _mapper.Map<DataBaseDTO>(dataBaseModify);
        }

        public async Task<PagedList<DataBaseDTO>> SelectAll(int pageNumber, int pageSize)
        {
            PagedList<DataBase> dataBases = await _repository.SelectAll(pageNumber, pageSize);
            IEnumerable<DataBaseDTO> dataBasesDTO = _mapper.Map<IEnumerable<DataBaseDTO>>(dataBases);

            return new PagedList<DataBaseDTO>(dataBasesDTO, pageNumber, pageSize, dataBases.TotalCount);
        }

        public async Task<PagedList<DataBaseDTO>> SelectAllByStatus(int pageNumber, int pageSize, StatusEnum status)
        {
            PagedList<DataBase> dataBases = await _repository.SelectAllByStatus(pageNumber, pageSize, status);
            IEnumerable<DataBaseDTO> dataBasesDTO = _mapper.Map<IEnumerable<DataBaseDTO>>(dataBases);

            return new PagedList<DataBaseDTO>(dataBasesDTO, pageNumber, pageSize, dataBases.TotalCount);
        }
    }
}
