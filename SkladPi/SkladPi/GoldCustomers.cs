using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SkladPi
{
    public partial class GoldCustomers : Form
    {
        /// <summary>
        /// The amount to determine the gold customers
        /// </summary>
        public long Price { get; set; } = -1;
        public GoldCustomers()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOkey_Click(object sender, EventArgs e)
        {
            if (long.TryParse(textBoxPrice.Text, out long price))
            {
                Price = price;
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect price", "Error", MessageBoxButtons.OK);
                return;
            }
        }
    }
}
