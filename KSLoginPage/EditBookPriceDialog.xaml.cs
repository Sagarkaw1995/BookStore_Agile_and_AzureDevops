using System;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using BookStoreLIB;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for EditBookPriceDialog.xaml
    /// </summary>
    public partial class EditBookPriceDialog : Window
    {
        public EditBookPriceDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {

            BookCatalog bookCatalog = new BookCatalog();
            
            string isbn = bookISBN.Text;
            string stringPrice = bookPrice.Text;
            decimal price;
            if (!decimal.TryParse(stringPrice, out price))
                MessageBox.Show("Please enter a valid price.");
            else
            {
                stringPrice = price.ToString("F");
                bool success = bookCatalog.EditBookPrice(isbn, stringPrice);
                if (success)
                {
                    MessageBox.Show("Book price changed successfully.");
                    DialogResult = false;
                }
                else
                    MessageBox.Show("Error changing book price. Please try again.");
            }

        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void PriceInputValidation(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);

        }

    }
}
