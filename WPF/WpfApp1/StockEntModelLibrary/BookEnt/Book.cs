using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockEntModelLibrary.BookEnt
{
    public class Book
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string BarcodeISBN { get; set; }
        public int Count { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public int? fullDescriptionId { get; set; }
        public BookFullDescription fullDescription { get; set; }

        public List<PurchaseDocRec> PurchaseDocRecs { get; set; }

        //public List<BookGenre> BookGenres { get; set; }

        public Book()
        {
            PurchaseDocRecs = new List<PurchaseDocRec>();
            //BookGenres = new List<BookGenre>();
        }

        public bool IsDelete { get; set; }

    }
}
