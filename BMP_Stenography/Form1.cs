using BMP_Stenography.Core;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace BMP_Stenography
{
    public partial class LoginForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=ARTLINE-2VLKCIA;Initial Catalog=bmp_db;Integrated Security=True");
        public LoginForm()
        {
            InitializeComponent();
            passwordTextBox.UseSystemPasswordChar = true;
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
   

          
            try
            {
              await  conn.OpenAsync();
                string query = $"SELECT * FROM [user] WHERE login='{loginTextBox.Text}' AND password='{HashStringSHA512(passwordTextBox.Text)}'";
                SqlDataAdapter sda = new SqlDataAdapter(query,conn);
                 
                DataTable dataTable= new DataTable();
                sda.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    User user = new User();
                    user.Login = loginTextBox.Text;
                    user.Password = passwordTextBox.Text;
                    
                    user.Id = Convert.ToInt16(dataTable.Rows[0]["id"]);
                    MainForm mainForm = new MainForm(user,conn);
                    conn.Close();
                    this.Hide();
                    mainForm.FormClosed += Reg_FormClosed;
                    mainForm.Show();
                   

                }
                else
                {
                    MessageBox.Show("Invalid details","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

                    loginTextBox.Clear();
                    passwordTextBox.Clear();

                    loginTextBox.Focus();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Error");
            }
            finally
            {
                conn.Close();
            }
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
        private async void exitButton_Click(object sender, EventArgs e)
        {
            try
            {

               
                DialogResult res;
                res = MessageBox.Show("Wanna exit?", "Exit?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    this.Show();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm reg = new RegisterForm(conn);
            reg.FormClosed += Reg_FormClosed; // Add event handler
            reg.ShowDialog();
        }

        private void Reg_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show(); // Show the main form when form is closed
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { passwordTextBox.UseSystemPasswordChar = false; }
            else { passwordTextBox.UseSystemPasswordChar = true; }
        }
    }
}