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
    /// 
    //class some
    //{
    //    public Custumer custumer { get; set; }
    //    public PurchaseDoc PurchaseDoc { get; set; }
    //    public List<PurchaseDocRec> purchaseDocRecs { get; set; }

    //}

    public partial class DialogPurchaseDoc : Window
    {
        Custumer _c;
        PurchaseDoc _pd;
        List<Book> _Lbooks;
        List<PurchaseDocRec> _pdrs;
        List<BookFullDescription> _bookFullDescriptions;
        DataTable dt;
        decimal sum = 0;
        int CountSum = 0;
        public DialogPurchaseDoc(Custumer custumer, PurchaseDoc purchaseDoc, List<Book> books, List<PurchaseDocRec> purchaseDocRecs, List<BookFullDescription> fullDescriptions )
        {
            InitializeComponent();
            if (purchaseDoc.Status == StaticDatas.DocStatuses.Проведен.ToString())
            {
                btnChangeCustumer.IsEnabled = false;
            }

           _c = custumer;
           _pd = purchaseDoc;
           _Lbooks = books;
            _pdrs = purchaseDocRecs;
            _bookFullDescriptions = fullDescriptions;
            LoadDatas();
            decimal sum = 0;
            int CountSum = 0;
            //reSum();
            //for (int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
            //{
            //    decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            //    decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));

            //    _pdrs[i].Count = Convert.ToInt32(x);
            //    _pdrs[i].PurchasePrice = y;
            //    _pdrs[i].SumPrice = x * y;
            //    sum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            //    CountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            //}
            //labelDocSum.Content = sum.ToString();
            //labelSumCount.Content = CountSum.ToString();

        }

        public void LoadDatas()
        {
           
            for (int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
            {
                decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));

                _pdrs[i].Count = Convert.ToInt32(x);
                _pdrs[i].PurchasePrice = y;
                _pdrs[i].SumPrice = x * y;
                sum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                CountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            }
            tbDocId.Text = _pd.Id.ToString();
            tbDate.Text = _pd.DateCreate.ToString();
            tbClientTitle.Text = _c.CustumerTitle;
            dt = new DataTable();
            dt.Columns.Add("№").ReadOnly = true;
            dt.Columns.Add("Наименование");
            dt.Columns.Add("кол-во");
            dt.Columns.Add("Закупочная Цена");
            dt.Columns.Add("Розничная Цена");
            dt.Columns.Add("Сумма").ReadOnly = true;
            string bookTitle = "";
            foreach (var item in _pdrs)
            {
                foreach (var item1 in _Lbooks)
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

        //public void reSum()
        //{
        //   for(int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
        //    {
        //        decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
        //        decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
        //        //int z = (int.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
        //        //MessageBox.Show(x.ToString());
        //        //MessageBox.Show(y.ToString());
        //        pdrs[i].Count = Convert.ToInt32(x);
        //        pdrs[i].PurchasePrice = y;
        //        pdrs[i].SumPrice = x * y; 
        //    }
        //    dataGridPurchaseDoc.ItemsSource = null;
            
        //    dt.Rows.Clear();
        //    //dt = new DataTable();
        //    //dt.Columns.Add("№").ReadOnly = true;
        //    //dt.Columns.Add("Наименование");
        //    //dt.Columns.Add("кол-во");
        //    //dt.Columns.Add("Закупочная Цена");
        //    //dt.Columns.Add("Розничная Цена");
        //    //dt.Columns.Add("Сумма").ReadOnly = true;
        //    string bookTitle = "";
        //    foreach (var item in pdrs)
        //    {
        //        foreach (var item1 in Lbooks)
        //        {
        //            if (item.BookId == item1.Id)
        //            {
        //                bookTitle = item1.BookTitle;
        //            }
        //        }

        //        dt.Rows.Add(item.LineNumber, bookTitle, item.Count, item.PurchasePrice, item.RetailPrice, item.Count * item.PurchasePrice);
        //    }
        //    dataGridPurchaseDoc.ItemsSource = dt.DefaultView;
        //}

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

                _pdrs[i].Count = Convert.ToInt32(x);
                _pdrs[i].PurchasePrice = y;
                _pdrs[i].SumPrice = x * y;
                sum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                CountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            }
            dataGridPurchaseDoc.ItemsSource = null;
            
            dt = new DataTable();
            dt.Columns.Add("№").ReadOnly = true;
            dt.Columns.Add("Наименование");
            dt.Columns.Add("кол-во");
            dt.Columns.Add("Закупочная Цена");
            dt.Columns.Add("Розничная Цена");
            dt.Columns.Add("Сумма").ReadOnly = true;
            string bookTitle = "";
            foreach (var item in _pdrs)
            {
                foreach (var item1 in _Lbooks)
                {
                    if (item.BookId == item1.Id)
                    {
                        bookTitle = item1.BookTitle;
                    }
                }

                dt.Rows.Add(item.LineNumber, bookTitle, item.Count, item.PurchasePrice, item.RetailPrice, item.Count * item.PurchasePrice);
                labelDocSum.Content = sum.ToString();
                labelSumCount.Content = CountSum.ToString();
            }
            dataGridPurchaseDoc.ItemsSource = dt.DefaultView;
            //LoadDatas();

            labelDocSum.Content = sum.ToString();
            labelSumCount.Content = CountSum.ToString();
        }

        private void BtnDiscount_Click(object sender, RoutedEventArgs e)
        {
            decimal discount = Convert.ToDecimal( tbDiscount.Text);
            decimal sum = 0;
            int CountSum = 0;
            for (int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
            {
                decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                decimal discountC = y-((discount * y) / 100);
                //int z = (int.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                //MessageBox.Show(x.ToString());
                //MessageBox.Show(y.ToString());
                _pdrs[i].Count = Convert.ToInt32(x);
                _pdrs[i].PurchasePrice = discountC;
                _pdrs[i].SumPrice = x * discountC;
                sum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                CountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            }
            dataGridPurchaseDoc.ItemsSource = null;

            dt.Rows.Clear();
            string bookTitle = "";
            foreach (var item in _pdrs)
            {
                foreach (var item1 in _Lbooks)
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

        private void BtnAddBook_Click(object sender, RoutedEventArgs e)
        {
            int BookIdFromDial=0;
            //reSum();
            DialogAddBookToDoc dialogAddBookToDoc = new DialogAddBookToDoc(_Lbooks,_bookFullDescriptions);
            if (dialogAddBookToDoc.ShowDialog() == true)
            {
                BookIdFromDial = dialogAddBookToDoc.GetIdBook;
            }
            if (BookIdFromDial == 0)
            {
                return;
            }
            Book book = _Lbooks.Find(i => i.Id == BookIdFromDial);
            int s = _pdrs.Count();
            PurchaseDocRec purchaseDocRec = new PurchaseDocRec();
            purchaseDocRec.PurchaseDocId = _pd.Id;
            purchaseDocRec.PurchasePrice = book.PurchasePrice;
            purchaseDocRec.RetailPrice = book.RetailPrice;
            purchaseDocRec.Count = 1;
            purchaseDocRec.LineNumber = s + 1;
            purchaseDocRec.BookId = book.Id;
            _pdrs.Add(purchaseDocRec);
            LoadDatas();
        }

        private void BtnChangeCustumer_Click(object sender, RoutedEventArgs e)
        {
            int custIdFromDial=0;
            MainWindow mw = new MainWindow();
            List<Custumer> custumers = mw.getCustumers;
            DialogChangeCustomer changeCustomer = new DialogChangeCustomer(custumers);
            if (changeCustomer.ShowDialog() == true)
            {
                custIdFromDial = changeCustomer.GetIdCust;
            }
            Custumer cc = custumers.Find(i=>i.Id==custIdFromDial);
            _c = cc;
            LoadDatas();

        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            unitedPurchaseDoc unitedPurchaseDoc = new unitedPurchaseDoc();
            unitedPurchaseDoc.custumer = _c;
            unitedPurchaseDoc.PurchaseDoc = _pd;
            unitedPurchaseDoc.purchaseDocRecs = _pdrs;

        }
    }
}
