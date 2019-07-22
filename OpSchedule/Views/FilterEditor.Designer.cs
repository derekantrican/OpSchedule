namespace OpSchedule.Views
{
    partial class FilterEditor
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.buttonCustomColor = new System.Windows.Forms.Button();
            this.comboBoxColor = new System.Windows.Forms.ComboBox();
            this.checkBoxInvertFilter = new System.Windows.Forms.CheckBox();
            this.panelColor = new System.Windows.Forms.Panel();
            this.textBoxShiftNote = new System.Windows.Forms.TextBox();
            this.textBoxShiftText = new System.Windows.Forms.TextBox();
            this.checkBoxTextContains = new System.Windows.Forms.CheckBox();
            this.checkBoxNoteContains = new System.Windows.Forms.CheckBox();
            this.checkBoxColor = new System.Windows.Forms.CheckBox();
            this.groupBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(160, 236);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(50, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(216, 236);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(50, 23);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 8;
            this.labelName.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(56, 6);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(210, 20);
            this.textBoxName.TabIndex = 0;
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.checkBoxColor);
            this.groupBoxOptions.Controls.Add(this.checkBoxNoteContains);
            this.groupBoxOptions.Controls.Add(this.checkBoxTextContains);
            this.groupBoxOptions.Controls.Add(this.buttonCustomColor);
            this.groupBoxOptions.Controls.Add(this.comboBoxColor);
            this.groupBoxOptions.Controls.Add(this.checkBoxInvertFilter);
            this.groupBoxOptions.Controls.Add(this.panelColor);
            this.groupBoxOptions.Controls.Add(this.textBoxShiftNote);
            this.groupBoxOptions.Controls.Add(this.textBoxShiftText);
            this.groupBoxOptions.Location = new System.Drawing.Point(10, 32);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(256, 198);
            this.groupBoxOptions.TabIndex = 10;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "Only show items where...";
            // 
            // buttonCustomColor
            // 
            this.buttonCustomColor.Location = new System.Drawing.Point(185, 116);
            this.buttonCustomColor.Name = "buttonCustomColor";
            this.buttonCustomColor.Size = new System.Drawing.Size(50, 23);
            this.buttonCustomColor.TabIndex = 8;
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
            this.comboBoxColor.Location = new System.Drawing.Point(133, 117);
            this.comboBoxColor.Name = "comboBoxColor";
            this.comboBoxColor.Size = new System.Drawing.Size(46, 21);
            this.comboBoxColor.TabIndex = 7;
            this.comboBoxColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ComboBoxColor_DrawItem);
            this.comboBoxColor.SelectedIndexChanged += new System.EventHandler(this.ComboBoxColor_SelectedIndexChanged);
            // 
            // checkBoxInvertFilter
            // 
            this.checkBoxInvertFilter.AutoSize = true;
            this.checkBoxInvertFilter.Location = new System.Drawing.Point(6, 175);
            this.checkBoxInvertFilter.Name = "checkBoxInvertFilter";
            this.checkBoxInvertFilter.Size = new System.Drawing.Size(78, 17);
            this.checkBoxInvertFilter.TabIndex = 6;
            this.checkBoxInvertFilter.Text = "Invert Filter";
            this.checkBoxInvertFilter.UseVisualStyleBackColor = true;
            this.checkBoxInvertFilter.CheckedChanged += new System.EventHandler(this.CheckBoxInvertFilter_CheckedChanged);
            // 
            // panelColor
            // 
            this.panelColor.BackColor = System.Drawing.Color.Red;
            this.panelColor.Location = new System.Drawing.Point(102, 115);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(25, 25);
            this.panelColor.TabIndex = 5;
            this.panelColor.Click += new System.EventHandler(this.PanelColor_Click);
            // 
            // textBoxShiftNote
            // 
            this.textBoxShiftNote.Location = new System.Drawing.Point(23, 91);
            this.textBoxShiftNote.Name = "textBoxShiftNote";
            this.textBoxShiftNote.Size = new System.Drawing.Size(226, 20);
            this.textBoxShiftNote.TabIndex = 3;
            // 
            // textBoxShiftText
            // 
            this.textBoxShiftText.Location = new System.Drawing.Point(24, 42);
            this.textBoxShiftText.Name = "textBoxShiftText";
            this.textBoxShiftText.Size = new System.Drawing.Size(226, 20);
            this.textBoxShiftText.TabIndex = 1;
            // 
            // checkBoxTextContains
            // 
            this.checkBoxTextContains.AutoSize = true;
            this.checkBoxTextContains.Location = new System.Drawing.Point(6, 19);
            this.checkBoxTextContains.Name = "checkBoxTextContains";
            this.checkBoxTextContains.Size = new System.Drawing.Size(118, 17);
            this.checkBoxTextContains.TabIndex = 9;
            this.checkBoxTextContains.Text = "Shift title contains...";
            this.checkBoxTextContains.UseVisualStyleBackColor = true;
            // 
            // checkBoxNoteContains
            // 
            this.checkBoxNoteContains.AutoSize = true;
            this.checkBoxNoteContains.Location = new System.Drawing.Point(6, 68);
            this.checkBoxNoteContains.Name = "checkBoxNoteContains";
            this.checkBoxNoteContains.Size = new System.Drawing.Size(123, 17);
            this.checkBoxNoteContains.TabIndex = 10;
            this.checkBoxNoteContains.Text = "Shift note contains...";
            this.checkBoxNoteContains.UseVisualStyleBackColor = true;
            // 
            // checkBoxColor
            // 
            this.checkBoxColor.AutoSize = true;
            this.checkBoxColor.Location = new System.Drawing.Point(6, 120);
            this.checkBoxColor.Name = "checkBoxColor";
            this.checkBoxColor.Size = new System.Drawing.Size(92, 17);
            this.checkBoxColor.TabIndex = 11;
            this.checkBoxColor.Text = "Shift color is...";
            this.checkBoxColor.UseVisualStyleBackColor = true;
            // 
            // FilterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 262);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FilterEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FilterEditor";
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.TextBox textBoxShiftNote;
        private System.Windows.Forms.TextBox textBoxShiftText;
        private System.Windows.Forms.Panel panelColor;
        private System.Windows.Forms.CheckBox checkBoxInvertFilter;
        private System.Windows.Forms.ComboBox comboBoxColor;
        private System.Windows.Forms.Button buttonCustomColor;
        private System.Windows.Forms.CheckBox checkBoxColor;
        private System.Windows.Forms.CheckBox checkBoxNoteContains;
        private System.Windows.Forms.CheckBox checkBoxTextContains;
    }
}