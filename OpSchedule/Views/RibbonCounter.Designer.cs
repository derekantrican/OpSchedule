namespace OpSchedule.Views
{
    partial class RibbonCounter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelText = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelText
            // 
            this.labelText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelText.Location = new System.Drawing.Point(0, 75);
            this.labelText.Name = "labelText";
            this.labelText.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelText.Size = new System.Drawing.Size(144, 33);
            this.labelText.TabIndex = 0;
            this.labelText.Text = "label";
            this.labelText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelCount
            // 
            this.labelCount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCount.BackColor = System.Drawing.Color.DarkGray;
            this.labelCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 70F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.labelCount.ForeColor = System.Drawing.Color.White;
            this.labelCount.Location = new System.Drawing.Point(3, 3);
            this.labelCount.Margin = new System.Windows.Forms.Padding(0);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(138, 69);
            this.labelCount.TabIndex = 1;
            this.labelCount.Text = "7";
            this.labelCount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelCount.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelCount_Paint);
            // 
            // RibbonCounter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelText);
            this.Name = "RibbonCounter";
            this.Size = new System.Drawing.Size(144, 109);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Label labelCount;
    }
}
