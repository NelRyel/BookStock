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
            dt.Columns.Add("Штрихкод");
            dt.Columns.Add("Наименование");
            dt.Columns.Add("Отпускная цена");
            dt.Columns.Add("Автор");
            dt.Columns.Add("Серия");
            dt.Columns.Add("Секция");
            dt.Columns.Add("количество на остатке");
            string auth=null;
            string section = null;
            string serie = null;
            foreach (var item in books)
            {
                foreach (var item1 in bookFulls)
                {
                    if(item1.Id==item.Id)
                    {
                        auth = item1.Author;
                        section = item1.Section;
                        serie = item1.Serie;
                    }
                }
                dt.Rows.Add(item.Id, item.BarcodeISBN, item.BookTitle, item.RetailPrice,auth, serie, section, item.Count);
            }
            return dt;
        }

        public DataTable FilteredBook(List<BookAndDesc> bookAndDescs) 
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Штрихкод");
            dt.Columns.Add("Наименование");
            dt.Columns.Add("Отпускная цена");
            dt.Columns.Add("Автор");
            dt.Columns.Add("Серия");
            dt.Columns.Add("Секция");
            dt.Columns.Add("количество на остатке");
            string auth = null;
            string section = null;
            string serie = null;





            return dt;

        }


    }
}
