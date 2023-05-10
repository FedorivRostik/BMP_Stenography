using BMP_Stenography.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
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

        private void uploadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set the file filter to display only BMP files
            openFileDialog1.Filter = "BMP files (*.bmp)|*.bmp";

            // Set the title of the dialog
            openFileDialog1.Title = "Select a BMP file";

            // Display the dialog and wait for the user to select a file
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the path of the selected file
                string filePath = openFileDialog1.FileName;

                // Read the file into a byte array
                byte[] fileData = File.ReadAllBytes(filePath);

                // Define the SQL query to insert the image into the table
                string query = "INSERT INTO [image] (title, image,user_id) VALUES (@ImageName, @ImageData,@UserId)";

                // Create a SqlConnection object to connect to the database

                // Create a SqlCommand object to execute the query
                SqlCommand command = new SqlCommand(query, _conn);

                // Create a SqlParameter object for the image name parameter
                SqlParameter imageNameParam = new SqlParameter("@ImageName", SqlDbType.VarChar);

                imageNameParam.Value = $"Image - {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.bmp";
                command.Parameters.Add(imageNameParam);

                // Create a SqlParameter object for the image data parameter
                SqlParameter imageDataParam = new SqlParameter("@ImageData", SqlDbType.Image);
                imageDataParam.Value = fileData;
                command.Parameters.Add(imageDataParam);

                SqlParameter UserId = new SqlParameter("@UserId", SqlDbType.Int);
                UserId.Value = _user.Id;
                command.Parameters.Add(UserId);

                try
                {
                    // Open the database connection
                    _conn.Open();

                    // Execute the query to insert the image into the table
                    command.ExecuteNonQuery();
                    // Display a success message
                    MessageBox.Show("Image saved successfully.");
                    using (MemoryStream ms = new MemoryStream(fileData))
                    {
                        // Use the FromStream method of the Image class to create an Image object from the MemoryStream
                        Image image = Image.FromStream(ms);

                        // Set the Image property of the PictureBox control to the Image object
                        pictureBox.Image = image;
                    }
                }
                catch (Exception ex)
                {
                    // Display an error message
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Close the database connection
                    _conn.Close();
                }
            }
        }
    }
}
