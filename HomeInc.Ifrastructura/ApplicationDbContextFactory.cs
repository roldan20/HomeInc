using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInc.Ifrastructura
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        // Método necesario para EF Core en tiempo de diseño
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Configura el proveedor de base de datos que desees (por ejemplo, en memoria, SQL Server, etc.)
            optionsBuilder.UseSqlServer("Data Source=ROLDAN;Initial Catalog=HomeInc; Integrated Security = True;TrustServerCertificate=True"); // Aquí puedes usar cualquier proveedor, como UseSqlServer

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
