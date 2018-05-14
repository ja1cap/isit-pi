namespace AdManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Zones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        WebsiteID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        AdPlacementWidth = c.Int(nullable: false),
                        AdPlacementHeight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.Websites", t => t.WebsiteID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.WebsiteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zones", "WebsiteID", "dbo.Websites");
            DropForeignKey("dbo.Zones", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Zones", new[] { "WebsiteID" });
            DropIndex("dbo.Zones", new[] { "UserID" });
            DropTable("dbo.Zones");
        }
    }
}
