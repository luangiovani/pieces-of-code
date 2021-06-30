namespace Framework.Database.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<Framework.Database.Context.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Framework.Database.Context.ApplicationDbContext context)
        {
            //context.Database.ExecuteSqlCommand("ALTER TABLE Customer ALTER COLUMN name [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AI");
        }
    }
}
