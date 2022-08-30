using MedicalManager.Domain.Models.Base;

namespace MedicalManager.Domain.Models;

public class Medication : BaseEntity, IEntityRoot
{
	public Medication(string name, decimal quantity)
	{
		Name = name;
		Quantity = quantity;
	}

    public string Name { get; private set; }
	public decimal Quantity { get; set; }
}
