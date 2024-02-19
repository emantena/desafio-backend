using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Repository.Repositories.Base
{
    public class DeliveryAppContext : DbContext
    {
        public DeliveryAppContext(DbContextOptions options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliveryAppContext).Assembly);
        }
    }
}
