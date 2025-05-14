namespace _10717proiect.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_10717proiect.BusinessLogic.Core.DbDataContext.EventContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "_10717proiect.BusinessLogic.Core.DbDataContext.EventContext";
        }

        protected override void Seed(_10717proiect.BusinessLogic.Core.DbDataContext.EventContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
