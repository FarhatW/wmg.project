using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using wmg.DataAccess.dbContext;

namespace wmg.DataAccess.DbContextFactory   
{
    class WmgDbContextFactory : IDesignTimeDbContextFactory<WmgDbContext>
    {
        readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();


        public WmgDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<WmgDbContext>();
            builder.UseSqlServer(_configuration.GetConnectionString("wmgDb"), b => b.MigrationsAssembly("wmg.DataAccess"));

            return new WmgDbContext(builder.Options);

        }
    }
}
