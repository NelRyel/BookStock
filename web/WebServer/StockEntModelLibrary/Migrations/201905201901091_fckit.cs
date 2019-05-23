namespace StockEntModelLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fckit : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Books", new[] { "BarcodeISBN" });
            AlterColumn("dbo.Books", "BarcodeISBN", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "BarcodeISBN", c => c.String(maxLength: 255));
            CreateIndex("dbo.Books", "BarcodeISBN", unique: true);
        }
    }
}
