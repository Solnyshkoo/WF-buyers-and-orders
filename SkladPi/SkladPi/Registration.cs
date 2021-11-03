using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using SkladPi.Model;

namespace SkladPi
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Telephone number check.
        /// </summary>
        private void textBoxPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (textBoxPhoneNumber.Text.Trim().Length == 0)
            {
                notificationPhoneNumber.Visible = true;
            }
            else
            {
                notificationPhoneNumber.Visible = false;
            }

            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            }

        }
        /// <summary>
        /// Shows notification if password is incorrect.
        /// </summary>
        private void textBoxRepeatPassword_TextChanged(object sender, EventArgs e)
        {
            notificationPassword.Visible = false;
            if (textBoxPassword.Text.Trim() != textBoxRepeatPassword.Text.Trim())
            {
                labelNotification.Visible = true;
                buttonOkey.Enabled = false;
            }
            else
            {
                labelNotification.Visible = false;
                buttonOkey.Enabled = true;
            }
        }
        /// <summary>
        /// Check entered data if they are correct.
        /// </summary>
        private void buttonOkey_Click(object sender, EventArgs e)
        {
            if (FindUser(textBoxLogin.Text.Trim()) != null || textBoxLogin.Text.Trim() == "admin")
            {
                MessageBox.Show("User already exists");
                return;
            }
            if (textBoxPhoneNumber.Text.Trim().Length > 10)
            {
                MessageBox.Show("Phone nuumber is too long");
                return;
            }

            if (textBoxName.Text.Trim().Length == 0)
            {
                notificationName.Visible = true;
                return;
            }
            else
                notificationName.Visible = false;

            if (textBoxSurname.Text.Trim().Length == 0)
            {
                notificationSurname.Visible = true;
                return;

            }
            else
                notificationSurname.Visible = false;

            if (textBoxPatronymic.Text.Trim().Length == 0)
            {
                notificationPatronymic.Visible = true;
                return;

            }
            else
                notificationPatronymic.Visible = false;
            if (textBoxAddress.Text.Trim().Length == 0)
            {
                notificationAddress.Visible = true;
                return;

            }
            else
                notificationAddress.Visible = false;
            if (textBoxPhoneNumber.Text.Trim().Length == 0)
            {
                notificationPhoneNumber.Visible = true;
                return;
            }
            else
                notificationPhoneNumber.Visible = false;
            if (textBoxLogin.Text.Trim().Length == 0)
            {
                notificationLogin.Visible = true;
                return;
            }
            else
                notificationLogin.Visible = false;
            if (textBoxPassword.Text.Trim().Length == 0)
            {
                notificationPassword.Visible = true;
                return;
            }
            else
                notificationPassword.Visible = false;
            if (textBoxRepeatPassword.Text.Trim().Length == 0)
            {
                notificationPassword.Visible = true;
                return;
            }
            else
                notificationPassword.Visible = false;

            double phoneNumber;

            double.TryParse(textBoxPhoneNumber.Text.Trim(), out phoneNumber);
            Customer customer = new Customer(textBoxName.Text.Trim(), textBoxSurname.Text.Trim(), textBoxPatronymic.Text.Trim(),
                phoneNumber, textBoxAddress.Text.Trim(), textBoxLogin.Text.Trim(), HashPassword(textBoxPassword.Text.Trim()));
            SkladPi.seller.AddCustomer(customer);
            this.Close();
        }
        /// <summary>
        /// Block the "Okey" button if empty name has been entered.
        /// </summary>
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxName.Text.Trim().Length == 0)
            {
                buttonOkey.Enabled = false;
            }
            else
            {
                buttonOkey.Enabled = true;
            }
        }
        /// <summary>
        /// Hashing the password.
        /// </summary>
        /// <param name="password">password</param>
        /// <returns>Hashed password</returns>
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
        /// <summary>
        /// Check if such user with such login is already registered.
        /// </summary>
        /// <param name="login">login</param>
        /// <returns>customer if found; null otherwise</returns>
        private Customer FindUser(string login)
        {
            foreach (var item in SkladPi.seller.Customers)
            {
                if (item.Login == login)
                {
                    return item;
                }
            }
            return null;
        }
        /// <summary>
        /// Closes the form.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
