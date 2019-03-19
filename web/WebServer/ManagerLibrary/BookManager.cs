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
        public IQueryable<Book> GetAllBook()
        {
            
                IQueryable<Book> AllBooksModel = stockDBcontext.Books.Where(i => i.IsDelete == false);
                return AllBooksModel;
            
        }

        



        public void EditBook(int id, string BookTitle, string BarcodeISBN, decimal PurchasePrice, decimal RetailPrice, string Section,
            string YearBookPublishing, string FirstYearBookPublishing, string Serie, string Description, string Author, string Publisher,
            string ImageUrl)
        {
           
                Book book = stockDBcontext.Books.Find(id);
                BookFullDescription bookFullDescription = stockDBcontext.BookFullDescriptions.Find(book.Id);

                book.BookTitle = BookTitle;
                book.BarcodeISBN = BarcodeISBN;
                book.PurchasePrice = PurchasePrice;
                book.RetailPrice = RetailPrice;


                //book.BookGenres.Add(bookGenre);

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

        public void ChangeIsDelete(int id, bool IsDelete)
        {
          
                Book book = stockDBcontext.Books.Find(id);

                book.IsDelete = (IsDelete == true) ? book.IsDelete = false : book.IsDelete = true;
                //switch(IsDelete)
                // {
                //     case true:
                //         book.IsDelete = false;
                //         break;
                //     case false:
                //         book.IsDelete = true;
                //         break;
                // }
                stockDBcontext.SaveChanges();
           
        }


    }
}
