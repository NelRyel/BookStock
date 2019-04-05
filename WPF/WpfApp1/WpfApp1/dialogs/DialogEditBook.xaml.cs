using Newtonsoft.Json;
using StockEntModelLibrary.BookEnt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1.dialogs
{
    /// <summary>
    /// Логика взаимодействия для DialogEditBook.xaml
    /// </summary>
    public partial class DialogEditBook : Window
    {
        MainWindow mw = new MainWindow();
        HttpClient client = new HttpClient();
        Book book;
        BookFullDescription bookFullDescription;

        public DialogEditBook(int id)
        {
            InitializeComponent();
            try
            {
                var respById = client.GetAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.Book.ToString() + "/" + id).Result;
                var jsonRespById = respById.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<Book>(jsonRespById);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error by find edit book: " + ex);
            }
            try
            {
                var respByIdDesc = client.GetAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.BookDescription.ToString() + "/" + id).Result;
                var jsonRespDesc = respByIdDesc.Content.ReadAsStringAsync().Result;
                bookFullDescription = JsonConvert.DeserializeObject<BookFullDescription>(jsonRespDesc);

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error by find edit book desc: " + ex);
            }

            tbBookTitle.Text = book.BookTitle;
            tbBarcode.Text = book.BarcodeISBN;
            tbPurchasePrice.Text = book.PurchasePrice.ToString();
            tbRetailPrice.Text = book.RetailPrice.ToString();
            tbYearFirstPubl.Text = bookFullDescription.FirstYearBookPublishing;
            tbYearLastPubl.Text = bookFullDescription.YearBookPublishing;
            tbSerie.Text = bookFullDescription.Serie;
            tbSection.Text = bookFullDescription.Section;
            tbAuthor.Text = bookFullDescription.Author;
            tbPublisher.Text = bookFullDescription.Publisher;
            tbUrlImg.Text = bookFullDescription.ImageUrl;
            tbDesc.Text = bookFullDescription.Description;

        }

        private void TbPurchasePrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal x = Convert.ToDecimal(tbSum.Text);
            decimal Pprice = Convert.ToDecimal(tbPurchasePrice.Text);
            decimal RetailPrice = x * Pprice;
            tbRetailPrice.Text = RetailPrice.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                 book.BookTitle = tbBookTitle.Text;
                book.BarcodeISBN =  tbBarcode.Text;
                book.PurchasePrice =  Convert.ToDecimal( tbPurchasePrice.Text);
                book.RetailPrice = Convert.ToDecimal(tbRetailPrice.Text);
                bookFullDescription.FirstYearBookPublishing = tbYearFirstPubl.Text;
                bookFullDescription.YearBookPublishing = tbYearLastPubl.Text;
                bookFullDescription.Serie = tbSerie.Text;
                bookFullDescription.Section = tbSection.Text;
                bookFullDescription.Author = tbAuthor.Text;
                bookFullDescription.Publisher = tbPublisher.Text;
                bookFullDescription.ImageUrl = tbUrlImg.Text;
                bookFullDescription.Description = tbDesc.Text;

                BookAndDesc bookAndDesc = new BookAndDesc();
                bookAndDesc.book = book;
                bookAndDesc.bookFullDescription = bookFullDescription;

                var jsonSpecial = JsonConvert.SerializeObject(bookAndDesc);
                client.PutAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.Book.ToString() + "/" + book.Id, jsonSpecial);

                DialogResult = true;
                Close();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
