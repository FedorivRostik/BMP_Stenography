using BMP_Stenography.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMP_Stenography
{
    public partial class MainForm : Form
    {
        private readonly User _user;
        private readonly SqlConnection _conn;
        public MainForm(User user, SqlConnection conn)
        {
            _user = user;
            _conn = conn;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
