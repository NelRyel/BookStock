using StockEntModelLibrary.CustumerEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary
{
   public class CustAndDesc//эта странная вещь, нужна для того, чтобы одним JSON-ом передавать и Кастумера и ОписаниеКастумера
    {
        public Custumer custumer { get; set; }
        public CustumerDescription custumerDescription { get; set; }
    }
}
