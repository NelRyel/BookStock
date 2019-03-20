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
            CustumerDesription,
            Book,
            BookDescription

        }
        public MainWindow()
        {
           
            InitializeComponent();
            string APP_CONNECT = "http://localhost:47914/api/";
            string API_CUSTUMER = "Custumer";
        
            var client = new HttpClient();

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
            


            // var responce2 = client.GetAsync(APP_CONNECT+API_CUSTUMER).Result;



            var responce = client.GetAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString()).Result;
            var json = responce.Content.ReadAsStringAsync().Result;
            List<Custumer> p = JsonConvert.DeserializeObject<List<Custumer>>(json);
            dataGrid1.ItemsSource = p;
            //var jj = JsonConvert.SerializeObject(p);
           
        }

         static async void UpdateCust(Custumer custumerById)
        {
            string APP_CONNECT = "http://localhost:47914/api/";
            var client = new HttpClient();
            var jjson = JsonConvert.SerializeObject(custumerById);
            var resp = await client.PutAsJsonAsync(APP_CONNECT + API_CON_TYPE.Custumer.ToString() + "/" + "5", jjson); //здесь JSON-Кастумер передаётся в АПИ-Контроллер
        }
    }

}
