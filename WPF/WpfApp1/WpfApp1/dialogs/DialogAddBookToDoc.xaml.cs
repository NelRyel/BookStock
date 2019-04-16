using ManagerWpfLibrary;
using StockEntModelLibrary.BookEnt;
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
    /// Логика взаимодействия для DialogAddBookToDoc.xaml
    /// </summary>
    public partial class DialogAddBookToDoc : Window
    {
        int _id;
        BookManager bookManager = new BookManager();
        List<Book> _books;
        List<BookFullDescription> _bookFullDescriptions;
        public DialogAddBookToDoc(List<Book> books, List<BookFullDescription> fullDescriptions) 
        {
            _books = books;
            _bookFullDescriptions = fullDescriptions;
            InitializeComponent();
            var dt = new DataTable();
            dt = bookManager.LoadBook(books, fullDescriptions);
            dataGridBookAddToDoc.ItemsSource = dt.DefaultView;
        }

        private void DataGridBookAddToDoc_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int sId = 0;
            try
            {
                string StringBookId = "";
                var selectedColumn = dataGridBookAddToDoc.CurrentCell.Column.DisplayIndex;
                var selectedCell = dataGridBookAddToDoc.SelectedCells[0];
                var cellContent = selectedCell.Column.GetCellContent(selectedCell.Item);    //эта вся махинация, чтобы получить ИД выбранной книги 
                if (cellContent is TextBlock)
                {
                    StringBookId = (cellContent as TextBlock).Text;
                }
                sId = Convert.ToInt32(StringBookId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error select ID" + ex);
            }
            _id = sId;
           // TESTlabel.Content = _id;
            DialogResult = true;
            Close();
        }

        public int GetIdBook
        {
            get { return _id; }
        }

        private void TbBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            string barcode = tbBarcode.Text;
            string title = tbTitle.Text;
            string author = tbAuthor.Text;
            string serie = tbSerie.Text;
            string section = tbSection.Text;
            List<BookAndDesc> bookAndDescs = new List<BookAndDesc>();
            foreach (var item in _books)
            {
                var bnd = new BookAndDesc();
                bnd.book = item;
                foreach (var item1 in _bookFullDescriptions)
                {
                    if (item.Id == item1.Id)
                    {
                        bnd.bookFullDescription = item1;
                    }
                }
                bookAndDescs.Add(bnd);
            }
       
            var dt = new DataTable();

            dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Barcode");
            dt.Columns.Add("BookTitle");
            dt.Columns.Add("Retail Price");
            dt.Columns.Add("auth");
            dt.Columns.Add("serie");
            dt.Columns.Add("Section");
            dt.Columns.Add("count");

            var bd = bookAndDescs.Where(i => i.book.BarcodeISBN.Contains(barcode)).Where(q=>q.book.BookTitle.ToLower().Contains(title.ToLower())).Where(w=>w.bookFullDescription.Author.ToLower().Contains(author.ToLower())).Where(t=>t.bookFullDescription.Serie.ToLower().Contains(serie.ToLower())).Where(r=>r.bookFullDescription.Section.ToLower().Contains(section.ToLower())); 

            foreach (var item in bd)
            {
                dt.Rows.Add(item.book.Id, item.book.BarcodeISBN, item.book.BookTitle, item.book.RetailPrice, item.bookFullDescription.Author, item.bookFullDescription.Serie, item.bookFullDescription.Section, item.book.Count);
            }
            dataGridBookAddToDoc.ItemsSource = dt.DefaultView;
        }

    }
    
}
