namespace EventRegister.Business.Sql.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EventRegister.Business.Sql.EventRegContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private void RunSqlCommand<T>(Action<EventRegContext> cmd, EventRegContext ctx)
        {
            try
            {
                ctx.BeginTransaction();
                cmd(ctx);
                ctx.CommitTransaction();
            }
            catch
            {
                ctx.RollbackTransaction();
            }
        }

        protected override void Seed(EventRegister.Business.Sql.EventRegContext context)
        {
            #region Countries
            RunSqlCommand<EventRegContext>((EventRegContext ctx) =>
            {
                if (!ctx.Countries.Any())
                {
                    ctx.Countries.AddOrUpdate(
                        new Country
                        {
                            CountryCode = "UK",
                            Name = "United Kingdom"
                        },
                        new Country
                        {
                            CountryCode = "USA",
                            Name = "United States"
                        },
                        new Country
                        {
                            CountryCode = "RUS",
                            Name = "Russia"
                        },
                        new Country
                        {
                            CountryCode = "CHI",
                            Name = "China"
                        });
                }
            }
            , context);
            #endregion

            #region LocationTimezones
            RunSqlCommand<EventRegContext>((EventRegContext ctx) =>
            {
                if (!ctx.LocationTimezones.Any())
                {
                    ctx.LocationTimezones.AddOrUpdate(
                        new LocationTimezone
                        {
                            Id = "UK",
                            Name = "GMT Standard Time"
                        },
                        new LocationTimezone
                        {
                            Id = "USAE",
                            Name = "US Eastern Standard Time"
                        },
                        new LocationTimezone
                        {
                            Id = "USAW",
                            Name = "Pacific Standard Time"
                        },
                        new LocationTimezone
                        {
                            Id = "RUS",
                            Name = "Russia Standard Time"
                        },
                        new LocationTimezone
                        {
                            Id = "CHI",
                            Name = "China Standard Time"
                        }
                        );
                    }
                }
            , context);
            #endregion

            #region Locations
            RunSqlCommand<EventRegContext>((EventRegContext ctx) =>
            {
                if (!ctx.Locations.Any())
                {
                    ctx.Locations.AddOrUpdate(
                        new Location
                        {
                            City = "London",
                            Country = ctx.Countries.FirstOrDefault(c => c.CountryCode == "UK"),
                            LocationTimezone = ctx.LocationTimezones.FirstOrDefault(t => t.Id == "UK")
                        },
                        new Location
                        {
                            City = "New York",
                            Country = ctx.Countries.FirstOrDefault(c => c.CountryCode == "USA"),
                            LocationTimezone = ctx.LocationTimezones.FirstOrDefault(t => t.Id == "USAE")
                        },
                        new Location
                        {
                            City = "Los Angeles",
                            Country = ctx.Countries.FirstOrDefault(c => c.CountryCode == "USA"),
                            LocationTimezone = ctx.LocationTimezones.FirstOrDefault(t => t.Id == "USAW")
                        });
                }
            }, context);
            #endregion

            #region Events
            RunSqlCommand<EventRegContext>((EventRegContext ctx) =>
            {
                if (!ctx.Events.Any())
                {
                    ctx.Events.AddOrUpdate(
                        new Event
                        {
                             Name = "London Retro Gaming",
                             Location = ctx.Locations.FirstOrDefault(l => l.City == "London"),
                             UkStartTime = new DateTime(2017,07,1,10,30,0),
                             UkEndTime = new DateTime(2017,07,15,17,0,0),
                             RegisteredEmails = new List<string> { "adam23stevens@gmail.com"}
                        }
                        );
                }
            }, context);
            #endregion
        }
    }
}
