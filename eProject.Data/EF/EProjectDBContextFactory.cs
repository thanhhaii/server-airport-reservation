using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace eProject.Data.EF
{
    public class EProjectDBContextFactory : IDesignTimeDbContextFactory<EProjectDBContext>
    {
        public EProjectDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("local");
            var optionBuilder = new DbContextOptionsBuilder<EProjectDBContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new EProjectDBContext(optionBuilder.Options);
        }
    }
}
