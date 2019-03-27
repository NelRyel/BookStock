using ManagerWpfLibrary;
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
        List<CustumerDescription> custumerDescriptions;
        List<SaleDoc> saleDocs;
        List<PurchaseDoc> purchaseDocs;
        List<SaleDocRec> saleDocRecs;
        List<PurchaseDocRec> purchaseDocRecs;
        string APP_CONNECT = "http://localhost:47914/api/";
        HttpClient client = new HttpClient();
        CustumerManager CustManager = new CustumerManager();
        BookManager BookManager = new BookManager();

        string rbChose=""; //нужен для проверки какой РадиоБаттон выбран 

        public enum API_CON_TYPE //выбор АпиКонтроллера
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
                var respCustDesc = client.GetAsync(APP_CONNECT + API_CON_TYPE.CustumerDesription.ToString()).Result;
                var jsonRespCustDesc = respCustDesc.Content.ReadAsStringAsync().Result;
                custumerDescriptions = JsonConvert.DeserializeObject<List<CustumerDescription>>(jsonRespCustDesc);
            }
            catch
            {
               // LoadDatas();
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
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
           
            RadioButton pressed = (RadioButton)sender;
            string check = pressed.Content.ToString();
            DataTable dt;
            //MessageBox.Show(pressed.Content.ToString());
            switch (check)
            {
                case "Номенклатура":
                    rbChose = check;
                    descStackPanel.Visibility = Visibility.Visible;
                    tbDescription.Visibility = Visibility.Visible;
                    imgDesc.Visibility = Visibility.Visible;
                    CustDescStackPanel.Visibility = Visibility.Hidden;
                    dt = BookManager.LoadBook(books, bookFullDescriptions);
                    dataGrid1.ItemsSource = dt.DefaultView;
                    break;

                case "Контрагенты":
                    rbChose = check;
                    descStackPanel.Visibility = Visibility.Hidden;
                    tbDescription.Visibility = Visibility.Hidden;
                    imgDesc.Visibility = Visibility.Hidden;
                    CustDescStackPanel.Visibility = Visibility.Visible;
                    dt = CustManager.LoadCustemer(custumers);
                    dataGrid1.ItemsSource = dt.DefaultView;
                    break;

                default: MessageBox.Show("wrong select");
                    break;

            }


        }
        private void RadioButton_Checked_1(object sender, RoutedEventArgs e) //нашел вывод в ДатаГрид симпотичней моего... может позже переделаю на него. А пока не вызываемый метод.
        {
            var s = books.Select(x => new { x.Id, x.BarcodeISBN, x.BookTitle });
            dataGrid1.ItemsSource = s.ToList();
        }

        public void DGLoadBookDesc(List<Book> books, List<BookFullDescription> bookFullDescriptions, int BookId)
        {
            descStackPanel.Visibility = Visibility.Visible;

            tbBookTitle.Text = "";
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
            catch (Exception e)
            {
                MessageBox.Show("LoadBookDesc Error: " + e.ToString());
            }
        }//это штука загружает Полное Описание в выделенные секции. Вызывается из ивента на DataGrid по клику на поле


        public void DGLoadCustDesc(List<Custumer> custumers, List<CustumerDescription> custumerDescriptions, int CustId)
        {
            tbCustTitle.Text = "";
            tbCustFullName.Text = "";
            tbCustAddress.Text = "";
            tbCustPhone.Text = "";
            tbCustEmail.Text = "";
            tbCustBalance.Text = "";
            labelCustType.Content = "";
            Custumer custumer = null;
            CustumerDescription custumerDescription = null;
            try
            {
                foreach (var item in custumers)
                {
                    if (item.Id == CustId)
                        custumer = item;
                }
                foreach (var item in custumerDescriptions)
                {
                    if (item.Id == CustId)
                        custumerDescription = item;
                }
                tbCustTitle.Text = custumer.CustumerTitle;
                tbCustFullName.Text = custumerDescription.FullName;
                tbCustAddress.Text = custumerDescription.Address;
                tbCustPhone.Text = custumerDescription.Phone;
                tbCustEmail.Text = custumerDescription.Email;
                tbCustBalance.Text = custumer.Balance.ToString();
                labelCustType.Content = (custumer.BuyerTrue_SuplierFalse)?"Покупатель":"Поставщик";
            }
            catch(Exception e)
            {
                MessageBox.Show("DGLoadCustDesc Error: " + e.ToString());
            }


        }
        public void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //--------------------------------------------------------------------------------------------------------------------------------------
            string StringBookId = "";
            int? selectedColumn = null;
            try
            {


                selectedColumn = dataGrid1.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGrid1.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringBookId = (cellContent as TextBlock).Text;
                }
                int selectedId = Convert.ToInt32(StringBookId);
                //---------------------------------------------------------------------------------------------------------------------------------------
                switch (rbChose)
                {
                    case "Номенклатура":
                        DGLoadBookDesc(books, bookFullDescriptions, selectedId);
                        break;
                    case "Контрагенты":
                        DGLoadCustDesc(custumers, custumerDescriptions, selectedId);
                        break;

                    default:
                        MessageBox.Show("wrong Chose DataGrid1_SelectionChanged");
                        break;
                }
            }
            catch
            {

            }
        }

        private void AddCustBtn_Click(object sender, RoutedEventArgs e)
        {
            tbCustTitle.Text = "";
            tbCustFullName.Text = "";
            tbCustAddress.Text = "";
            tbCustPhone.Text = "";
            tbCustEmail.Text = "";
            tbCustBalance.Text = "";
            labelCustType.Content = "";


        }
    }

}
