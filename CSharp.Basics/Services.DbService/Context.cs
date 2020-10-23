using Services.DbService.Configurations;
using Services.DbService.Migrations;
using System.Data.Entity;

namespace Services.DbService
{
    public class Context : DbContext
    {
        public Context() : base("Server=(LocalDb)\\MSSQLLocalDB; Initial Catalog=CSharpBasics; Integrated Security=true")
        {
            Configuration.LazyLoadingEnabled = false;

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Configuration>(true));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }

}
