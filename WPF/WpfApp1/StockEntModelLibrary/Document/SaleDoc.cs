using StockEntModelLibrary.CustumerEnt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockEntModelLibrary.Document
{
    public class SaleDoc//Расходная Накладная
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateOfLastChangeStatus { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public decimal FullSum { get; set; }//рассчитывается из суммы всех  SaleDocRec.SumPrice в накладной

        public int? CustumerId { get; set; }
        public Custumer Custumer { get; set; }

        public List<SaleDocRec> SaleDocRecs { get; set; }
        public SaleDoc()
        {
            SaleDocRecs = new List<SaleDocRec>();
        }

        [DefaultValue("False")]
        public bool IsDelete { get; set; }

    }
}
