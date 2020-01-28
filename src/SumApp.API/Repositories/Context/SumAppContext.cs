using Microsoft.EntityFrameworkCore;
using SumApp.API.Models;

namespace SumApp.API.Repositories.Context
{
    public class SumAppContext : DbContext
    {
        public SumAppContext() { }

        public DbSet<Team> Teams { get; set; }

        public SumAppContext(DbContextOptions<SumAppContext> options)
            : base(options) { }

    }
}
