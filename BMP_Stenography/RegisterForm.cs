using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BMP_Stenography
{
    public partial class RegisterForm : Form
    {
        private readonly SqlConnection _conn;
        public RegisterForm(SqlConnection conn)
        {
            _conn = conn;
            InitializeComponent();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            if (loginTextBox.Text.Count() > 5 && passwordTextBox.Text.Count() > 5)
            {


                try
                {
                    _conn.Open();
                    string query = "INSERT INTO [user] (login, password) VALUES (@login, @password)";
                    using (SqlCommand command = new SqlCommand(query, _conn))
                    {
                        command.Parameters.AddWithValue("@login", loginTextBox.Text);
                        command.Parameters.AddWithValue("@password", HashStringSHA512(passwordTextBox.Text));
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Created");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                }
                finally { _conn.Close(); }
            }
            else
            {
                MessageBox.Show("Login and password must be >5");
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string HashStringSHA512(string input)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha512.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the window?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Cancel the closing event if the user chooses not to close
            }
        }
    }
}
