using Esp32ImageApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Esp32ImageApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ImageModel> Images { get; set; }

    }
}
