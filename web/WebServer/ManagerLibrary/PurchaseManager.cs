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
    public class PurchaseManager
    {
        private  StockDBcontext stockDBcontext = new StockDBcontext();
        
        public IEnumerable<PurchaseDoc> GetAllPurchaseDocs()
        {

            IEnumerable<PurchaseDoc> purchaseDocs = stockDBcontext.PurchaseDocs.Where(u => u.IsDelete == false);
                return purchaseDocs;
           
        }

        public PurchaseDoc GetPurchaseDocById(int id)
        {
            
                PurchaseDoc purchaseDoc = stockDBcontext.PurchaseDocs.Find(id);

                return purchaseDoc;
            
        }
        public IEnumerable<PurchaseDocRec> GetPurchaseDocRecs(int PurchaseDocId)
        {
            IEnumerable<PurchaseDocRec> purchaseDocRecs = stockDBcontext.PurchaseDocRecs.Where(i => i.PurchaseDocId == PurchaseDocId);
                return purchaseDocRecs;
            
        }

        //public void CreatePurchaseDoc()
        //{
          
        //        Custumer custumer = stockDBcontext.Custumers.Where(p => p.CustumerTitle == "Основной поставщик").FirstOrDefault();
        //        PurchaseDoc purchaseDoc = new PurchaseDoc();
        //        DateTime dateTime = DateTime.Today;
        //        purchaseDoc.DateCreate = dateTime;
        //    purchaseDoc.DateOfLastChangeStatus = dateTime;
        //        purchaseDoc.Status = DocStatuses.Непроведен.ToString();
        //        purchaseDoc.Comment = "";
        //        purchaseDoc.FullSum = 0;
        //        purchaseDoc.CustumerId = custumer.Id;

        //        stockDBcontext.PurchaseDocs.Add(purchaseDoc);
        //        stockDBcontext.SaveChanges();
           

        //}
        public void CreatePurchaseDoc(PurchaseDoc purchaseDoc)
        {
            stockDBcontext.PurchaseDocs.Add(purchaseDoc);
            stockDBcontext.SaveChanges();
        }


        public void ChangePurchaseDocCustumer(int DocId,int CustumerId)
        {
           
                PurchaseDoc purchaseDoc = stockDBcontext.PurchaseDocs.Find(DocId);
                purchaseDoc.CustumerId = CustumerId;
                stockDBcontext.SaveChanges();
           
        }

        //public void AddPurchaseRec(int PurchaseDocId, int BookId, int Count, int PurchasePrice)
        //{
           
        //        PurchaseDoc purchaseDoc = stockDBcontext.PurchaseDocs.Find(PurchaseDocId);
        //        Book book = stockDBcontext.Books.Find(BookId);

        //        int lineCount = purchaseDoc.PurchaseDocRecs.Count();
        //        int LineNumber = (lineCount == 0) ? 1 : lineCount + 1;

        //        PurchaseDocRec purchaseDocRec = new PurchaseDocRec();

        //        purchaseDocRec.LineNumber = LineNumber;
        //        purchaseDocRec.Count = Count;
        //        purchaseDocRec.PurchasePrice = PurchasePrice;
        //        purchaseDocRec.RetailPrice = PurchasePrice * 2;
        //        purchaseDocRec.SumPrice = PurchasePrice * Count;
        //        purchaseDocRec.BookId = book.Id;

        //        stockDBcontext.PurchaseDocRecs.Add(purchaseDocRec);

        //        purchaseDoc.PurchaseDocRecs.Add(purchaseDocRec);

        //        stockDBcontext.SaveChanges();
            
        //}

        public void AddPurchaseDocRec(int id, IEnumerable<PurchaseDocRec> purchaseDocs)
        {
            PurchaseDoc purchaseDoc = stockDBcontext.PurchaseDocs.Find(id);
            purchaseDoc.PurchaseDocRecs.Clear();
            foreach (var item in purchaseDocs)
            {
                purchaseDoc.PurchaseDocRecs.Add(item);
            }
            stockDBcontext.SaveChanges();
        }

        public ErrorsMessage ChangeStatus(int id)
        {

                ErrorsMessage msg = new ErrorsMessage();
                PurchaseDoc purchaseDoc = stockDBcontext.PurchaseDocs.Find(id);
                if (purchaseDoc.Status == StaticDatas.DocStatuses.Непроведен.ToString())
                {
                    IEnumerable<PurchaseDocRec> purchaseDocRecs = stockDBcontext.PurchaseDocRecs.Where(i => i.PurchaseDocId == purchaseDoc.Id).ToList();
                    try
                    {
                        foreach (var item in purchaseDocRecs)
                        {
                            Book b = stockDBcontext.Books.Find(item.BookId);
                            //if (item.Count > b.Count) throw new Exception("Недостаточно единиц");
                            b.Count = b.Count + item.Count;
                        }
                    purchaseDoc.Status = StaticDatas.DocStatuses.Проведен.ToString();
                    purchaseDoc.DateOfLastChangeStatus = DateTime.Now;
                    Custumer c = stockDBcontext.Custumers.Find(purchaseDoc.CustumerId);
                    c.Balance = c.Balance - purchaseDoc.FullSum;
                    //purchaseDoc.Custumer.Balance = purchaseDoc.Custumer.Balance - purchaseDoc.FullSum;
                    stockDBcontext.SaveChanges();

                    msg.boolen = 1;
                    msg.message = "OK";
                    return msg;
                    }
                    catch (Exception e)
                    {

                    msg.boolen = 0;
                    msg.message = "False on coming Не Проведено: "+ e;
                    return msg;

                }
                }
                else if (purchaseDoc.Status == StaticDatas.DocStatuses.Проведен.ToString())
                {
                    IEnumerable<PurchaseDocRec> purchaseDocRecs = stockDBcontext.PurchaseDocRecs.Where(i => i.PurchaseDocId == purchaseDoc.Id).ToList();
                    try
                    {
                        foreach (var item in purchaseDocRecs)
                        {
                            Book b = stockDBcontext.Books.Find(item.BookId);
                        if (item.Count > b.Count) throw new Exception("Недостаточно единиц");
                        b.Count = b.Count - item.Count;
                    }
                        purchaseDoc.Status = StaticDatas.DocStatuses.Непроведен.ToString();
                    purchaseDoc.DateOfLastChangeStatus = DateTime.Now;
                    Custumer c = stockDBcontext.Custumers.Find(purchaseDoc.CustumerId);
                        c.Balance = c.Balance + purchaseDoc.FullSum;
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
                else
                {
                
                msg.boolen = 0;
                msg.message = "False в Элсе";
                return msg;

            }
            
        }

        public IEnumerable<PurchaseDocRec> GetPurchaseDocRecsByDocId(int docId)
        {
            IEnumerable<PurchaseDocRec> purchaseDocRecs = stockDBcontext.PurchaseDocRecs.Where(i => i.PurchaseDocId == docId);
            return purchaseDocRecs;


        }


        public void SavePurchaseDoc(unitedPurchaseDoc doc)
        {
            Custumer editedCustumer = doc.custumer;
            PurchaseDoc editedPurchaseDoc = doc.PurchaseDoc;
            List<PurchaseDocRec> editedPurchaseDocRecs = doc.purchaseDocRecs;
            PurchaseDoc pd = stockDBcontext.PurchaseDocs.Find(editedPurchaseDoc.Id);
            List<PurchaseDocRec> purchaseDocRecs = stockDBcontext.PurchaseDocRecs.Where(i => i.PurchaseDocId == pd.Id).ToList();
         

          
            purchaseDocRecs.Clear();
            pd.PurchaseDocRecs.Clear();
            
            foreach (var item in purchaseDocRecs)
            {
                pd.PurchaseDocRecs.Remove(item);
                //purchaseDocRecs.Remove(item);
            }

            foreach (var item in editedPurchaseDocRecs)
            {
                purchaseDocRecs.Add(item);
                pd.PurchaseDocRecs.Add(item);
            }

            pd.DateCreate = editedPurchaseDoc.DateCreate;
            pd.DateOfLastChangeStatus = editedPurchaseDoc.DateOfLastChangeStatus;
            pd.Status = editedPurchaseDoc.Status;
            pd.Comment = editedPurchaseDoc.Comment;
            pd.FullSum = editedPurchaseDoc.FullSum;
            pd.CustumerId = editedPurchaseDoc.CustumerId;
            //pd.Custumer = editedCustumer;
            pd.IsDelete = editedPurchaseDoc.IsDelete;


            stockDBcontext.SaveChanges();



        }


        public void ChangeIsDelete(int id, bool IsDelete)
        {
            PurchaseDoc purchaseDoc = stockDBcontext.PurchaseDocs.Find(id);
            purchaseDoc.IsDelete = (IsDelete == true) ? purchaseDoc.IsDelete = false : purchaseDoc.IsDelete = true;
            stockDBcontext.SaveChanges();
        }

    }
}
