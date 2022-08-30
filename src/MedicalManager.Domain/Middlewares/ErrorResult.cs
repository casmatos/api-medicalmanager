namespace MedicalManager.Domain.Middlewares;

public class ErrorResult
{
    public string Error { get; set; } = default!;
    public int StatusCode { get; set; }
}
