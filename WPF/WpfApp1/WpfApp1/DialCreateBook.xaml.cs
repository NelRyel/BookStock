using Newtonsoft.Json;
using StockEntModelLibrary.BookEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для DialCreateBook.xaml
    /// </summary>
    public partial class DialCreateBook : Window
    {
        MainWindow mw = new MainWindow();
        HttpClient client = new HttpClient();
        int IdFromMain;
        public DialCreateBook(int id)
        {
            IdFromMain = id + 1;
            InitializeComponent();
        }

        private void TbPurchasePrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal x = Convert.ToDecimal(tbSum.Text);
            decimal Pprice =Convert.ToDecimal( tbPurchasePrice.Text);
            decimal RetailPrice = x * Pprice;
            tbRetailPrice.Text = RetailPrice.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book book = new Book();
                BookFullDescription bookFullDescription = new BookFullDescription();
                book.Id = IdFromMain;
                book.BookTitle = tbBookTitle.Text;
                book.BarcodeISBN = tbBarcode.Text;
                book.Count = 0;
                book.PurchasePrice = Convert.ToDecimal( tbPurchasePrice.Text);
                book.RetailPrice = Convert.ToDecimal( tbRetailPrice.Text);

                var jsonBook = JsonConvert.SerializeObject(book);
                client.PostAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.Book.ToString(), jsonBook);
                Thread.Sleep(1000);

                bookFullDescription.Id = IdFromMain;
                bookFullDescription.YearBookPublishing = tbYearFirstPubl.Text;
                bookFullDescription.FirstYearBookPublishing = tbYearFirstPubl.Text;
                bookFullDescription.Serie = tbSerie.Text;
                bookFullDescription.Section = tbSection.Text;
                bookFullDescription.Description = tbDesc.Text;
                bookFullDescription.Author = tbAuthor.Text;
                bookFullDescription.Publisher = tbPublisher.Text;
                bookFullDescription.ImageUrl = tbUrlImg.Text;

                var jsonDesc = JsonConvert.SerializeObject(bookFullDescription);
                client.PostAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.BookDescription.ToString(), jsonDesc);

                DialogResult = true;
                Close();




            }
            catch(Exception ex)
            {
                MessageBox.Show("Error create Book: " + ex);
            }


        }
    }
}
