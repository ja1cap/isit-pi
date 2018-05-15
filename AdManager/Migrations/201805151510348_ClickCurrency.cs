namespace AdManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClickCurrency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clicks", "Currency", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clicks", "Currency");
        }
    }
}
