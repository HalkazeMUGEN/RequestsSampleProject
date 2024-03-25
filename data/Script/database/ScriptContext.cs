using Microsoft.EntityFrameworkCore;

namespace RequestsSampleProject.data.Script.database
{
    public class ScriptContext : DbContext
    {
        private static readonly string _SCRIPT_DB_FILENAME = "scripts.db";

        public DbSet<model.Script> Scripts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=" + _SCRIPT_DB_FILENAME);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
