﻿using StockEntModelLibrary;
using StockEntModelLibrary.BookEnt;
using StockEntModelLibrary.CustumerEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebServer.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        StockDBcontext stockDBcontext = new StockDBcontext(); 
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    //return new string[] { "value1", "value2" };
        //    string[] s = { "hey", "hey"};
        //    return s;
        //}

        public IEnumerable<Custumer> GetAllCustumers()
        {
            return stockDBcontext.Custumers;
        }



        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
