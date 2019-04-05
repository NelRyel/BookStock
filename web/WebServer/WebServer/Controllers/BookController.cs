using ManagerLibrary;
using ManagerLibrary.UnitedModels;
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
        public void CreateBook([FromBody] string JsonBookSpecial)
        {
            BookAndDesc bookAndDesc = JsonConvert.DeserializeObject<BookAndDesc>(JsonBookSpecial);
            manager.SpecialCreateBook(bookAndDesc);

            //Book book = JsonConvert.DeserializeObject<Book>(JsonBook);
            //manager.CreateBook(book);
        }

        [HttpPut]
        public void EditBook(int id, [FromBody] string jsonBookAndDesc)//редактировать черезе Пут
        {
            //Book book = JsonConvert.DeserializeObject<Book>(JsonBook);
            BookAndDesc bookAndDesc = JsonConvert.DeserializeObject<BookAndDesc>(jsonBookAndDesc);
            manager.EditBook(id, bookAndDesc);
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
        public IEnumerable<BookFullDescription> GetBookFullDescriptions()
        {
            return manager.GetAllBookDesc();
        }

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
