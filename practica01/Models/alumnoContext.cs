using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using practica01.Models;

namespace practica01.Models
{
    public class alumnoContext : DbContext
    {
        public alumnoContext(DbContextOptions<alumnoContext> options):base(options) { }

        public DbSet<Alumno> Alumno { get; set; }
    }
}
