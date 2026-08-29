using Microsoft.EntityFrameworkCore;
using evaluacion20262.Models;

namespace evaluacion20262.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<SolicitudServicio> Solicitudes { get; set; }
}
