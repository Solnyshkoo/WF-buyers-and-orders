using System;
using System.Collections.Generic;
using System.Text;

namespace SkladPi
{
    public class Product
    {
        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product Path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Product article.
        /// </summary>
        public int Article { get;  set; }

        /// <summary>
        /// Product price.
        /// </summary>
        public double Price { get;  set;  }

        /// <summary>
        /// Product amount. 
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Minimum quantity of goods in stock.
        /// </summary>
        public int MinAmount { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
        public string Description { get; set; }

        public Product()
        {
          
        }
    }
}
