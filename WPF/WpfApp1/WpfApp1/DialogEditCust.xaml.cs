using Newtonsoft.Json;
using StockEntModelLibrary.CustumerEnt;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для DialogEditCust.xaml
    /// </summary>
    public partial class DialogEditCust : Window
    {
        MainWindow mw = new MainWindow();
        HttpClient client = new HttpClient();
        Custumer custumer;
        CustumerDescription custumerDescription;
        public DialogEditCust(int id)
        {
            InitializeComponent();
            try
            {
                var responceById = client.GetAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.Custumer.ToString() + "/" + id).Result;//конектимся и получаем по ид кастомера
                var jsonFromResponceById = responceById.Content.ReadAsStringAsync().Result;//здесь полеченное делается JSON-ом 
                custumer = JsonConvert.DeserializeObject<Custumer>(jsonFromResponceById);//здесь JSON превращается непосредственно в Кастомера
            }
            catch (Exception e)
            {
                MessageBox.Show("Error by find Edit Cust: " + e.ToString());
            }

            try
            {
                var responceDescById = client.GetAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.CustumerDesription.ToString() + "/" + id).Result;//конектимся и получаем по ид кастомераОписание
                var jsonFromResponceDescById = responceDescById.Content.ReadAsStringAsync().Result;//здесь полеченное делается JSON-ом 
                custumerDescription = JsonConvert.DeserializeObject<CustumerDescription>(jsonFromResponceDescById);//здесь JSON превращается непосредственно в КастомераОписание
            }
            catch (Exception e)
            {
                MessageBox.Show("Error by find Edit CustDesc: " + e.ToString());
            }

            if (custumerDescription == null)
            {
                custumerDescription = new CustumerDescription();
                custumerDescription.Address = "";
                custumerDescription.Email = "";
                custumerDescription.FullName = "";
                custumerDescription.Phone = "";
                custumerDescription.Id = id;
            }

            tbCustTitle.Text = custumer.CustumerTitle;
            tbCustFullName.Text = custumerDescription.FullName;
            tbCustAddress.Text = custumerDescription.Address;
            tbCustPhone.Text = custumerDescription.Phone;
            tbCustEmail.Text = custumerDescription.Email;
            if (custumer.BuyerTrue_SuplierFalse == true)
            { rbBuyer.IsChecked = true; }
            else { rbSup.IsChecked = true; }




            
        }


    }
}
