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

         
            tbCustTitle.Text = custumer.CustumerTitle;
            tbCustFullName.Text = custumerDescription.FullName;
            tbCustAddress.Text = custumerDescription.Address;
            tbCustPhone.Text = custumerDescription.Phone;
            tbCustEmail.Text = custumerDescription.Email;
            if (custumer.BuyerTrue_SuplierFalse == true)
            { rbBuyer.IsChecked = true; }
            else { rbSup.IsChecked = true; }
        }

        private void BtnSaveCust_Click(object sender, RoutedEventArgs e)
        {
            
            //CustumerDescription custumerDescription = new CustumerDescription();
            try
            {
                custumer.CustumerTitle = tbCustTitle.Text;
                custumer.BuyerTrue_SuplierFalse = (rbBuyer.IsChecked == true) ? true : false;

              //  var jsonEditedCust = JsonConvert.SerializeObject(custumer);
               // client.PutAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.Custumer.ToString()+"/"+custumer.Id, jsonEditedCust);

                custumerDescription.FullName = tbCustFullName.Text;
                custumerDescription.Address = tbCustAddress.Text;
                custumerDescription.Phone = tbCustPhone.Text;
                custumerDescription.Email = tbCustEmail.Text;

              //  var jsonEditedDesc = JsonConvert.SerializeObject(custumerDescription);
              //  client.PutAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.CustumerDesription.ToString()+"/"+custumerDescription.Id, jsonEditedCust);

                custAndDesc custAndDesc = new custAndDesc();
                custAndDesc.custumer = custumer;
                custAndDesc.custumerDescription = custumerDescription;

                var jsonSpecial = JsonConvert.SerializeObject(custAndDesc);
                client.PutAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.SpecialCustumer.ToString() + "/" + custumer.Id, jsonSpecial);


                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }


        }
    }
}
