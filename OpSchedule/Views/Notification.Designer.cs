namespace OpSchedule.Views
{
    partial class Notification
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.SystemColors.Control;
            this.labelTitle.Location = new System.Drawing.Point(52, 7);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(266, 17);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "label1";
            // 
            // labelMessage
            // 
            this.labelMessage.ForeColor = System.Drawing.SystemColors.Control;
            this.labelMessage.Location = new System.Drawing.Point(55, 24);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(263, 37);
            this.labelMessage.TabIndex = 2;
            this.labelMessage.Text = "label2";
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBoxIcon.Image = global::OpSchedule.Properties.Resources.OpSchedule;
            this.pictureBoxIcon.Location = new System.Drawing.Point(12, 19);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(30, 30);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxIcon.TabIndex = 3;
            this.pictureBoxIcon.TabStop = false;
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(330, 71);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Notification";
            this.ShowInTaskbar = false;
            this.Text = "Notification";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Notification_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
    }
}