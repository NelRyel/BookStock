using ManagerWpfLibrary;
using Microsoft.Win32;
using Newtonsoft.Json;
using StockEntModelLibrary;
using StockEntModelLibrary.BookEnt;
using StockEntModelLibrary.CustumerEnt;
using StockEntModelLibrary.Document;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
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
        DataTable dt = new DataTable();
        HttpClient client = new HttpClient();
        MainWindow mw = new MainWindow();
        decimal _sum;
        int _CountSum;
        int SelectedId;
        bool _isNew;
        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        public DialogPurchaseDoc(Custumer custumer, PurchaseDoc purchaseDoc, List<Book> books, List<PurchaseDocRec> purchaseDocRecs, List<BookFullDescription> fullDescriptions, bool IsNew )
        {
            InitializeComponent();
            if (purchaseDoc.Status == StaticDatas.DocStatuses.Проведен.ToString())
            {
                btnChangeCustumer.IsEnabled = false;
                btnAddBook.IsEnabled = false;
                btnDellBook.IsEnabled = false;
                btnDiscount.IsEnabled = false;
            }

           _c = custumer;
           _pd = purchaseDoc;
           _Lbooks = books;
            _pdrs = purchaseDocRecs;
            _bookFullDescriptions = fullDescriptions;
            _isNew = IsNew;
            LoadDatas();
            btnOk.IsEnabled = false;
        }

        //public void GodDamnCount()
        //{
        //    _sum = 0;
        //    _CountSum = 0;
        //    for (int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
        //    {
        //        decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
        //        decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));

        //        _pdrs[i].Count = Convert.ToInt32(x);
        //        _pdrs[i].PurchasePrice = y;
        //        _pdrs[i].SumPrice = x * y;
        //        _sum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
        //        _CountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
        //    }
        //    labelDocSum.Content = _sum.ToString();
        //    _pd.FullSum = _sum;
        //    labelSumCount.Content = _CountSum.ToString();
        //}

        public void LoadDatas()
        {

            dataGridPurchaseDoc.Columns.Clear();
            dataGridPurchaseDoc.ItemsSource = null;
            if (dt != null)
            {
                dt.Clear();
            }
           
            tbDocId.Text = (_isNew==false)? _pd.Id.ToString(): "null";
            tbDate.Text =(_isNew == false)? _pd.DateCreate.ToString() : DateTime.Now.ToString(); 
            tbClientTitle.Text = _c.CustumerTitle;
            //dt = new DataTable();
            if (dt.Columns.Count==0)
            {
                dt.Columns.Add("№").ReadOnly = true;
                dt.Columns.Add("Наименование").ReadOnly = true;
                dt.Columns.Add("кол-во");
                dt.Columns.Add("Закупочная Цена");
                dt.Columns.Add("Розничная Цена");
                dt.Columns.Add("Сумма").ReadOnly = true;
                dt.Columns.Add("id").ReadOnly = true;
            }
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

                dt.Rows.Add(item.LineNumber, bookTitle, item.Count, item.PurchasePrice, item.RetailPrice, item.Count * item.PurchasePrice, item.Id);
            }
            dataGridPurchaseDoc.ItemsSource = dt.DefaultView;
        }

        public void TrueVis_FalseHide(bool b)
        {
            btnChangeCustumer.Visibility = (b == true) ? Visibility.Visible : Visibility.Hidden;

        }

        public void CntSumm(object sender, RoutedEventArgs e)
        {

            decimal inCntsum = 0;
            decimal inCntCountSum = 0;
            _CountSum = 0;
            _sum = 0;
            
            try
            {
                for (int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
                {
                    decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                    decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));

                    _pdrs[i].Count = Convert.ToInt32(x);
                    _pdrs[i].PurchasePrice = y;
                    _pdrs[i].SumPrice = x * y;
                    inCntsum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                    inCntCountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                }
            }
            catch (Exception ex)
            {

            }
            
            LoadDatas();

            labelDocSum.Content = inCntsum.ToString();
            labelSumCount.Content = inCntCountSum.ToString();
            _pd.FullSum = inCntsum;

            _sum = inCntsum;
            _CountSum = Convert.ToInt32(inCntCountSum);
            
        }

        private void BtnDiscount_Click(object sender, RoutedEventArgs e)
        {
            btnOk.IsEnabled = false;
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
            LoadDatas();
            //dataGridPurchaseDoc.ItemsSource = null;

            //dt.Rows.Clear();
            //string bookTitle = "";
            //foreach (var item in _pdrs)
            //{
            //    foreach (var item1 in _Lbooks)
            //    {
            //        if (item.BookId == item1.Id)
            //        {
            //            bookTitle = item1.BookTitle;
            //        }
            //    }

            //    dt.Rows.Add(item.LineNumber, bookTitle, item.Count, item.PurchasePrice, item.RetailPrice, item.Count * item.PurchasePrice);
            //}
            //dataGridPurchaseDoc.ItemsSource = dt.DefaultView;

            labelDocSum.Content = sum.ToString();
            labelSumCount.Content = CountSum.ToString();
        }

        private void BtnAddBook_Click(object sender, RoutedEventArgs e)
        {
            btnOk.IsEnabled = false;
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
            purchaseDocRec.Id = s + 1;
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
            btnOk.IsEnabled = false;
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
            _pd.CustumerId = _c.Id;
            unitedPurchaseDoc unitedPurchaseDoc = new unitedPurchaseDoc();
            unitedPurchaseDoc.custumer = _c;
            unitedPurchaseDoc.PurchaseDoc = _pd;
            unitedPurchaseDoc.purchaseDocRecs = _pdrs;
            unitedPurchaseDoc.IsNew = _isNew;
            string jsonUnited = JsonConvert.SerializeObject(unitedPurchaseDoc);
            client.PutAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.UnitedPurchaseDoc.ToString(), jsonUnited);
            Thread.Sleep(100);
            btnOk.IsEnabled = true;

        }

        
        private void DataGridPurchaseDoc_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
        
            //decimal inCntsum = 0;
            //decimal inCntCountSum = 0;

            ////decimal sum = 0;
            ////int CountSum = 0;
            ////reSum();
            //for (int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
            //{
            //    decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            //    decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));

            //    _pdrs[i].Count = Convert.ToInt32(x);
            //    _pdrs[i].PurchasePrice = y;
            //    _pdrs[i].SumPrice = x * y;
            //    inCntsum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            //    inCntCountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            //}

            //dataGridPurchaseDoc.Columns.Clear();
            //dataGridPurchaseDoc.ItemsSource = null;
         
            //LoadDatas();

            //labelDocSum.Content = inCntsum.ToString();
            //labelSumCount.Content = inCntCountSum.ToString();
            //_pd.FullSum = inCntsum;

            //_sum = inCntsum;
            //_CountSum = Convert.ToInt32(inCntCountSum);


            SelectedId = 0;
            try
            {
                string StringBookId = "";
                var selectedColumn = dataGridPurchaseDoc.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGridPurchaseDoc.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringBookId = (cellContent as TextBlock).Text;
                }
                SelectedId = Convert.ToInt32(StringBookId);
                labelTestId.Content = SelectedId.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error select ID" + ex);
            }


        }

        private void DataGridPurchaseDoc_MouseMove(object sender, MouseEventArgs e)
        {
            decimal inCntsum = 0;
            decimal inCntCountSum = 0;
            
            for (int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
            {
                decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));

                _pdrs[i].Count = Convert.ToInt32(x);
                _pdrs[i].PurchasePrice = y;
                _pdrs[i].SumPrice = x * y;
                inCntsum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                inCntCountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            }

            dataGridPurchaseDoc.Columns.Clear();
            dataGridPurchaseDoc.ItemsSource = null;

            LoadDatas();

            labelDocSum.Content = inCntsum.ToString();
            labelSumCount.Content = inCntCountSum.ToString();
            _pd.FullSum = inCntsum;

            _sum = inCntsum;
            _CountSum = Convert.ToInt32(inCntCountSum);
        }

        private void CntSumm(object sender, EventArgs e)
        {
            decimal inCntsum = 0;
            decimal inCntCountSum = 0;
            _CountSum = 0;
            _sum = 0;
            
            try
            {
                for (int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
                {
                    decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                    decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));

                    _pdrs[i].Count = Convert.ToInt32(x);
                    _pdrs[i].PurchasePrice = y;
                    _pdrs[i].SumPrice = x * y;
                    inCntsum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                    inCntCountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshIt();
            //btnOk.IsEnabled = false;
            //decimal inCntsum = 0;
            //decimal inCntCountSum = 0;
            //_CountSum = 0;
            //_sum = 0;
           
            
            //try
            //{
            //    for (int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
            //    {
            //        decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            //        decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));

            //        _pdrs[i].Count = Convert.ToInt32(x);
            //        _pdrs[i].PurchasePrice = y;
            //        _pdrs[i].SumPrice = x * y;
            //        inCntsum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            //        inCntCountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("BtnRefresh_Click Error: " + ex);
            //}

            //LoadDatas();

            //labelDocSum.Content = inCntsum.ToString();
            //labelSumCount.Content = inCntCountSum.ToString();
            //_pd.FullSum = inCntsum;

            //_sum = inCntsum;
            //_CountSum = Convert.ToInt32(inCntCountSum);



        }

        public void RefreshIt()
        {
            btnOk.IsEnabled = false;
            decimal inCntsum = 0;
            decimal inCntCountSum = 0;
            _CountSum = 0;
            _sum = 0;


            try
            {
                for (int i = 0; i < dataGridPurchaseDoc.Items.Count - 1; i++)
                {
                    decimal x = (decimal.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                    decimal y = (decimal.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));

                    _pdrs[i].Count = Convert.ToInt32(x);
                    _pdrs[i].PurchasePrice = y;
                    _pdrs[i].SumPrice = x * y;
                    inCntsum += (decimal.Parse((dataGridPurchaseDoc.Columns[5].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                    inCntCountSum += (int.Parse((dataGridPurchaseDoc.Columns[2].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BtnRefresh_Click Error: " + ex);
            }

            LoadDatas();

            labelDocSum.Content = inCntsum.ToString();
            labelSumCount.Content = inCntCountSum.ToString();
            _pd.FullSum = inCntsum;

            _sum = inCntsum;
            _CountSum = Convert.ToInt32(inCntCountSum);

        }



        private void DataGridPurchaseDoc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
            SelectedId = 0;
            try
            {
                string StringBookId = "";
                var selectedColumn = dataGridPurchaseDoc.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGridPurchaseDoc.SelectedCells[6];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringBookId = (cellContent as TextBlock).Text;
                }
                SelectedId = Convert.ToInt32(StringBookId);
                labelTestId.Content = SelectedId.ToString();
                btnDellBook.Content = "- " + SelectedId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error select ID" + ex);
            }
        }

        private void BtnDellBook_Click_1(object sender, RoutedEventArgs e)
        {
            
            foreach (var item in _pdrs)
            {
                if (item.Id == SelectedId)
                {
                    _pdrs.Remove(item);
                    break;
                }
            }
            LoadDatas();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            

            var json = JsonConvert.SerializeObject(_pd.Id);
            var responce = client.GetAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.PurchaseDocChange.ToString() + "/" + _pd.Id).Result;
            var jsonResp = responce.Content.ReadAsStringAsync().Result;
            ErrorsMessage t = JsonConvert.DeserializeObject<ErrorsMessage>(jsonResp);
            if (t.boolen == 0)
            {
                MessageBox.Show(t.message);
            }
            
            btnOk.IsEnabled = false;
            MessageBox.Show(json.ToString());

            Close();


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.LoadDatas();
        }

        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            RefreshIt();
            if (xlApp == null)
            {
                MessageBox.Show("Excel not found");
                return;
            }

            object misValue = System.Reflection.Missing.Value;
            var xlWorkBook = xlApp.Workbooks.Add(misValue);

            var xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
           //____________________________________________________________________________________________________ОТРИСОВКА ГРАНИЦ В ЯЧЕЙКАХ_______________________
            int LeftKray = 5;
            int RightKray = 5;
            for (int i=0;i<=_pdrs.Count; i++)
            {
               
                var cells = xlWorkSheet.get_Range("A"+ LeftKray, "G"+ RightKray);

                cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние вертикальные
                cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние горизонтальные            
                cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // верхняя внешняя
                cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // правая внешняя
                cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // левая внешняя
                cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                LeftKray++;
                RightKray++;
            }
            //____________________________________________________________________________________________________ОТРИСОВКА ГРАНИЦ В ЯЧЕЙКАХ_______________________



           xlWorkSheet.Range[xlWorkSheet.Cells[3,2], xlWorkSheet.Cells[3, 4]].Merge();///объединение ячеек
          
            xlWorkSheet.Cells[1, 1] = "Клиент";
            xlWorkSheet.Cells[1, 2] = _c.CustumerTitle;
            xlWorkSheet.Cells[2, 1] = "Дата: ";
            xlWorkSheet.Cells[2, 2] =_pd.DateCreate;
            xlWorkSheet.Cells[3, 2] = "Приходная №" + _pd.Id;
            int y = _pdrs.Count;

            int rowCount = 5;

           // Microsoft.Office.Interop.Excel.CellFormat Excelcells = xlWorkSheet.Range[xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[5, 7]].Select;
        
            

            //  Y  X
            xlWorkSheet.Cells[5, 1] = "№";
            xlWorkSheet.Cells[5, 2] = "Наименование";
            xlWorkSheet.Cells[5, 3]="кол-во";
            xlWorkSheet.Cells[5, 4] = "Закупочная Цена";
            xlWorkSheet.Cells[5, 5] = "Розничная Цена";
            xlWorkSheet.Cells[5, 6] = "Сумма";
            xlWorkSheet.Cells[5, 7] = "ID";

            xlWorkSheet.Cells[LeftKray, 2] = "Общее Кол-во: ";
            xlWorkSheet.Cells[LeftKray, 3] = _CountSum;
            xlWorkSheet.Cells[LeftKray, 5] = "Сумма: ";
            xlWorkSheet.Cells[LeftKray, 6] = _sum;




            foreach (DataRow item in dt.Rows)
            {
                rowCount += 1;
                for(int i = 0; i < dt.Columns.Count; i++)
                {
                    xlWorkSheet.Cells[rowCount, i + 1] = item[i];
                }
                
            }


            //for(int i = 0; i <= x; i++)
            //{
            //    for(int s = 0; s <= y; i++)
            //    {

            //        xlWorkSheet.Cells[i, s] = dt[i, s];
            //    }
            //}


            //xlWorkSheet.Cells[1, 1] = "ID";
            //xlWorkSheet.Cells[1, 2] = "NAME";
            //xlWorkSheet.Cells[2, 1] = "1";
            //xlWorkSheet.Cells[2, 2] = "One";
            //xlWorkSheet.Cells[3, 1] = "2";
            //xlWorkSheet.Cells[3, 2] = "two";
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel(*.xls)|*.xls|All files(*.*)|*.*";
                if (saveFileDialog.ShowDialog() == false)
                {
                    return;
                }
                string fileName = saveFileDialog.FileName;

                xlWorkBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);

                MessageBox.Show("Excel done");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error on saving: " + ex);
            }
        }
    }
}
