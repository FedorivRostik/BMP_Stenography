using BMP_Stenography.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
            List<ComboItem> comboItems = new List<ComboItem>
            {
               new ComboItem { Id = 1, Text = "starts" },
                 new ComboItem { Id = 2, Text = "circles" },
                   new ComboItem { Id = 3, Text = "rectangles" },
                     new ComboItem { Id = 4, Text = "starts2" },
                       new ComboItem { Id = 5, Text = "lines" },
                         new ComboItem { Id = 6, Text = "rectangles2" },
            };
            templateComboBox.DataSource = comboItems;

            comboItems = new List<ComboItem>
            {
               new ComboItem { Id = 1, Text = "yellow" },
                new ComboItem { Id = 2, Text = "green" },
                 new ComboItem { Id = 3, Text = "silver" },

            };
            colorComboBox.DataSource = comboItems;
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
        private void createButton_Click(object sender, EventArgs e)
        {
            try
            {
                GetImage();

            }
            catch (IOException ex)
            {

                MessageBox.Show("Save to another file: " + ex.Message);
            }
        }
        private void GetImage()
        {
            // connection string to the MSSQL database
            string sqlQuery = "SELECT image FROM [image] WHERE Id = @Id;";
            int imageId = 3;
            // create a new SQL connection and command objects

            using (SqlCommand command = new SqlCommand(sqlQuery, _conn))
            {
                // add the ID parameter to the command object
                command.Parameters.AddWithValue("@Id", imageId);

                // open the connection to the database
                _conn.Open();

                // execute the SQL command and retrieve the image data as a byte array
                byte[] imageData = (byte[])command.ExecuteScalar();

                // create a new memory stream and write the image data to it
                string str = createNegativeImage(imageData);
                LoadChangedImage(str);
                _conn.Close();
            }
        }
        private string createNegativeImage(byte[] inputFileData)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            ComboItem selectedComboItem = (ComboItem)colorComboBox.SelectedItem;
            int colorCombinationType = selectedComboItem.Id;

             selectedComboItem = (ComboItem)templateComboBox.SelectedItem;
            int colorDependenceType = selectedComboItem.Id;

            using (var fin = new MemoryStream(inputFileData)) // using MemoryStream instead of FileStream to read the input file data
            using (var fout = new FileStream($"Image_{DateTime.Now.ToString("yyyy - MM - dd HH_mm_ss")}.bmp", FileMode.Create, FileAccess.Write))
            {
                // Read the BMP header
                byte[] header = new byte[54];
                fin.Read(header, 0, header.Length);
                fout.Write(header, 0, header.Length);
                long fileSize = fin.Length;
                fout.Seek(fileSize, SeekOrigin.Begin);
                fout.WriteByte(0);
                while (fout.Position < fileSize)
                {
                    fout.WriteByte(0);
                }
                fout.Seek(0, SeekOrigin.Begin);
                fout.Write(header, 0, header.Length);
                // Read the pixel data into a dynamic array
                int width = BitConverter.ToInt32(header, 18);
                int height = BitConverter.ToInt32(header, 22);
                int rowSize = (width * 3 + 3) & (~3); // round up to a multiple of 4
                int dataSize = (int)(fileSize - header.Length);
                Pixel[] pixels = new Pixel[dataSize / Marshal.SizeOf(typeof(Pixel))];
                for (int i = 0; i < pixels.Length; i++)
                {
                    pixels[i].b = (byte)fin.ReadByte();
                    pixels[i].g = (byte)fin.ReadByte();
                    pixels[i].r = (byte)fin.ReadByte();
                }

                // Invert the colors of each pixel
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int index = y * width + x;

                        // Compute the color based on pixel coordinates
                        if (colorDependenceType == 1)
                        {
                            // First color dependence type
                            pixels[index].r = (byte)0;
                            pixels[index].g = (byte)(((x * y) * 255 * 255) / 65535);
                            pixels[index].b = (byte)0;
                        }
                        else if (colorDependenceType == 2)
                        {
                            // First color dependence type
                            pixels[index].r = (byte)0;
                            pixels[index].g = (byte)(((x * y) * 255 * 255) / Math.Sqrt(width + height * 10000));
                            pixels[index].b = (byte)0;

                        }
                        else if (colorDependenceType == 3)
                        {
                            // First color dependence type
                            pixels[index].r = (byte)0;
                            pixels[index].g = (byte)(((x * y) * 255 * 255) / (width + 1 * 10));
                            pixels[index].b = (byte)0;
                        }
                        else if (colorDependenceType == 4)
                        {
                            // First color dependence type
                            pixels[index].r = (byte)0;
                            pixels[index].g = (byte)(((index % width + 1) * (x ^ y) + Math.Sqrt(width ^ height)));
                            pixels[index].b = (byte)0;
                        }
                        else if (colorDependenceType == 5)
                        {
                            // First color dependence type
                            pixels[index].r = (byte)0;
                            pixels[index].g = (byte)(((x + y) * 255 * 3.14159265358979));
                            pixels[index].b = (byte)0;
                        }
                        else if (colorDependenceType == 6)
                        {
                            // First color dependence type
                            pixels[index].r = (byte)0;
                            pixels[index].g = (byte)((x ^ y) * 255 / ((width + height) / 2));
                            pixels[index].b = (byte)0;
                        }

                    }
                }

                // Apply the selected color combination to the inverted pixel data
                if (colorCombinationType == 1)
                {
                    // First color combination type
                    for (int i = 0; i < pixels.Length; i++)
                    {
                        pixels[i].r ^= pixels[i].g;
                    }
                }
                else if (colorCombinationType ==2)
                {
                    // First color combination type
                    for (int i = 0; i < pixels.Length; i++)
                    {
                        pixels[i].g ^= pixels[i].b;
                    }
                }
                else if (colorCombinationType == 3)
                {
                    // First color combination type
                    for (int i = 0; i < pixels.Length; i++)
                    {
                        pixels[i].r ^= pixels[i].b^=pixels[i].g;
                    }
                }

                // Write the final pixel data to the output file
                for (int i = 0; i < pixels.Length; i++)
                {
                    fout.WriteByte(pixels[i].b);
                    fout.WriteByte(pixels[i].g);
                    fout.WriteByte(pixels[i].r);
                }




                // Save the final pixel data to a file using a SaveFileDialog

                saveFileDialog.Filter = "Bitmap Image|*.bmp";
                saveFileDialog.Title = "Save Negative Image As...";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var foutt = new BinaryWriter(File.Open(saveFileDialog.FileName, FileMode.Create)))
                    {
                        foutt.Write(header);
                        for (int i = 0; i < pixels.Length; i++)
                        {
                            foutt.Write(pixels[i].b);
                            foutt.Write(pixels[i].g);
                            foutt.Write(pixels[i].r);
                        }
                    }
                    // Load the saved BMP file into the PictureBox control

                }
            }
            return saveFileDialog.FileName;


        }
        private void LoadChangedImage(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Bitmap bmp = new Bitmap(str);
                createPictureBox.Image = bmp;
            }
        }
    }
}

