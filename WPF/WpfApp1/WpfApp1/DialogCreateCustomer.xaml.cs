using Newtonsoft.Json;
using StockEntModelLibrary.CustumerEnt;
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
    /// Логика взаимодействия для DialogCreateCustomer.xaml
    /// </summary>
    public partial class DialogCreateCustomer : Window
    {
        MainWindow mw = new MainWindow();
        HttpClient client = new HttpClient();
        public DialogCreateCustomer()
        {
            InitializeComponent();
        }

        private void BtnAddCust_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Custumer custumer = new Custumer();
                CustumerDescription custumerDescription = new CustumerDescription();

                custumer.CustumerTitle = tbCustTitle.Text;
                custumer.BuyerTrue_SuplierFalse = (rbBuyer.IsChecked == true) ? true : false;

                var jjson = JsonConvert.SerializeObject(custumer);//здесь Кастумер перегоняется в JSON 

                //var resp = client.PostAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.Custumer.ToString(), jjson);
                client.PostAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.Custumer.ToString(), jjson); //здесь JSON-Кастумер передаётся в АПИ-Контроллер

                Thread.Sleep(1000);

                var responceCust = client.GetAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.Custumer.ToString()).Result;
                var jsonRespCust = responceCust.Content.ReadAsStringAsync().Result;
                var custumers = JsonConvert.DeserializeObject<List<Custumer>>(jsonRespCust);
                var cc = custumers.Select(p => p.Id).Max();
                //int custID = custumers.Select(p => p.Id).Max();

                //var responceCust = client.GetAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.CustumerForGetByName.ToString() + "/" + custumer.CustumerTitle).Result;
                //var jsonResponceCust = responceCust.Content.ReadAsStringAsync().Result;
                //var c = JsonConvert.DeserializeObject<Custumer>(jsonResponceCust);
                
                //MessageBox.Show(c.CustumerTitle + " "+ c.Id);

                //custumerDescription.Id = custID+1;
                //custumerDescription.FullName = tbCustFullName.Text;
                //custumerDescription.Address = tbCustAddress.Text;
                //custumerDescription.Phone = tbCustPhone.Text;
                //custumerDescription.Email = tbCustEmail.Text;

                //var jjsonDesc = JsonConvert.SerializeObject(custumerDescription);
                ////var respDesc = client.PostAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.CustumerDesription.ToString(), jjsonDesc);
                //client.PostAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.CustumerDesription.ToString(), jjsonDesc); //здесь JSON-Кастумер передаётся в АПИ-Контроллер
                DialogResult = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Create Customer: " + ex);
            }
            
        }
    }
}
