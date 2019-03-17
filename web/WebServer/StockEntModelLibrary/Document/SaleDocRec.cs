using StockEntModelLibrary.BookEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockEntModelLibrary.Document
{
    public class SaleDocRec
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public int Count { get; set; }
      
        public decimal RetailPrice { get; set; }//берется из Book.RetailPrice
        public decimal SumPrice { get; set; }//расчитывается из Count * RetailPrice


        public int? BookId { get; set; }
        public Book Book { get; set; }

        public int? SaleDocId { get; set; }
        public SaleDoc SaleDoc { get; set; }
    }
}
