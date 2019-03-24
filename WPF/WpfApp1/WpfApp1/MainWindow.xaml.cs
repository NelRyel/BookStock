﻿using ManagerWpfLibrary;
using Newtonsoft.Json;
using StockEntModelLibrary;
using StockEntModelLibrary.BookEnt;
using StockEntModelLibrary.CustumerEnt;
using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Book> books;
        List<BookFullDescription> bookFullDescriptions;
        List<Custumer> custumers;
        List<SaleDoc> saleDocs;
        List<PurchaseDoc> purchaseDocs;
        List<SaleDocRec> saleDocRecs;
        List<PurchaseDocRec> purchaseDocRecs;
        string APP_CONNECT = "http://localhost:47914/api/";
        HttpClient client = new HttpClient();
        CustumerManager CustManager = new CustumerManager();
        BookManager BookManager = new BookManager();
        public enum API_CON_TYPE
        {
            Custumer,
            CustumerDesription,
            Book,
            BookDescription,
            SaleDoc,
            PurchaseDoc,
            PurchaseDocRec,
            SaleDocRec
        }

        public MainWindow()
        {
            LoadDatas();
            InitializeComponent();

            //string API_CUSTUMER = "Custumer";



            //StockDBcontext ctx = new StockDBcontext();
            //ctx.Custumers.Load();
            //Custumer custumer = new Custumer();
            //custumer.CustumerTitle = "SomeShit";
            //custumer.BuyerTrue_SuplierFalse = true;
            //custumer.Balance = 0;

            //var responceById = client.GetAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString() +"/"+"5").Result;//конектимся и получаем по ид кастомера
            //var jsonFromResponceById = responceById.Content.ReadAsStringAsync().Result;//здесь полеченное делается JSON-ом 
            //   Custumer custumerById = JsonConvert.DeserializeObject<Custumer>(jsonFromResponceById);//здесь JSON превращается непосредственно в Кастомера
            //custumerById.CustumerTitle = "New Shit 33";

            //var respCustDesc = client.GetAsync(APP_CONNECT + API_CON_TYPE.CustumerDesription.ToString()+custumerById.Id.ToString()).Result;
            //var jsonFromRespCustDesc = respCustDesc.Content.ReadAsStringAsync().Result;
            //CustumerDescription cd = JsonConvert.DeserializeObject<CustumerDescription>(jsonFromRespCustDesc);


            //// UpdateCust(custumerById); 

            //var jjson = JsonConvert.SerializeObject(custumerById);//здесь Кастумер перегоняется в JSON 
            //var jjsonDesc = JsonConvert.SerializeObject(cd);
            ////var resp = client.PostAsJsonAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString(), jjson);
            //client.PutAsJsonAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString() + "/" + "5", jjson); //здесь JSON-Кастумер передаётся в АПИ-Контроллер


            //StockDBcontext stockDBcontext = new StockDBcontext();
            //// var responce2 = client.GetAsync(APP_CONNECT+API_CUSTUMER).Result;
            //IEnumerable<SaleDocRec> saleDocRecs = stockDBcontext.SaleDocRecs;
            //string jjj = JsonConvert.SerializeObject(saleDocRecs);

            //var responce = client.GetAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString()).Result;
            //var json = responce.Content.ReadAsStringAsync().Result;
            //List<Custumer> p = JsonConvert.DeserializeObject<List<Custumer>>(json);
            //dataGrid1.ItemsSource = p;
            //var jj = JsonConvert.SerializeObject(p);

        }

        static async void UpdateCust(Custumer custumerById)
        {
            string APP_CONNECT = "http://localhost:47914/api/";
            var client = new HttpClient();
            var jjson = JsonConvert.SerializeObject(custumerById);
            var resp = await client.PutAsJsonAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString() + "/" + "5", jjson); //здесь JSON-Кастумер передаётся в АПИ-Контроллер
        }
     
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //dataGrid1.Columns.Clear();
            //dataGrid1.ItemsSource = null;
            //var responce = client.GetAsync(APP_CONNECT + API_CON_TYPE.Book.ToString()).Result;
            //var jsonResp = responce.Content.ReadAsStringAsync().Result;
            //books = JsonConvert.DeserializeObject<List<Book>>(jsonResp);

            var dt = BookManager.LoadBook(books, bookFullDescriptions);
            dataGrid1.ItemsSource = dt.DefaultView;

        }
        public void LoadDatas()
        {
            try
            {
                var responceBook = client.GetAsync(APP_CONNECT + API_CON_TYPE.Book.ToString()).Result;
                var jsonRespBook = responceBook.Content.ReadAsStringAsync().Result;
                books = JsonConvert.DeserializeObject<List<Book>>(jsonRespBook);
            }
            catch
                { }

            try
            {
                var respBooDesk = client.GetAsync(APP_CONNECT + API_CON_TYPE.BookDescription.ToString()).Result;
                var jsonRespBookDesk = respBooDesk.Content.ReadAsStringAsync().Result;
                bookFullDescriptions = JsonConvert.DeserializeObject<List<BookFullDescription>>(jsonRespBookDesk);
            }
            catch
            {

            }

            try
            {
                var responceCust = client.GetAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString()).Result;
                var jsonRespCust = responceCust.Content.ReadAsStringAsync().Result;
                custumers = JsonConvert.DeserializeObject<List<Custumer>>(jsonRespCust);
            }
            catch
            {

            }
            try
            {
                var respSaleDoc = client.GetAsync(APP_CONNECT + API_CON_TYPE.SaleDoc.ToString()).Result;
                var jsonRespSaleDoc = respSaleDoc.Content.ReadAsStringAsync().Result;
                saleDocs = JsonConvert.DeserializeObject<List<SaleDoc>>(jsonRespSaleDoc);
            }
            catch (Exception e)
            {
                saleDocs = new List<SaleDoc>();
                SaleDoc c = new SaleDoc();
                c.Id = 0;
                saleDocs.Add(c);
            }
            try { 
            var respPurchaseDoc = client.GetAsync(APP_CONNECT + API_CON_TYPE.PurchaseDoc.ToString()).Result;
                var jsonRespPurchaseDoc = respPurchaseDoc.Content.ReadAsStringAsync().Result;
                purchaseDocs = JsonConvert.DeserializeObject<List<PurchaseDoc>>(jsonRespPurchaseDoc);
            }
            catch
            {

            }
           
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            DataTable dt = CustManager.LoadCustemer(custumers);
            dataGrid1.ItemsSource = dt.DefaultView;
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbBookTitle.Text ="";
            tbBarcode.Text = "";
            tbFirstYear.Text = "";
            tbLastYear.Text = "";
            tbSeria.Text = "";
            tbSection.Text = "";
            tbAuthor.Text = "";
            tbPublisher.Text = "";
            tbPurchasePrice.Text = "";
            tbRetailPrice.Text = "";
            tbDescription.Text = "";
            imgDesc.Background = null;
            string s="";
            Book book = null;
            BookFullDescription bookFull = null;
            try
            {

                int selectedColumn = dataGrid1.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGrid1.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);
                if (cellContent is TextBlock)
                {
                    s = (cellContent as TextBlock).Text;
                }
                int ass = Convert.ToInt32(s);
             
                foreach (var item in books)
                {
                    if (item.Id == ass)
                    {
                        book = item;
                        

                    }
                }
                foreach (var item in bookFullDescriptions)
                {
                    if (item.Id == ass)
                        bookFull = item;
                }
                tbBookTitle.Text = book.BookTitle;
                tbBarcode.Text = book.BarcodeISBN;
                tbFirstYear.Text = bookFull.FirstYearBookPublishing;
                tbLastYear.Text = bookFull.YearBookPublishing;
                tbSeria.Text = bookFull.Serie;
                tbSection.Text = bookFull.Section;
                tbAuthor.Text = bookFull.Author;
                tbPublisher.Text = bookFull.Publisher;
                tbPurchasePrice.Text = book.PurchasePrice.ToString();
                tbRetailPrice.Text = book.RetailPrice.ToString();
                tbDescription.Text = bookFull.Description;

                ImageBrush ib = new ImageBrush();
                ib.ImageSource = new BitmapImage(new Uri(bookFull.ImageUrl, UriKind.RelativeOrAbsolute));
                imgDesc.Background = ib;

            }
            catch
            {

            }
        }
    }

}
