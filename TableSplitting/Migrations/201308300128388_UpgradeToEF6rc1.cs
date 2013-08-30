namespace TableSplitting
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpgradeToEF6rc1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Itineraries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItineraryAddresses",
                c => new
                    {
                        Itinerary_Id = c.Int(nullable: false),
                        Address_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Itinerary_Id, t.Address_Id })
                .ForeignKey("dbo.Itineraries", t => t.Itinerary_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Address_Id, cascadeDelete: true)
                .Index(t => t.Itinerary_Id)
                .Index(t => t.Address_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItineraryAddresses", "Address_Id", "dbo.Users");
            DropForeignKey("dbo.ItineraryAddresses", "Itinerary_Id", "dbo.Itineraries");
            DropIndex("dbo.ItineraryAddresses", new[] { "Address_Id" });
            DropIndex("dbo.ItineraryAddresses", new[] { "Itinerary_Id" });
            DropTable("dbo.ItineraryAddresses");
            DropTable("dbo.Itineraries");
            DropTable("dbo.Users");
        }
    }
}
