namespace MedicalManagerTestUnit;

public class MedicationControllerUnitTest
{
    private readonly Mock<IMedicationService> _mockMedicatiuonService;
    private readonly MedicationController _medicationController;
    private readonly MedicationConfigurationControllerUnitTest _configController;

    private MedicationDto medicationDto { get; set; } = default!;
    private List<MedicationDto> listMedicationsDto { get; set; } = new();

    public MedicationControllerUnitTest()
    {
        _mockMedicatiuonService = new Mock<IMedicationService>();

        _configController = new MedicationConfigurationControllerUnitTest(_mockMedicatiuonService);
        _configController.ConfigureMedicationServiceTests(listMedicationsDto);

        _medicationController = new MedicationController(_mockMedicatiuonService.Object);
    }

    [Fact]
    public async Task Medication_GetAllMedication_ReturnsList()
    {
        var resultResponse = await _medicationController.Get();

        // Assert
        Assert.NotNull(resultResponse);

        Assert.IsType<OkObjectResult>(resultResponse.Result);

        Assert.IsType<List<MedicationDto>>(
                                            ((OkObjectResult)resultResponse.Result!)
                                            .Value);
    }

    [Theory]
    [InlineData("Medication 1", 2.0)]
    [InlineData("Medication 2", 12.0)]
    public async Task Medication_CreateNewMedication_ReturnsMedication_Validate_Is_Not_Null_Valite_Type_Created(string name, decimal quantity)
    {
        CreateMedicationTestUnit(Guid.NewGuid(), name, quantity);

        var resultResponse = await _medicationController.Post(medicationDto);

        // Assert
        Assert.NotNull(resultResponse);

        Assert.IsType<CreatedAtActionResult>(resultResponse.Result);

        Assert.IsType<MedicationDto>(
                                     ((CreatedAtActionResult)resultResponse.Result!)
                                            .Value);
    }

    [Theory]
    [InlineData("119ca2b9-a8e4-48f7-1a33-08da8a8a7e94")]
    public async Task Medication_RemoveMedicationById_ReturnsNotContent(Guid id)
    {
        CreateMedicationTestUnit(id, "Medicication 1 to Delete", 12.0m);

        _mockMedicatiuonService
            .Setup(service => service.RemoveMedication(id))
            .ReturnsAsync(true);

        var resultResponse = await _medicationController.Delete(id);

        // Assert
        Assert.NotNull(resultResponse);

        Assert.IsType<NoContentResult>(resultResponse.Result);
    }

    [Theory]
    [InlineData("119ca2b9-a8e4-48f7-1a33-08da8a8a7e94")]
    public async Task Medication_GetMedicationById_ReturnsNotFoundObject(Guid id)
    {
        _mockMedicatiuonService
                .Setup(service => service.GetMedicationById(id))
                .ReturnsAsync(medicationDto)
                .Verifiable();

        var resultResponse = await _medicationController.GetById(id);

        // Assert
        Assert.NotNull(resultResponse);

        Assert.IsType<NotFoundObjectResult>(resultResponse.Result);
    }

    internal void CreateMedicationTestUnit(Guid id, string name, decimal quantity)
    {
        medicationDto = new MedicationDto(id, name, quantity);

        _mockMedicatiuonService
            .Setup(service => service.CreateMedication(It.IsAny<MedicationDto>()))
            .ReturnsAsync(medicationDto)
            .Verifiable();

    }
}