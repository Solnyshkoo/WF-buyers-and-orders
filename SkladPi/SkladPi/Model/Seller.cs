using SkladPi.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

namespace SkladPi.Model
{
    [KnownType(typeof(Customer))]
    [DataContract]
    public class Seller
    {
        /// <summary>
        /// Surname, name, patronymic
        /// </summary>
        public  string SNP { get; set; }
        /// <summary>
        /// Login
        /// </summary>
        [DataMember]
        public  string Login { get; set; } = "admin";
        /// <summary>
        /// Password
        /// </summary>
        [DataMember]
        public string Password { get; set; }
        /// <summary>
        /// List of all customers.
        /// </summary>
        [DataMember]
        public List<Customer> Customers { get; set; } = new List<Customer>();

        public Seller() 
        {
            Password = HashPassword("0000");
        }
       
        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="password">user password</param>
        /// <returns>hash password</returns>
        public string HashPassword(string password)
        {
            // возвращает пароль + соль
            byte[] salt;
            byte[] buffer2;
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
        /// <summary>
        /// Add Customer.
        /// </summary>
        /// <param name="newCustomer">new customer</param>
        public void AddCustomer(Customer newCustomer)
        {
            if (newCustomer != null)
            {
                Customers.Add(newCustomer);
            }
        }
        /// <summary>
        /// Get all orders from all customers.
        /// </summary>
        /// <returns>List with orders</returns>
        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            foreach (var item in Customers)
            {
                if (item.Orders != null)
                {
                    foreach (var offer  in item.Orders)
                    {
                        orders.Add(offer);
                    }
                }
            }
            return orders;
        }
        /// <summary>
        /// Get all active orders(which are not done).
        /// </summary>
        /// <returns>list with orders</returns>
        public List<Order> GetAllActiveOrders()
        {
            List<Order> orders = new List<Order>();
            if (Customers.Count != 0)
            {
                foreach (var item in Customers)
                {
                    if (item.Orders != null)
                    {
                        foreach (var offer in item.Orders)
                        {
                            if (offer.IsDone == false)
                            {
                                orders.Add(offer);
                            }
                        }
                    }
                }
            }
            return orders;
        }
        /// <summary>
        /// Get all active orders(have at least one order).
        /// </summary>
        /// <returns>list of customers</returns>
        public List<Customer> GetAllActiveCustomers()
        {
            List<Customer> customers = new List<Customer>();
            if (Customers.Count != 0)
            {
                foreach (var item in Customers)
                {
                    if (item.Orders != null)
                    {
                        if (item.Orders.Count != 0)
                        {
                            customers.Add(item);
                        }
                    }
                }
            }
            return customers;
        }
        /// <summary>
        /// Return customer according to its login
        /// </summary>
        /// <param name="login">login</param>
        /// <returns>customer</returns>
        public Customer GetCustomer(string login)
        {
            if (Customers != null)
            {
                foreach (var item in Customers)
                {
                    if (item.Login == login)
                    {
                        return item;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Replace old customer's data with new one.
        /// </summary>
        /// <param name="customer">customer</param>
        public void ReplaceCustomer(Customer customer)
        {
            if (customer != null)
            {
                if (Customers != null)
                {
                    for (int i = 0; i < Customers.Count; i++)
                    {
                        if (Customers[i].Login == customer.Login)
                        {
                            Customers[i] = customer;
                            break;
                        }
                    }
                }
            }
        }
    }
}
