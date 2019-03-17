namespace StockEntModelLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class neMudak : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Custumers", "IsMudak");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Custumers", "IsMudak", c => c.Boolean(nullable: false));
        }
    }
}
