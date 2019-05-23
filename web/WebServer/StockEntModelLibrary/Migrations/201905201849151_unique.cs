namespace StockEntModelLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unique : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookFullDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        YearBookPublishing = c.String(),
                        FirstYearBookPublishing = c.String(),
                        Serie = c.String(),
                        Section = c.String(),
                        Description = c.String(),
                        Author = c.String(),
                        Publisher = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookTitle = c.String(),
                        BarcodeISBN = c.String(maxLength: 255),
                        Count = c.Int(nullable: false),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        fullDescriptionId = c.Int(),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.BarcodeISBN, unique: true);
            
            CreateTable(
                "dbo.PurchaseDocRecs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LineNumber = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BookId = c.Int(),
                        PurchaseDocId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.PurchaseDocs", t => t.PurchaseDocId)
                .Index(t => t.BookId)
                .Index(t => t.PurchaseDocId);
            
            CreateTable(
                "dbo.PurchaseDocs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreate = c.DateTime(nullable: false),
                        DateOfLastChangeStatus = c.DateTime(nullable: false),
                        Status = c.String(),
                        Comment = c.String(),
                        FullSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustumerId = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Custumers", t => t.CustumerId, cascadeDelete: true)
                .Index(t => t.CustumerId);
            
            CreateTable(
                "dbo.Custumers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustumerTitle = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyerTrue_SuplierFalse = c.Boolean(nullable: false),
                        CustumerDescriptionId = c.Int(),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustumerDescriptions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FullName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Custumers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.SaleDocs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreate = c.DateTime(nullable: false),
                        DateOfLastChangeStatus = c.DateTime(nullable: false),
                        Status = c.String(),
                        Comment = c.String(),
                        FullSum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustumerId = c.Int(),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Custumers", t => t.CustumerId)
                .Index(t => t.CustumerId);
            
            CreateTable(
                "dbo.SaleDocRecs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LineNumber = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        RetailPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BookId = c.Int(),
                        SaleDocId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.SaleDocs", t => t.SaleDocId)
                .Index(t => t.BookId)
                .Index(t => t.SaleDocId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookFullDescriptions", "Id", "dbo.Books");
            DropForeignKey("dbo.PurchaseDocRecs", "PurchaseDocId", "dbo.PurchaseDocs");
            DropForeignKey("dbo.SaleDocRecs", "SaleDocId", "dbo.SaleDocs");
            DropForeignKey("dbo.SaleDocRecs", "BookId", "dbo.Books");
            DropForeignKey("dbo.SaleDocs", "CustumerId", "dbo.Custumers");
            DropForeignKey("dbo.PurchaseDocs", "CustumerId", "dbo.Custumers");
            DropForeignKey("dbo.CustumerDescriptions", "Id", "dbo.Custumers");
            DropForeignKey("dbo.PurchaseDocRecs", "BookId", "dbo.Books");
            DropIndex("dbo.SaleDocRecs", new[] { "SaleDocId" });
            DropIndex("dbo.SaleDocRecs", new[] { "BookId" });
            DropIndex("dbo.SaleDocs", new[] { "CustumerId" });
            DropIndex("dbo.CustumerDescriptions", new[] { "Id" });
            DropIndex("dbo.PurchaseDocs", new[] { "CustumerId" });
            DropIndex("dbo.PurchaseDocRecs", new[] { "PurchaseDocId" });
            DropIndex("dbo.PurchaseDocRecs", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "BarcodeISBN" });
            DropIndex("dbo.BookFullDescriptions", new[] { "Id" });
            DropTable("dbo.SaleDocRecs");
            DropTable("dbo.SaleDocs");
            DropTable("dbo.CustumerDescriptions");
            DropTable("dbo.Custumers");
            DropTable("dbo.PurchaseDocs");
            DropTable("dbo.PurchaseDocRecs");
            DropTable("dbo.Books");
            DropTable("dbo.BookFullDescriptions");
        }
    }
}
