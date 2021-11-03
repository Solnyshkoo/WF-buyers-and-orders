using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkladPi.Model;

namespace SkladPi
{
    public partial class PurchasesInOrder : Form
    {
        /// <summary>
        /// Order to show.
        /// </summary>
        public Order Order { get; set; }
        public PurchasesInOrder(Order order)
        {
            InitializeComponent();
            this.Order = order;
            ShowPurchases();
        }
        /// <summary>
        /// Show order's purchase.
        /// </summary>
        private void ShowPurchases()
        {
            dgvPurchase.Rows.Clear();
            foreach (var item in Order.Purchases)
            {
                dgvPurchase.Rows.Add(new object[] { item.Name, item.Article, item.Amount, item.Price });
            }
            dgvPurchase.Refresh();
        }
      
    }
}
