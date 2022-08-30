using MedicalManager.Domain.Models.Base;

namespace MedicalManager.Domain.Repository.Base;

public interface IRepositoryBase<TRepository> where TRepository : IEntityRoot
{
    Task<TRepository> GetById(Guid id, CancellationToken cancelation = default);
    Task<TRepository> Add(TRepository entity, CancellationToken cancelation = default);
    Task Remove(TRepository record, CancellationToken cancelation = default);
}
