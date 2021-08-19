using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;
using BookStoreDAL;

namespace BookStoreLIB
{
    public class UserOrderList
    {
        // returns a user's orders
        public DataTable FetchOrders(int userID)
        {
            DALUserOrders userOrders = new DALUserOrders();
            return userOrders.Fetch(userID);
        }

        // returns the books (price, quantity and title) for a OrderID
        public DataTable FetchBooks(int orderId)
        {
            DALOrderBooks orderBooks = new DALOrderBooks();
            return orderBooks.Fetch(orderId);
        }

        public Boolean returnBook(int orderId, string bookISBN, int quantity)
        {
            if (quantity < 0 || orderId < 0 || bookISBN == "" || bookISBN == null)
            {
                return false;
            }
            DALUpdateOrderItemQuantity quantity_updater = new DALUpdateOrderItemQuantity();
            return quantity_updater.update_quantity(orderId, bookISBN, quantity);
        }
    }
}
