using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Esp32ImageApi.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Make sure to adjust this connection string to your needs
            var connectionString = "Server=DESKTOP-1FNINHN\\MSSQLSERVER2;Database=Esp32ImagesDb;Trusted_Connection=True;TrustServerCertificate=True;";  // Replace with your actual connection string

            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
