using ManagerLibrary;
using Newtonsoft.Json;
using StockEntModelLibrary;
using StockEntModelLibrary.CustumerEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebServer.Controllers
{
    public class CustumerController : ApiController
    {
        CustumerManager manager = new CustumerManager();
        public IQueryable<Custumer> Get()
        {
            return manager.GetAllCustumers();
        }
      
        [HttpPost]
        public void CreateCustumer([FromBody] string StringCustumer)
        {
            Custumer custumer = JsonConvert.DeserializeObject<Custumer>(StringCustumer);
            manager.CreateCustumer(custumer);
        }


    }

    public class CustumerBuyerController: ApiController
    {
        CustumerManager manager = new CustumerManager();
        public IEnumerable<Custumer> GetAllBuyers()
        {
            // var custumers = (id == 1) ? stockDBcontext.Custumers.Where(i => i.BuyerTrue_SuplierFalse == true) : stockDBcontext.Custumers.Where(i => i.BuyerTrue_SuplierFalse == false);
            return manager.GetAllCustumerBuyers();
        }
    }
}
