using MedicalManager.Domain.Repository;
using MedicalManager.Infrastructure.Context;

namespace MedicalManager.Infrastructure.Repository;

public class MedicationRepository : IMedicationRepository
{
    private readonly ApplicationDatabaseContext _context;

    public MedicationRepository(ApplicationDatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<Medication>> GetAllMedication(CancellationToken cancelation = default)
    {
        return await _context.Medications
                                .ToListAsync(cancelation);
    }

    public async Task<Medication> GetById(Guid id, CancellationToken cancelation = default)
    {
        return await _context.Medications
                                .FindAsync(new object[]{ id }, cancellationToken: cancelation);
    }

    public async Task<Medication> Add(Medication entity, CancellationToken cancelation = default)
    {
        entity.Created = DateTime.UtcNow;

        if (!await _context.Medications.AnyAsync(existe => existe.Name.Equals(entity.Name)))
        {

            _context.Add(entity);

            await _context.SaveChangesAsync(cancelation);

            return entity;
        }

        return null;
    }

    public async Task Remove(Medication record, CancellationToken cancelation = default)
    {
        _context.Medications.Remove(record);

        await _context.SaveChangesAsync(cancelation);
    }
}
