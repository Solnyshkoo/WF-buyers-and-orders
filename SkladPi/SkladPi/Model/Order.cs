using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SkladPi.Model
{
    [KnownType(typeof(Purchase))]
    [DataContract]
    public class Order
    {
        /// <summary>
        /// Order Number.
        /// </summary>
        [DataMember]
        public long OrderNumber { get; set; }
        /// <summary>
        /// Date of the order.
        /// </summary>
        [DataMember]
        public DateTime Date { get; set; }
        /// <summary>
        /// Login of the customer.
        /// </summary>
        [DataMember]
        public string Customer { get; set; }
        /// <summary>
        /// Check if order Processed or not.
        /// </summary>
        [DataMember]
        public bool IsProcessed { get; set; }
        /// <summary>
        /// Check if order paid or not.
        /// </summary>
        [DataMember]
        public bool IsPaid { get; set; }
        /// <summary>
        /// Check if order shipped or not.
        /// </summary>
        [DataMember]
        public bool IsShipped { get; set; }
        /// <summary>
        /// Check if order done or not.
        /// </summary>
        [DataMember]
        public bool IsDone { get; set; }
        /// <summary>
        /// List of all purchses in the order.
        /// </summary>
        [DataMember]
        public List<Purchase> Purchases { get; set; }

        public Order() { }

        public Order(DateTime date, string customer, List<Purchase> purchase)
        {
            OrderNumber = GetHashCode();
            Date = date;
            Customer = customer;
            IsProcessed = false;
            IsPaid = false;
            IsShipped = false;
            IsDone = false;
            Purchases = purchase;
        }
        /// <summary>
        /// Get total price of one order.
        /// </summary>
        /// <returns></returns>
        public  double GetTotalPrice()
        {
            double sum = 0;
            if (Purchases != null)
            {
                foreach (var item in Purchases)
                {
                    sum += item.Price;
                }
            }
            return sum;
        }
     
    }
}
