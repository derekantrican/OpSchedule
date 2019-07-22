using OpSchedule.Objects;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OpSchedule.Views
{
    public partial class FilterEditor : Form
    {
        //Todo "inverting" should be for each "parameter" (not an "all or nothing")
        public FilterEditor(Filter filter)
        {
            InitializeComponent();

            comboBoxColor.DataSource = Common.CurrentlyUsedColors;
            comboBoxColor.DisplayMember = "None";

            if (filter != null)
            {
                textBoxName.Text = filter.Name;

                if (!string.IsNullOrEmpty(filter.TextContains))
                {
                    checkBoxTextContains.Checked = true;
                    textBoxShiftText.Text = filter.TextContains;
                }

                if (!string.IsNullOrEmpty(filter.NoteContains))
                {
                    checkBoxNoteContains.Checked = true;
                    textBoxShiftNote.Text = filter.NoteContains;
                }

                if (filter.Color.HasValue)
                {
                    checkBoxColor.Checked = true;
                    panelColor.BackColor = filter.Color.Value;
                }

                checkBoxInvertFilter.Checked = filter.InvertFilter;
            }
        }

        public delegate void FilterResultDelegate(Filter result);
        public event FilterResultDelegate FilterResult;

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (AreParametersValid())
            {
                Filter result = new Filter(textBoxName.Text);

                if (checkBoxTextContains.Checked)
                    result.TextContains = textBoxShiftText.Text;

                if (checkBoxNoteContains.Checked)
                    result.NoteContains = textBoxShiftNote.Text;

                if (checkBoxColor.Checked)
                    result.Color = panelColor.BackColor;

                result.InvertFilter = checkBoxInvertFilter.Checked;

                FilterResult?.Invoke(result);

                this.Close();
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool AreParametersValid()
        {
            if (checkBoxTextContains.Checked && string.IsNullOrWhiteSpace(textBoxShiftText.Text))
            {
                MessageBox.Show("If you want to filter by shift title, you need to specify some text");
                return false;
            }
            else if (checkBoxNoteContains.Checked && string.IsNullOrWhiteSpace(textBoxShiftNote.Text))
            {
                MessageBox.Show("If you want to filter by shift note, you need to specify some text");
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

        private void CheckBoxInvertFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxInvertFilter.Checked)
                groupBoxOptions.Text = "DON'T show items where...";
            else
                groupBoxOptions.Text = "Only show items where...";
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

        private void ComboBoxColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelColor.BackColor = (comboBoxColor.SelectedItem as Color?).Value;
        }

        private void ButtonCustomColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
                panelColor.BackColor = dialog.Color;
        }
    }
}
