using AutoMapper;
using MedicalManager.Domain.Repository;
using MedicalManager.Infrastructure.DTO;
using MedicalManager.Infrastructure.Service.Model.Base;

namespace MedicalManager.Infrastructure.Service.Model;

public class MedicationService : IMedicationService
{
    private readonly IMedicationRepository _medicationRepository;
    private readonly IMapper _mapper;

    public MedicationService(IMedicationRepository medicationRepository,
                            IMapper mapper)
    {
        _medicationRepository = medicationRepository;
        _mapper = mapper;
    }

    public async Task<MedicationDto> CreateMedication(MedicationDto medicationDto)
    {
        ArgumentNullException.ThrowIfNull(medicationDto);

        if (medicationDto.Id != Guid.Empty)
        {
            throw new ArgumentException("Can't create a medication with id associated!");
        }

        var medication = _mapper.Map<Medication>(medicationDto);

        ArgumentNullException.ThrowIfNull(medication);

        var newMedication = await _medicationRepository.Add(medication);

        if (newMedication is null)
        {
            return null;
        }

        return _mapper.Map<MedicationDto>(newMedication);
    }

    public async Task<MedicationDto> GetMedicationById(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Medication id is not assigned!");
        }

        var medication = await _medicationRepository.GetById(id);

        if (medication is null)
        {
            return null;
        }

        return _mapper.Map<MedicationDto>(medication);
    }

    public async Task<List<MedicationDto>> GetMedications()
    {
        var medications = await _medicationRepository.GetAllMedication();

        ArgumentNullException.ThrowIfNull(medications);

        return _mapper.Map<List<MedicationDto>>(medications);
    }

    public async Task<bool> RemoveMedication(Guid id)
    {
        var medication = await _medicationRepository.GetById(id);

        if (medication is null)
        {
            return false;
        }

        await _medicationRepository.Remove(medication);

        return true;
    }
}
