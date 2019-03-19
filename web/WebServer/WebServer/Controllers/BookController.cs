using ManagerLibrary;
using Newtonsoft.Json;
using StockEntModelLibrary;
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
    public class BookController : ApiController
    {
      BookManager manager = new BookManager();

        [HttpGet]
        public IEnumerable<Book> books()
        {
            return manager.GetAllBook();
        }

        [HttpGet]
        public Book GetBookById(int id)
        {
            return manager.GetBookById(id);
        }

        [HttpPost]
        public void CreateBook([FromBody] string JsonBook)
        {
            Book book = JsonConvert.DeserializeObject<Book>(JsonBook);
            manager.CreateBook(book);
        }

    }
}
