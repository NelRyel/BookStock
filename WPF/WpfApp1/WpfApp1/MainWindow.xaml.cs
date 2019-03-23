﻿using Newtonsoft.Json;
using StockEntModelLibrary;
using StockEntModelLibrary.BookEnt;
using StockEntModelLibrary.CustumerEnt;
using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
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
        List<Custumer> custumers;
        string APP_CONNECT = "http://localhost:47914/api/";
        HttpClient client = new HttpClient();
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
        class s{ //не ну это хрень какаято
            public int id { get; set; }
            public string name { get; set; }
            public decimal price { get; set; }

            }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //dataGrid1.Columns.Clear();
            //dataGrid1.ItemsSource = null;
            //var responce = client.GetAsync(APP_CONNECT + API_CON_TYPE.Book.ToString()).Result;
            //var jsonResp = responce.Content.ReadAsStringAsync().Result;
            //books = JsonConvert.DeserializeObject<List<Book>>(jsonResp);
            List<s> listS = new List<s>();
            s ss = new s();
            foreach (var item in books)
            {
                ss.id = item.Id;
                ss.name = item.BookTitle;
                ss.price = item.RetailPrice;
                listS.Add(ss);
            }

            dataGrid1.ItemsSource = listS;

        }
        public void LoadDatas()
        {
            var responceBook = client.GetAsync(APP_CONNECT + API_CON_TYPE.Book.ToString()).Result;
            var jsonRespBook = responceBook.Content.ReadAsStringAsync().Result;
            books = JsonConvert.DeserializeObject<List<Book>>(jsonRespBook);

            var responceCust = client.GetAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString()).Result;
            var jsonRespCust = responceCust.Content.ReadAsStringAsync().Result;
            custumers = JsonConvert.DeserializeObject<List<Custumer>>(jsonRespCust);

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            //dataGrid1.Columns.Clear();
            //dataGrid1.ItemsSource = null;
            //var responce = client.GetAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString()).Result;
            //var jsonResp = responce.Content.ReadAsStringAsync().Result;
            //custumers = JsonConvert.DeserializeObject<List<Custumer>>(jsonResp);
            dataGrid1.ItemsSource = custumers;

        }
    }

}
