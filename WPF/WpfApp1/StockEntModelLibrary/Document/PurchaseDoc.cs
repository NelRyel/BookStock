using StockEntModelLibrary.CustumerEnt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockEntModelLibrary.Document
{
   public class PurchaseDoc//Приходная Накладная
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public decimal FullSum { get; set; }//рассчитывается из суммы всех  PurchaseDocRecs.SumPrice в накладной

        public int CustumerId { get; set; }
        public Custumer Custumer { get; set; }

        public List<PurchaseDocRec> PurchaseDocRecs { get; set; }
        public PurchaseDoc()
        {
            PurchaseDocRecs = new List<PurchaseDocRec>();
        }

        [DefaultValue("False")]
        public bool IsDelete { get; set; }
    }
}
