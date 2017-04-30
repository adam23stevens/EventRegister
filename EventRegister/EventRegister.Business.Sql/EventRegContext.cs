namespace EventRegister.Business.Sql
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EventRegContext : DbContext, IEventRegContext
    {
        public EventRegContext()
            : base("name=EventRegContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Country> Countries { get ; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationTimezone> LocationTimezones { get; set ; }
        public DbContextTransaction DbContextTransaction { get; set ; }

        public void BeginTransaction()
        {
            if (DbContextTransaction == null)
            {
                DbContextTransaction = Database.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            try
            {
                if (DbContextTransaction != null)
                {
                    DbContextTransaction.Commit();
                }
            }
            catch
            {
                RollbackTransaction();
            }
            finally
            {
                DbContextTransaction = null;
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                if (DbContextTransaction?.UnderlyingTransaction != null)
                {
                    DbContextTransaction.Rollback();
                }
            }
            finally
            {
                DbContextTransaction = null;
            }
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
