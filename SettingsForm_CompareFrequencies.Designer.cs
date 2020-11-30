namespace CompareFrequencies
{
    partial class SettingsForm_CompareFrequencies
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm_CompareFrequencies));
            this.SetOutputFileButton = new System.Windows.Forms.Button();
            this.OutputFileTextbox = new System.Windows.Forms.TextBox();
            this.EncodingDropdown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.CSVQuoteTextbox = new System.Windows.Forms.TextBox();
            this.CSVDelimiterTextbox = new System.Windows.Forms.TextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CorpusNameTextbox = new System.Windows.Forms.TextBox();
            this.GetInputFileButton = new System.Windows.Forms.Button();
            this.IncludedCorporaListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RemoveCorpusButton = new System.Windows.Forms.Button();
            this.OutputSettingsGroupbox = new System.Windows.Forms.GroupBox();
            this.OutputFileQuoteTextbox = new System.Windows.Forms.TextBox();
            this.OutputFileEncodingDropdown = new System.Windows.Forms.ComboBox();
            this.OutputFileDelimiterTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.OutputSettingsGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SetOutputFileButton
            // 
            this.SetOutputFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetOutputFileButton.Location = new System.Drawing.Point(15, 237);
            this.SetOutputFileButton.Name = "SetOutputFileButton";
            this.SetOutputFileButton.Size = new System.Drawing.Size(108, 40);
            this.SetOutputFileButton.TabIndex = 0;
            this.SetOutputFileButton.Text = "Choose File";
            this.SetOutputFileButton.UseVisualStyleBackColor = true;
            this.SetOutputFileButton.Click += new System.EventHandler(this.SetOutputButton_Click);
            // 
            // OutputFileTextbox
            // 
            this.OutputFileTextbox.Enabled = false;
            this.OutputFileTextbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputFileTextbox.Location = new System.Drawing.Point(15, 208);
            this.OutputFileTextbox.MaxLength = 2147483647;
            this.OutputFileTextbox.Name = "OutputFileTextbox";
            this.OutputFileTextbox.Size = new System.Drawing.Size(414, 23);
            this.OutputFileTextbox.TabIndex = 1;
            // 
            // EncodingDropdown
            // 
            this.EncodingDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncodingDropdown.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncodingDropdown.FormattingEnabled = true;
            this.EncodingDropdown.Location = new System.Drawing.Point(26, 63);
            this.EncodingDropdown.Name = "EncodingDropdown";
            this.EncodingDropdown.Size = new System.Drawing.Size(268, 23);
            this.EncodingDropdown.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Choose Output File Location:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select Frequency List File Encoding";
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(423, 658);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(118, 40);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CSVQuoteTextbox
            // 
            this.CSVQuoteTextbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CSVQuoteTextbox.Location = new System.Drawing.Point(149, 126);
            this.CSVQuoteTextbox.MaxLength = 1;
            this.CSVQuoteTextbox.Name = "CSVQuoteTextbox";
            this.CSVQuoteTextbox.Size = new System.Drawing.Size(101, 23);
            this.CSVQuoteTextbox.TabIndex = 22;
            this.CSVQuoteTextbox.Text = "\"";
            this.CSVQuoteTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CSVDelimiterTextbox
            // 
            this.CSVDelimiterTextbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CSVDelimiterTextbox.Location = new System.Drawing.Point(27, 125);
            this.CSVDelimiterTextbox.MaxLength = 1;
            this.CSVDelimiterTextbox.Name = "CSVDelimiterTextbox";
            this.CSVDelimiterTextbox.Size = new System.Drawing.Size(101, 23);
            this.CSVDelimiterTextbox.TabIndex = 21;
            this.CSVDelimiterTextbox.Text = ",";
            this.CSVDelimiterTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(146, 107);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(86, 16);
            this.label42.TabIndex = 20;
            this.label42.Text = "CSV Quote:";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(26, 107);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(102, 16);
            this.label41.TabIndex = 19;
            this.label41.Text = "CSV Delimiter:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.CorpusNameTextbox);
            this.groupBox1.Controls.Add(this.GetInputFileButton);
            this.groupBox1.Controls.Add(this.CSVQuoteTextbox);
            this.groupBox1.Controls.Add(this.EncodingDropdown);
            this.groupBox1.Controls.Add(this.CSVDelimiterTextbox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.label41);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 302);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import Frequency Lists";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(24, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "Set Name for Corpus:";
            // 
            // CorpusNameTextbox
            // 
            this.CorpusNameTextbox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CorpusNameTextbox.Location = new System.Drawing.Point(26, 194);
            this.CorpusNameTextbox.MaxLength = 100;
            this.CorpusNameTextbox.Name = "CorpusNameTextbox";
            this.CorpusNameTextbox.Size = new System.Drawing.Size(221, 25);
            this.CorpusNameTextbox.TabIndex = 27;
            // 
            // GetInputFileButton
            // 
            this.GetInputFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GetInputFileButton.Location = new System.Drawing.Point(26, 240);
            this.GetInputFileButton.Name = "GetInputFileButton";
            this.GetInputFileButton.Size = new System.Drawing.Size(127, 40);
            this.GetInputFileButton.TabIndex = 26;
            this.GetInputFileButton.Text = "Choose Freq. List";
            this.GetInputFileButton.UseVisualStyleBackColor = true;
            this.GetInputFileButton.Click += new System.EventHandler(this.GetInputFileButton_Click);
            // 
            // IncludedCorporaListBox
            // 
            this.IncludedCorporaListBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IncludedCorporaListBox.FormattingEnabled = true;
            this.IncludedCorporaListBox.HorizontalScrollbar = true;
            this.IncludedCorporaListBox.ItemHeight = 18;
            this.IncludedCorporaListBox.Location = new System.Drawing.Point(21, 373);
            this.IncludedCorporaListBox.Name = "IncludedCorporaListBox";
            this.IncludedCorporaListBox.ScrollAlwaysVisible = true;
            this.IncludedCorporaListBox.Size = new System.Drawing.Size(432, 166);
            this.IncludedCorporaListBox.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "Corpora Included:";
            // 
            // RemoveCorpusButton
            // 
            this.RemoveCorpusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemoveCorpusButton.Location = new System.Drawing.Point(179, 545);
            this.RemoveCorpusButton.Name = "RemoveCorpusButton";
            this.RemoveCorpusButton.Size = new System.Drawing.Size(127, 45);
            this.RemoveCorpusButton.TabIndex = 28;
            this.RemoveCorpusButton.Text = "Remove Selected Corpus";
            this.RemoveCorpusButton.UseVisualStyleBackColor = true;
            this.RemoveCorpusButton.Click += new System.EventHandler(this.RemoveCorpusButton_Click);
            // 
            // OutputSettingsGroupbox
            // 
            this.OutputSettingsGroupbox.Controls.Add(this.OutputFileQuoteTextbox);
            this.OutputSettingsGroupbox.Controls.Add(this.SetOutputFileButton);
            this.OutputSettingsGroupbox.Controls.Add(this.OutputFileEncodingDropdown);
            this.OutputSettingsGroupbox.Controls.Add(this.OutputFileTextbox);
            this.OutputSettingsGroupbox.Controls.Add(this.OutputFileDelimiterTextbox);
            this.OutputSettingsGroupbox.Controls.Add(this.label1);
            this.OutputSettingsGroupbox.Controls.Add(this.label5);
            this.OutputSettingsGroupbox.Controls.Add(this.label6);
            this.OutputSettingsGroupbox.Controls.Add(this.label7);
            this.OutputSettingsGroupbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputSettingsGroupbox.Location = new System.Drawing.Point(496, 29);
            this.OutputSettingsGroupbox.Name = "OutputSettingsGroupbox";
            this.OutputSettingsGroupbox.Size = new System.Drawing.Size(441, 302);
            this.OutputSettingsGroupbox.TabIndex = 28;
            this.OutputSettingsGroupbox.TabStop = false;
            this.OutputSettingsGroupbox.Text = "Output File Settings";
            // 
            // OutputFileQuoteTextbox
            // 
            this.OutputFileQuoteTextbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputFileQuoteTextbox.Location = new System.Drawing.Point(138, 126);
            this.OutputFileQuoteTextbox.MaxLength = 1;
            this.OutputFileQuoteTextbox.Name = "OutputFileQuoteTextbox";
            this.OutputFileQuoteTextbox.Size = new System.Drawing.Size(101, 23);
            this.OutputFileQuoteTextbox.TabIndex = 33;
            this.OutputFileQuoteTextbox.Text = "\"";
            this.OutputFileQuoteTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OutputFileEncodingDropdown
            // 
            this.OutputFileEncodingDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OutputFileEncodingDropdown.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputFileEncodingDropdown.FormattingEnabled = true;
            this.OutputFileEncodingDropdown.Location = new System.Drawing.Point(15, 63);
            this.OutputFileEncodingDropdown.Name = "OutputFileEncodingDropdown";
            this.OutputFileEncodingDropdown.Size = new System.Drawing.Size(268, 23);
            this.OutputFileEncodingDropdown.TabIndex = 28;
            // 
            // OutputFileDelimiterTextbox
            // 
            this.OutputFileDelimiterTextbox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputFileDelimiterTextbox.Location = new System.Drawing.Point(16, 125);
            this.OutputFileDelimiterTextbox.MaxLength = 1;
            this.OutputFileDelimiterTextbox.Name = "OutputFileDelimiterTextbox";
            this.OutputFileDelimiterTextbox.Size = new System.Drawing.Size(101, 23);
            this.OutputFileDelimiterTextbox.TabIndex = 32;
            this.OutputFileDelimiterTextbox.Text = ",";
            this.OutputFileDelimiterTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(192, 16);
            this.label5.TabIndex = 29;
            this.label5.Text = "Select Output File Encoding";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(135, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 16);
            this.label6.TabIndex = 31;
            this.label6.Text = "CSV Quote:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 16);
            this.label7.TabIndex = 30;
            this.label7.Text = "CSV Delimiter:";
            // 
            // SettingsForm_CompareFrequencies
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 709);
            this.Controls.Add(this.OutputSettingsGroupbox);
            this.Controls.Add(this.RemoveCorpusButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IncludedCorporaListBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OKButton);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm_CompareFrequencies";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plugin Name";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.OutputSettingsGroupbox.ResumeLayout(false);
            this.OutputSettingsGroupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SetOutputFileButton;
        private System.Windows.Forms.TextBox OutputFileTextbox;
        private System.Windows.Forms.ComboBox EncodingDropdown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TextBox CSVQuoteTextbox;
        private System.Windows.Forms.TextBox CSVDelimiterTextbox;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox IncludedCorporaListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button GetInputFileButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CorpusNameTextbox;
        private System.Windows.Forms.Button RemoveCorpusButton;
        private System.Windows.Forms.GroupBox OutputSettingsGroupbox;
        private System.Windows.Forms.TextBox OutputFileQuoteTextbox;
        private System.Windows.Forms.ComboBox OutputFileEncodingDropdown;
        private System.Windows.Forms.TextBox OutputFileDelimiterTextbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}