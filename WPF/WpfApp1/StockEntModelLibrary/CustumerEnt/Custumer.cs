using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockEntModelLibrary.CustumerEnt
{
    public class Custumer
    {
        public int Id { get; set; }
        public string CustumerTitle { get; set; }
        public decimal Balance { get; set; }
        public bool BuyerTrue_SuplierFalse { get; set; }

        public CustumerDescription CustumerDescription { get; set; }
        public List<PurchaseDoc> PurchaseDocs { get; set; }
        public List<SaleDoc> SaleDocs { get; set; }

        public Custumer()
        {
            SaleDocs = new List<SaleDoc>();
            PurchaseDocs = new List<PurchaseDoc>();
        }

        [DefaultValue("False")]
        public bool IsDelete { get; set; }
    }
}
