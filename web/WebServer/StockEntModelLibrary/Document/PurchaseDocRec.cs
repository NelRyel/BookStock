using StockEntModelLibrary.BookEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockEntModelLibrary.Document
{
    public class PurchaseDocRec
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public int Count { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }//расчитывается из PurchasePrice * 2. 
        public decimal SumPrice { get; set; }//расчитывается из Count * RetailPrice


        public int? BookId { get; set; }
        public Book Book { get; set; }

        public int? PurchaseDocId { get; set; }
        public PurchaseDoc PurchaseDoc { get; set; }
    }
}
