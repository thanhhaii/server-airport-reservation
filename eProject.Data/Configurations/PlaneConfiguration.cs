using eProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eProject.Data.Configurations
{
    public class PlaneConfiguration : IEntityTypeConfiguration<Plane>
    {
        public void Configure(EntityTypeBuilder<Plane> builder)
        {
            builder.ToTable("Planes");

            builder.Property(e => e.PlaneName)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.HasOne(d => d.Airline)
                .WithMany(p => p.Planes)
                .HasForeignKey(d => d.AirlineId)
                .HasConstraintName("FK_Plane_Airline");
        }
    }
}
