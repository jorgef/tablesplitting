namespace TableSplitting
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Itinerary_Id", "dbo.Itineraries");
            DropIndex("dbo.Users", new[] { "Itinerary_Id" });
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
            
            DropColumn("dbo.Users", "Itinerary_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Itinerary_Id", c => c.Int());
            DropIndex("dbo.ItineraryAddresses", new[] { "Address_Id" });
            DropIndex("dbo.ItineraryAddresses", new[] { "Itinerary_Id" });
            DropForeignKey("dbo.ItineraryAddresses", "Address_Id", "dbo.Users");
            DropForeignKey("dbo.ItineraryAddresses", "Itinerary_Id", "dbo.Itineraries");
            DropTable("dbo.ItineraryAddresses");
            CreateIndex("dbo.Users", "Itinerary_Id");
            AddForeignKey("dbo.Users", "Itinerary_Id", "dbo.Itineraries", "Id");
        }
    }
}
