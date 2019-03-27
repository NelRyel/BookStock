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
   public class SaleManager
    {
        private  StockDBcontext stockDBcontext = new StockDBcontext();
        
        public IEnumerable<SaleDoc> GetAllSaleDocs()
        {

            IEnumerable<SaleDoc> saleDocs = stockDBcontext.SaleDocs.Include("Custumers").Where(i => i.IsDelete == false);
                return saleDocs;
            
        }
            
        public SaleDoc GetSaleDocById(int id)
        {
                SaleDoc saleDoc = stockDBcontext.SaleDocs.Find(id);
                return saleDoc;
        }

        public IEnumerable<SaleDocRec> GetSaleDocRecs(int SaleDocId)
        {

            IEnumerable<SaleDocRec> saleDocRecs = stockDBcontext.SaleDocRecs.Where(i => i.SaleDocId == SaleDocId);
                return saleDocRecs;
          
        }

        //public void CreateSaleDoc()
        //{
           
        //        Custumer custumer = stockDBcontext.Custumers.Where(p => p.CustumerTitle == "Основной Покупатель").FirstOrDefault();
        //        SaleDoc saleDoc = new SaleDoc();
        //        DateTime dateTime = DateTime.Today;
        //        saleDoc.DateCreate = dateTime;
        //       saleDoc.DateOfLastChangeStatus = dateTime;
        //        saleDoc.Status = DocStatuses.Непроведен.ToString();
        //        saleDoc.Comment = "";
        //        saleDoc.FullSum = 0;
        //        saleDoc.CustumerId = custumer.Id;

        //        stockDBcontext.SaleDocs.Add(saleDoc);
        //        stockDBcontext.SaveChanges();

        //}
        public void CreateSaleDoc(SaleDoc saleDoc)
        {
            stockDBcontext.SaleDocs.Add(saleDoc);
            stockDBcontext.SaveChanges();
        }

        public void ChangeSaleDocCustumer(int DocId,int CustumerId)
        {
           
                SaleDoc saleDoc = stockDBcontext.SaleDocs.Find(DocId);
                saleDoc.CustumerId = CustumerId;
                stockDBcontext.SaveChanges();
           
        }

        //public void AddSaleDocRec(int SaleDocId, int BookId, int Count, int Price)
        //{
           
        //        SaleDoc saleDoc = stockDBcontext.SaleDocs.Find(SaleDocId);
        //        Book book = stockDBcontext.Books.Find(BookId);

        //        int lineCount = saleDoc.SaleDocRecs.Count;
        //        int LineNumber = (lineCount == 0) ? 1 : lineCount + 1;

        //        SaleDocRec saleDocRec = new SaleDocRec();
        //        saleDocRec.LineNumber = LineNumber;
        //        saleDocRec.Count = Count;
        //        saleDocRec.RetailPrice = Price;
        //        saleDocRec.SumPrice = Price * Count;
        //        saleDocRec.BookId = book.Id;

        //        stockDBcontext.SaleDocRecs.Add(saleDocRec);

        //        saleDoc.SaleDocRecs.Add(saleDocRec);

        //        stockDBcontext.SaveChanges();
            
        //}
        public void AddSaleDocRec(int id, IEnumerable<SaleDocRec> saleDocRecs)
        {
            SaleDoc saleDoc = stockDBcontext.SaleDocs.Find(id);
            saleDoc.SaleDocRecs.Clear();

            foreach (var item in saleDocRecs)
            {
                saleDoc.SaleDocRecs.Add(item);
            }
            //saleDoc.SaleDocRecs.Add(saleDocRec);
            stockDBcontext.SaveChanges();
        }

        public bool ChangeStatus(int id)
        {

            SaleDoc saleDoc = stockDBcontext.SaleDocs.Find(id);
            if (saleDoc.Status == StaticDatas.DocStatuses.Непроведен.ToString())
            {
                IEnumerable<SaleDocRec> saleDocRecs = stockDBcontext.SaleDocRecs.Where(i => i.SaleDocId == saleDoc.Id);
                try
                {
                    foreach (var item in saleDocRecs)
                    {
                        Book b = stockDBcontext.Books.Find(item.BookId);
                        if (item.Count > b.Count) throw new Exception("Недостаточно единиц");
                        b.Count = b.Count - item.Count;
                    }
                    saleDoc.Status = StaticDatas.DocStatuses.Проведен.ToString();
                    saleDoc.Custumer.Balance = saleDoc.Custumer.Balance + saleDoc.FullSum;
                    stockDBcontext.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }else if (saleDoc.Status == StaticDatas.DocStatuses.Проведен.ToString())
            {
                IEnumerable<SaleDocRec> saleDocRecs = stockDBcontext.SaleDocRecs.Where(i => i.SaleDocId == saleDoc.Id);
                try
                {
                    foreach (var item in saleDocRecs)
                    {
                        Book b = stockDBcontext.Books.Find(item.BookId);
                        b.Count = b.Count + item.Count;
                    }
                    saleDoc.Status = StaticDatas.DocStatuses.Непроведен.ToString();
                    saleDoc.Custumer.Balance = saleDoc.Custumer.Balance - saleDoc.FullSum;
                    stockDBcontext.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
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
