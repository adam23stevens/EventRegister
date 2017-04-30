namespace EventRegister.Business.Sql.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UkStartTime = c.DateTime(nullable: false),
                        UkEndTime = c.DateTime(nullable: false),
                        Location_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.Location_Id)
                .Index(t => t.Location_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Country_Id = c.Int(),
                        LocationTimezone_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .ForeignKey("dbo.LocationTimezones", t => t.LocationTimezone_Id)
                .Index(t => t.Country_Id)
                .Index(t => t.LocationTimezone_Id);
            
            CreateTable(
                "dbo.LocationTimezones",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Location_Id", "dbo.Locations");
            DropForeignKey("dbo.Locations", "LocationTimezone_Id", "dbo.LocationTimezones");
            DropForeignKey("dbo.Locations", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Locations", new[] { "LocationTimezone_Id" });
            DropIndex("dbo.Locations", new[] { "Country_Id" });
            DropIndex("dbo.Events", new[] { "Location_Id" });
            DropTable("dbo.LocationTimezones");
            DropTable("dbo.Locations");
            DropTable("dbo.Events");
            DropTable("dbo.Countries");
        }
    }
}
