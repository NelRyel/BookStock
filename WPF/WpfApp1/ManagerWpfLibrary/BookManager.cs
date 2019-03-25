using StockEntModelLibrary.BookEnt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerWpfLibrary
{
   public class BookManager
    {
        public DataTable LoadBook(List<Book> books, List<BookFullDescription> bookFulls)
        {
            
            var dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Barcode");
            dt.Columns.Add("BookTitle");
            dt.Columns.Add("Retail Price");
            dt.Columns.Add("auth");
            dt.Columns.Add("Section");
            dt.Columns.Add("count");
            string auth=null;
            string section = null;
            foreach (var item in books)
            {
                foreach (var item1 in bookFulls)
                {
                    if(item1.Id==item.Id)
                    {
                        auth = item1.Author;
                        section = item1.Section;
                    }
                }
                dt.Rows.Add(item.Id, item.BarcodeISBN, item.BookTitle, item.RetailPrice,auth, section, item.Count);
            }
            return dt;
        }

    }
}
