using ManagerLibrary.UnitedModels;
using StockEntModelLibrary;
using StockEntModelLibrary.BookEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary
{
   public class BookManager
    {
        private StockDBcontext stockDBcontext = new StockDBcontext();

        public void CreateBook(Book book)
        {
            stockDBcontext.Books.Add(book);
            stockDBcontext.SaveChanges();
        }

        public void CreateBookDescription(BookFullDescription fullDescription )
        {
            stockDBcontext.BookFullDescriptions.Add(fullDescription);
            stockDBcontext.SaveChanges();
        }

        public void SpecialCreateBook(BookAndDesc bookAndDesc)
        {
            Book book = bookAndDesc.book;
            BookFullDescription bookFullDescription = bookAndDesc.bookFullDescription;
            stockDBcontext.Books.Add(book);
            stockDBcontext.SaveChanges();
            stockDBcontext.BookFullDescriptions.Add(bookFullDescription);
            stockDBcontext.SaveChanges();

        }

        //public void CreateBook(string BookTitle, string BarcodeISBN, int count, decimal PurchasePrice, decimal RetailPrice, string Section,
        //    string YearBookPublishing, string FirstYearBookPublishing, string Serie, string Description, string Author, string Publisher,
        //    string ImageUrl
        //    )
        //{
           
        //        // BookGenre bookGenre = stockDBcontext.BookGenres.Find(BookGenreId);
        //        Book book = new Book();
        //        BookFullDescription bookFullDescription = new BookFullDescription();

        //        book.BookTitle = BookTitle;
        //        book.BarcodeISBN = BarcodeISBN;
        //        book.Count = count;
        //        book.PurchasePrice = PurchasePrice;
        //        book.RetailPrice = RetailPrice;


        //        //book.BookGenres.Add(bookGenre);
        //        bookFullDescription.Id = book.Id;
        //        bookFullDescription.YearBookPublishing = YearBookPublishing;
        //        bookFullDescription.FirstYearBookPublishing = FirstYearBookPublishing;
        //        bookFullDescription.Serie = Serie;
        //        bookFullDescription.Description = Description;
        //        bookFullDescription.Author = Author;
        //        bookFullDescription.Publisher = Publisher;
        //        bookFullDescription.ImageUrl = ImageUrl;
        //        bookFullDescription.Section = Section;
        //        stockDBcontext.Books.Add(book);
        //        stockDBcontext.BookFullDescriptions.Add(bookFullDescription);
        //        stockDBcontext.SaveChanges();
           
        //}

        public  Book GetBookById(int id)
        {
            Book book = stockDBcontext.Books.Find(id);
                return book;
            
        }
        public BookFullDescription GetBookFullDescriptionById(int id)
        {
            BookFullDescription bookFull = stockDBcontext.BookFullDescriptions.Find(id);
            return bookFull;
        }

        public IEnumerable<Book> GetAllBook()
        {
            //IQueryable<Book> model = stockDBcontext.Books.Include("fullDescription").Where(i => i.IsDelete == false);
            IEnumerable<Book> AllBooksModel = stockDBcontext.Books.Where(i => i.IsDelete == false);
            //stockDBcontext.Dispose();
                return AllBooksModel;
            
        }
        public IEnumerable<BookFullDescription> GetAllBookDesc()
        {
            IEnumerable<BookFullDescription> model = stockDBcontext.BookFullDescriptions;
            // IQueryable<Book> AllBooksModel = stockDBcontext.Books.Where(i => i.IsDelete == false);
            return model;

        }

        public  void EditBook(int id, BookAndDesc bookAndDesc)
        {
            Book book = stockDBcontext.Books.Find(id);
            BookFullDescription bookFullDescription = stockDBcontext.BookFullDescriptions.Find(id);

            Book editedBook = bookAndDesc.book;
            BookFullDescription editedBookDesc = bookAndDesc.bookFullDescription;

            book.BarcodeISBN = editedBook.BarcodeISBN;
            book.BookTitle = editedBook.BookTitle;
            book.PurchasePrice = editedBook.PurchasePrice;
            book.RetailPrice = editedBook.RetailPrice;

            bookFullDescription.Author = editedBookDesc.Author;
            bookFullDescription.Description = editedBookDesc.Description;
            bookFullDescription.FirstYearBookPublishing = editedBookDesc.FirstYearBookPublishing;
            bookFullDescription.ImageUrl = editedBookDesc.ImageUrl;
            bookFullDescription.Publisher = editedBookDesc.Publisher;
            bookFullDescription.Section = editedBookDesc.Section;
            bookFullDescription.Serie = editedBookDesc.Serie;
            bookFullDescription.YearBookPublishing = editedBookDesc.YearBookPublishing;


            stockDBcontext.SaveChanges();
            //Book book = stockDBcontext.Books.Find(id);
              
            //    book.BookTitle = BookTitle;
            //    book.BarcodeISBN = BarcodeISBN;
            //    book.PurchasePrice = PurchasePrice;
            //    book.RetailPrice = RetailPrice;
            //    stockDBcontext.SaveChanges();
        }

        public void EditBookDesc(int id, string Section,
            string YearBookPublishing, string FirstYearBookPublishing, string Serie, string Description, string Author, string Publisher,
            string ImageUrl)
        {
            BookFullDescription bookFullDescription = stockDBcontext.BookFullDescriptions.Find(id);

            bookFullDescription.YearBookPublishing = YearBookPublishing;
            bookFullDescription.FirstYearBookPublishing = FirstYearBookPublishing;
            bookFullDescription.Serie = Serie;
            bookFullDescription.Description = Description;
            bookFullDescription.Author = Author;
            bookFullDescription.Publisher = Publisher;
            bookFullDescription.ImageUrl = ImageUrl;
            bookFullDescription.Section = Section;
            stockDBcontext.SaveChanges();
        }

        public ErrorsMessage ChangeIsDelete(int id)
        {
            ErrorsMessage message = new ErrorsMessage();
            Book book = stockDBcontext.Books.Find(id);
            try
            {
                book.IsDelete = (book.IsDelete == true) ? book.IsDelete = false : book.IsDelete = true;

                stockDBcontext.SaveChanges();
                message.boolen = 1;
                message.message = "OK";
            }
            catch(Exception ex)
            {
                message.boolen = 0;
                message.message = "False on delete Book: "+ex;
                return message;
            }
            return message;
        }
    }
}
