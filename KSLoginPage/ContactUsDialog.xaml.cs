﻿using System;
using System.Collections.Generic;
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
using System.Net.Mail;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for ContactUsDialog.xaml
    /// </summary>
    public partial class ContactUsDialog : Window
    {
        public ContactUsDialog()
        {
            InitializeComponent();
        }
        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
