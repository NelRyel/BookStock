namespace StockEntModelLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fullID1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Custumers", "CustumerDescriptionId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Custumers", "CustumerDescriptionId");
        }
    }
}
