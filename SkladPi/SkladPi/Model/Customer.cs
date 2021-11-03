using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace SkladPi.Model
{
    [KnownType(typeof(Purchase)), KnownType(typeof(Order))]
    [DataContract]
    public class Customer
    {
        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// Surname
        /// </summary>
        [DataMember]
        public string Surname { get; set; }
        /// <summary>
        /// Patronymic
        /// </summary>
        [DataMember]
        public string Patronymic { get; set; }
        /// <summary>
        /// Phone number
        /// </summary>
        [DataMember]
        public double Number { get; set; }
        /// <summary>
        /// Adress
        /// </summary>
        [DataMember]
        public string Address { get; set; }
        /// <summary>
        /// Login
        /// </summary>
        [DataMember]
        public string Login { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [DataMember]
        public string Password { get; set; }
        /// <summary>
        /// List with all orders
        /// </summary>
        [DataMember]
        public List<Order> Orders { get; set; } = new List<Order>();

        [DataMember]
        /// <summary>
        /// Future order, Purchases from shopping cart
        /// </summary>
        public List<Purchase> AllPurchases{ get; set; } = new List<Purchase>();


        public Customer() { }
        public Customer(string name, string surname, string patronymic,
            double number, string address, string login, string password)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Number = number;
            Address = address;
            Login = login;
            Password = password;
           
            
        }
        /// <summary>
        /// Total for all orders.
        /// </summary>
        /// <returns>full amount.</returns>
        public double GetTotalPrice()
        {
            double sum = 0;
            foreach (var item in Orders)
            {
                if (item.IsPaid == true)
                {
                    foreach (var purchase in item.Purchases)
                    {
                        sum += purchase.Price;
                    }
                }
            }
            return sum;
        }
        /// <summary>
        /// Add purchase
        /// </summary>
        /// <param name="purchase"></param>
        public void AddPurchase(Purchase purchase)
        {
            if (AllPurchases.Count != 0)
            {
                // проверяем есть ли такой товар в корзине, если да, то увеличиваем кол-во и стомость
                if (Exists(purchase.Name) != -1)
                {
                    int n = Exists(purchase.Name);
                    AllPurchases[n].Price += AllPurchases[n].Price / AllPurchases[n].Amount;
                    AllPurchases[n].Amount += 1;
                }
                else
                {
                    AllPurchases.Add(purchase);
                }
            }
            else
            {
                AllPurchases.Add(purchase);
            }

        }
        /// <summary>
        /// Сhecks whether there is such a product.
        /// </summary>
        /// <param name="name">name.</param>
        /// <returns>index</returns>
        private int Exists(string name)
        {

            for (int i = 0; i < AllPurchases.Count; i++)
            {
                if (AllPurchases[i].Name == name)
                {
                    return i;
                }
            }

            return -1;
        }
        /// <summary>
        /// Find Purchase for its name.
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>purchase</returns>
        public Purchase GetPurchase(string name)
        {
            for (int i = 0; i < AllPurchases.Count; i++)
            {
                if (AllPurchases[i].Name == name)
                {
                    return AllPurchases[i];
                }
            }
            return null;
        }
    }
}
