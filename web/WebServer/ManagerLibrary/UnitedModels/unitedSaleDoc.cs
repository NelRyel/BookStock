using StockEntModelLibrary.CustumerEnt;
using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary.UnitedModels
{
    public class unitedSaleDoc
    {
        public Custumer custumer { get; set; }
        public SaleDoc SaleDoc { get; set; }
        public List<SaleDocRec> SaleDocRecs { get; set; }
        public bool IsNew { get; set; }
    }
}
