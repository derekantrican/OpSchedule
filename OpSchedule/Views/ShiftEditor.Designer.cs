namespace OpSchedule
{
    partial class ShiftEditor
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
            this.labelText = new System.Windows.Forms.Label();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.labelColor = new System.Windows.Forms.Label();
            this.panelColor = new System.Windows.Forms.Panel();
            this.dateTimePickerEndTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerEndDay = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
            this.labelEnd = new System.Windows.Forms.Label();
            this.dateTimePickerStartDay = new System.Windows.Forms.DateTimePicker();
            this.labelStart = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.labelNotes = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonCustomColor = new System.Windows.Forms.Button();
            this.comboBoxColor = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(7, 9);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(31, 13);
            this.labelText.TabIndex = 0;
            this.labelText.Text = "Text:";
            // 
            // textBoxText
            // 
            this.textBoxText.Location = new System.Drawing.Point(44, 6);
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.Size = new System.Drawing.Size(216, 20);
            this.textBoxText.TabIndex = 1;
            // 
            // labelColor
            // 
            this.labelColor.AutoSize = true;
            this.labelColor.Location = new System.Drawing.Point(8, 196);
            this.labelColor.Name = "labelColor";
            this.labelColor.Size = new System.Drawing.Size(34, 13);
            this.labelColor.TabIndex = 2;
            this.labelColor.Text = "Color:";
            // 
            // panelColor
            // 
            this.panelColor.BackColor = System.Drawing.Color.Red;
            this.panelColor.Location = new System.Drawing.Point(45, 190);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(25, 25);
            this.panelColor.TabIndex = 3;
            this.panelColor.Click += new System.EventHandler(this.PanelColor_Click);
            // 
            // dateTimePickerEndTime
            // 
            this.dateTimePickerEndTime.CustomFormat = "h tt";
            this.dateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEndTime.Location = new System.Drawing.Point(203, 57);
            this.dateTimePickerEndTime.Name = "dateTimePickerEndTime";
            this.dateTimePickerEndTime.ShowUpDown = true;
            this.dateTimePickerEndTime.Size = new System.Drawing.Size(57, 20);
            this.dateTimePickerEndTime.TabIndex = 21;
            // 
            // dateTimePickerEndDay
            // 
            this.dateTimePickerEndDay.CustomFormat = "dddd, M/d";
            this.dateTimePickerEndDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEndDay.Location = new System.Drawing.Point(45, 57);
            this.dateTimePickerEndDay.Name = "dateTimePickerEndDay";
            this.dateTimePickerEndDay.Size = new System.Drawing.Size(152, 20);
            this.dateTimePickerEndDay.TabIndex = 20;
            // 
            // dateTimePickerStartTime
            // 
            this.dateTimePickerStartTime.CustomFormat = "h tt";
            this.dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartTime.Location = new System.Drawing.Point(203, 30);
            this.dateTimePickerStartTime.Name = "dateTimePickerStartTime";
            this.dateTimePickerStartTime.ShowUpDown = true;
            this.dateTimePickerStartTime.Size = new System.Drawing.Size(57, 20);
            this.dateTimePickerStartTime.TabIndex = 19;
            // 
            // labelEnd
            // 
            this.labelEnd.AutoSize = true;
            this.labelEnd.Location = new System.Drawing.Point(8, 63);
            this.labelEnd.Name = "labelEnd";
            this.labelEnd.Size = new System.Drawing.Size(29, 13);
            this.labelEnd.TabIndex = 17;
            this.labelEnd.Text = "End:";
            // 
            // dateTimePickerStartDay
            // 
            this.dateTimePickerStartDay.CustomFormat = "dddd, M/d";
            this.dateTimePickerStartDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartDay.Location = new System.Drawing.Point(45, 30);
            this.dateTimePickerStartDay.Name = "dateTimePickerStartDay";
            this.dateTimePickerStartDay.Size = new System.Drawing.Size(152, 20);
            this.dateTimePickerStartDay.TabIndex = 18;
            // 
            // labelStart
            // 
            this.labelStart.AutoSize = true;
            this.labelStart.Location = new System.Drawing.Point(7, 36);
            this.labelStart.Name = "labelStart";
            this.labelStart.Size = new System.Drawing.Size(32, 13);
            this.labelStart.TabIndex = 16;
            this.labelStart.Text = "Start:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(45, 83);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(215, 96);
            this.richTextBox1.TabIndex = 22;
            this.richTextBox1.Text = "";
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(7, 86);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(38, 13);
            this.labelNotes.TabIndex = 23;
            this.labelNotes.Text = "Notes:";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(215, 234);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(50, 23);
            this.buttonAdd.TabIndex = 24;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(159, 234);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(50, 23);
            this.buttonDelete.TabIndex = 25;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonCustomColor
            // 
            this.buttonCustomColor.Location = new System.Drawing.Point(128, 191);
            this.buttonCustomColor.Name = "buttonCustomColor";
            this.buttonCustomColor.Size = new System.Drawing.Size(50, 23);
            this.buttonCustomColor.TabIndex = 27;
            this.buttonCustomColor.Text = "Custom";
            this.buttonCustomColor.UseVisualStyleBackColor = true;
            this.buttonCustomColor.Click += new System.EventHandler(this.ButtonCustomColor_Click);
            // 
            // comboBoxColor
            // 
            this.comboBoxColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColor.ForeColor = System.Drawing.Color.Transparent;
            this.comboBoxColor.FormattingEnabled = true;
            this.comboBoxColor.Location = new System.Drawing.Point(76, 192);
            this.comboBoxColor.Name = "comboBoxColor";
            this.comboBoxColor.Size = new System.Drawing.Size(46, 21);
            this.comboBoxColor.TabIndex = 26;
            this.comboBoxColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ComboBoxColor_DrawItem);
            this.comboBoxColor.SelectedIndexChanged += new System.EventHandler(this.ComboBoxColor_SelectedIndexChanged);
            // 
            // ShiftEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 262);
            this.Controls.Add(this.buttonCustomColor);
            this.Controls.Add(this.comboBoxColor);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelNotes);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.dateTimePickerEndTime);
            this.Controls.Add(this.dateTimePickerEndDay);
            this.Controls.Add(this.dateTimePickerStartTime);
            this.Controls.Add(this.labelEnd);
            this.Controls.Add(this.dateTimePickerStartDay);
            this.Controls.Add(this.labelStart);
            this.Controls.Add(this.panelColor);
            this.Controls.Add(this.labelColor);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.labelText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ShiftEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Timeblock";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Label labelColor;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndDay;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartTime;
        private System.Windows.Forms.Label labelEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartDay;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonCustomColor;
        private System.Windows.Forms.ComboBox comboBoxColor;
    }
}