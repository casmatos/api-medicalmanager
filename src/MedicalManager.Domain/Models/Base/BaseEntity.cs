namespace MedicalManager.Domain.Models.Base;

public class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTime Created { get; set; }
}
