namespace TableSplitting
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Itineraries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Itineraries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Itinerary_Id", c => c.Int());
            AddForeignKey("dbo.Users", "Itinerary_Id", "dbo.Itineraries", "Id");
            CreateIndex("dbo.Users", "Itinerary_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Itinerary_Id" });
            DropForeignKey("dbo.Users", "Itinerary_Id", "dbo.Itineraries");
            DropColumn("dbo.Users", "Itinerary_Id");
            DropTable("dbo.Itineraries");
        }
    }
}
