using System.ComponentModel.DataAnnotations;

namespace MedicalManager.Infrastructure.DTO;

public record MedicationDto(Guid Id, [Required, MinLength(5), MaxLength(500)]string Name, [Range(1, 99999)]decimal Quantity);
