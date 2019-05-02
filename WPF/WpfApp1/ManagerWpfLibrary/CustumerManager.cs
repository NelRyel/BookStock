
using StockEntModelLibrary.CustumerEnt;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace ManagerWpfLibrary
{
    public class CustumerManager
    {
        public DataTable LoadCustemer(List<Custumer> custumers)
        {
            var dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Имя Клиента");
          
            foreach (var item in custumers)
            {
                dt.Rows.Add(item.Id, item.CustumerTitle);
            }
            return dt;
        } 
    }
}