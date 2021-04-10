// RoutersContext.cs controls our connection to the DB with EFCore using our models.
/* DEV NOTE:: I decided to not set up a repository layer between DbContext and the API / Console App, the reason I did this was after inital research it seemed redundant as DbContext itself has the features of a repo, so changes made
    In the repo would have to be made multiple places and with little gain in the extra layer of abstaction. I am still unsure if this is the best method but for now I will proceed with the project without a repo layer.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.DataAccess
{
    public class RoutersContext : DbContext
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
