namespace OpSchedule
{
    partial class SettingsWindow
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
            this.comboBoxTimezoneSelection = new System.Windows.Forms.ComboBox();
            this.labelTimezone = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxAutoAdjustTZ = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.tabPageFilters = new System.Windows.Forms.TabPage();
            this.buttonEditFilter = new System.Windows.Forms.Button();
            this.buttonDeleteFilter = new System.Windows.Forms.Button();
            this.buttonAddFilter = new System.Windows.Forms.Button();
            this.listBoxFilters = new System.Windows.Forms.ListBox();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.pictureBoxTeams = new System.Windows.Forms.PictureBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.linkLabelEmail = new System.Windows.Forms.LinkLabel();
            this.labelDeveloper = new System.Windows.Forms.Label();
            this.labelApplication = new System.Windows.Forms.Label();
            this.buttonCheckUpdate = new System.Windows.Forms.Button();
            this.buttonFeedback = new System.Windows.Forms.Button();
            this.checkBoxNotifyBeforeShift = new System.Windows.Forms.CheckBox();
            this.numericUpDownNotifyBeforeShift = new System.Windows.Forms.NumericUpDown();
            this.labelMinText = new System.Windows.Forms.Label();
            this.checkBoxNotifyShiftChange = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabPageFilters.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTeams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNotifyBeforeShift)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxTimezoneSelection
            // 
            this.comboBoxTimezoneSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTimezoneSelection.FormattingEnabled = true;
            this.comboBoxTimezoneSelection.Location = new System.Drawing.Point(65, 3);
            this.comboBoxTimezoneSelection.Name = "comboBoxTimezoneSelection";
            this.comboBoxTimezoneSelection.Size = new System.Drawing.Size(59, 21);
            this.comboBoxTimezoneSelection.TabIndex = 3;
            // 
            // labelTimezone
            // 
            this.labelTimezone.AutoSize = true;
            this.labelTimezone.Location = new System.Drawing.Point(3, 6);
            this.labelTimezone.Name = "labelTimezone";
            this.labelTimezone.Size = new System.Drawing.Size(56, 13);
            this.labelTimezone.TabIndex = 2;
            this.labelTimezone.Text = "Timezone:";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(227, 231);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(50, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(171, 231);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(50, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // checkBoxAutoAdjustTZ
            // 
            this.checkBoxAutoAdjustTZ.AutoSize = true;
            this.checkBoxAutoAdjustTZ.Location = new System.Drawing.Point(131, 6);
            this.checkBoxAutoAdjustTZ.Name = "checkBoxAutoAdjustTZ";
            this.checkBoxAutoAdjustTZ.Size = new System.Drawing.Size(120, 17);
            this.checkBoxAutoAdjustTZ.TabIndex = 6;
            this.checkBoxAutoAdjustTZ.Text = "Automatically Adjust";
            this.checkBoxAutoAdjustTZ.UseVisualStyleBackColor = true;
            this.checkBoxAutoAdjustTZ.CheckedChanged += new System.EventHandler(this.CheckBoxAutoAdjustTZ_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageGeneral);
            this.tabControl1.Controls.Add(this.tabPageFilters);
            this.tabControl1.Controls.Add(this.tabPageAbout);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(284, 225);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.checkBoxNotifyShiftChange);
            this.tabPageGeneral.Controls.Add(this.labelMinText);
            this.tabPageGeneral.Controls.Add(this.numericUpDownNotifyBeforeShift);
            this.tabPageGeneral.Controls.Add(this.checkBoxNotifyBeforeShift);
            this.tabPageGeneral.Controls.Add(this.checkBoxAutoAdjustTZ);
            this.tabPageGeneral.Controls.Add(this.labelTimezone);
            this.tabPageGeneral.Controls.Add(this.comboBoxTimezoneSelection);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(276, 199);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // tabPageFilters
            // 
            this.tabPageFilters.Controls.Add(this.buttonEditFilter);
            this.tabPageFilters.Controls.Add(this.buttonDeleteFilter);
            this.tabPageFilters.Controls.Add(this.buttonAddFilter);
            this.tabPageFilters.Controls.Add(this.listBoxFilters);
            this.tabPageFilters.Location = new System.Drawing.Point(4, 22);
            this.tabPageFilters.Name = "tabPageFilters";
            this.tabPageFilters.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFilters.Size = new System.Drawing.Size(276, 199);
            this.tabPageFilters.TabIndex = 1;
            this.tabPageFilters.Text = "Filters";
            this.tabPageFilters.UseVisualStyleBackColor = true;
            // 
            // buttonEditFilter
            // 
            this.buttonEditFilter.Location = new System.Drawing.Point(162, 159);
            this.buttonEditFilter.Name = "buttonEditFilter";
            this.buttonEditFilter.Size = new System.Drawing.Size(50, 23);
            this.buttonEditFilter.TabIndex = 3;
            this.buttonEditFilter.Text = "Edit";
            this.buttonEditFilter.UseVisualStyleBackColor = true;
            this.buttonEditFilter.Click += new System.EventHandler(this.ButtonEditFilter_Click);
            // 
            // buttonDeleteFilter
            // 
            this.buttonDeleteFilter.Location = new System.Drawing.Point(106, 159);
            this.buttonDeleteFilter.Name = "buttonDeleteFilter";
            this.buttonDeleteFilter.Size = new System.Drawing.Size(50, 23);
            this.buttonDeleteFilter.TabIndex = 2;
            this.buttonDeleteFilter.Text = "Delete";
            this.buttonDeleteFilter.UseVisualStyleBackColor = true;
            this.buttonDeleteFilter.Click += new System.EventHandler(this.ButtonDeleteFilter_Click);
            // 
            // buttonAddFilter
            // 
            this.buttonAddFilter.Location = new System.Drawing.Point(218, 159);
            this.buttonAddFilter.Name = "buttonAddFilter";
            this.buttonAddFilter.Size = new System.Drawing.Size(50, 23);
            this.buttonAddFilter.TabIndex = 1;
            this.buttonAddFilter.Text = "Add";
            this.buttonAddFilter.UseVisualStyleBackColor = true;
            this.buttonAddFilter.Click += new System.EventHandler(this.ButtonAddFilter_Click);
            // 
            // listBoxFilters
            // 
            this.listBoxFilters.DisplayMember = "Name";
            this.listBoxFilters.FormattingEnabled = true;
            this.listBoxFilters.Location = new System.Drawing.Point(6, 6);
            this.listBoxFilters.Name = "listBoxFilters";
            this.listBoxFilters.Size = new System.Drawing.Size(264, 147);
            this.listBoxFilters.TabIndex = 0;
            this.listBoxFilters.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListBoxFilters_MouseDoubleClick);
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.pictureBoxTeams);
            this.tabPageAbout.Controls.Add(this.labelVersion);
            this.tabPageAbout.Controls.Add(this.linkLabelEmail);
            this.tabPageAbout.Controls.Add(this.labelDeveloper);
            this.tabPageAbout.Controls.Add(this.labelApplication);
            this.tabPageAbout.Controls.Add(this.buttonCheckUpdate);
            this.tabPageAbout.Controls.Add(this.buttonFeedback);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Size = new System.Drawing.Size(276, 199);
            this.tabPageAbout.TabIndex = 2;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // pictureBoxTeams
            // 
            this.pictureBoxTeams.Image = global::OpSchedule.Properties.Resources.Teams_icon;
            this.pictureBoxTeams.Location = new System.Drawing.Point(197, 55);
            this.pictureBoxTeams.Name = "pictureBoxTeams";
            this.pictureBoxTeams.Size = new System.Drawing.Size(15, 15);
            this.pictureBoxTeams.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxTeams.TabIndex = 21;
            this.pictureBoxTeams.TabStop = false;
            this.pictureBoxTeams.Click += new System.EventHandler(this.PictureBoxTeams_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(65, 42);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(48, 13);
            this.labelVersion.TabIndex = 20;
            this.labelVersion.Text = "Version: ";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabelEmail
            // 
            this.linkLabelEmail.AutoSize = true;
            this.linkLabelEmail.Location = new System.Drawing.Point(55, 71);
            this.linkLabelEmail.Name = "linkLabelEmail";
            this.linkLabelEmail.Size = new System.Drawing.Size(156, 13);
            this.linkLabelEmail.TabIndex = 19;
            this.linkLabelEmail.TabStop = true;
            this.linkLabelEmail.Text = "derek.antrican@sigmanest.com";
            this.linkLabelEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelEmail_LinkClicked);
            // 
            // labelDeveloper
            // 
            this.labelDeveloper.AutoSize = true;
            this.labelDeveloper.Location = new System.Drawing.Point(63, 56);
            this.labelDeveloper.Name = "labelDeveloper";
            this.labelDeveloper.Size = new System.Drawing.Size(133, 13);
            this.labelDeveloper.TabIndex = 18;
            this.labelDeveloper.Text = "Developer: Derek Antrican";
            this.labelDeveloper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelApplication
            // 
            this.labelApplication.AutoSize = true;
            this.labelApplication.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelApplication.Location = new System.Drawing.Point(80, 11);
            this.labelApplication.Name = "labelApplication";
            this.labelApplication.Size = new System.Drawing.Size(113, 13);
            this.labelApplication.TabIndex = 17;
            this.labelApplication.Text = "About OpSchedule";
            // 
            // buttonCheckUpdate
            // 
            this.buttonCheckUpdate.Location = new System.Drawing.Point(8, 144);
            this.buttonCheckUpdate.Name = "buttonCheckUpdate";
            this.buttonCheckUpdate.Size = new System.Drawing.Size(112, 23);
            this.buttonCheckUpdate.TabIndex = 1;
            this.buttonCheckUpdate.Text = "Check For Update";
            this.buttonCheckUpdate.UseVisualStyleBackColor = true;
            this.buttonCheckUpdate.Click += new System.EventHandler(this.ButtonCheckUpdate_Click);
            // 
            // buttonFeedback
            // 
            this.buttonFeedback.Location = new System.Drawing.Point(8, 173);
            this.buttonFeedback.Name = "buttonFeedback";
            this.buttonFeedback.Size = new System.Drawing.Size(75, 23);
            this.buttonFeedback.TabIndex = 0;
            this.buttonFeedback.Text = "Feedback";
            this.buttonFeedback.UseVisualStyleBackColor = true;
            this.buttonFeedback.Click += new System.EventHandler(this.ButtonFeedback_Click);
            // 
            // checkBoxNotifyBeforeShift
            // 
            this.checkBoxNotifyBeforeShift.AutoSize = true;
            this.checkBoxNotifyBeforeShift.Location = new System.Drawing.Point(8, 30);
            this.checkBoxNotifyBeforeShift.Name = "checkBoxNotifyBeforeShift";
            this.checkBoxNotifyBeforeShift.Size = new System.Drawing.Size(152, 17);
            this.checkBoxNotifyBeforeShift.TabIndex = 7;
            this.checkBoxNotifyBeforeShift.Text = "Notify before my shift starts";
            this.checkBoxNotifyBeforeShift.UseVisualStyleBackColor = true;
            this.checkBoxNotifyBeforeShift.CheckedChanged += new System.EventHandler(this.CheckBoxNotifyBeforeShift_CheckedChanged);
            // 
            // numericUpDownNotifyBeforeShift
            // 
            this.numericUpDownNotifyBeforeShift.Location = new System.Drawing.Point(166, 29);
            this.numericUpDownNotifyBeforeShift.Name = "numericUpDownNotifyBeforeShift";
            this.numericUpDownNotifyBeforeShift.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownNotifyBeforeShift.TabIndex = 8;
            // 
            // labelMinText
            // 
            this.labelMinText.AutoSize = true;
            this.labelMinText.Location = new System.Drawing.Point(210, 31);
            this.labelMinText.Name = "labelMinText";
            this.labelMinText.Size = new System.Drawing.Size(23, 13);
            this.labelMinText.TabIndex = 9;
            this.labelMinText.Text = "min";
            // 
            // checkBoxNotifyShiftChange
            // 
            this.checkBoxNotifyShiftChange.AutoSize = true;
            this.checkBoxNotifyShiftChange.Location = new System.Drawing.Point(8, 53);
            this.checkBoxNotifyShiftChange.Name = "checkBoxNotifyShiftChange";
            this.checkBoxNotifyShiftChange.Size = new System.Drawing.Size(164, 17);
            this.checkBoxNotifyShiftChange.TabIndex = 10;
            this.checkBoxNotifyShiftChange.Text = "Notify when my shift changes";
            this.checkBoxNotifyShiftChange.UseVisualStyleBackColor = true;
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SettingsWindow";
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabPageFilters.ResumeLayout(false);
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTeams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNotifyBeforeShift)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxTimezoneSelection;
        private System.Windows.Forms.Label labelTimezone;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxAutoAdjustTZ;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageFilters;
        private System.Windows.Forms.Button buttonEditFilter;
        private System.Windows.Forms.Button buttonDeleteFilter;
        private System.Windows.Forms.Button buttonAddFilter;
        private System.Windows.Forms.ListBox listBoxFilters;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.Button buttonCheckUpdate;
        private System.Windows.Forms.Button buttonFeedback;
        private System.Windows.Forms.PictureBox pictureBoxTeams;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.LinkLabel linkLabelEmail;
        private System.Windows.Forms.Label labelDeveloper;
        private System.Windows.Forms.Label labelApplication;
        private System.Windows.Forms.CheckBox checkBoxNotifyShiftChange;
        private System.Windows.Forms.Label labelMinText;
        private System.Windows.Forms.NumericUpDown numericUpDownNotifyBeforeShift;
        private System.Windows.Forms.CheckBox checkBoxNotifyBeforeShift;
    }
}