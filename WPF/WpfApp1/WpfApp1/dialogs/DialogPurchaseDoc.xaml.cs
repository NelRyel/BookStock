using StockEntModelLibrary;
using StockEntModelLibrary.BookEnt;
using StockEntModelLibrary.CustumerEnt;
using StockEntModelLibrary.Document;
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
    /// Логика взаимодействия для DialogPurchaseDoc.xaml
    /// </summary>
    public partial class DialogPurchaseDoc : Window
    {
        Custumer c;
        PurchaseDoc pd;
        List<Book> Lbooks;
        List<PurchaseDocRec> pdrs;
        DataTable dt;
        public DialogPurchaseDoc(Custumer custumer, PurchaseDoc purchaseDoc, List<Book> books, List<PurchaseDocRec> purchaseDocRecs )
        {
            if (purchaseDoc.Status == StaticDatas.DocStatuses.Проведен.ToString())
            {
                btnChangeCustumer.IsEnabled = false;
            }
            c = custumer;
            pd = purchaseDoc;
            Lbooks = books;
            pdrs = purchaseDocRecs;

            InitializeComponent();
            tbDocId.Text = purchaseDoc.Id.ToString();
            tbDate.Text = purchaseDoc.DateCreate.ToString();
            tbClientTitle.Text = custumer.CustumerTitle;
            
            dt = new DataTable();
            dt.Columns.Add("№").ReadOnly=true;
            dt.Columns.Add("Наименование");
            dt.Columns.Add("кол-во");
            dt.Columns.Add("Закупочная Цена");
            dt.Columns.Add("Розничная Цена");
            dt.Columns.Add("Сумма").ReadOnly=true;
            string bookTitle = "";
            foreach (var item in purchaseDocRecs)
            {
                foreach (var item1 in books)
                {
                    if (item.BookId == item1.Id)
                    {
                        bookTitle = item1.BookTitle;
                    }
                }

                dt.Rows.Add(item.LineNumber,bookTitle,item.Count,item.PurchasePrice,item.RetailPrice,item.Count*item.PurchasePrice);
            }
            dataGridPurchaseDoc.ItemsSource = dt.DefaultView;
          
            //foreach (var item in purchaseDocRecs)
            //{
            //    TextBlock tb = new TextBlock();
            //    tb.Text = item.Id.ToString();
            //    tb.
            //}



        }

        public void reSum()
        {
           for(int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
            {
                decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                //int z = (int.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                //MessageBox.Show(x.ToString());
                //MessageBox.Show(y.ToString());
                pdrs[i].Count = Convert.ToInt32(x);
                pdrs[i].PurchasePrice = y;
                pdrs[i].SumPrice = x * y; 
            }
            dataGridPurchaseDoc.ItemsSource = null;
            
            dt.Rows.Clear();
            //dt = new DataTable();
            //dt.Columns.Add("№").ReadOnly = true;
            //dt.Columns.Add("Наименование");
            //dt.Columns.Add("кол-во");
            //dt.Columns.Add("Закупочная Цена");
            //dt.Columns.Add("Розничная Цена");
            //dt.Columns.Add("Сумма").ReadOnly = true;
            string bookTitle = "";
            foreach (var item in pdrs)
            {
                foreach (var item1 in Lbooks)
                {
                    if (item.BookId == item1.Id)
                    {
                        bookTitle = item1.BookTitle;
                    }
                }

                dt.Rows.Add(item.LineNumber, bookTitle, item.Count, item.PurchasePrice, item.RetailPrice, item.Count * item.PurchasePrice);
            }
            dataGridPurchaseDoc.ItemsSource = dt.DefaultView;
        }

        public void TrueVis_FalseHide(bool b)
        {
            btnChangeCustumer.Visibility = (b == true) ? Visibility.Visible : Visibility.Hidden;

        }

        public void CntSumm(object sender, RoutedEventArgs e)
        {
            decimal sum = 0;
            int CountSum = 0;
            //reSum();
            for (int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
            {
                decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                //int z = (int.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                //MessageBox.Show(x.ToString());
                //MessageBox.Show(y.ToString());
                pdrs[i].Count = Convert.ToInt32(x);
                pdrs[i].PurchasePrice = y;
                pdrs[i].SumPrice = x * y;
                sum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                CountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            }
            dataGridPurchaseDoc.ItemsSource = null;

            dt.Rows.Clear();
            //dt = new DataTable();
            //dt.Columns.Add("№").ReadOnly = true;
            //dt.Columns.Add("Наименование");
            //dt.Columns.Add("кол-во");
            //dt.Columns.Add("Закупочная Цена");
            //dt.Columns.Add("Розничная Цена");
            //dt.Columns.Add("Сумма").ReadOnly = true;
            string bookTitle = "";
            foreach (var item in pdrs)
            {
                foreach (var item1 in Lbooks)
                {
                    if (item.BookId == item1.Id)
                    {
                        bookTitle = item1.BookTitle;
                    }
                }

                dt.Rows.Add(item.LineNumber, bookTitle, item.Count, item.PurchasePrice, item.RetailPrice, item.Count * item.PurchasePrice);
            }
            dataGridPurchaseDoc.ItemsSource = dt.DefaultView;

            labelDocSum.Content = sum.ToString();
            labelSumCount.Content = CountSum.ToString();
        }

        private void BtnDiscount_Click(object sender, RoutedEventArgs e)
        {
            decimal discount = Convert.ToDecimal( tbDiscount.Text);



            decimal sum = 0;
            int CountSum = 0;
            //reSum();
            for (int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
            {
                decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                decimal discountC = y-((discount * y) / 100);
                //int z = (int.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                //MessageBox.Show(x.ToString());
                //MessageBox.Show(y.ToString());
                pdrs[i].Count = Convert.ToInt32(x);
                pdrs[i].PurchasePrice = discountC;
                pdrs[i].SumPrice = x * discountC;
                sum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                CountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            }
            dataGridPurchaseDoc.ItemsSource = null;

            dt.Rows.Clear();
            //dt = new DataTable();
            //dt.Columns.Add("№").ReadOnly = true;
            //dt.Columns.Add("Наименование");
            //dt.Columns.Add("кол-во");
            //dt.Columns.Add("Закупочная Цена");
            //dt.Columns.Add("Розничная Цена");
            //dt.Columns.Add("Сумма").ReadOnly = true;
            string bookTitle = "";
            foreach (var item in pdrs)
            {
                foreach (var item1 in Lbooks)
                {
                    if (item.BookId == item1.Id)
                    {
                        bookTitle = item1.BookTitle;
                    }
                }

                dt.Rows.Add(item.LineNumber, bookTitle, item.Count, item.PurchasePrice, item.RetailPrice, item.Count * item.PurchasePrice);
            }
            dataGridPurchaseDoc.ItemsSource = dt.DefaultView;

            labelDocSum.Content = sum.ToString();
            labelSumCount.Content = CountSum.ToString();
        }
    }
}
