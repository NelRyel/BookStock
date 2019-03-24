namespace StockEntModelLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fullID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "fullDescriptionId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "fullDescriptionId");
        }
    }
}
