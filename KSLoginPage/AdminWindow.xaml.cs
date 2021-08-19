using System;
using System.Windows;
using System.Data;
using BookStoreLIB;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            LoadBookList();
        }

        private void LoadBookList()
        {
            
            BookCatalog bookCat = new BookCatalog();
            BooksDataGrid.ItemsSource = bookCat.GetBookInfo().Tables["Books"].DefaultView;

        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            
            var mw = new MainWindow();
            mw.Show();
            Close();
            MessageBox.Show("Logged Out Successfully");
            
        }

        private void exitButton_Click(object sender, RoutedEventArgs e) { Close(); }

        private void newBookButton_Click(object sender, RoutedEventArgs e)
        {
            //open AddBookDialog
            AddBookDialog dlg = new AddBookDialog();
            dlg.ShowDialog();
            LoadBookList();
        }

        private void editPriceButton_Click(object sender, RoutedEventArgs e)
        {
            
            DataRowView selectedRow;
            selectedRow = (DataRowView)BooksDataGrid.SelectedItem;
            
            if (selectedRow != null)
            {
               
                //open EditBookPriceDialog
                EditBookPriceDialog dlg = new EditBookPriceDialog();
                
                dlg.bookISBN.Text = selectedRow.Row.ItemArray[0].ToString();
                dlg.bookTitle.Text = selectedRow.Row.ItemArray[2].ToString();
                dlg.bookPrice.Text = selectedRow.Row.ItemArray[4].ToString();
                dlg.ShowDialog();
                LoadBookList();

            }
            else MessageBox.Show("Please select book from list to edit price");

        }
    }
}
