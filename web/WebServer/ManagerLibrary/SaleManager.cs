using ManagerLibrary.UnitedModels;
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
        private StockDBcontext stockDBcontext = new StockDBcontext();

        public IEnumerable<SaleDoc> GetAllSaleDocs()
        {

            IEnumerable<SaleDoc> saleDocs = stockDBcontext.SaleDocs.Where(i => i.IsDelete == false);
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

        public void ChangeSaleDocCustumer(int DocId, int CustumerId)
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

        public ErrorsMessage ChangeStatus(int id)
        {

            ErrorsMessage msg = new ErrorsMessage();
            SaleDoc saleDoc = stockDBcontext.SaleDocs.Find(id);
            if (saleDoc.Status == StaticDatas.DocStatuses.Непроведен.ToString())
            {
                IEnumerable<SaleDocRec> saleDocRecs = stockDBcontext.SaleDocRecs.Where(i => i.SaleDocId == saleDoc.Id).ToList();
                try
                {
                    foreach (var item in saleDocRecs)
                    {
                        Book b = stockDBcontext.Books.Find(item.BookId);
                        if (item.Count > b.Count) throw new Exception("Недостаточно единиц");
                        b.Count = b.Count - item.Count;
                    }
                    saleDoc.Status = StaticDatas.DocStatuses.Проведен.ToString();
                    saleDoc.DateOfLastChangeStatus = DateTime.Now;
                    Custumer c = stockDBcontext.Custumers.Find(saleDoc.CustumerId);
                    c.Balance = c.Balance + saleDoc.FullSum;
                    //purchaseDoc.Custumer.Balance = purchaseDoc.Custumer.Balance - purchaseDoc.FullSum;
                    stockDBcontext.SaveChanges();

                    msg.boolen = 1;
                    msg.message = "OK";
                    return msg;
                }
                catch (Exception e)
                {
                    msg.boolen = 0;
                    msg.message = "False on coming Проведено: " + e;
                    return msg;

                }
            }
            else if (saleDoc.Status == StaticDatas.DocStatuses.Проведен.ToString())
            {
                IEnumerable<SaleDocRec> saleDocRecs = stockDBcontext.SaleDocRecs.Where(i => i.SaleDocId == saleDoc.Id).ToList();
                try
                {
                    foreach (var item in saleDocRecs)
                    {
                        Book b = stockDBcontext.Books.Find(item.BookId);
                       // if (item.Count > b.Count) throw new Exception("Недостаточно единиц");
                        b.Count = b.Count + item.Count;
                    }
                    saleDoc.Status = StaticDatas.DocStatuses.Непроведен.ToString();
                    saleDoc.DateOfLastChangeStatus = DateTime.Now;
                    Custumer c = stockDBcontext.Custumers.Find(saleDoc.CustumerId);
                    c.Balance = c.Balance - saleDoc.FullSum;
                    //purchaseDoc.Custumer.Balance = purchaseDoc.Custumer.Balance - purchaseDoc.FullSum;
                    stockDBcontext.SaveChanges();
                    msg.boolen = 1;
                    msg.message = "OK";
                    return msg;
                }
                catch (Exception e)
                {
                    msg.boolen = 0;
                    msg.message = "False on coming Не Проведено: " + e;
                    return msg;
                }

            }
            else
            {
                msg.boolen = 0;
                msg.message = "False в Элсе";
                return msg;

            }

        }



        public ErrorsMessage ChangeIsDelete(int id)
        {
            ErrorsMessage message = new ErrorsMessage();
            SaleDoc saleDoc = stockDBcontext.SaleDocs.Find(id);

            saleDoc.IsDelete = (saleDoc.IsDelete == true) ? saleDoc.IsDelete = false : saleDoc.IsDelete = true;
        
            stockDBcontext.SaveChanges();
            message.boolen = 1;
            message.message = "OK";
            return message;
        }

        public void SaveSaleDoc(unitedSaleDoc doc)
        {
            Custumer editedCustumer = doc.custumer;
            SaleDoc editedSaleDoc = doc.SaleDoc;
            List<SaleDocRec> editedSaleDocRecs = doc.SaleDocRecs;
            SaleDoc sd;
            List<SaleDocRec> saleDocRecs;

            if (doc.IsNew == true)
            {

                sd = new SaleDoc();
                sd.CustumerId = editedCustumer.Id;
                saleDocRecs = new List<SaleDocRec>();
            }
            else
            {
                sd = stockDBcontext.SaleDocs.Find(editedSaleDoc.Id);
                saleDocRecs = stockDBcontext.SaleDocRecs.Where(i => i.SaleDocId == sd.Id).ToList();
                saleDocRecs.Clear();
                sd.SaleDocRecs.Clear();
            }



            foreach (var item in saleDocRecs)
            {
                sd.SaleDocRecs.Remove(item);
                //purchaseDocRecs.Remove(item);
            }

            foreach (var item in editedSaleDocRecs)
            {
                saleDocRecs.Add(item);
                sd.SaleDocRecs.Add(item);
            }

            sd.DateCreate = (doc.IsNew == false) ? editedSaleDoc.DateCreate : DateTime.Now;
            sd.DateOfLastChangeStatus = (doc.IsNew == false) ? editedSaleDoc.DateOfLastChangeStatus : DateTime.Now;
            sd.Status = editedSaleDoc.Status;
            sd.Comment = editedSaleDoc.Comment;
            sd.FullSum = editedSaleDoc.FullSum;
            sd.CustumerId = editedCustumer.Id;
            //pd.Custumer = editedCustumer;
            sd.IsDelete = editedSaleDoc.IsDelete;

            if (doc.IsNew == true)
            {
                stockDBcontext.SaleDocs.Add(sd);

                foreach (var item in saleDocRecs)
                {
                    stockDBcontext.SaleDocRecs.Add(item);
                }
            }


            stockDBcontext.SaveChanges();
        }
    }
}
