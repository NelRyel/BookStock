using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockEntModelLibrary.BookEnt
{
    public class Book
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }

        //[Index(IsUnique = true)]
        //[MaxLength(255)]
        public string BarcodeISBN { get; set; }
        public int Count { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public int? fullDescriptionId { get; set; }
        public BookFullDescription fullDescription { get; set; }
        public List<PurchaseDocRec> PurchaseDocRecs { get; set; }
        public Book()
        {
            PurchaseDocRecs = new List<PurchaseDocRec>();
        }
        public bool IsDelete { get; set; }

    }
}
