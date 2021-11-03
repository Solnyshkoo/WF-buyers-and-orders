using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkladPi.Model;

namespace SkladPi
{
    public partial class SkladPi : Form
    {
        /// <summary>
        /// Selected TreeNode.
        /// </summary>
        public TreeNode CurrenTreeNode { get; set; } = null;

        public SkladPi()
        {
            InitializeComponent();

            tvFiles.ContextMenuStrip = contextMenu;
            delete.Click += delete_Click;
            addSubsection.Click += addSubsection_Click;
            addItem.Click += createItem_Click;
        }

        /// <summary>
        /// Delete folder.
        /// </summary>
        private void delete_Click(object sender, EventArgs e)
        {
            TreeNode treeNode = tvFiles.SelectedNode;
            Vertex vertex = root.ReturnVertex(treeNode.FullPath);

            Vertex fatherVertex = root.ReturnVertex(vertex.FullPath);
            fatherVertex.Vertices.Remove(vertex);
            tvFiles.Nodes.Remove(treeNode);

        }

        /// <summary>
        /// Create folder.
        /// </summary>
        private void addSubsection_Click(object sender, EventArgs e)
        {
            TreeNode newNode = new TreeNode("");

            tvFiles.SelectedNode.Nodes.Add(newNode);
            tvFiles.LabelEdit = true;
            tvFiles.SelectedNode = newNode;

            tvFiles.AfterLabelEdit += tvFiles_AfterLabelEdit;
            tvFiles.SelectedNode.BeginEdit();
        }


        /// <summary>
        /// Create product.
        /// </summary>
        private void createItem_Click(object sender, EventArgs e)
        {
            TreeNode treeNode = tvFiles.SelectedNode;
            Vertex vertex = root.ReturnVertex(treeNode.FullPath);

            Product product = new Product();
            product.Name = "";
            product.Path = vertex.FullPath;
            product.Article = 0;
            product.Amount = 0;
            product.MinAmount = 0;
            product.Description = "";
            var createProduct = new Item(vertex, product);
            createProduct.ShowDialog();

            if (!createProduct.DeleteOrNot && !createProduct.CancelOrNot)
            {
                vertex.Products.Add(createProduct.Products);
                ShowProducts(treeNode);
            }

        }



        /// <summary>
        /// Show folder products at the table.
        /// </summary>
        /// <param name="node"> Current folder. </param>
        private void ShowProducts(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            dgvData.Rows.Clear();
            dgvData.Refresh();

            Vertex vertex = root.ReturnVertex(node.FullPath);

            if (vertex != null)
            {
                foreach (var item in vertex.Products)
                {
                    dgvData.Rows.Add(new object[] { item.Name, item.Article, item.Amount, item.Price });

                }
                dgvData.Refresh();
            }
        }

        /// <summary>
        /// Save warehouse as file.
        /// </summary>
        private void saveFile_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Title = "Cохранить файл как...";
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;
                sfd.Filter = "Json file (.json)|*.json";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, JsonSerializer.Serialize(root));
                }
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Open warehouse from file.
        /// </summary>
        private void openFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog OP = new OpenFileDialog();
            try
            {
                OP.FileName = "";
                OP.Title = "Открыть документ";
                OP.Filter = "Json file (.json)|*.json";

                if (OP.ShowDialog() != DialogResult.Cancel)
                {
                    root = JsonSerializer.Deserialize<Vertex>(File.ReadAllText(OP.FileName));
                    tvFiles.Nodes.Clear();
                    BuildRoot();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно открыть", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Make CSV report of warehouse.
        /// </summary>
        private void createReport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;
                sfd.Filter = "CSV (*.csv)|*.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    string columnNames = "<путь_классификатора>,артикул,название товара,количество на складе";
                    List<Product> allProducts = root.ReturnProducts();
                    string[] output = new string[allProducts.Count + 1];
                    output[0] += columnNames;
                    int i = 1;
                    foreach (var item in allProducts)
                    {
                        if (item.MinAmount > item.Amount)
                        {
                            output[i] += item.Path + "\\" + item.Name + "," + item.Article.ToString() + "," + item.Name
                                + "," + item.Amount.ToString();
                            i++;
                        }
                    }
                    File.WriteAllLines(sfd.FileName, output, System.Text.Encoding.UTF8);
                    MessageBox.Show("Your file was generated and its ready for use.");
                }
            }
            catch
            {

                MessageBox.Show("Не получается создать отчёт", "Ошибка", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Create root.
        /// </summary>
        private void BuildRoot()
        {
            tvFiles.Nodes.Add(ReturnTreeNode(root));

            foreach (var item in root.Vertices)
            {
                RootChildren(tvFiles.Nodes[0], item);
            }
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
        /// Return treenode for appropriate folder.
        /// </summary>
        /// <param name="vertex"> Folder. </param>
        /// <returns> Treenode. </returns>
        private TreeNode ReturnTreeNode(Vertex vertex)
        {
            return new TreeNode(vertex.Name);
        }

        /// <summary>
        /// Load form.
        /// </summary>
        private void SkladPi_Load_1(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "Root.json");
                string jsonVertex = File.ReadAllText(path);
                root = JsonSerializer.Deserialize<Vertex>(jsonVertex);
                string pathSeller = Path.Combine(Application.StartupPath, "Seller.json");
                string jsonSeller = File.ReadAllText(pathSeller);
                seller = JsonSerializer.Deserialize<Seller>(jsonSeller);
            }
            catch
            {
            }

            if (root == null)
            {
                root = new Vertex();
                root.Name = "Склад";
            }
            BuildRoot();
            if (seller == null)
            {
                seller = new Seller();
            }
            else
            {

            }
        }

        /// <summary>
        /// Name new folder.
        /// </summary>
        private void tvFiles_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }

            if (e.Node.Parent == null)
            {
                return;
            }

            if (e.Label != null && e.Label.Length > 0 && !e.Label.Contains("\\") && !e.Label.Contains(",") && ExistOrNot(root.ReturnVertex(e.Node.Parent.FullPath), e.Label))
            {
                e.Node.Text = e.Label;

                Vertex vertex = new Vertex();
                vertex.Name = e.Label;
                root.ReturnVertex(e.Node.Parent.FullPath).AddVertex(vertex);
                tvFiles.LabelEdit = false;
                tvFiles.AfterLabelEdit -= tvFiles_AfterLabelEdit;
            }
            else
            {
                e.CancelEdit = true;
                MessageBox.Show("Имя не должно быть пустым, содержать символ \\ и ," + Environment.NewLine +
                    " и быть уникальным для классификатора.", "Ошибка", MessageBoxButtons.OK);
                e.Node.BeginEdit();
            }
        }


        /// <summary>
        /// Invoke after folder renamed.
        /// </summary>
        private void tvFiles_AfterLabelEdit1(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }

            if (e.Label != null && e.Label.Length > 0 && !e.Label.Contains("\\") && !e.Label.Contains(",")
                && (e.Node.Parent == null || ExistOrNot(root.ReturnVertex(e.Node.Parent.FullPath), e.Label)))
            {
                root.ReturnVertex(e.Node.FullPath).Rename(e.Label);
                e.Node.Text = e.Label;
                tvFiles.LabelEdit = false;
                tvFiles.AfterLabelEdit -= tvFiles_AfterLabelEdit1;

            }
            else
            {
                e.CancelEdit = true;
                MessageBox.Show("Имя не должно быть пустым, содержать символ \\ и ," + Environment.NewLine +
                    " и быть уникальным для классификатора.", "Ошибка", MessageBoxButtons.OK);

                tvFiles.AfterLabelEdit -= tvFiles_AfterLabelEdit1;

            }
        }

        /// <summary>
        /// Exist folder or not.
        /// </summary>
        /// <param name="vertex"> Folder. </param>
        /// <param name="name"> Name. </param>
        /// <returns> True or false. </returns>
        private bool ExistOrNot(Vertex vertex, string name)
        {
            foreach (var item in vertex.Vertices)
            {
                if (item.Name == name)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Hiding components.
        /// </summary>
        private void tvFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = tvFiles.SelectedNode;
            Vertex vertex = root.ReturnVertex(e.Node.FullPath);
            if (vertex != null)
            {
                if (treeNode.Parent == null || treeNode.Nodes.Count != 0 || vertex.Products.Count != 0)
                {
                    contextMenu.Items[0].Visible = false;
                }
                else
                {
                    contextMenu.Items[0].Visible = true;
                }
            }
            ShowProducts(treeNode);
            CurrenTreeNode = treeNode;
        }


        /// <summary>
        /// Closing from.
        /// </summary>
        private void SkladPi_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string jsonVertex = JsonSerializer.Serialize(root);
                string jsonSeller = JsonSerializer.Serialize(seller);
                File.WriteAllText(Path.Combine(Application.StartupPath, "Root.json"), jsonVertex);
                File.WriteAllText(Path.Combine(Application.StartupPath, "Seller.json"), jsonSeller);

            }
            catch
            {

            }
        }

        /// <summary>
        /// Chamge name.
        /// </summary>
        private void change_Click_1(object sender, EventArgs e)
        {
            tvFiles.LabelEdit = true;

            tvFiles.AfterLabelEdit += tvFiles_AfterLabelEdit1;
            tvFiles.SelectedNode.BeginEdit();
        }

        /// <summary>
        /// Show information about product.
        /// </summary>
        private void dgvData_DoubleClick(object sender, EventArgs e)
        {
            if (CurrenTreeNode == null)
            {
                return;
            }
            Vertex vertex = root.ReturnVertex(CurrenTreeNode.FullPath);

            if (dgvData.SelectedRows == null || dgvData.SelectedRows.Count != 1)
            {
                return;
            }
            int index = dgvData.SelectedRows[0].Index;

            string name = dgvData[0, index].Value.ToString();

            Product product = GetProduct(vertex, name);
            if (product == null)
            {
                return;
            }
            Item item = new Item(vertex, product);

            vertex.Products.Remove(GetProduct(vertex, name));

            item.ShowDialog();

            if (!item.DeleteOrNot && !item.CancelOrNot)
            {
                vertex.Products.Add(item.Products);
            }
            else if (item.CancelOrNot && !item.DeleteOrNot)
            {
                vertex.Products.Add(item.Products);
            }
            ShowProducts(CurrenTreeNode);
        }

        /// <summary>
        /// Get product of folder by name.
        /// </summary>
        /// <param name="vertex"> Folder. </param>
        /// <param name="name"> Find name. </param>
        /// <returns> Finding product. </returns>
        private Product GetProduct(Vertex vertex, string name)
        {
            foreach (var item in vertex.Products)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// Refreah all data
        /// </summary>
        private void tabControlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // обновляю таблицу со всеми заказами
            if (tabControlMenu.SelectedIndex == 1)
            {
                dgvAllOrders.Rows.Clear();
                if (seller.GetAllOrders() != null && seller.GetAllOrders().Count != 0)
                {
                    foreach (var item in seller.GetAllOrders())
                    {
                        dgvAllOrders.Rows.Add(new object[] {item.Date, item.Customer, item.OrderNumber,
                            item.IsProcessed, item.IsPaid, item.IsShipped, item.IsDone});
                    }
                    dgvData.Refresh();
                }
            }
            // обновляю таблицу со всеми пользователями
            if (tabControlMenu.SelectedIndex == 2)
            {
                dgvAllCustomers.Rows.Clear();
                if (seller.GetAllActiveCustomers().Count != 0)
                {
                    foreach (var item in seller.GetAllActiveCustomers())
                    {
                        dgvAllCustomers.Rows.Add(new object[] {item.Login, item.Name, item.Surname,
                            item.Patronymic, item.Number, item.Address });
                    }
                    dgvAllCustomers.Refresh();
                }
            }
            // обновляю таблицу со всеми активными закзами
            if (tabControlMenu.SelectedIndex == 3)
            {
                dgvAllActiveOrders.Rows.Clear();
                if (seller.GetAllActiveOrders() != null && seller.GetAllActiveOrders().Count != 0)
                {
                    foreach (var item in seller.GetAllActiveOrders())
                    {
                        dgvAllActiveOrders.Rows.Add(new object[] {item.Date, item.Customer, item.OrderNumber,
                            item.IsProcessed, item.IsPaid, item.IsShipped, item.IsDone});
                    }
                }
            }
        }
        /// <summary>
        /// Shoe selected customer's orders.
        /// </summary>
        private void dgvAllCustomers_DoubleClick(object sender, EventArgs e)
        {
            if (dgvAllCustomers.SelectedRows == null || dgvAllCustomers.SelectedRows.Count != 1)
            {
                return;
            }
            int index = dgvAllCustomers.SelectedRows[0].Index;
            string name = dgvAllCustomers[0, index].Value.ToString();
            Customer customer = seller.GetCustomer(name);
            if (customer == null)
            {
                return;
            }
            dgvSelectedCustomer.Rows.Clear();
            foreach (var item in customer.Orders)
            {
                dgvSelectedCustomer.Rows.Add(new object[] {item.Date, item.Customer, item.OrderNumber,
                            item.IsProcessed, item.IsPaid, item.IsShipped, item.IsDone, item.GetTotalPrice()});
            }
            dgvSelectedCustomer.Refresh();
        }
        /// <summary>
        /// Get order.
        /// </summary>
        /// <param name="date">date</param>
        /// <param name="number">number</param>
        /// <param name="login">customer login</param>
        /// <returns>order</returns>
        private Order GetOrder(string date, long number, string login)
        {
            foreach (var item in (seller.GetCustomer(login)).Orders)
            {
                if (item.Date.ToString() == date && item.OrderNumber == number)
                {
                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// Reads the data after the change
        /// </summary>
        private void dgvAllOrders_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {
                string date = dgvAllOrders[0, index].Value.ToString();
                long number = (long)dgvAllOrders[2, index].Value;
                string login = dgvAllOrders[1, index].Value.ToString().Trim();
                Order order = GetOrder(date, number, login);
                order.IsProcessed = (bool)dgvAllOrders[3, index].Value;
                order.IsShipped = (bool)dgvAllOrders[5, index].Value;
                order.IsDone = (bool)dgvAllOrders[6, index].Value;
            }
        }
        /// <summary>
        /// Reads the data after the change
        /// </summary>
        private void dgvAllActiveOrders_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {
                string date = dgvAllActiveOrders[0, index].Value.ToString();
                long number = (long)dgvAllActiveOrders[2, index].Value;
                string login = dgvAllActiveOrders[1, index].Value.ToString().Trim();
                Order order = GetOrder(date, number, login);
                order.IsProcessed = (bool)dgvAllActiveOrders[3, index].Value;
                order.IsShipped = (bool)dgvAllActiveOrders[5, index].Value;
                order.IsDone = (bool)dgvAllActiveOrders[6, index].Value;
            }
        }
        /// <summary>
        /// Reads the data after the change
        /// </summary>
        private void dgvSelectedCustomer_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {
                string date = dgvSelectedCustomer[0, index].Value.ToString();
                long number = (long)dgvSelectedCustomer[2, index].Value;
                string login = dgvSelectedCustomer[1, index].Value.ToString().Trim();
                Order order = GetOrder(date, number, login);
                order.IsProcessed = (bool)dgvSelectedCustomer[3, index].Value;
                order.IsShipped = (bool)dgvSelectedCustomer[5, index].Value;
                order.IsDone = (bool)dgvSelectedCustomer[6, index].Value;
            }
        }
        /// <summary>
        /// Show selected order's purchases.
        /// </summary>
        private void dgvAllOrders_DoubleClick(object sender, EventArgs e)
        {
            if (dgvAllOrders.SelectedRows == null || dgvAllOrders.SelectedRows.Count != 1)
            {
                return;
            }

            int index = dgvAllOrders.SelectedRows[0].Index;
            string data = dgvAllOrders[0, index].Value.ToString();
            string login = dgvAllOrders[1, index].Value.ToString();
            long number = (long)dgvAllOrders[2, index].Value;

            Order order = GetOrder(data, number, login);
            if (order == null)
            {
                return;
            }
            PurchasesInOrder purchasesInOrder = new PurchasesInOrder(order);
            purchasesInOrder.ShowDialog();

        }
        /// <summary>
        /// Show selected order's purchases.
        /// </summary>
        private void dgvAllActiveOrders_DoubleClick(object sender, EventArgs e)
        {
            if (dgvAllActiveOrders.SelectedRows == null || dgvAllActiveOrders.SelectedRows.Count != 1)
            {
                return;
            }

            int index = dgvAllActiveOrders.SelectedRows[0].Index;
            string data = dgvAllActiveOrders[0, index].Value.ToString();
            string login = dgvAllActiveOrders[1, index].Value.ToString();
            long number = (long)dgvAllActiveOrders[2, index].Value;

            Order order = GetOrder(data, number, login);
            if (order == null)
            {
                return;
            }
            PurchasesInOrder purchasesInOrder = new PurchasesInOrder(order);
            purchasesInOrder.ShowDialog();

        }
        /// <summary>
        /// Show selected order's purchases.
        /// </summary>
        private void dgvSelectedCustomer_DoubleClick(object sender, EventArgs e)
        {
            if (dgvSelectedCustomer.SelectedRows == null || dgvSelectedCustomer.SelectedRows.Count != 1)
            {
                return;
            }
            int index = dgvSelectedCustomer.SelectedRows[0].Index;
            string data = dgvSelectedCustomer[0, index].Value.ToString();
            string login = dgvSelectedCustomer[1, index].Value.ToString();
            long number = (long)dgvSelectedCustomer[2, index].Value;

            Order order = GetOrder(data, number, login);
            if (order == null)
            {
                return;
            }
            PurchasesInOrder purchasesInOrder = new PurchasesInOrder(order);
            purchasesInOrder.ShowDialog();
        }
        /// <summary>
        /// Create CSV about gold customers
        /// </summary>
        private void reportOfGoldCustomers_Click(object sender, EventArgs e)
        {
            try
            {
                string[] output = CreateTextRorCSV();
                if (output != null)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.OverwritePrompt = true;
                    sfd.CheckPathExists = true;
                    sfd.Filter = "CSV (*.csv)|*.csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllLines(sfd.FileName, output, System.Text.Encoding.UTF8);
                        MessageBox.Show("Your file was generated and its ready for use.");

                    }
                }
            }
            catch
            {
                MessageBox.Show("Failed", "Error", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// Create text to csf of gold customers.
        /// </summary>
        /// <returns></returns>
        private string[] CreateTextRorCSV()
        {
            List<string> customers = new List<string>();
            string columnNames = "Customer name, Customer surname, Customer patronymic, Customer login, Total price";
            GoldCustomers goldCustomers = new GoldCustomers();
            goldCustomers.ShowDialog();
            if (goldCustomers.Price != -1 && seller.Customers != null)
            {
                customers.Add(columnNames);
                foreach (var item in seller.Customers)
                {
                    if (item.GetTotalPrice() > goldCustomers.Price)
                    {
                        string client = item.Name + "," + item.Surname + ","
                            + item.Patronymic + "," + item.Login + "," + item.GetTotalPrice().ToString();
                        customers.Add(client);
                    }
                }
                string[] newGoldCustomers = new string[customers.Count];
                int i = 0;
                foreach (var item in customers)
                {
                    newGoldCustomers[i] = item;
                    i++;
                }
                return newGoldCustomers;
            }
            return null;
        }
    }
}
