namespace BMP_Stenography
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.uploadButton = new System.Windows.Forms.Button();
            this.createPictureBox = new System.Windows.Forms.PictureBox();
            this.createButton = new System.Windows.Forms.Button();
            this.imageComboBox = new System.Windows.Forms.ComboBox();
            this.codeButton = new System.Windows.Forms.Button();
            this.selectedPicture = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.colorComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.templateComboBox = new System.Windows.Forms.ComboBox();
            this.decodeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.messageTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.decodeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.createPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.DarkRed;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(874, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(300, 300);
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // uploadButton
            // 
            this.uploadButton.BackColor = System.Drawing.Color.Brown;
            this.uploadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.uploadButton.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.uploadButton.Location = new System.Drawing.Point(874, 309);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(300, 30);
            this.uploadButton.TabIndex = 6;
            this.uploadButton.Text = "Upload";
            this.uploadButton.UseVisualStyleBackColor = false;
            this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // createPictureBox
            // 
            this.createPictureBox.BackColor = System.Drawing.Color.DarkRed;
            this.createPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.createPictureBox.Location = new System.Drawing.Point(568, 3);
            this.createPictureBox.Name = "createPictureBox";
            this.createPictureBox.Size = new System.Drawing.Size(300, 300);
            this.createPictureBox.TabIndex = 7;
            this.createPictureBox.TabStop = false;
            // 
            // createButton
            // 
            this.createButton.BackColor = System.Drawing.Color.Brown;
            this.createButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createButton.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.createButton.Location = new System.Drawing.Point(568, 309);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(300, 30);
            this.createButton.TabIndex = 8;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = false;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // imageComboBox
            // 
            this.imageComboBox.BackColor = System.Drawing.Color.Brown;
            this.imageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.imageComboBox.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.imageComboBox.ForeColor = System.Drawing.Color.Black;
            this.imageComboBox.FormattingEnabled = true;
            this.imageComboBox.Location = new System.Drawing.Point(135, 12);
            this.imageComboBox.Name = "imageComboBox";
            this.imageComboBox.Size = new System.Drawing.Size(121, 29);
            this.imageComboBox.TabIndex = 9;
            this.imageComboBox.SelectedIndexChanged += new System.EventHandler(this.imageComboBox_SelectedIndexChanged);
            // 
            // codeButton
            // 
            this.codeButton.BackColor = System.Drawing.Color.Brown;
            this.codeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.codeButton.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.codeButton.Location = new System.Drawing.Point(144, 309);
            this.codeButton.Name = "codeButton";
            this.codeButton.Size = new System.Drawing.Size(112, 30);
            this.codeButton.TabIndex = 11;
            this.codeButton.Text = "Code";
            this.codeButton.UseVisualStyleBackColor = false;
            this.codeButton.Click += new System.EventHandler(this.codeButton_Click);
            // 
            // selectedPicture
            // 
            this.selectedPicture.BackColor = System.Drawing.Color.DarkRed;
            this.selectedPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selectedPicture.Location = new System.Drawing.Point(262, 3);
            this.selectedPicture.Name = "selectedPicture";
            this.selectedPicture.Size = new System.Drawing.Size(300, 300);
            this.selectedPicture.TabIndex = 10;
            this.selectedPicture.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(9, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Select image";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(9, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Select color";
            // 
            // colorComboBox
            // 
            this.colorComboBox.BackColor = System.Drawing.Color.Brown;
            this.colorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorComboBox.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.colorComboBox.ForeColor = System.Drawing.Color.Black;
            this.colorComboBox.FormattingEnabled = true;
            this.colorComboBox.Location = new System.Drawing.Point(135, 82);
            this.colorComboBox.Name = "colorComboBox";
            this.colorComboBox.Size = new System.Drawing.Size(121, 29);
            this.colorComboBox.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.DarkRed;
            this.label5.Location = new System.Drawing.Point(9, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "Select template";
            // 
            // templateComboBox
            // 
            this.templateComboBox.BackColor = System.Drawing.Color.Brown;
            this.templateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.templateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.templateComboBox.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.templateComboBox.ForeColor = System.Drawing.Color.Black;
            this.templateComboBox.FormattingEnabled = true;
            this.templateComboBox.Location = new System.Drawing.Point(135, 152);
            this.templateComboBox.Name = "templateComboBox";
            this.templateComboBox.Size = new System.Drawing.Size(121, 29);
            this.templateComboBox.TabIndex = 19;
            // 
            // decodeTextBox
            // 
            this.decodeTextBox.BackColor = System.Drawing.Color.Maroon;
            this.decodeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.decodeTextBox.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.decodeTextBox.ForeColor = System.Drawing.Color.Black;
            this.decodeTextBox.Location = new System.Drawing.Point(17, 262);
            this.decodeTextBox.Name = "decodeTextBox";
            this.decodeTextBox.ReadOnly = true;
            this.decodeTextBox.Size = new System.Drawing.Size(239, 29);
            this.decodeTextBox.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(53, 184);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Write message to code";
            // 
            // messageTextbox
            // 
            this.messageTextbox.BackColor = System.Drawing.Color.Maroon;
            this.messageTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.messageTextbox.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.messageTextbox.ForeColor = System.Drawing.Color.Black;
            this.messageTextbox.Location = new System.Drawing.Point(17, 207);
            this.messageTextbox.Name = "messageTextbox";
            this.messageTextbox.Size = new System.Drawing.Size(239, 29);
            this.messageTextbox.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.DarkRed;
            this.label4.Location = new System.Drawing.Point(74, 239);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 20);
            this.label4.TabIndex = 24;
            this.label4.Text = "Decoded value";
            // 
            // decodeButton
            // 
            this.decodeButton.BackColor = System.Drawing.Color.Brown;
            this.decodeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.decodeButton.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.decodeButton.Location = new System.Drawing.Point(17, 309);
            this.decodeButton.Name = "decodeButton";
            this.decodeButton.Size = new System.Drawing.Size(112, 30);
            this.decodeButton.TabIndex = 25;
            this.decodeButton.Text = "Decode";
            this.decodeButton.UseVisualStyleBackColor = false;
            this.decodeButton.Click += new System.EventHandler(this.decodeButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1186, 347);
            this.Controls.Add(this.decodeButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.messageTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.decodeTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.templateComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.colorComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.codeButton);
            this.Controls.Add(this.selectedPicture);
            this.Controls.Add(this.imageComboBox);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.createPictureBox);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.pictureBox);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.createPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.PictureBox createPictureBox;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.ComboBox imageComboBox;
        private System.Windows.Forms.Button codeButton;
        private System.Windows.Forms.PictureBox selectedPicture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox colorComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox templateComboBox;
        private System.Windows.Forms.TextBox decodeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox messageTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button decodeButton;
    }
}