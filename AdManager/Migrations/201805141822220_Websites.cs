namespace AdManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Websites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Websites",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        Url = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        ContactName = c.String(nullable: false),
                        ContactEmail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Websites", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Websites", new[] { "UserID" });
            DropTable("dbo.Websites");
        }
    }
}
