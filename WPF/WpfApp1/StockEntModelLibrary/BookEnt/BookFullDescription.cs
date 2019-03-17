using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockEntModelLibrary.BookEnt
{
    public class BookFullDescription
    {
        [Key]
        [ForeignKey("Book")]
        public int Id { get; set; }
      
        public string YearBookPublishing { get; set; }
        public string FirstYearBookPublishing { get; set; }
        public string Serie { get; set; }
        public string Section { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ImageUrl { get; set; }
        //public List<BookGenre> BookGenres { get; set; }
        //public BookFullDescription()
        //{
        //    BookGenres = new List<BookGenre>();
        //}
        public Book Book { get; set; }


    }
}
