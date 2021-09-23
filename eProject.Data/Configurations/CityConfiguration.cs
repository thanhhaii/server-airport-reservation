using eProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eProject.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");

            builder.Property(e => e.CityName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("cityName");

            builder.Property(e => e.CountryName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
