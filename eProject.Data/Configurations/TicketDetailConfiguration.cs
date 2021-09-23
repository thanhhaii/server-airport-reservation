using eProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eProject.Data.Configurations
{
    public class TicketDetailConfiguration : IEntityTypeConfiguration<TicketDetail>
    {
        public void Configure(EntityTypeBuilder<TicketDetail> builder)
        {
            builder.ToTable("TicketDetails");

            builder.Property(e => e.FirstName).HasMaxLength(30);

            builder.Property(e => e.LastName).HasMaxLength(30);


            builder.Property(e => e.TicketId)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.TicketPrice).HasColumnType("money");

            builder.HasOne(d => d.Ticket)
                .WithMany(p => p.TicketDetails)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("FK_TicketDetail_Ticket");
        }
    }
}
