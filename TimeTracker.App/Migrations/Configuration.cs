using TimeTracker.App.Models;

namespace TimeTracker.App.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TimeTracker.App.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TimeTracker.App.Models.ApplicationDbContext";
        }

        protected override void Seed(TimeTracker.App.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.PayCycles.AddOrUpdate(pc => pc.Name,
                new PayCycle { Name = "Weekly"},
                new PayCycle { Name = "BiWeekly"},
                new PayCycle { Name = "Monthly"},
                new PayCycle { Name = "Semi Monthly"},
                new PayCycle { Name = "Annual"}
                );

            context.Shifts.AddOrUpdate(pc => pc.Name,
              new Shift() { Name = "Night Shift" },
              new Shift { Name = "Day Shift" },
              new Shift { Name = "Morning" },
              new Shift { Name = "Evening" },
              new Shift { Name = "Part Time" }
              );
        }
    }
}
