using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TimeTracker.App.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual Team OwnerOf { get; set; }

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
            : base("DefaultConnection", false)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<TimeEntry> TimeEntries { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Shift> Shifts { get; set; }

        public DbSet<PayCycle> PayCycles { get; set; }

        public DbSet<Position> Positions { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            var entities =
                ChangeTracker.Entries()
                    .Where(x => x.Entity is IEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = HttpContext.Current?.User?.Identity.Name ?? "Anonymous";

            foreach (var entity in entities)
            {
                var e = ((IEntity)entity.Entity);
                if (entity.State == EntityState.Added)
                {
                    e.CreatedOn = DateTime.Now;
                    e.CreatedBy = currentUsername;
                }

                e.ModifiedOn = DateTime.Now;
                e.ModifiedBy = currentUsername;
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasOptional(x => x.OwnerOf).WithOptionalPrincipal(x => x.Owner);
        }
    }
}