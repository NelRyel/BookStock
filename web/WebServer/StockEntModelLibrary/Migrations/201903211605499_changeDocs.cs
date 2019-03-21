namespace StockEntModelLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeDocs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseDocs", "DateOfLastChangeStatus", c => c.DateTime(nullable: false));
            AddColumn("dbo.SaleDocs", "DateOfLastChangeStatus", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SaleDocs", "DateOfLastChangeStatus");
            DropColumn("dbo.PurchaseDocs", "DateOfLastChangeStatus");
        }
    }
}
