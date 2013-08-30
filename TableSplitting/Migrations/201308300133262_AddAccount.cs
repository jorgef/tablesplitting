namespace TableSplitting
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "User_Id", "dbo.Users");
            DropIndex("dbo.Accounts", new[] { "User_Id" });
            DropTable("dbo.Accounts");
        }
    }
}
