using StockEntModelLibrary.BookEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
   public class loadManager
    {
        public void LoadBookDesc(List<Book> books, List<BookFullDescription> bookFullDescriptions, int BookId)
        {
            MainWindow md = new MainWindow();

            md.tbBookTitle.Text = "";
            md.tbBarcode.Text = "";
            md.tbFirstYear.Text = "";
            md.tbLastYear.Text = "";
            md.tbSeria.Text = "";
            md.tbSection.Text = "";
            md.tbAuthor.Text = "";
            md.tbPublisher.Text = "";
            md.tbPurchasePrice.Text = "";
            md.tbRetailPrice.Text = "";
            md.tbDescription.Text = "";
            md.imgDesc.Background = null;
            Book book = null;
            BookFullDescription bookFull = null;
            try
            {

                //int selectedColumn = md.dataGrid1.CurrentCell.Column.DisplayIndex;
                //var selectedCell = md.dataGrid1.SelectedCells[0];
                //var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                //if (cellContent is TextBlock)
                //{
                //    StringBookId = (cellContent as TextBlock).Text;
                //}
                //int BookId = Convert.ToInt32(StringBookId);

                foreach (var item in books)
                {
                    if (item.Id == BookId)
                    {
                        book = item;


                    }
                }
                foreach (var item in bookFullDescriptions)
                {
                    if (item.Id == BookId)
                        bookFull = item;
                }
                md.tbBookTitle.Text = book.BookTitle;
                md.tbBarcode.Text = book.BarcodeISBN;
                md.tbFirstYear.Text = bookFull.FirstYearBookPublishing;
                md.tbLastYear.Text = bookFull.YearBookPublishing;
                md.tbSeria.Text = bookFull.Serie;
                md.tbSection.Text = bookFull.Section;
                md.tbAuthor.Text = bookFull.Author;
                md.tbPublisher.Text = bookFull.Publisher;
                md.tbPurchasePrice.Text = book.PurchasePrice.ToString();
                md.tbRetailPrice.Text = book.RetailPrice.ToString();
                md.tbDescription.Text = bookFull.Description;

                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(bookFull.ImageUrl, UriKind.RelativeOrAbsolute));
                md.imgDesc.Background = ib;

            }
            catch(Exception e)
            {
                MessageBox.Show("LoadBookDesc Error: " + e.ToString());
            }
        }
    }
}
