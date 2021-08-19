using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using BookStoreLIB;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for AddBookDialog.xaml
    /// </summary>
    public partial class AddBookDialog : Window
    {
        public AddBookDialog()
        {
            InitializeComponent();
            FillComboBox();
        }

        
            private void saveBookButton_Click(object sender, RoutedEventArgs e)
        {
            BookCatalog bookCatalog = new BookCatalog();

            string isbn = bookISBN.Text;
            string title = bookTitle.Text;
            string author = bookAuthor.Text;
            string stringPrice = bookPrice.Text;
            string category = bookCategoryComboBox.SelectedValue.ToString();
            string year = bookYear.Text;
            string edition = bookEdition.Text;
            string publisher = bookPublisher.Text;
            string supplierID = bookSupplierIDComboBox.SelectedValue.ToString();

                      
            if (isbn == "" || title == "" || author == "" || stringPrice == "" || category == "" || year == ""
                || edition == "" || publisher == "" || supplierID == "")
                MessageBox.Show("Please fill in all fields.");
            else
            {
                decimal price;
                if (!decimal.TryParse(stringPrice, out price))
                    MessageBox.Show("Please enter a valid price.");
                else
                {
                    stringPrice = price.ToString("F");
                    bool success = bookCatalog.AddNewBook(isbn, title, author, stringPrice, category, year, edition, publisher, supplierID);
                    if (success)
                    {
                        MessageBox.Show("Book added successfully.");
                        DialogResult = false;
                    }
                    else
                        MessageBox.Show("Error adding book. Please try again.");
                }
            }
        }

     
        private void FillComboBox()
        {
            BookCatalog bookCatalog = new BookCatalog();
            bookSupplierIDComboBox.ItemsSource = bookCatalog.GetBookSuppliers().Tables[0].DefaultView;
            bookCategoryComboBox.ItemsSource = bookCatalog.GetBookCategories().Tables[0].DefaultView;
        }


        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void NumbericInputValidation(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void PriceInputValidation(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch(e.Text);

        }

    }
}
