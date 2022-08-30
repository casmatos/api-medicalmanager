using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalManager.Infrastructure.Context.TypeConfiguration;

internal class MeducationTypeConfiguration : IEntityTypeConfiguration<Medication>
{
    public void Configure(EntityTypeBuilder<Medication> builder)
    {
        builder.Property(property => property.Name)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(property => property.Quantity)
            .HasPrecision(5, 2)
            .IsRequired();
    }
}
