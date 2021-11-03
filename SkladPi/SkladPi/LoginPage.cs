using System;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Data;
using SkladPi.Model;
using System.Text.Json;
using System.IO;

namespace SkladPi
{
    public partial class LoginPage : Form
    {
        /// <summary>
        /// Check if seller or not.
        /// </summary>
        public static bool IsSeller { get; set; } = false;
        /// <summary>
        /// Check if customer or not.
        /// </summary>
        public static bool IsCustomer { get; set; } = false;
        /// <summary>
        /// Active customer.
        /// </summary>
        public static Customer Customer { get; set; }
        public LoginPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Check if entered data is correct.
        /// </summary>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (tbLogin.Text.Trim().Length == 0 || tbPassword.Text.Trim().Length == 0)
            {
                if (tbLogin.Text.Trim().Length == 0)
                {
                    labelNotificationLogin.Visible = true;
                    labelNotificationLogin.Text = "Field must not be empty";
                    return;
                }
                if (tbPassword.Text.Trim().Length == 0)
                {
                    labelNotificationPassword.Visible = true;
                    labelNotificationPassword.Text = "Field must not be empty";
                    return;
                }
            }
            else
            {
                labelNotificationPassword.Visible = false;
                labelNotificationLogin.Visible = false;

                var customer = FindUser(tbLogin.Text.Trim());

                var index = ReturnIndex(tbLogin.Text.Trim());             

                if (tbLogin.Text.Trim() == "admin" && VerifyHashedPassword(SkladPi.seller.Password, tbPassword.Text.Trim()))
                {
                    MessageBox.Show("Hello, admin", "Greating", MessageBoxButtons.OK);
                    IsSeller = true;
                    IsCustomer = false;
                    this.Close();
                }
                else if (customer != null)
                {
                    if (VerifyHashedPassword(customer.Password, tbPassword.Text.Trim()))
                    {
                        MessageBox.Show($"Hello, {customer.Name}", "Greating", MessageBoxButtons.OK);
                        IsSeller = false;
                        IsCustomer = true;
                        Customer = customer;
                        CustomerPage.CurrentCustomer = SkladPi.seller.Customers[index];
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Password is wrong", "Error", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    MessageBox.Show("User not found", "Greating", MessageBoxButtons.OK);
                }
            }
        }
        /// <summary>
        /// Return customer's index in list.
        /// </summary>
        /// <param name="login">login</param>
        /// <returns>index</returns>
        private int ReturnIndex(string login)
        {
            for (int i = 0; i < SkladPi.seller.Customers.Count; i++)
            {
                if (SkladPi.seller.Customers[i].Login == login)
                {
                    return i;
                }
            }
            return -1;
        }
        /// <summary>
        /// Return customer according to login.
        /// </summary>
        /// <param name="login">login</param>
        /// <returns>customer</returns>
        private Customer FindUser(string login)
        {
            for (int i = 0; i < SkladPi.seller.Customers.Count; i++)
            {
                if (SkladPi.seller.Customers[i].Login == login)
                {
                    return SkladPi.seller.Customers[i];
                }
            }
            return null;
        }
        /// <summary>
        /// Open registration form.
        /// </summary>
        private void linkLabelSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IsSeller = false;
            var registrate = new Registration();
            registrate.ShowDialog();
        }
        /// <summary>
        /// Check if entered data is correct.
        /// </summary>
        private void tbLogin_TextChanged(object sender, EventArgs e)
        {
            if (tbLogin.Text.Trim().Contains('\\') || tbLogin.Text.Trim().Contains(';'))
            {
                buttonOk.Enabled = false;
                labelNotificationLogin.Visible = true;
                labelNotificationLogin.Text = "You cannot use: ; , \\  ";
            }
            else
            {
                buttonOk.Enabled = true;
                labelNotificationLogin.Visible = false;
            }
        }
        /// <summary>
        /// Verify password.
        /// </summary>
        /// <param name="hashedPassword">Hashed password</param>
        /// <param name="password">Password</param>
        /// <returns>true if correct; false otherwise</returns>
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }
        /// <summary>
        /// Check if bytes arrays are equel.
        /// </summary>
        /// <returns>bool</returns>
        private static bool ByteArraysEqual(byte[] buffer3, byte[] buffer4)
        {
            if (buffer3 == buffer4) return true;
            if (buffer3 == null || buffer4 == null) return false;
            if (buffer3.Length != buffer4.Length) return false;
            for (int i = 0; i < buffer3.Length; i++)
            {
                if (buffer3[i] != buffer4[i]) return false;
            }
            return true;
        }
        /// <summary>
        /// Deserialization.
        /// </summary>
        private void LoginPage_Load(object sender, EventArgs e)
        {
            try
            {
                string pathSeller = Path.Combine(Application.StartupPath, "Seller.json");
                string jsonSeller = File.ReadAllText(pathSeller);
                SkladPi.seller = JsonSerializer.Deserialize<Seller>(jsonSeller);
            }
            catch
            { }
            if (SkladPi.seller == null)
            {
                SkladPi.seller = new Seller();
            }
        }

        /// <summary>
        /// Serialization.
        /// </summary>
        private void LoginPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string jsonSeller = JsonSerializer.Serialize(SkladPi.seller);
                File.WriteAllText(Path.Combine(Application.StartupPath, "Seller.json"), jsonSeller);

            }
            catch { }
        }
    }
}
