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
        int IdFromMain;
        public DialogCreateCustomer(int id)
        {
            IdFromMain = id+1;
            InitializeComponent();
        }

        private void BtnAddCust_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Custumer custumer = new Custumer();
                CustumerDescription custumerDescription = new CustumerDescription();
                custumer.Id = IdFromMain;
                custumer.CustumerTitle = tbCustTitle.Text;
                custumer.BuyerTrue_SuplierFalse = (rbBuyer.IsChecked == true) ? true : false;

                var jjson = JsonConvert.SerializeObject(custumer);//здесь Кастумер перегоняется в JSON 

                client.PostAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.Custumer.ToString(), jjson); //здесь JSON-Кастумер передаётся в АПИ-Контроллер

                Thread.Sleep(1000); //как то не очень надежно, но пусть пока как заглушка

               
                custumerDescription.Id = IdFromMain;
                custumerDescription.FullName = tbCustFullName.Text;
                custumerDescription.Address = tbCustAddress.Text;
                custumerDescription.Phone = tbCustPhone.Text;
                custumerDescription.Email = tbCustEmail.Text;

                var jjsonDesc = JsonConvert.SerializeObject(custumerDescription);
                client.PostAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.CustumerDesription.ToString(), jjsonDesc); //здесь JSON-Кастумер передаётся в АПИ-Контроллер
            
                DialogResult = true;
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Create Customer: " + ex);
            }
            
        }
    }
}
