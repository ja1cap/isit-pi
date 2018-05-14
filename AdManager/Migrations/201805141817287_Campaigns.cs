namespace AdManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campaigns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campaigns",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        UserID = c.String(maxLength: 128),
                        Revenue = c.Int(nullable: false),
                        Budget = c.Int(nullable: false),
                        Currency = c.String(nullable: false),
                        BannerImageUrl = c.String(nullable: false),
                        BannerImageWidth = c.Int(nullable: false),
                        BannerImageHeight = c.Int(nullable: false),
                        ClickUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Campaigns", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Campaigns", new[] { "UserID" });
            DropTable("dbo.Campaigns");
        }
    }
}
