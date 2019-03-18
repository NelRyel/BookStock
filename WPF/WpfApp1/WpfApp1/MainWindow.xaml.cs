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
        public enum API_CON_TYPE
        {
            Custumer,
            CustumerDesription
            
        }
        public MainWindow()
        {
            InitializeComponent();
            string APP_CONNECT = "http://localhost:47914/api/";
            string API_CUSTUMER = "Custumer";
            string API_CUST_DESC = "CustumerDesription";
            var client = new HttpClient();

            //StockDBcontext ctx = new StockDBcontext();
            //ctx.Custumers.Load();
            //Custumer custumer = new Custumer();
            //custumer.CustumerTitle = "SomeShit";
            //custumer.BuyerTrue_SuplierFalse = true;
            //custumer.Balance = 0;

            var responceById = client.GetAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString() +"/"+"5").Result;
            var jsonFromResponceById = responceById.Content.ReadAsStringAsync().Result;
               Custumer custumerById = JsonConvert.DeserializeObject<Custumer>(jsonFromResponceById);
            custumerById.CustumerTitle = "New Shit";

            var respCustDesc = client.GetAsync(APP_CONNECT + API_CON_TYPE.CustumerDesription.ToString()+custumerById.Id.ToString()).Result;
            var jsonFromRespCustDesc = respCustDesc.Content.ReadAsStringAsync().Result;
            CustumerDescription cd = JsonConvert.DeserializeObject<CustumerDescription>(jsonFromRespCustDesc);




            var jjson = JsonConvert.SerializeObject(custumerById);
            var jjsonDesc = JsonConvert.SerializeObject(cd);
            //var resp = client.PostAsJsonAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString(), jjson);
            var resp = client.PutAsJsonAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString(), jjson);


            // var responce2 = client.GetAsync(APP_CONNECT+API_CUSTUMER).Result;



            var responce = client.GetAsync(APP_CONNECT + API_CUSTUMER).Result;
            var json = responce.Content.ReadAsStringAsync().Result;
            List<Custumer> p = JsonConvert.DeserializeObject<List<Custumer>>(json);
            dataGrid1.ItemsSource = p;
            //var jj = JsonConvert.SerializeObject(p);
           
        }
    }

}
