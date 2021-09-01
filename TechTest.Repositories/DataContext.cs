using Microsoft.EntityFrameworkCore;
using TechTest.Models;

namespace TechTest.Repositories
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }
        public DbSet<Robot> Robots { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
