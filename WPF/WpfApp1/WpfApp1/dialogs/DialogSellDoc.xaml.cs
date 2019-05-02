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
using System.Diagnostics;
using System.IO;
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
    /// Логика взаимодействия для DialogSellDoc.xaml
    /// </summary>
    public partial class DialogSellDoc : Window
    {
        Custumer _c;
        SaleDoc _sd;
        List<SaleDocRec> _saleDocRecs;
        List<Book> _books;
        List<BookFullDescription> _bookFullDescriptions;
        DataTable dt = new DataTable();
        HttpClient client = new HttpClient();
        MainWindow mw = new MainWindow();
        decimal _sum;
        int _CountSum;
        int _SelectedId;
        bool _isNew;
        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();


        public DialogSellDoc(Custumer custumer, SaleDoc saleDoc, List<SaleDocRec> saleDocRecs, List<Book> books, List<BookFullDescription> bookFulls, bool IsNew)
        {
            InitializeComponent();

            if(saleDoc.Status == StaticDatas.DocStatuses.Проведен.ToString())
            {
                btnChangeCustumer.IsEnabled = false;
                btnAddBook.IsEnabled = false;
                btnDellBook.IsEnabled = false;
                btnDiscount.IsEnabled = false;
            }

            _c = custumer;
            _sd = saleDoc;
            _books = books;
            _saleDocRecs = saleDocRecs;
            _bookFullDescriptions = bookFulls;
            _isNew = IsNew;
            LoadDatas();
            btnOk.IsEnabled = false;


        }

        public void LoadDatas()
        {
            dataGridSaleDoc.Columns.Clear();
            dataGridSaleDoc.ItemsSource = null;
            if (dt != null)
            {
                dt.Clear();
            }

            tbDocId.Text = (_isNew == false) ? _sd.Id.ToString() : "null";
            tbDate.Text = (_isNew == false) ? _sd.DateCreate.ToString() : DateTime.Now.ToString();
            tbClientTitle.Text = _c.CustumerTitle;

            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("№").ReadOnly = true;
                dt.Columns.Add("Наименование").ReadOnly = true;
                dt.Columns.Add("кол-во");
                dt.Columns.Add("Цена");
                dt.Columns.Add("Сумма").ReadOnly = true;
                dt.Columns.Add("id").ReadOnly = true;
            }
            string bookTitle = "";
            foreach (var item in _saleDocRecs)
            {
                foreach (var item1 in _books)
                {
                    if (item.BookId == item1.Id)
                    {
                        bookTitle = item1.BookTitle;
                    }
                }
                dt.Rows.Add(item.LineNumber, bookTitle, item.Count, item.RetailPrice, item.SumPrice, item.Id);

            }
            dataGridSaleDoc.ItemsSource = dt.DefaultView;
        }

        public void CntSumm(object sender, RoutedEventArgs e)
        {

            decimal inCntsum = 0;
            decimal inCntCountSum = 0;
            _CountSum = 0;
            _sum = 0;


            try
            {
                for (int i = 0; i < dataGridSaleDoc.Items.Count - 1; i++)
                {
                    decimal x = (decimal.Parse((dataGridSaleDoc.Columns[2].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
                    decimal y = (decimal.Parse((dataGridSaleDoc.Columns[3].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));

                    _saleDocRecs[i].Count = Convert.ToInt32(x);
                    _saleDocRecs[i].RetailPrice = y;
                    _saleDocRecs[i].SumPrice = x * y;
                    inCntsum += (decimal.Parse((dataGridSaleDoc.Columns[4].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
                    inCntCountSum += (int.Parse((dataGridSaleDoc.Columns[2].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
                }
            }
            catch (Exception ex)
            {

            }

            LoadDatas();

            labelDocSum.Content = inCntsum.ToString();
            labelSumCount.Content = inCntCountSum.ToString();
            _sd.FullSum = inCntsum;

            _sum = inCntsum;
            _CountSum = Convert.ToInt32(inCntCountSum);
        }


        private void DataGridSaleDoc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            _SelectedId = 0;
            try
            {
                string StringBookId = "";
                var selectedColumn = dataGridSaleDoc.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGridSaleDoc.SelectedCells[5];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringBookId = (cellContent as TextBlock).Text;
                }
                _SelectedId = Convert.ToInt32(StringBookId);
                labelTestId.Content = _SelectedId.ToString();
                btnDellBook.Content = "- " + _SelectedId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error select ID" + ex);
            }
        }




        private void BtnDiscount_Click(object sender, RoutedEventArgs e)
        {
            btnOk.IsEnabled = false;
            decimal discount = Convert.ToDecimal(tbDiscount.Text);
            decimal sum = 0;
            int CountSum = 0;
            for (int i = 0; i < dataGridSaleDoc.Items.Count - 1; i++)
            {
                decimal x = (decimal.Parse((dataGridSaleDoc.Columns[2].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
                decimal y = (decimal.Parse((dataGridSaleDoc.Columns[3].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
                decimal discountC = y - ((discount * y) / 100);
                //int z = (int.Parse((dataGridPurchaseDoc.Columns[3].GetCellContent(dataGridPurchaseDoc.Items[i]) as TextBlock).Text));
                //MessageBox.Show(x.ToString());
                //MessageBox.Show(y.ToString());
                _saleDocRecs[i].Count = Convert.ToInt32(x);
                _saleDocRecs[i].RetailPrice = discountC;
                _saleDocRecs[i].SumPrice = x * discountC;
                sum += (decimal.Parse((dataGridSaleDoc.Columns[4].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
                CountSum += (int.Parse((dataGridSaleDoc.Columns[2].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
            }
            LoadDatas();
            labelDocSum.Content = sum.ToString();
            labelSumCount.Content = CountSum.ToString();
        }

        private void BtnAddBook_Click(object sender, RoutedEventArgs e)
        {
            btnOk.IsEnabled = false;
            int BookIdFromDial = 0;
            //reSum();
            DialogAddBookToDoc dialogAddBookToDoc = new DialogAddBookToDoc(_books, _bookFullDescriptions);
            if (dialogAddBookToDoc.ShowDialog() == true)
            {
                BookIdFromDial = dialogAddBookToDoc.GetIdBook;
            }
            if (BookIdFromDial == 0)
            {
                return;
            }
            Book book = _books.Find(i => i.Id == BookIdFromDial);
            int s = _saleDocRecs.Count();
            SaleDocRec saleDocRec = new SaleDocRec();
            saleDocRec.Id = s + 1;
            saleDocRec.SaleDocId = _sd.Id;
            saleDocRec.RetailPrice = book.RetailPrice;
            saleDocRec.Count = 1;
            saleDocRec.LineNumber = s + 1;
            saleDocRec.BookId = book.Id;
            _saleDocRecs.Add(saleDocRec);
            LoadDatas();
        }

        private void BtnChangeCustumer_Click(object sender, RoutedEventArgs e)
        {
            btnOk.IsEnabled = false;
            int custIdFromDial = 0;
            MainWindow mw = new MainWindow();
            List<Custumer> custumers = mw.getCustumers;
            DialogChangeCustomer changeCustomer = new DialogChangeCustomer(custumers);
            if (changeCustomer.ShowDialog() == true)
            {
                custIdFromDial = changeCustomer.GetIdCust;
            }
            Custumer cc = custumers.Find(i => i.Id == custIdFromDial);
            _c = cc;
            LoadDatas();
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _sd.CustumerId = _c.Id;
            unitedSaleDoc unitedSaleDoc = new unitedSaleDoc();
            unitedSaleDoc.custumer = _c;
            unitedSaleDoc.SaleDoc = _sd;
            unitedSaleDoc.SaleDocRecs = _saleDocRecs;
            unitedSaleDoc.IsNew = _isNew;
            string jsonUnited = JsonConvert.SerializeObject(unitedSaleDoc);
            client.PutAsJsonAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.UnitedSaleDoc.ToString(), jsonUnited);
            Thread.Sleep(100);
            btnOk.IsEnabled = true;

        }

        private void DataGridPSaleDoc_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            _SelectedId = 0;
            try
            {
                string StringBookId = "";
                var selectedColumn = dataGridSaleDoc.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGridSaleDoc.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringBookId = (cellContent as TextBlock).Text;
                }
                _SelectedId = Convert.ToInt32(StringBookId);
                labelTestId.Content = _SelectedId.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error select ID" + ex);
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
            //    for (int i = 0; i < dataGridSaleDoc.Items.Count - 1; i++)
            //    {
            //        decimal x = (decimal.Parse((dataGridSaleDoc.Columns[2].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
            //        decimal y = (decimal.Parse((dataGridSaleDoc.Columns[3].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));

            //        _saleDocRecs[i].Count = Convert.ToInt32(x);
            //        _saleDocRecs[i].RetailPrice = y;
            //        _saleDocRecs[i].SumPrice = x * y;
            //        inCntsum += (decimal.Parse((dataGridSaleDoc.Columns[4].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
            //        inCntCountSum += (int.Parse((dataGridSaleDoc.Columns[2].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("BtnRefresh_Click Error: " + ex);
            //}

            //LoadDatas();

            //labelDocSum.Content = inCntsum.ToString();
            //labelSumCount.Content = inCntCountSum.ToString();
            //_sd.FullSum = inCntsum;

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
                for (int i = 0; i < dataGridSaleDoc.Items.Count - 1; i++)
                {
                    decimal x = (decimal.Parse((dataGridSaleDoc.Columns[2].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
                    decimal y = (decimal.Parse((dataGridSaleDoc.Columns[3].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));

                    _saleDocRecs[i].Count = Convert.ToInt32(x);
                    _saleDocRecs[i].RetailPrice = y;
                    _saleDocRecs[i].SumPrice = x * y;
                    inCntsum += (decimal.Parse((dataGridSaleDoc.Columns[4].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
                    inCntCountSum += (int.Parse((dataGridSaleDoc.Columns[2].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("BtnRefresh_Click Error: " + ex);
            }

            LoadDatas();

            labelDocSum.Content = inCntsum.ToString();
            labelSumCount.Content = inCntCountSum.ToString();
            _sd.FullSum = inCntsum;

            _sum = inCntsum;
            _CountSum = Convert.ToInt32(inCntCountSum);

            
        }

        private void BtnDellBook_Click_1(object sender, RoutedEventArgs e)
        {
            //int index = dataGridPurchaseDoc.SelectedIndex;
            // DataTable dt = new DataTable();

            foreach (var item in _saleDocRecs)
            {
                if (item.Id == _SelectedId)
                {
                    _saleDocRecs.Remove(item);
                    break;
                }
            }
            LoadDatas();
            //dataGridPurchaseDoc.Items.Remove(index);
            //btnDellBook.Content = "- " + index;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();

            // BtnSave_Click(null,null);

            var json = JsonConvert.SerializeObject(_sd.Id);
            var responce = client.GetAsync(mw.APP_CONNECT + MainWindow.API_CON_TYPE.SaleDocChange.ToString() + "/" + _sd.Id).Result;
            var jsonResp = responce.Content.ReadAsStringAsync().Result;
            ErrorsMessage t = JsonConvert.DeserializeObject<ErrorsMessage>(jsonResp);
            if (t.boolen == 0)
            {
                MessageBox.Show(t.message);
            }


            //LoadPurchaseDocs();
            btnOk.IsEnabled = false;
            MessageBox.Show(json.ToString());

            Close();


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.LoadDatas();
        }


        private void CntSumm(object sender, EventArgs e)
        {
            decimal inCntsum = 0;
            decimal inCntCountSum = 0;
            _CountSum = 0;
            _sum = 0;

            //decimal sum = 0;
            //int CountSum = 0;
            //reSum();
            try
            {
                for (int i = 0; i < dataGridSaleDoc.Items.Count - 1; i++)
                {
                    decimal x = (decimal.Parse((dataGridSaleDoc.Columns[2].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
                    decimal y = (decimal.Parse((dataGridSaleDoc.Columns[3].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));

                    _saleDocRecs[i].Count = Convert.ToInt32(x);
                    _saleDocRecs[i].RetailPrice = y;
                    _saleDocRecs[i].SumPrice = x * y;
                    inCntsum += (decimal.Parse((dataGridSaleDoc.Columns[4].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
                    inCntCountSum += (int.Parse((dataGridSaleDoc.Columns[2].GetCellContent(dataGridSaleDoc.Items[i]) as TextBlock).Text));
                }
            }
            catch (Exception ex)
            {

            }
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
            for (int i = 0; i <= _saleDocRecs.Count; i++)
            {

                var cells = xlWorkSheet.get_Range("A" + LeftKray, "F" + RightKray);

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



            xlWorkSheet.Range[xlWorkSheet.Cells[3, 2], xlWorkSheet.Cells[3, 4]].Merge();///объединение ячеек

            xlWorkSheet.Cells[1, 1] = "Клиент";
            xlWorkSheet.Cells[1, 2] = _c.CustumerTitle;
            xlWorkSheet.Cells[2, 1] = "Дата: ";
            xlWorkSheet.Cells[2, 2] = _sd.DateCreate;
            xlWorkSheet.Cells[3, 2] = "Расходная №" + _sd.Id;
            int y = _saleDocRecs.Count;

            int rowCount = 5;



            //  Y  X
            xlWorkSheet.Cells[5, 1] = "№";
            xlWorkSheet.Cells[5, 2] = "Наименование";
            xlWorkSheet.Cells[5, 3] = "кол-во";
            xlWorkSheet.Cells[5, 4] = "Цена";
            xlWorkSheet.Cells[5, 5] = "Сумма";
            xlWorkSheet.Cells[5, 6] = "ID";

            xlWorkSheet.Cells[LeftKray, 2] = "Общее Кол-во: ";
            xlWorkSheet.Cells[LeftKray, 3] = _CountSum;
            xlWorkSheet.Cells[LeftKray, 5] = "Сумма: ";
            xlWorkSheet.Cells[LeftKray, 6] = _sum;


            string fileName = "";

            foreach (DataRow item in dt.Rows)
            {
                rowCount += 1;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    xlWorkSheet.Cells[rowCount, i + 1] = item[i];
                }

            }

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel(*.xls)|*.xls|All files(*.*)|*.*";
                if (saveFileDialog.ShowDialog() == false)
                {
                    return;
                }
                fileName = saveFileDialog.FileName;

                xlWorkBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
                try
                {
                    foreach (Process proc in Process.GetProcessesByName("EXCEL.EXE"))
                    {
                        proc.Kill();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                MessageBox.Show("Excel done");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error on saving: " + ex);
            }

           


            Process.Start(fileName);

            // FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read);
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //if (openFileDialog.ShowDialog() == false)
            //{
            //    return;
            //}

        }
    }
}
