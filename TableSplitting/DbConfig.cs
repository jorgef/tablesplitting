using System.Data.Entity.Migrations;

namespace TableSplitting
{
    public class DbConfig : DbMigrationsConfiguration<Context>
    {
        public DbConfig()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
