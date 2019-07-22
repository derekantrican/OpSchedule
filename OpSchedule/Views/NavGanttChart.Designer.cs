namespace OpSchedule.Views
{
    partial class NavGanttChart
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
            this.dateTimePickerGoTo = new System.Windows.Forms.DateTimePicker();
            this.buttonGoTo = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.ganttChart = new GanttChart.Chart();
            this.SuspendLayout();
            // 
            // dateTimePickerGoTo
            // 
            this.dateTimePickerGoTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerGoTo.Location = new System.Drawing.Point(141, 5);
            this.dateTimePickerGoTo.Name = "dateTimePickerGoTo";
            this.dateTimePickerGoTo.Size = new System.Drawing.Size(96, 20);
            this.dateTimePickerGoTo.TabIndex = 15;
            // 
            // buttonGoTo
            // 
            this.buttonGoTo.Location = new System.Drawing.Point(84, 3);
            this.buttonGoTo.Name = "buttonGoTo";
            this.buttonGoTo.Size = new System.Drawing.Size(51, 23);
            this.buttonGoTo.TabIndex = 14;
            this.buttonGoTo.Text = "Go to...";
            this.buttonGoTo.UseVisualStyleBackColor = true;
            this.buttonGoTo.Click += new System.EventHandler(this.ButtonGoTo_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.Location = new System.Drawing.Point(32, 3);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(23, 23);
            this.buttonRight.TabIndex = 13;
            this.buttonRight.Text = ">";
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Click += new System.EventHandler(this.ButtonRight_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Location = new System.Drawing.Point(3, 3);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(23, 23);
            this.buttonLeft.TabIndex = 12;
            this.buttonLeft.Text = "<";
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Click += new System.EventHandler(this.ButtonLeft_Click);
            // 
            // ganttChart
            // 
            this.ganttChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ganttChart.AutoScroll = true;
            this.ganttChart.AutoScrollMinSize = new System.Drawing.Size(1030, 406);
            this.ganttChart.DefaultDayLabelFormat = "dddd";
            this.ganttChart.DefaultTimeLabelFormat = "htt";
            this.ganttChart.EndDate = new System.DateTime(((long)(0)));
            this.ganttChart.Location = new System.Drawing.Point(0, 29);
            this.ganttChart.Name = "ganttChart";
            this.ganttChart.Size = new System.Drawing.Size(1050, 336);
            this.ganttChart.StartDate = new System.DateTime(((long)(0)));
            this.ganttChart.TabIndex = 16;
            // 
            // NavGanttChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ganttChart);
            this.Controls.Add(this.dateTimePickerGoTo);
            this.Controls.Add(this.buttonGoTo);
            this.Controls.Add(this.buttonRight);
            this.Controls.Add(this.buttonLeft);
            this.Name = "NavGanttChart";
            this.Size = new System.Drawing.Size(1050, 368);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerGoTo;
        private System.Windows.Forms.Button buttonGoTo;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonLeft;
        private GanttChart.Chart ganttChart;
    }
}
