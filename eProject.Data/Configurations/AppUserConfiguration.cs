using eProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eProject.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            
            builder.HasIndex(e => e.Email, "UQ__User__A9D105341DEBB978")
                .IsUnique();
          
            builder.Property(e => e.Address).HasMaxLength(250);

            builder.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.FirstName).HasMaxLength(30);

            builder.Property(e => e.LastName).HasMaxLength(30);

    
        }
    }
}
