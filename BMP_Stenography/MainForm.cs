using BMP_Stenography.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
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


            UpdateImagePictureBox();
            if (_conn.State != ConnectionState.Open)
            {

                _conn.Open();

            }
            string query = $"SELECT * FROM [user] WHERE id='{_user.Id}'";
            SqlDataAdapter sda = new SqlDataAdapter(query, _conn);

            DataTable dataTable = new DataTable();
            sda.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
              
                 Convert.ToInt16(dataTable.Rows[0]["id"]);
            }
            colorComboBox.SelectedIndex = Convert.ToInt16(dataTable.Rows[0]["color"])-1;
            templateComboBox.SelectedIndex = Convert.ToInt16(dataTable.Rows[0]["template"])-1;
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
                if (_conn.State != ConnectionState.Open)
                {
                   
                        _conn.Open();
                    
                }
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
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
                try
                {
                    // Open the database connection
                    if (_conn.State != ConnectionState.Open)
                    {
                        _conn.Open();
                    }

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
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
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
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                }

                try
                {
                    UpdateImagePictureBox();

                }
                catch (Exception)
                {
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
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
            ComboItem selectedComboItem = (ComboItem)imageComboBox.SelectedItem;
            if (selectedComboItem!=null)
            {

            int imageId = selectedComboItem.Id;
                // create a new SQL connection and command objects
                if (_conn.State != ConnectionState.Open)
                {
                    _conn.Open();
                }
                using (SqlCommand command = new SqlCommand(sqlQuery, _conn))
            {
                // add the ID parameter to the command object
                command.Parameters.AddWithValue("@Id", imageId);

                // open the connection to the database


                // execute the SQL command and retrieve the image data as a byte array
                byte[] imageData = (byte[])command.ExecuteScalar();

                // create a new memory stream and write the image data to it
                string str = createNegativeImage(imageData);
                LoadChangedImage(str);
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Add photo");
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
                else if (colorCombinationType == 2)
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
                        pixels[i].r ^= pixels[i].b ^= pixels[i].g;
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

        private void imageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboItem selectedComboItem = (ComboItem)imageComboBox.SelectedItem;
            int imageId = selectedComboItem.Id;
            // create a SqlCommand object with a SELECT statement
            SqlCommand command = new SqlCommand("SELECT image FROM [image] WHERE Id = @Id", _conn);

            // add a parameter for the picture ID
            command.Parameters.AddWithValue("@Id", imageId);

            // open the connection
            try
            {
                if (_conn.State != ConnectionState.Open)
                {
                    _conn.Open();
                }
                byte[] imageData = (byte[])command.ExecuteScalar();

                // close the connection

                // create a MemoryStream object from the image data
                using (MemoryStream stream = new MemoryStream(imageData))
                {

                    // create an Image object from the MemoryStream
                    Image image = Image.FromStream(stream);

                    // display the image in a PictureBox control
                    selectedPicture.Image = image;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }


        }

        private void UpdateImagePictureBox()
        {
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }
            // create a SqlCommand object with a SELECT statement
            SqlCommand command = new SqlCommand("SELECT Id FROM [image] WHERE user_id = @UserId", _conn);

            // add a parameter for the UserId
            command.Parameters.AddWithValue("@UserId", _user.Id);


            // create a SqlDataReader object
            SqlDataReader reader = command.ExecuteReader();

            // loop through the results and add each ID to a list
            List<int> ids = new List<int>();
            while (reader.Read())
            {
                int id = (int)reader["Id"];
                ids.Add(id);
            }
            ids = ids.OrderByDescending(x => x).Take(3).ToList();
            List<ComboItem> comboItems = new List<ComboItem>();
            if (ids.Count > 0)
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    comboItems.Add(new ComboItem { Id = ids[i], Text = $"image {ids[i]}" });
                }
            }
            imageComboBox.DataSource = comboItems;
            // close the reader and the connection
            reader.Close();
            if (_conn.State == ConnectionState.Open)
            {
                _conn.Close();
            }

            ComboItem selectedComboItem = (ComboItem)imageComboBox.SelectedItem;
            if (selectedComboItem != null)
            {
                int imageId = selectedComboItem.Id;
                // create a SqlCommand object with a SELECT statement
                command = new SqlCommand("SELECT image FROM [image] WHERE Id = @Id", _conn);

                // add a parameter for the picture ID
                command.Parameters.AddWithValue("@Id", imageId);

                // open the connection
                try
                {
                    if (_conn.State != ConnectionState.Open)
                    {
                        _conn.Open();
                    }
                    byte[] imageData = (byte[])command.ExecuteScalar();

                    // close the connection

                    // create a MemoryStream object from the image data
                    using (MemoryStream stream = new MemoryStream(imageData))
                    {

                        // create an Image object from the MemoryStream
                        Image image = Image.FromStream(stream);

                        // display the image in a PictureBox control
                        selectedPicture.Image = image;
                    }
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                }
            }
        }

        private void codeButton_Click(object sender, EventArgs e)
        {
            string str = "";
            string sqlQuery = "SELECT image FROM [image] WHERE Id = @Id;";
            ComboItem selectedComboItem = (ComboItem)imageComboBox.SelectedItem;
            if (selectedComboItem != null)
            {

                int imageId = selectedComboItem.Id;
                // create a new SQL connection and command objects
                if (_conn.State != ConnectionState.Open)
                {
                    _conn.Open();
                }
                using (SqlCommand command = new SqlCommand(sqlQuery, _conn))
                {
                    // add the ID parameter to the command object
                    command.Parameters.AddWithValue("@Id", imageId);

                    // open the connection to the database


                    // execute the SQL command and retrieve the image data as a byte array
                    byte[] imageData = (byte[])command.ExecuteScalar();
                    str = StoreMessageInBmp(imageData, messageTextbox.Text);
                    if (str == "error")
                    {
                        MessageBox.Show("Choose another file");
                    }
                    else
                    {

                    LoadChangedImage(str);
                    }
                    // create a new memory stream and write the image data to it

                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                }



                try
                {
                    byte[] fileData = File.ReadAllBytes(str) ?? null;

                    // Define the SQL query to insert the image into the table
                    string query = "INSERT INTO [image] (title, image,user_id) VALUES (@ImageName, @ImageData,@UserId)";

                    // Create a SqlConnection object to connect to the database
                    if (_conn.State != ConnectionState.Open)
                    {
                        _conn.Open();
                    }
                    // Create a SqlCommand object to execute the query
                    SqlCommand commandd = new SqlCommand(query, _conn);

                    // Create a SqlParameter object for the image name parameter
                    SqlParameter imageNameParam = new SqlParameter("@ImageName", SqlDbType.VarChar);

                    imageNameParam.Value = $"Image - {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}.bmp";
                    commandd.Parameters.Add(imageNameParam);

                    // Create a SqlParameter object for the image data parameter
                    SqlParameter imageDataParam = new SqlParameter("@ImageData", SqlDbType.Image);
                    imageDataParam.Value = fileData;
                    commandd.Parameters.Add(imageDataParam);

                    SqlParameter UserId = new SqlParameter("@UserId", SqlDbType.Int);
                    UserId.Value = _user.Id;
                    commandd.Parameters.Add(UserId);
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                    try
                    {
                        // Open the database connection
                        if (_conn.State != ConnectionState.Open)
                        {
                            _conn.Open();
                        }

                        // Execute the query to insert the image into the table
                        commandd.ExecuteNonQuery();
                        // Display a success message
                        MessageBox.Show("Image saved successfully.");
                        using (MemoryStream ms = new MemoryStream(fileData))
                        {
                            // Use the FromStream method of the Image class to create an Image object from the MemoryStream
                            Image image = Image.FromStream(ms);

                            // Set the Image property of the PictureBox control to the Image object
                            pictureBox.Image = image;
                        }
                        if (_conn.State == ConnectionState.Open)
                        {
                            _conn.Close();
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
                        if (_conn.State == ConnectionState.Open)
                        {
                            _conn.Close();
                        }
                    }

                    try
                    {
                        UpdateImagePictureBox();

                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        if (_conn.State == ConnectionState.Open)
                        {
                            _conn.Close();
                        }
                    }
                }
                catch (Exception)
                {

                    
                }
                finally 
                {
                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                }
               
            }
            else
            {
                MessageBox.Show("Add photo");
            }
        }




        private int GetMaxMessageSize(byte[] imageData)
        {
            // Open the BMP file
            using (var ms = new MemoryStream(imageData))
            using (var bmp = new Bitmap(ms))
            {
                // Calculate the maximum message size in bytes
                int maxMessageBytes = bmp.Width * bmp.Height * 3 / 8;
                return maxMessageBytes;
            }
        }

        private string StoreMessageInBmp(byte[] imageData, string message)
        {
            using (var ms = new MemoryStream(imageData))
            using (var bmp = new Bitmap(ms))
            {
                string stopSequence = "0000000000000000";
                if (string.IsNullOrEmpty(message)) message = "emptyOrNull";

                int maxMessageSize = GetMaxMessageSize(imageData);
                if (message.Length + stopSequence.Length > maxMessageSize)
                {
                    MessageBox.Show("File is too small");
                    return string.Empty;
                }

                byte[] messageBytes = Encoding.ASCII.GetBytes(message);
                string binaryMessage = "";
                foreach (byte b in messageBytes)
                {
                    binaryMessage += Convert.ToString(b, 2).PadLeft(8, '0');
                }

                binaryMessage += stopSequence;

                // Embed the message in the image
                int index = 0;
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color pixel = bmp.GetPixel(x, y);
                        if (index < binaryMessage.Length)
                        {
                            //note: binaryMessage contains only 0 and 1
                            int red = pixel.R & 0xFE | (binaryMessage[index++] - '0');
                            int green = pixel.G & 0xFE | (index < binaryMessage.Length ? binaryMessage[index++] - '0' : 0);
                            int blue = pixel.B & 0xFE | (index < binaryMessage.Length ? binaryMessage[index++] - '0' : 0);
                            bmp.SetPixel(x, y, Color.FromArgb(pixel.A, red, green, blue));
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (index >= binaryMessage.Length)
                    {
                        break;
                    }
                }

                // Save the modified BMP file
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Bitmap files (*.bmp)|*.bmp";
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (var outputMs = new MemoryStream())
                        {
                            bmp.Save(outputMs, ImageFormat.Bmp);
                            try
                            {
                            File.WriteAllBytes(saveDialog.FileName, outputMs.ToArray());

                            }
                            catch (Exception)
                            {

                                return "error";
                            }
                        }
                    }
                    return saveDialog.FileName;
                }
            }
        }

        private void decodeButton_Click(object sender, EventArgs e)
        {
            string str = "";
            string sqlQuery = "SELECT image FROM [image] WHERE Id = @Id;";
            ComboItem selectedComboItem = (ComboItem)imageComboBox.SelectedItem;
            if (selectedComboItem != null)
            {

                int imageId = selectedComboItem.Id;
                // create a new SQL connection and command objects
                if (_conn.State != ConnectionState.Open)
                {
                    _conn.Open();
                }
                using (SqlCommand command = new SqlCommand(sqlQuery, _conn))
                {
                    // add the ID parameter to the command object
                    command.Parameters.AddWithValue("@Id", imageId);

                    // open the connection to the database


                    // execute the SQL command and retrieve the image data as a byte array
                    byte[] imageData = (byte[])command.ExecuteScalar();
                    ReadMessageFromBmp(imageData);

                    // create a new memory stream and write the image data to it

                    if (_conn.State == ConnectionState.Open)
                    {
                        _conn.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Add photo");
            }
        }

        private void ReadMessageFromBmp(byte[] filePath)
        {
            // Open the BMP file
            Bitmap bmp;
            using (var ms = new MemoryStream(filePath))
            {
                bmp = new Bitmap(ms);
            }

            string stopSequence = "0000000000000000";

            // Read the message from the image
            string binaryMessage = "";
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color pixel = bmp.GetPixel(x, y);
                    int redBit = pixel.R & 0x01;
                    int greenBit = pixel.G & 0x01;
                    int blueBit = pixel.B & 0x01;
                    binaryMessage += redBit.ToString() + greenBit.ToString() + blueBit.ToString();
                    if (binaryMessage.EndsWith(stopSequence))
                    {
                        break;
                    }
                }
                if (binaryMessage.EndsWith(stopSequence))
                {
                    break;
                }
            }

            // Remove the stop sequence from the binary message
            binaryMessage = binaryMessage.Substring(0, binaryMessage.Length - stopSequence.Length);

            // It may contain additional zeroes at the end since we were adding 0 to bmp if binaryMessage was out of chars (line 60,61)
            int paddingLength = binaryMessage.Length % 8;
            binaryMessage = binaryMessage.Substring(0, binaryMessage.Length - paddingLength);

            // Convert the binary message to ASCII
            List<byte> messageBytes = new List<byte>();
            for (int i = 0; i < binaryMessage.Length; i += 8)
            {
                string byteString = binaryMessage.Substring(i, 8);
                messageBytes.Add(Convert.ToByte(byteString, 2));
            }
            if (messageBytes.Count == 0)
            {
                decodeTextBox.Text = "Message not found";
            }
            foreach (byte b in messageBytes)
            {
                if (b < '0')
                {
                    decodeTextBox.Text = "Message not found";
                }
            }

            decodeTextBox.Text = Encoding.ASCII.GetString(messageBytes.ToArray());
            bmp.Dispose();



        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
          
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_conn.State != ConnectionState.Open)
                {
                    _conn.Open();
                }
                string query = "Update [user] SET [template]=@template, [color]=@color where [id]=@id";
                using (SqlCommand command = new SqlCommand(query, _conn))
                {
                    command.Parameters.AddWithValue("@id", _user.Id);
                    command.Parameters.AddWithValue("@color", ((ComboItem)colorComboBox.SelectedItem).Id);
                    command.Parameters.AddWithValue("@template", ((ComboItem)templateComboBox.SelectedItem).Id);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error:" + ex);
            }
            finally 
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
        
            DialogResult result = MessageBox.Show("Are you sure you want to close the window?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Cancel the closing event if the user chooses not to close
            }
        }
    }
}

