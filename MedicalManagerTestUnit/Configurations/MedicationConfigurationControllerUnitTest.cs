namespace MedicalManagerTestUnit.Configurations;

public class MedicationConfigurationControllerUnitTest
{
    private readonly Mock<IMedicationService> _mockMedicatiuonService;

    public MedicationConfigurationControllerUnitTest(Mock<IMedicationService> mockMedicatiuonService)
    {
        _mockMedicatiuonService = mockMedicatiuonService;
    }

    public void ConfigureMedicationServiceTests(List<MedicationDto> listMedicationsDto)
    {
        listMedicationsDto.Add(new MedicationDto(Guid.NewGuid(), "Medication 1", 2.0m));
        listMedicationsDto.Add(new MedicationDto(Guid.NewGuid(), "Medication 2", 21.0m));

        _mockMedicatiuonService
                .Setup(service => service.GetMedications())
                .ReturnsAsync(listMedicationsDto)
                .Verifiable();

    }
}