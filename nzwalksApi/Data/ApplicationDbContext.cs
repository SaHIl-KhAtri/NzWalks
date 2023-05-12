using Microsoft.EntityFrameworkCore;
using NZWalksApi.Model.Domain;

namespace NZWalksApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Difficulty> Difficulty { get; set; }
        public DbSet<Walk> Walks { get; set; }

    }
}
