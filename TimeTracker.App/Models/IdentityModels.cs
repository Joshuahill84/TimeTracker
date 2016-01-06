using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TimeTracker.App.Controllers;

namespace TimeTracker.App.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
        }

        public virtual ICollection<Team> OwnerOf { get; set; } = new Collection<Team>();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is IEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = HttpContext.Current?.User?.Identity.Name?? "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IEntity)entity.Entity).CreatedOn = DateTime.Now;
                    ((IEntity)entity.Entity).CreatedBy = currentUsername;
                }

                ((IEntity)entity.Entity).ModifiedOn = DateTime.Now;
                ((IEntity)entity.Entity).ModifiedBy = currentUsername;
            }

            return base.SaveChanges();
        }

        public System.Data.Entity.DbSet<TimeTracker.App.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<TimeTracker.App.Models.Team> Teams { get; set; }

        public System.Data.Entity.DbSet<TimeTracker.App.Models.Shift> Shifts { get; set; }

        public System.Data.Entity.DbSet<TimeTracker.App.Models.PayCycle> PayCycles { get; set; }

        public System.Data.Entity.DbSet<TimeTracker.App.Models.Position> Positions { get; set; }
    }
}