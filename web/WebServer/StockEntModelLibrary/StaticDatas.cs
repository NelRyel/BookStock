using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockEntModelLibrary
{
   public class StaticDatas
    {
        public enum DocStatuses
        {
            Проведен,
            Непроведен,
            Удаление
        }

        public string GetStatus(int id)
        {
            DocStatuses ds;
            switch (id)
            {
                case 0:
                    ds = DocStatuses.Непроведен;
                    break;
                case 1:
                    ds = DocStatuses.Проведен;
                    break;
                case 2:
                    ds = DocStatuses.Удаление;
                    break;
                default: 
                    break;
            }
            //string status = ds.ToString();

            //return status;
            return "wrong";

        }

    }
}
