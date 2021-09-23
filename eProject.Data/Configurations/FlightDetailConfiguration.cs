using eProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eProject.Data.Configurations
{
    public class FlightDetailConfiguration : IEntityTypeConfiguration<FlightDetail>
    {
        public void Configure(EntityTypeBuilder<FlightDetail> builder)
        {
            builder.ToTable("FlightDetails");

            builder.Property(e => e.FlightDetailId)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.FlightId)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.HasOne<Airport>(d => d.AirportGoNavigation)
                .WithMany(p => p.FlightDetailsGo)
                .HasForeignKey(d => d.AirportGoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlightDetail_CityGo");

            builder.HasOne(d => d.DestinationAirportNavigation)
                .WithMany(p => p.FlightDetailDestination)
                .HasForeignKey(d => d.DestinationAirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FlightDetail_DestinationCity");
      
        }
    }
}
