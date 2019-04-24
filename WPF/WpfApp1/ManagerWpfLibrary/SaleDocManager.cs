using StockEntModelLibrary.CustumerEnt;
using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerWpfLibrary
{
    public class SaleDocManager
    {
        public DataTable LoadPurchaseDocsDataTable(List<SaleDoc> saleDocs, List<Custumer> custumers)
        {
            var dt = new DataTable();
            dt.Columns.Add("№");
            dt.Columns.Add("Дата Создания");
            dt.Columns.Add("Дата Последнего изменения");
            dt.Columns.Add("Клиент");
            dt.Columns.Add("Статус");
            dt.Columns.Add("Сумма");
            dt.Columns.Add("Комментарий");
            string client = "";
            foreach (var item in saleDocs)
            {
                foreach (var item1 in custumers)
                {
                    if (item1.Id == item.CustumerId)
                    {
                        client = "";
                        client = item1.CustumerTitle;
                    }
                }
                dt.Rows.Add(item.Id, item.DateCreate, item.DateOfLastChangeStatus, client, item.Status, item.FullSum, item.Comment);
            }
            return dt;
        }
    }
}
