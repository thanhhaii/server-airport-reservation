using eProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eProject.Data.Configurations
{
    public class AirlineConfiguration : IEntityTypeConfiguration<Airline>
    {
        public void Configure(EntityTypeBuilder<Airline> builder)
        {
            builder.ToTable("Airlines");

            builder.Property(e => e.AirlineName)
                .HasMaxLength(30)
                .IsUnicode(false);
        }
    }
}
