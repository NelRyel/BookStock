using StockEntModelLibrary.CustumerEnt;
using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary.UnitedModels
{
    public class unitedPurchaseDoc
    {
        public Custumer custumer { get; set; }
        public PurchaseDoc PurchaseDoc { get; set; }
        public List<PurchaseDocRec> purchaseDocRecs { get; set; }
        public bool IsNew { get; set; }
    }
}
