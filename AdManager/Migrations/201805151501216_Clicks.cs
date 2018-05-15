namespace AdManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Clicks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clicks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ZoneID = c.Int(nullable: false),
                        CampaignID = c.Int(nullable: false),
                        Revenue = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Campaigns", t => t.CampaignID, cascadeDelete: true)
                .ForeignKey("dbo.Zones", t => t.ZoneID, cascadeDelete: true)
                .Index(t => t.ZoneID)
                .Index(t => t.CampaignID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clicks", "ZoneID", "dbo.Zones");
            DropForeignKey("dbo.Clicks", "CampaignID", "dbo.Campaigns");
            DropIndex("dbo.Clicks", new[] { "CampaignID" });
            DropIndex("dbo.Clicks", new[] { "ZoneID" });
            DropTable("dbo.Clicks");
        }
    }
}
