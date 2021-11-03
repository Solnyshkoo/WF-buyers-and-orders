using SkladPi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace SkladPi
{
    public partial class CustomerPage : Form
    {
        /// <summary>
        /// Selected treenode.
        /// </summary>
        public TreeNode CurrenTreeNode { get; set; } = null;
        /// <summary>
        /// Current customer.
        /// </summary>
        public static Customer CurrentCustomer { get; set; }
        public CustomerPage()
        {
            InitializeComponent();
            dgvStore.ContextMenuStrip = contextMenuStore;
            addToShoppingCart.Click += addToShoppingCart_Click;
            dgvShoppingCart.ContextMenuStrip = contextMenuShoppingStore;
            delete.Click += delete_Click;
        }
        /// <summary>
        /// Delete purchase from shopping cart.
        /// </summary>
        private void delete_Click(object sender, EventArgs e)
        {
            if (dgvShoppingCart.SelectedRows == null || dgvShoppingCart.SelectedRows.Count != 1)
            {
                return;
            }
            // нахожу предмет, который надо удалить 
            int index = dgvShoppingCart.SelectedRows[0].Index;
            string name = dgvShoppingCart[0, index].Value.ToString();
            if (CurrentCustomer.GetPurchase(name) != null)
            {
                CurrentCustomer.AllPurchases.Remove(CurrentCustomer.GetPurchase(name));
            }
            // переписываю таблицу, чтобы показывал актуальную информацию.
            dgvShoppingCart.Rows.Clear();
            foreach (var item in CurrentCustomer.AllPurchases)
            {
                dgvShoppingCart.Rows.Add(new object[] { item.Name, item.Article, item.Amount, item.Price });
            }
            dgvShoppingCart.Refresh();
        }
        /// <summary>
        /// Add purchase to shopping cart.
        /// </summary>
        private void addToShoppingCart_Click(object sender, EventArgs e)
        {
            if (dgvStore.SelectedRows == null || CurrenTreeNode == null)
            {
                return;
            }
            Vertex vertex = SkladPi.root.ReturnVertex(CurrenTreeNode.FullPath);
            dgvShoppingCart.Rows.Clear();
            if (vertex != null)
            {
                int index = dgvStore.SelectedRows[0].Index;
                string name = dgvStore[0, index].Value.ToString();
                Product product = vertex.GetProduct(name);
                if (product != null)
                {
                    // проверяю есть ли товар на складе
                    if (product.Amount == 0)
                    {
                        MessageBox.Show("Product out of stock, Sorry :(");
                        return;
                    }
                    // если да, то уменьшаю его кол-во на один
                    Purchase purchase = new Purchase(product.Name, product.Path, product.Article.ToString(), 1, product.Price);
                    product.Amount -= 1;
                    if (purchase != null)
                    {
                        CurrentCustomer.AddPurchase(purchase);
                    }
                    // обновляю таблицу магазина
                    RefreshStore();
                }
            }
            // обновляю корзину
            foreach (var item in LoginPage.Customer.AllPurchases)
            {
                dgvShoppingCart.Rows.Add(new object[] { item.Name, item.Article, item.Amount, item.Price });
            }
            dgvShoppingCart.Refresh();

        }
        /// <summary>
        /// Refresh store.
        /// </summary>
        private void RefreshStore()
        {
            if (treeViewStore.SelectedNode == null)
            {
                return;
            }
            dgvStore.Rows.Clear();
            dgvStore.Refresh();
            Vertex vertex = SkladPi.root.ReturnVertex(treeViewStore.SelectedNode.FullPath);
            if (vertex != null)
            {
                foreach (var item in vertex.Products)
                {
                    dgvStore.Rows.Add(new object[] { item.Name, item.Article, item.Amount, item.Price });

                }
                dgvStore.Refresh();
            }
        }
        /// <summary>
        /// Update data
        /// </summary>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvShoppingCart.Rows.Clear();
            // обновляю корзину
            if (tabControl1.SelectedIndex == 1)
            {
                foreach (var item in LoginPage.Customer.AllPurchases)
                {
                    dgvShoppingCart.Rows.Add(new object[] { item.Name, item.Article, item.Amount, item.Price });
                }
                dgvShoppingCart.Refresh();
            }
            // обновляю заказы
            if (tabControl1.SelectedIndex == 2)
            {
                if (CurrentCustomer.Orders.Count != 0)
                {
                    dgvOrders.Rows.Clear();
                    int i = -1;
                    foreach (var item in CurrentCustomer.Orders)
                    {
                        i++;
                        dgvOrders.Rows.Add(new object[] {item.Date,
                        item.OrderNumber, item.IsProcessed, item.IsPaid, item.IsShipped, item.IsDone});

                        if (item.IsProcessed == true)
                        {
                            dgvOrders[3, i].ReadOnly = false;
                        }
                        else
                        {
                            dgvOrders[3, i].ReadOnly = true;
                        }
                    }
                    dgvOrders.Refresh();

                }
            }

        }
        /// <summary>
        /// Deserialization
        /// </summary>
        private void CustomerPage_Load(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "Root.json");
                string jsonVertex = File.ReadAllText(path);
                SkladPi.root = JsonSerializer.Deserialize<Vertex>(jsonVertex);
                string pathSeller = Path.Combine(Application.StartupPath, "Seller.json");
                string jsonSeller = File.ReadAllText(pathSeller);
                SkladPi.seller = JsonSerializer.Deserialize<Seller>(jsonSeller);
            }
            catch 
            {
                
            }
            if (SkladPi.root == null)
            {
                SkladPi.root = new Vertex();
                SkladPi.root.Name = "Склад";
            }
            BuildRoot();
            if (SkladPi.seller == null)
            {
                SkladPi.seller = new Seller();
            }
            else{}
        }
        /// <summary>
        /// Build root
        /// </summary>
        private void BuildRoot()
        {
            treeViewStore.Nodes.Add(ReturnTreeNode(SkladPi.root));

            foreach (var item in SkladPi.root.Vertices)
            {
                RootChildren(treeViewStore.Nodes[0], item);
            }
        }
        /// <summary>
        /// Returns treenode.
        /// </summary>
        /// <param name="vertex">vertex</param>
        /// <returns>treenode</returns>
        private TreeNode ReturnTreeNode(Vertex vertex)
        {
            return new TreeNode(vertex.Name);
        }
        /// <summary>
        /// Create root folders.
        /// </summary>
        private void RootChildren(TreeNode treeNode, Vertex vertex)
        {
            TreeNode tree = ReturnTreeNode(vertex);
            treeNode.Nodes.Add(tree);
            foreach (var item in vertex.Vertices)
            {
                RootChildren(tree, item);
            }
        }
        /// <summary>
        /// Serialisation.
        /// </summary>
        private void CustomerPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            SkladPi.seller.ReplaceCustomer(CurrentCustomer);
            try
            {
                string jsonVertex = JsonSerializer.Serialize(SkladPi.root);
                string jsonSeller = JsonSerializer.Serialize(SkladPi.seller);
                File.WriteAllText(Path.Combine(Application.StartupPath, "Root.json"), jsonVertex);
                File.WriteAllText(Path.Combine(Application.StartupPath, "Seller.json"), jsonSeller);
            }
            catch 
            {
              
            }
        }
        /// <summary>
        /// Show product of selected treenode.
        /// </summary>
        private void treeViewStore_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = treeViewStore.SelectedNode;
            Vertex vertex = SkladPi.root.ReturnVertex(e.Node.FullPath);
            ShowProducts(treeNode);
            CurrenTreeNode = treeNode;
        }
        /// <summary>
        /// Show product.
        /// </summary>
        /// <param name="node">treenode</param>
        private void ShowProducts(TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            dgvStore.Rows.Clear();
            dgvStore.Refresh();
            Vertex vertex = SkladPi.root.ReturnVertex(node.FullPath);
            if (vertex != null)
            {
                foreach (var item in vertex.Products)
                {
                    dgvStore.Rows.Add(new object[] { item.Name, item.Article, item.Amount, item.Price });
                }
                dgvStore.Refresh();
            }
        }
        /// <summary>
        /// Create order.
        /// </summary>
        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            if (CurrentCustomer.AllPurchases.Count != 0)
            {
                Order order = new Order(DateTime.Now, CurrentCustomer.Login, CurrentCustomer.AllPurchases);
                CurrentCustomer.Orders.Add(order);
                CurrentCustomer.AllPurchases = new List<Purchase>();
                dgvShoppingCart.Rows.Clear();
                dgvOrders.Rows.Clear();
                int i = -1;
                foreach (var item in CurrentCustomer.Orders)
                {
                    dgvOrders.Rows.Add(new object[] {item.Date, item.Customer,
                        item.OrderNumber, item.IsProcessed, item.IsPaid, item.IsShipped, item.IsDone});
                    i++;
                    if (item.IsProcessed == true)
                    {
                        dgvOrders[3, i].ReadOnly = false;
                    }
                    else
                    {
                        dgvOrders[3, i].ReadOnly = true;
                    }
                }
                dgvOrders.Refresh();
            }
        }
        /// <summary>
        /// Reads the data after the change.
        /// </summary>
        private void dgvOrders_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.ColumnIndex == 3)
            {
                string date = dgvOrders[0, index].Value.ToString();
                long number = (long)dgvOrders[1, index].Value;
                Order order = GetOrder(date, number);
                order.IsPaid = (bool)dgvOrders[3, index].Value;
            }
        }
        /// <summary>
        /// Return order.
        /// </summary>
        /// <param name="date">data</param>
        /// <param name="number">number</param>
        /// <returns>order</returns>
        private Order GetOrder(string date, long number)
        {
            foreach (var item in CurrentCustomer.Orders)
            {
                if (item.Date.ToString() == date && item.OrderNumber == number)
                {
                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// Show order's purchases.
        /// </summary>
        private void dgvOrders_DoubleClick(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows == null || dgvOrders.SelectedRows.Count != 1)
            {
                return;
            }
            
            int index = dgvOrders.SelectedRows[0].Index;
            string data = dgvOrders[0, index].Value.ToString();
            long number = (long)dgvOrders[1, index].Value;

            Order order = GetOrder(data, number);
            if (order == null)
            {
                return;
            }
            PurchasesInOrder purchasesInOrder = new PurchasesInOrder(order);
            purchasesInOrder.ShowDialog();
            
        }
    }
}
