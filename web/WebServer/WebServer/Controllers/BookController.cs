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

        [HttpPut]
        public void EditBook(int id, [FromBody] string JsonBook)//редактировать черезе Пут
        {
            Book book = JsonConvert.DeserializeObject<Book>(JsonBook);
            manager.EditBook(book.Id, book.BookTitle ,book.BarcodeISBN, book.PurchasePrice, book.RetailPrice);
        }

        [HttpDelete]
        public void DeleteBook(int id)
        {
            Book book = manager.GetBookById(id);
            manager.ChangeIsDelete(id, book.IsDelete);
        }

    }

    public class BookDescriptionController: ApiController
    {
        BookManager manager = new BookManager();
        [HttpGet]
        public BookFullDescription GetBookFullDescription(int id)
        {
            return manager.GetBookFullDescriptionById(id);
        }

        [HttpPost]
        public void CreateBookDescription([FromBody] string JsonBookDesc)
        {
            BookFullDescription fullDescription = JsonConvert.DeserializeObject<BookFullDescription>(JsonBookDesc);
            manager.CreateBookDescription(fullDescription);
        }

        [HttpPut]
        public void EditBookFullDesc(int id, [FromBody] string JsonBookDesc)
        {
            BookFullDescription bfd = JsonConvert.DeserializeObject<BookFullDescription>(JsonBookDesc);
            manager.EditBookDesc(bfd.Id, bfd.Section, bfd.YearBookPublishing, bfd.FirstYearBookPublishing, bfd.Serie, bfd.Description, bfd.Author, bfd.Publisher, bfd.ImageUrl);
        }

    }
}
