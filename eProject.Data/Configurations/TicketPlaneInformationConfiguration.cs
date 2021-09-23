using eProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eProject.Data.Configurations
{
    public class TicketPlaneInformationConfiguration : IEntityTypeConfiguration<TicketPlaneInformation>
    {
        public void Configure(EntityTypeBuilder<TicketPlaneInformation> builder)
        {
            builder.ToTable("TicketPlaneInformations");

            builder.Property(e => e.FlightId)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.PriceTicketBusiness).HasColumnType("money");

            builder.Property(e => e.PriceTicketEconomy).HasColumnType("money");

            builder.Property(e => e.PriceTicketFirstClass).HasColumnType("money");

            builder.Property(e => e.PriceTicketPremiumEconomy).HasColumnType("money");
        }
    }
}
