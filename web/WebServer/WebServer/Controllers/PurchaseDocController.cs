using ManagerLibrary;
using Newtonsoft.Json;
using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebServer.Controllers
{
    public class PurchaseDocController : ApiController
    {
        PurchaseManager manager = new PurchaseManager();

        [HttpGet]
        public IEnumerable<PurchaseDoc> GetAllPurchaseDoc()
        {
            return manager.GetAllPurchaseDocs();
        }

        [HttpGet]
        public PurchaseDoc GetPurchaseDocById(int id)
        {
            return manager.GetPurchaseDocById(id);
        }

        [HttpPost]
        public void CreatePurchaseDoc([FromBody] string JsonPurchaseDoc)
        {
            PurchaseDoc purchaseDoc = JsonConvert.DeserializeObject<PurchaseDoc>(JsonPurchaseDoc);
            manager.CreatePurchaseDoc(purchaseDoc);
        }

        [HttpPut]
        public void ChangeCustumer(int CustumerId, int PurchaseDocId)
        {
            manager.ChangePurchaseDocCustumer(PurchaseDocId, CustumerId);
        }

        [HttpPut]
        public void ChangeStatus(int PurchaceDocId)
        {
            manager.ChangeStatus(PurchaceDocId);
        }

    }

    public class PurchaseDocRecController: ApiController
    {
        PurchaseManager manager = new PurchaseManager();

        [HttpGet]
        public IEnumerable<PurchaseDocRec> GetPurchaseDocRecs(int id)
        {
            IEnumerable<PurchaseDocRec> purchaseDocRecs = manager.GetPurchaseDocRecsByDocId(id);
            return purchaseDocRecs;
        }



        [HttpPost]
        public void AddPurchaseDocRec(int id,[FromBody] string JsonPurchaseDocRecs)
        {
            IEnumerable<PurchaseDocRec> purchaseDocsRecs = JsonConvert.DeserializeObject<IEnumerable<PurchaseDocRec>>(JsonPurchaseDocRecs);
            manager.AddPurchaseDocRec(id, purchaseDocsRecs);
        }
    }

}
