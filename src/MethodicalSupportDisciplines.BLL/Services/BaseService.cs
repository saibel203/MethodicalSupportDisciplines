using MethodicalSupportDisciplines.Data.Interfaces;

namespace MethodicalSupportDisciplines.BLL.Services;

public abstract class BaseService<TRepository> 
    where TRepository : IRepositoryBase
{
    protected readonly TRepository _repository;

    protected BaseService(TRepository repository)
    {
        _repository = repository;
    }
}