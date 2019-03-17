using Newtonsoft.Json;
using StockEntModelLibrary;
using StockEntModelLibrary.CustumerEnt;
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
        public MainWindow()
        {
            InitializeComponent();
            string APP_CONNECT = "http://localhost:47914/";
            string API_CUSTUMER = "api/Custumer";
            var client = new HttpClient();
          
            //StockDBcontext ctx = new StockDBcontext();
            //ctx.Custumers.Load();
            Custumer custumer = new Custumer();
            custumer.CustumerTitle = "SomeShit";
            custumer.BuyerTrue_SuplierFalse = true;
            custumer.Balance = 0;
            var jjson = JsonConvert.SerializeObject(custumer);
            var resp = client.PostAsJsonAsync(APP_CONNECT + API_CUSTUMER, jjson);


            // var responce2 = client.GetAsync(APP_CONNECT+API_CUSTUMER).Result;



            var responce = client.GetAsync(APP_CONNECT + API_CUSTUMER).Result;
            var json = responce.Content.ReadAsStringAsync().Result;
            List<Custumer> p = JsonConvert.DeserializeObject<List<Custumer>>(json);
            dataGrid1.ItemsSource = p;
            //var jj = JsonConvert.SerializeObject(p);
           
        }
    }

}
