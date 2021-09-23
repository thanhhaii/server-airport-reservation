using eProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eProject.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.Property(e => e.TicketId)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.BookingDate).HasColumnType("datetime");

            builder.Property(e => e.FlightId)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.Note).HasMaxLength(250);

            builder.Property(e => e.PaymentMethod)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.UserId)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.HasOne(d => d.Flight)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK_Ticket_Flight");

            builder.HasOne(d => d.AppUser)
                .WithMany(p => p.Tickets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Ticket_User");
        }
    }
}
