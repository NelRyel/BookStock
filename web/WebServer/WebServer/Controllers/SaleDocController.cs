using ManagerLibrary;
using ManagerLibrary.UnitedModels;
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
    public class SaleDocController : ApiController
    {
        SaleManager manager = new SaleManager();
        
        [HttpGet]
        public IEnumerable<SaleDoc> GetAllSaleDoc()
        {
            return manager.GetAllSaleDocs();
        }

        [HttpGet]
        public SaleDoc GetSaleDocById(int id)
        {
            return manager.GetSaleDocById(id);
        }


        [HttpPost]
        public void CreateSaleDoc([FromBody] string JsonSaleDoc)
        {
            SaleDoc saleDoc = JsonConvert.DeserializeObject<SaleDoc>(JsonSaleDoc);
            manager.CreateSaleDoc(saleDoc);
        }

        [HttpPut]
        public void ChangeCustumer(int CustumerId, int SaleDocId)
        {
            manager.ChangeSaleDocCustumer(SaleDocId, CustumerId);
        }

        [HttpPut]
        public void ChangeStatus(int SaleDocId)
        {
            manager.ChangeStatus(SaleDocId);
        }
    }

    public class SaleDocRecController: ApiController
    {
        SaleManager manager = new SaleManager();

        [HttpGet]
        public IEnumerable<SaleDocRec> GetSaleDocRecs(int id)
        {
            IEnumerable<SaleDocRec> saleDocRecs = manager.GetSaleDocRecs(id);
            return saleDocRecs;
        }



        [HttpPost]
        public void AddSellDocRec(int id,[FromBody] string JsonSaleDocRecs)
        {
            IEnumerable<SaleDocRec> saleDocRecs = JsonConvert.DeserializeObject<IEnumerable<SaleDocRec>>(JsonSaleDocRecs);
            manager.AddSaleDocRec(id, saleDocRecs);
        }
    }

    public class UnitedSaleDocController : ApiController
    {
        SaleManager manager = new SaleManager();

        [HttpPut]
        public void SavePurchaseDoc([FromBody] string jsonUnitedSaleDoc)
        {
            unitedSaleDoc usd = JsonConvert.DeserializeObject<unitedSaleDoc>(jsonUnitedSaleDoc);
            manager.SaveSaleDoc(usd);

        }
    }

    public class SaleDocChangeController : ApiController
    {
        SaleManager manager = new SaleManager();

        [HttpGet]
        public ErrorsMessage GetStrange(int id)
        {
            ErrorsMessage msg = manager.ChangeStatus(id);
            return msg;
        }

    }

    public class SaleDocDelController : ApiController
    {
        SaleManager manager = new SaleManager();
        [HttpGet]
        public ErrorsMessage DelSaleDoc(int id)
        {
            ErrorsMessage msg = manager.ChangeIsDelete(id);
            return msg;
        }
    }

}
