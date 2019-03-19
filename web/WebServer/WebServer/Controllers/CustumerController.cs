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
        public IEnumerable<Custumer> GetAll()//получить всех кастов 
        {
            return manager.GetAllCustumers();
        }

        [HttpGet]
        public Custumer GetCustumer(int id)//получить одного по ИД
        {
            return manager.GetCustumerById(id);
        }

      
        [HttpPost]
        public void CreateCustumer([FromBody] string JsonStringCustumer)//создать через Пост запрос
        {
            Custumer custumer = JsonConvert.DeserializeObject<Custumer>(JsonStringCustumer);
            manager.CreateCustumer(custumer);
        }

        [HttpPut]
        public void EditCustumer(int id, [FromBody] string JsonStringCustumer)//редактировать черезе Пут
        {
            Custumer c = JsonConvert.DeserializeObject<Custumer>(JsonStringCustumer);
            manager.EditCustumer(c.Id, c.CustumerTitle, c.BuyerTrue_SuplierFalse);
        }


    }

    public class CustumerBuyerController: ApiController
    {
        CustumerManager manager = new CustumerManager();
        [HttpGet]
        public IEnumerable<Custumer> GetAllBuyers()//не придумал как сделать в одном контроле, поэтому в отдельном. Получить Всех Покупателей
        {
            return manager.GetAllCustumerBuyers();
        }
    }
    public class CustumerSuplierController : ApiController
    {
        CustumerManager manager = new CustumerManager();
        [HttpGet]
        public IEnumerable<Custumer> GetAllSuplier()//не придумал как сделать в одном контроле, поэтому в отдельном. Получить Всех ПОСТАВЩИКОВ
        {
            return manager.GetAllCustumerSupliers();
        }
    }

    public class CustumerDesriptionController : ApiController
    {
        CustumerManager manager = new CustumerManager();

        [HttpGet]
        public CustumerDescription GetCustumerDescription(int id) //это для получения Описания Кастумера по ИД
        {
           return manager.GetCustumerDescriptionById(id);
        }

        [HttpPost]
        public void CreateDesription([FromBody] string JsonStringCustumerDesc)//создаёт Описание кастумера !!! ОБЯЗАТЕЛЬНО ПЕРЕДАВАТЬ СЮДА ОПИСАНИЕ С ПРИСВОЕННЫМ ИД !!!!
        {
            CustumerDescription custumerDesc = JsonConvert.DeserializeObject<CustumerDescription>(JsonStringCustumerDesc);
            manager.CreateCustumerDescription(custumerDesc);
        }

        [HttpPut]
        public void EditCustumerDesc(int id, [FromBody] string JsonStringCustumerDesc)//редактировать черезе Пут
        {
            CustumerDescription custumerDesc = JsonConvert.DeserializeObject<CustumerDescription>(JsonStringCustumerDesc);
            manager.EditCustumerDesc(custumerDesc.Id, custumerDesc.FullName, custumerDesc.Address, custumerDesc.Phone, custumerDesc.Email);
        }

    }
}
