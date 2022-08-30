using MedicalManager.Infrastructure.DTO;

namespace MedicalManager.Infrastructure.Service.Model.Base;

public interface IMedicationService
{
    Task<List<MedicationDto>> GetMedications();
    Task<MedicationDto> GetMedicationById(Guid id);
    Task<MedicationDto> CreateMedication(MedicationDto medicationDto);
    Task<bool> RemoveMedication(Guid id);
}
