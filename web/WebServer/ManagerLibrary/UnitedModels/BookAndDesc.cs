using StockEntModelLibrary.BookEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLibrary.UnitedModels
{
   public class BookAndDesc
    {
        public Book book { get; set; }
        public BookFullDescription bookFullDescription { get; set; }
    }
}
