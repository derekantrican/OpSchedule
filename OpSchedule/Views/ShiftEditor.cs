using GanttChart;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpSchedule
{
    public partial class ShiftEditor : Form
    {
        public ShiftEditor(Row parentRow, DateTime targetDateTime)
        {
            InitializeComponent();

            comboBoxColor.DataSource = Common.CurrentlyUsedColors;
            comboBoxColor.DisplayMember = "None";

            this.Text = $"Add Timeblock ({parentRow.Text})";

            dateTimePickerStartDay.Value = targetDateTime;
            dateTimePickerStartTime.Value = targetDateTime;
            dateTimePickerEndDay.Value = targetDateTime.AddHours(1); //Default end
            dateTimePickerEndTime.Value = targetDateTime.AddHours(1); //Default end

            buttonDelete.Visible = false;
        }

        public ShiftEditor(Row parentRow, Shift editingShift)
        {
            InitializeComponent();

            comboBoxColor.DataSource = Common.CurrentlyUsedColors;
            comboBoxColor.DisplayMember = "None";

            this.Text = $"Edit Timeblock ({parentRow.Text})";

            textBoxText.Text = editingShift.Text;

            dateTimePickerStartDay.Value = editingShift.StartTime;
            dateTimePickerStartTime.Value = editingShift.StartTime;
            dateTimePickerEndDay.Value = editingShift.EndTime;
            dateTimePickerEndTime.Value = editingShift.EndTime;

            richTextBox1.Text = editingShift.Notes;

            panelColor.BackColor = editingShift.Color;

            buttonAdd.Text = "Save";
        }

        public delegate void ShiftResultDelegate(Shift result);
        public event ShiftResultDelegate ShiftResult;

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (AreParametersValid())
            {
                Shift shift = new Shift(textBoxText.Text, StartDateAndTime, EndDateAndTime)
                {
                    Color = panelColor.BackColor,
                    Notes = richTextBox1.Text
                };

                ShiftResult?.Invoke(shift);

                this.Close();
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            ShiftResult?.Invoke(null);

            this.Close();
        }

        private DateTime StartDateAndTime
        {
            get
            {
                DateTime result = dateTimePickerStartDay.Value;
                result = new DateTime(result.Year, result.Month, result.Day,
                                      dateTimePickerStartTime.Value.Hour, 0, 0);

                return result;
            }
        }

        private DateTime EndDateAndTime
        {
            get
            {
                DateTime result = dateTimePickerEndDay.Value;
                result = new DateTime(result.Year, result.Month, result.Day,
                                      dateTimePickerEndTime.Value.Hour, 0, 0);

                return result;
            }
        }

        private bool AreParametersValid()
        {
            if (string.IsNullOrEmpty(textBoxText.Text))
            {
                MessageBox.Show("Please enter a title for the shift");
                return false;
            }

            if (EndDateAndTime <= StartDateAndTime)
            {
                MessageBox.Show("Start/End values are not valid");
                return false;
            }

            return true;
        }

        private void PanelColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
                panelColor.BackColor = dialog.Color;
        }

        private void ButtonCustomColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
                panelColor.BackColor = dialog.Color;
        }

        private void ComboBoxColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelColor.BackColor = (comboBoxColor.SelectedItem as Color?).Value;
        }

        private void ComboBoxColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                Color color = (((ComboBox)sender).Items[e.Index] as Color?).Value;
                Brush b = new SolidBrush(color);
                g.FillRectangle(b, rect.X, rect.Y, rect.Width, rect.Height);
            }
        }
    }
}
