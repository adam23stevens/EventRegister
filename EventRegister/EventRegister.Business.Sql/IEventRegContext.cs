using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventRegister.Business.Sql
{
    public interface IEventRegContext
    {
        DbSet<Event> Events { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<LocationTimezone> LocationTimezones { get; set; }

        DbContextTransaction DbContextTransaction { get; set; }
        void SetModified(object entity);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
