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
    public class PurchaseManager
    {
        private static StockDBcontext stockDBcontext = new StockDBcontext();
        
        public IQueryable<PurchaseDoc> GetAllPurchaseDocs()
        {
          
                IQueryable<PurchaseDoc> purchaseDocs = stockDBcontext.PurchaseDocs.Include("Custumers").Where(u => u.IsDelete == false);
                return purchaseDocs;
           
        }

        public PurchaseDoc GetPurchaseDocById(int id)
        {
            
                PurchaseDoc purchaseDoc = stockDBcontext.PurchaseDocs.Find(id);

                return purchaseDoc;
            
        }
        public IQueryable<PurchaseDocRec> GetPurchaseDocRecs(int PurchaseDocId)
        {
           
                IQueryable<PurchaseDocRec> purchaseDocRecs = stockDBcontext.PurchaseDocRecs.Where(i => i.PurchaseDocId == PurchaseDocId);
                return purchaseDocRecs;
            
        }

        public void CreatePurchaseDoc()
        {
          
                Custumer custumer = stockDBcontext.Custumers.Where(p => p.CustumerTitle == "Основной поставщик").FirstOrDefault();
                PurchaseDoc purchaseDoc = new PurchaseDoc();
                DateTime dateTime = DateTime.Today;
                purchaseDoc.DateCreate = dateTime;
                purchaseDoc.Status = DocStatuses.Непроведен.ToString();
                purchaseDoc.Comment = "";
                purchaseDoc.FullSum = 0;
                purchaseDoc.CustumerId = custumer.Id;

                stockDBcontext.PurchaseDocs.Add(purchaseDoc);
                stockDBcontext.SaveChanges();
           

        }
       
        public void ChangePurchaseDocCustumer(int id,int CustumerId)
        {
           
                PurchaseDoc purchaseDoc = stockDBcontext.PurchaseDocs.Find(id);
                purchaseDoc.CustumerId = CustumerId;
                stockDBcontext.SaveChanges();
           
        }

        public void AddPurchaseRec(int PurchaseDocId, int BookId, int Count, int PurchasePrice)
        {
           
                PurchaseDoc purchaseDoc = stockDBcontext.PurchaseDocs.Find(PurchaseDocId);
                Book book = stockDBcontext.Books.Find(BookId);

                int lineCount = purchaseDoc.PurchaseDocRecs.Count();
                int LineNumber = (lineCount == 0) ? 1 : lineCount + 1;

                PurchaseDocRec purchaseDocRec = new PurchaseDocRec();

                purchaseDocRec.LineNumber = LineNumber;
                purchaseDocRec.Count = Count;
                purchaseDocRec.PurchasePrice = PurchasePrice;
                purchaseDocRec.RetailPrice = PurchasePrice * 2;
                purchaseDocRec.SumPrice = PurchasePrice * Count;
                purchaseDocRec.BookId = book.Id;

                stockDBcontext.PurchaseDocRecs.Add(purchaseDocRec);

                purchaseDoc.PurchaseDocRecs.Add(purchaseDocRec);

                stockDBcontext.SaveChanges();
            
        }

        public void ChangeIsDelete(int id, bool IsDelete)
        {
           
                PurchaseDoc purchaseDoc = stockDBcontext.PurchaseDocs.Find(id);

                purchaseDoc.IsDelete = (IsDelete == true) ? purchaseDoc.IsDelete = false : purchaseDoc.IsDelete = true;
                //switch(IsDelete)
                // {
                //     case true:
                //         purchaseDoc.IsDelete = false;
                //         break;
                //     case false:
                //         purchaseDoc.IsDelete = true;
                //         break;
                // }
                stockDBcontext.SaveChanges();
            
        }

    }
}
