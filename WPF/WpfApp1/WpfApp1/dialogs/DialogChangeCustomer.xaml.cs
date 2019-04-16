using StockEntModelLibrary.CustumerEnt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

namespace WpfApp1.dialogs
{
    /// <summary>
    /// Логика взаимодействия для DialogChangeCustomer.xaml
    /// </summary>
    public partial class DialogChangeCustomer : Window
    {
        int _id = 0;
        List<Custumer> _custumers;
        public DialogChangeCustomer(List<Custumer> custumers)
        {
            _custumers = custumers;
            InitializeComponent();
            loadCust(_custumers);


        }
        public int GetIdCust
        {
            get { return _id; }
        }

        public void loadCust(List<Custumer> custumers)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Наименование");
            foreach (var item in custumers)
            {
                dt.Rows.Add(item.Id, item.CustumerTitle);
            }
            dataGridChangeCust.ItemsSource = dt.DefaultView;
        }

        private void DataGridChangeCust_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int sId = 0;
            try
            {
                string StringCustID = "";
                var selectedColumn = dataGridChangeCust.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGridChangeCust.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringCustID = (cellContent as TextBlock).Text;
                }
                sId = Convert.ToInt32(StringCustID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error select Change Cust ID" + ex);
            }
            _id = sId;
            // TESTlabel.Content = _id;
            DialogResult = true;
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string title = tbTitleCust.Text;
            var c = _custumers.Where(i => i.CustumerTitle.ToLower().Contains(title));
            List<Custumer> cc = new List<Custumer>();
            foreach (var item in c)
            {
                cc.Add(item);
            }
            loadCust(cc);


        }
    }
}
