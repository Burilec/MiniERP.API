using Microsoft.EntityFrameworkCore;

namespace MiniERP.API.Data
{
    public class MiniERPDbContext : DbContext
    {
        public MiniERPDbContext(DbContextOptions<MiniERPDbContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(MiniERPDbContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}