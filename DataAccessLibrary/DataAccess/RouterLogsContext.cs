using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.DataAccess
{
    class RouterLogsContext : DbContext
    {
        // Connection String for our database, we are using "localdb" native MSSQL driver 
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=RoutersDB;Trusted_Connection=True;";

        // OnConfiguring method allows us to configure the DBContext. EF Core calls this method when it instantiates the context for the first time.
        // The OnConfiguring method gets the instance of the DbContextOptionsBuilder as its argument. The DbContextOptionsBuilder provides API to configure the DBContext.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<RouterLog> RouterLogs { get; set; }
    }
}
