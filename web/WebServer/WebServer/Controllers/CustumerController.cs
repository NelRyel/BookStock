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
        [HttpGet]
        public IQueryable<Custumer> GetAll()
        {
            return manager.GetAllCustumers();
        }

        [HttpGet]
        public Custumer GetCustumer(int id)
        {
            return manager.GetCustumerById(id);
        }

      
        [HttpPost]
        public void CreateCustumer([FromBody] string JsonStringCustumer)
        {
            Custumer custumer = JsonConvert.DeserializeObject<Custumer>(JsonStringCustumer);
            manager.CreateCustumer(custumer);
        }

        [HttpPut]
        public void EditCustumer(int id, [FromBody] string JsonStringCustumer)
        {
            Custumer c = JsonConvert.DeserializeObject<Custumer>(JsonStringCustumer);
           // CustumerDescription cd = JsonConvert.DeserializeObject<CustumerDescription>(JsonCustDescr);
            manager.EditCustumer(c.Id, c.CustumerTitle, c.BuyerTrue_SuplierFalse,"", "", "", "");

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
    public class CustumerDesriptionController : ApiController
    {
        CustumerManager manager = new CustumerManager();
        [HttpGet]
        public CustumerDescription GetCustumerDescription(int id)
        {
           return manager.GetCustumerDescriptionById(id);
        }
    }
}
