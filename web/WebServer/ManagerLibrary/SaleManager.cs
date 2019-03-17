using StockEntModelLibrary;
using StockEntModelLibrary.BookEnt;
using StockEntModelLibrary.CustumerEnt;
using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ManagerLibrary.StaticDatasManager;

namespace ManagerLibrary
{
    class SaleManager
    {
        private static StockDBcontext stockDBcontext = new StockDBcontext();
        
        public IQueryable<SaleDoc> GetAllSaleDocs()
        {
          
                IQueryable<SaleDoc> saleDocs = stockDBcontext.SaleDocs.Include("Custumers").Where(i => i.IsDelete == false);
                return saleDocs;
            
        }
            
        public SaleDoc GetSaleDocById(int id)
        {
            


                SaleDoc saleDoc = stockDBcontext.SaleDocs.Find(id);
                return saleDoc;
           
        }

        public IQueryable<SaleDocRec> GetSaleDocRecs(int SaleDocId)
        {
           
                IQueryable<SaleDocRec> saleDocRecs = stockDBcontext.SaleDocRecs.Where(i => i.SaleDocId == SaleDocId);
                return saleDocRecs;
          
        }

        public void CreateSaleDoc()
        {
           
                Custumer custumer = stockDBcontext.Custumers.Where(p => p.CustumerTitle == "Основной Покупатель").FirstOrDefault();
                SaleDoc saleDoc = new SaleDoc();
                DateTime dateTime = DateTime.Today;

                saleDoc.DateCreate = dateTime;
                saleDoc.Status = DocStatuses.Непроведен.ToString();
                saleDoc.Comment = "";
                saleDoc.FullSum = 0;
                saleDoc.CustumerId = custumer.Id;

                stockDBcontext.SaleDocs.Add(saleDoc);
                stockDBcontext.SaveChanges();

        }

        public void ChangeSaleDocCustumer(int id,int CustumerId)
        {
           
                SaleDoc saleDoc = stockDBcontext.SaleDocs.Find(id);
                saleDoc.CustumerId = CustumerId;
                stockDBcontext.SaveChanges();
           
        }

        public void AddSaleDocRec(int SaleDocId, int BookId, int Count, int Price)
        {
           
                SaleDoc saleDoc = stockDBcontext.SaleDocs.Find(SaleDocId);
                Book book = stockDBcontext.Books.Find(BookId);

                int lineCount = saleDoc.SaleDocRecs.Count;
                int LineNumber = (lineCount == 0) ? 1 : lineCount + 1;

                SaleDocRec saleDocRec = new SaleDocRec();
                saleDocRec.LineNumber = LineNumber;
                saleDocRec.Count = Count;
                saleDocRec.RetailPrice = Price;
                saleDocRec.SumPrice = Price * Count;
                saleDocRec.BookId = book.Id;

                stockDBcontext.SaleDocRecs.Add(saleDocRec);

                saleDoc.SaleDocRecs.Add(saleDocRec);

                stockDBcontext.SaveChanges();
            
        }

        public void ChangeIsDelete(int id, bool IsDelete)
        {
                SaleDoc saleDoc = stockDBcontext.SaleDocs.Find(id);

                saleDoc.IsDelete = (IsDelete == true) ? saleDoc.IsDelete = false : saleDoc.IsDelete = true;
                //switch(IsDelete)
                // {
                //     case true:
                //         saleDoc.IsDelete = false;
                //         break;
                //     case false:
                //         saleDoc.IsDelete = true;
                //         break;
                // }
                stockDBcontext.SaveChanges();
        }

    }
}
