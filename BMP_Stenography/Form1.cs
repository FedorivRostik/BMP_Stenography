using BMP_Stenography.Core;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BMP_Stenography
{
    public partial class LoginForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=ARTLINE-2VLKCIA;Initial Catalog=bmp_db;Integrated Security=True");
        public LoginForm()
        {
            InitializeComponent();
        }


        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
   

          
            try
            {
              await  conn.OpenAsync();
                string query = $"SELECT * FROM [user] WHERE login='{loginTextBox.Text}' AND password='{passwordTextBox.Text}'";
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

                    mainForm.Show();

                    this.Hide();
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
    }
}