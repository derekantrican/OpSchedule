namespace OpSchedule
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tableLayoutPanelRibbon = new System.Windows.Forms.TableLayoutPanel();
            this.ribbonButtonOpenEditor = new OpSchedule.Views.RibbonButton();
            this.ribbonButtonSettings = new OpSchedule.Views.RibbonButton();
            this.ribbonButtonFilter = new OpSchedule.Views.RibbonButton();
            this.ribbonCounterOperator = new OpSchedule.Views.RibbonCounter();
            this.ribbonCounterTier1 = new OpSchedule.Views.RibbonCounter();
            this.ribbonCounterTier2 = new OpSchedule.Views.RibbonCounter();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.navGanttChart = new OpSchedule.Views.NavGanttChart();
            this.tableLayoutPanelRibbon.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelRibbon
            // 
            this.tableLayoutPanelRibbon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelRibbon.ColumnCount = 8;
            this.tableLayoutPanelRibbon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelRibbon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelRibbon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelRibbon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelRibbon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelRibbon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelRibbon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelRibbon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanelRibbon.Controls.Add(this.ribbonButtonOpenEditor, 0, 0);
            this.tableLayoutPanelRibbon.Controls.Add(this.ribbonButtonSettings, 7, 0);
            this.tableLayoutPanelRibbon.Controls.Add(this.ribbonButtonFilter, 6, 0);
            this.tableLayoutPanelRibbon.Controls.Add(this.ribbonCounterOperator, 3, 0);
            this.tableLayoutPanelRibbon.Controls.Add(this.ribbonCounterTier1, 4, 0);
            this.tableLayoutPanelRibbon.Controls.Add(this.ribbonCounterTier2, 5, 0);
            this.tableLayoutPanelRibbon.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelRibbon.Name = "tableLayoutPanelRibbon";
            this.tableLayoutPanelRibbon.RowCount = 1;
            this.tableLayoutPanelRibbon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRibbon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelRibbon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelRibbon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelRibbon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelRibbon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelRibbon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelRibbon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelRibbon.Size = new System.Drawing.Size(1050, 80);
            this.tableLayoutPanelRibbon.TabIndex = 6;
            // 
            // ribbonButtonOpenEditor
            // 
            this.ribbonButtonOpenEditor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ribbonButtonOpenEditor.BackColor = System.Drawing.SystemColors.Control;
            this.ribbonButtonOpenEditor.Image = global::OpSchedule.Properties.Resources.edit;
            this.ribbonButtonOpenEditor.Location = new System.Drawing.Point(3, 3);
            this.ribbonButtonOpenEditor.Name = "ribbonButtonOpenEditor";
            this.ribbonButtonOpenEditor.Size = new System.Drawing.Size(125, 74);
            this.ribbonButtonOpenEditor.TabIndex = 7;
            this.ribbonButtonOpenEditor.Text = "Open Editor";
            this.ribbonButtonOpenEditor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RibbonButtonOpenEditor_MouseClick);
            // 
            // ribbonButtonSettings
            // 
            this.ribbonButtonSettings.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ribbonButtonSettings.BackColor = System.Drawing.SystemColors.Control;
            this.ribbonButtonSettings.Image = global::OpSchedule.Properties.Resources.settings_gears;
            this.ribbonButtonSettings.Location = new System.Drawing.Point(934, 3);
            this.ribbonButtonSettings.Name = "ribbonButtonSettings";
            this.ribbonButtonSettings.Size = new System.Drawing.Size(99, 74);
            this.ribbonButtonSettings.TabIndex = 9;
            this.ribbonButtonSettings.Text = "Settings";
            this.ribbonButtonSettings.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RibbonButtonSettings_MouseClick);
            // 
            // ribbonButtonFilter
            // 
            this.ribbonButtonFilter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ribbonButtonFilter.BackColor = System.Drawing.SystemColors.Control;
            this.ribbonButtonFilter.Image = global::OpSchedule.Properties.Resources.filter;
            this.ribbonButtonFilter.Location = new System.Drawing.Point(802, 3);
            this.ribbonButtonFilter.Name = "ribbonButtonFilter";
            this.ribbonButtonFilter.Size = new System.Drawing.Size(99, 74);
            this.ribbonButtonFilter.TabIndex = 8;
            this.ribbonButtonFilter.Text = "Filter View";
            this.ribbonButtonFilter.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RibbonButtonFilter_MouseClick);
            // 
            // ribbonCounterOperator
            // 
            this.ribbonCounterOperator.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ribbonCounterOperator.Count = 0;
            this.ribbonCounterOperator.Location = new System.Drawing.Point(396, 3);
            this.ribbonCounterOperator.Name = "ribbonCounterOperator";
            this.ribbonCounterOperator.Size = new System.Drawing.Size(125, 74);
            this.ribbonCounterOperator.TabIndex = 11;
            this.ribbonCounterOperator.Text = "Operator";
            // 
            // ribbonCounterTier1
            // 
            this.ribbonCounterTier1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ribbonCounterTier1.Count = 0;
            this.ribbonCounterTier1.Location = new System.Drawing.Point(527, 3);
            this.ribbonCounterTier1.Name = "ribbonCounterTier1";
            this.ribbonCounterTier1.Size = new System.Drawing.Size(125, 74);
            this.ribbonCounterTier1.TabIndex = 10;
            this.ribbonCounterTier1.Text = "Tier 1";
            // 
            // ribbonCounterTier2
            // 
            this.ribbonCounterTier2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ribbonCounterTier2.Count = 0;
            this.ribbonCounterTier2.Location = new System.Drawing.Point(658, 3);
            this.ribbonCounterTier2.Name = "ribbonCounterTier2";
            this.ribbonCounterTier2.Size = new System.Drawing.Size(125, 74);
            this.ribbonCounterTier2.TabIndex = 7;
            this.ribbonCounterTier2.Text = "Tier 2";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 448);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1050, 22);
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // navGanttChart
            // 
            this.navGanttChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.navGanttChart.Location = new System.Drawing.Point(0, 83);
            this.navGanttChart.Name = "navGanttChart";
            this.navGanttChart.Size = new System.Drawing.Size(1050, 362);
            this.navGanttChart.TabIndex = 8;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 470);
            this.Controls.Add(this.navGanttChart);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tableLayoutPanelRibbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "OpSchedule";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.tableLayoutPanelRibbon.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRibbon;
        private Views.RibbonButton ribbonButtonOpenEditor;
        private Views.RibbonButton ribbonButtonFilter;
        private Views.RibbonButton ribbonButtonSettings;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private Views.RibbonCounter ribbonCounterTier2;
        private Views.RibbonCounter ribbonCounterTier1;
        private Views.RibbonCounter ribbonCounterOperator;
        private Views.NavGanttChart navGanttChart;
    }
}

