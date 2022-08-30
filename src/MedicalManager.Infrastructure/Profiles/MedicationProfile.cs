using AutoMapper;
using MedicalManager.Infrastructure.DTO;

namespace MedicalManager.Infrastructure.Profiles;

public class MedicationProfile : Profile
{
	public MedicationProfile()
	{
		CreateMap<Medication, MedicationDto>()
				.ReverseMap();
	}
}
