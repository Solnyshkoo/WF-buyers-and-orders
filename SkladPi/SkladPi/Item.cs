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
    public partial class Item : Form
    {
        /// <summary>
        /// Selected folder.
        /// </summary>
        Vertex vertex;

        /// <summary>
        /// Finished product.
        /// </summary>
        public Product Products { get; set; }
      
        /// <summary>
        /// Change or not.
        /// </summary>
        public bool CancelOrNot { get; set; }

        /// <summary>
        /// Delete product or not.
        /// </summary>
        public bool DeleteOrNot { get; set; }


        public Item(Vertex vertex, Product item)
        {
            InitializeComponent();
            this.vertex = vertex;
            Products = item;
            DeleteOrNot = false;
            CancelOrNot = true;

        }

        /// <summary>
        /// Save product.
        /// </summary>
        private void Save_Click(object sender, EventArgs e)
        {
            if (tBItemName.Text.Length == 0 || tBItemName.Text.Contains("\\") || tBItemName.Text.Contains(","))
            {
                MessageBox.Show("Название не может быть пустым и содержать символ \\ и ,", "Ошибка", MessageBoxButtons.OK);
                return;
            }

            if (!ExistOrNot(vertex, tBItemName.Text))
            {
                MessageBox.Show("Такой товар уже существует", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            if (tbArticle.Text.Length == 0 || !int.TryParse(tbArticle.Text, out int article) || article <= 0)
            {
                MessageBox.Show("Артикул должен быть целым положительным числом", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            if (tbPrice.Text.Length == 0 || !int.TryParse(tbPrice.Text, out int price) || price <= 0)
            {
                MessageBox.Show("Цена должна быть целым пололжительным числом", "Ошибка", MessageBoxButtons.OK);
                return;
            }

            if (tbAmount.Text.Length == 0 || !int.TryParse(tbAmount.Text, out int amount) || amount <= 0)
            {
                MessageBox.Show("Количество должно быть целым положительным числом", "Ошибка", MessageBoxButtons.OK);
                return;
            }

            if (tbMinAmount.Text.Length == 0 || !int.TryParse(tbMinAmount.Text, out int minAmount) || minAmount <= 0)
            {
                MessageBox.Show("Минимальное количество должно быть целым положительным числом", "Ошибка", MessageBoxButtons.OK);
                return;
            }
           
            Products.Name = tBItemName.Text;
            Products.Path = (Products.Path == null) ? vertex.FullPath : Products.Path + "\\" + vertex.Name;
            Products.Article = article;
            Products.Amount = amount;
            Products.MinAmount = minAmount;
            Products.Description = rtbDescription.Text;
            Products.Price = price;

            CancelOrNot = false;
            this.Close();

        }

        /// <summary>
        /// Exists product in folder or not.
        /// </summary>
        /// <param name="vertex"> Folder. </param>
        /// <param name="name"> Product name. </param>
        /// <returns> Exists product in folder or not. </returns>
        private bool ExistOrNot(Vertex vertex, string name)
        {
            foreach (var item in vertex.Products)
            {
                if (item.Name == name)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Close form.
        /// </summary>
        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Delete product from folder.
        /// </summary>
        private void bDelete_Click(object sender, EventArgs e)
        {
            DeleteOrNot = true;
            this.Close();
        }

        /// <summary>
        /// Form load.
        /// </summary>
        private void Item_Load(object sender, EventArgs e)
        {
            tBItemName.Text = Products.Name;

            tbFullPath.Text = (Products.Path == null) ? vertex.Name: Products.Path + "\\" + vertex.Name;
            tbArticle.Text = Products.Article.ToString();
            tbAmount.Text = Products.Amount.ToString();
            tbMinAmount.Text = Products.MinAmount.ToString();
            tbPrice.Text = Products.Price.ToString();
            rtbDescription.Text = Products.Description.ToString();

        }
    }
}
