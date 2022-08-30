using MedicalManager.Domain.Models;
using MedicalManager.Domain.Repository.Base;

namespace MedicalManager.Domain.Repository;

public interface IMedicationRepository : IRepositoryBase<Medication>, IRepositoryEntity
{
    Task<List<Medication>> GetAllMedication(CancellationToken cancelation = default);
}
