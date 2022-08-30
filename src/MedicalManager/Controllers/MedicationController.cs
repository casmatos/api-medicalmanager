namespace MedicalManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicationController : ControllerBase
{
	private readonly IMedicationService _medicationService;

	public MedicationController(IMedicationService medicationService)
	{
		_medicationService = medicationService;
	}

	[HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MedicationDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<MedicationDto>>> Get()
	{
		var medications = await _medicationService.GetMedications();

		if (medications is null)
		{
			return NotFound();
		}

		return Ok(medications);
	}

    [HttpGet("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<MedicationDto>>> GetById(Guid id)
    {
        var medications = await _medicationService.GetMedicationById(id);

        if (medications is null)
        {
            return Return_NotFound();
        }

        return Ok(medications);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicationDto>> Post([FromBody] MedicationDto medicationDto)
	{
        ArgumentNullException.ThrowIfNull(medicationDto);

        try
        {
            var newMedicationDto = await _medicationService.CreateMedication(medicationDto);

            if (newMedicationDto is null)
            {
                return BadRequest(new ErrorResult
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Error = "Can't create Medication/Duplication."
                });
            }

            return CreatedAtAction(nameof(GetById), new { id = newMedicationDto.Id }, newMedicationDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorResult
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Error = ex.Message
            });
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicationDto>> Delete(Guid id)
    {
        var isDeleted = await _medicationService.RemoveMedication(id);

        if (!isDeleted)
        {
            return Return_NotFound();
        }

        return NoContent();
    }

    internal NotFoundObjectResult Return_NotFound() =>
        NotFound(new ErrorResult
        {
            StatusCode = StatusCodes.Status404NotFound,
            Error = "Medication doesn't exist!"
        });
}
