namespace StockEntModelLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mudakl11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Custumers", "IsMudak", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Custumers", "IsMudak");
        }
    }
}
