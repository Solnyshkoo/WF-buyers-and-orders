using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace SkladPi.Model
{
    
    [DataContract]
    public class Purchase
    {
        /// <summary>
        /// Name
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// FullPath
        /// </summary>
        [DataMember]
        public string FullPath { get; set; }
        /// <summary>
        /// Article
        /// </summary>
        [DataMember]
        public string Article { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        [DataMember]
        public int Amount { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [DataMember]
        public double Price { get; set; }

        public Purchase() { }

        public Purchase(string name, string fullPath, string article, int amount, double price)
        {
            Name = name;
            FullPath = fullPath;
            Article = article;
            Amount = amount;
            Price = price;
        }
    }
}
