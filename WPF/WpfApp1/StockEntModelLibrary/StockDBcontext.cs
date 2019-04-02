using StockEntModelLibrary.BookEnt;
using StockEntModelLibrary.CustumerEnt;
using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockEntModelLibrary
{
    public class StockDBcontext: DbContext
    {
        public StockDBcontext()
        {
            //Database.SetInitializer(new DefaultInit());
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookFullDescription> BookFullDescriptions { get; set; }
        //public DbSet<BookGenre> BookGenres { get; set; }

        public DbSet<Custumer> Custumers { get; set; }
        public DbSet<CustumerDescription> CustumerDescriptions { get; set; }

        public DbSet<PurchaseDoc> PurchaseDocs { get; set; }
        public DbSet<PurchaseDocRec> PurchaseDocRecs { get; set; }
        public DbSet<SaleDoc> SaleDocs { get; set; }
        public DbSet<SaleDocRec> SaleDocRecs { get; set; }

        public DbSet<custAndDesc> custAndDescs { get; set; }
 


    }
}
