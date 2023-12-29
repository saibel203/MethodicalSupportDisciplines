using AutoMapper;
using MethodicalSupportDisciplines.Data.Interfaces;

namespace MethodicalSupportDisciplines.BLL.Services;

public abstract class BaseService<TRepository> 
    where TRepository : IRepositoryBase
{
    protected readonly TRepository _repository;
    protected readonly IMapper _mapper;
    
    protected BaseService(TRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
}