using eProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eProject.Data.Configurations
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder.ToTable("Flights");

            builder.Property(e => e.FlightId)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.ArrivalTime).HasColumnType("datetime");

            builder.Property(e => e.FlightTime).HasColumnType("datetime");


            builder.HasOne(d => d.Plane)
                .WithMany(p => p.Flights)
                .HasForeignKey(d => d.PlaneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Flight_Plane");

            builder.HasOne<FlightDetail>(f => f.FlightDetails)
                .WithOne(f => f.Flight)
                .HasForeignKey<FlightDetail>(f => f.FlightId);
        }   
    }
}
