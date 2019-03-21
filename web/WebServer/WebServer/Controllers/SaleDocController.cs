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

        [HttpPost]
        public void AddSellDocRec(int id,[FromBody] string JsonSaleDocRecs)
        {
            IEnumerable<SaleDocRec> saleDocRecs = JsonConvert.DeserializeObject<IEnumerable<SaleDocRec>>(JsonSaleDocRecs);
            manager.AddSaleDocRec(id, saleDocRecs);
        }
    }
}
