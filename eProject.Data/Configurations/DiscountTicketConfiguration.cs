using eProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eProject.Data.Configurations
{
    public class DiscountTicketConfiguration : IEntityTypeConfiguration<DiscountTicket>
    {
        public void Configure(EntityTypeBuilder<DiscountTicket> builder)
        {
            builder.ToTable("DiscountTickets");

            builder.Property(e => e.Since).HasColumnType("date");

            builder.Property(e => e.ToDate).HasColumnType("date");

            builder.HasOne(d => d.TicketPlaneInformation)
                .WithMany(p => p.DiscountTickets)
                .HasForeignKey(d => d.TicketPlaneInformationId)
                .HasConstraintName("FK_DiscountTicket_TicketPlaneInformation");
        }
    }
}
