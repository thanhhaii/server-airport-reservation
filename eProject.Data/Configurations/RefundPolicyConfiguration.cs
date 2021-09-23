using eProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eProject.Data.Configurations
{
    public class RefundPolicyConfiguration : IEntityTypeConfiguration<RefundPolicy>
    {
        public void Configure(EntityTypeBuilder<RefundPolicy> builder)
        {
            builder.ToTable("RefundPolicies");
        }
    }
}
