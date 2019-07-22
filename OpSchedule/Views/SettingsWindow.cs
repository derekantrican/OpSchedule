using OpSchedule.Objects;
using OpSchedule.Views;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace OpSchedule
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow()
        {
            InitializeComponent();

            labelVersion.Text = "Version: " + Common.ApplicationVersion;

            InitTimeZoneSelector();

            checkBoxAutoAdjustTZ.Checked = Settings.Instance.AutoAdjustTZ;

            checkBoxNotifyBeforeShift.Checked = Settings.Instance.NotifyBeforeShift;
            numericUpDownNotifyBeforeShift.Value = Settings.Instance.NotifyMinBeforeShift;
            numericUpDownNotifyBeforeShift.Enabled = Settings.Instance.NotifyBeforeShift;

            checkBoxNotifyShiftChange.Checked = Settings.Instance.NotifyScheduleChange;

            listBoxFilters.Items.AddRange(Settings.Instance.CustomFilters.ToArray());
        }

        private void InitTimeZoneSelector()
        {
            comboBoxTimezoneSelection.DataSource = new BindingSource(Common.TZDict, null);
            comboBoxTimezoneSelection.DisplayMember = "Key";
            comboBoxTimezoneSelection.ValueMember = "Value";

            comboBoxTimezoneSelection.SelectedItem = Common.TZDict.FirstOrDefault(p => p.Value.Equals(Settings.Instance.TimeZone));
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            Settings.Instance.TimeZone = comboBoxTimezoneSelection.SelectedValue as TimeZoneInfo;
            Settings.Instance.AutoAdjustTZ = checkBoxAutoAdjustTZ.Checked;
            Settings.Instance.NotifyBeforeShift = checkBoxNotifyBeforeShift.Checked;
            Settings.Instance.NotifyMinBeforeShift = Convert.ToInt32(numericUpDownNotifyBeforeShift.Value);
            Settings.Instance.NotifyScheduleChange = checkBoxNotifyShiftChange.Checked;
            Settings.Instance.CustomFilters = listBoxFilters.Items.Cast<Filter>().ToList();

            Settings.SaveSettings();

            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckBoxAutoAdjustTZ_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxTimezoneSelection.Enabled = !checkBoxAutoAdjustTZ.Checked;
        }

        private void ButtonAddFilter_Click(object sender, EventArgs e)
        {
            FilterEditor editor = new FilterEditor(null);
            editor.FilterResult += (result) => listBoxFilters.Items.Add(result);
            editor.ShowDialog();
        }

        private void ButtonEditFilter_Click(object sender, EventArgs e)
        {
            Filter selectedFilter = listBoxFilters.SelectedItem as Filter;
            FilterEditor editor = new FilterEditor(selectedFilter);
            editor.FilterResult += (result) =>
            {
                int index = listBoxFilters.SelectedIndex;
                listBoxFilters.Items.Remove(selectedFilter);
                listBoxFilters.Items.Insert(index, result);
            };
            editor.ShowDialog();
        }

        private void ButtonDeleteFilter_Click(object sender, EventArgs e)
        {
            listBoxFilters.Items.Remove(listBoxFilters.SelectedItem);
        }

        private void ListBoxFilters_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Filter selectedFilter = listBoxFilters.SelectedItem as Filter;
            FilterEditor editor = new FilterEditor(selectedFilter);
            editor.FilterResult += (result) =>
            {
                int index = listBoxFilters.SelectedIndex;
                listBoxFilters.Items.Remove(selectedFilter);
                listBoxFilters.Items.Insert(index, result);
            };
            editor.ShowDialog();
        }

        private void ButtonCheckUpdate_Click(object sender, EventArgs e)
        {
            Common.CheckForUpdate(true);
        }

        private void ButtonFeedback_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:derek.antrican@sigmanest.com&Subject=OpSchedule%20Feedback");
        }

        private void LinkLabelEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:derek.antrican@sigmanest.com&Subject=OpSchedule%20Feedback");
        }

        private void PictureBoxTeams_Click(object sender, EventArgs e)
        {
            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = "/C start im:\"<sip:derek.antrican@sigmatek.net>\"";
            Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.FileName = "cmd.exe";
            Process infoProcess = Process.Start(Info);
        }

        private void CheckBoxNotifyBeforeShift_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownNotifyBeforeShift.Enabled = checkBoxNotifyBeforeShift.Checked;
        }
    }
}
