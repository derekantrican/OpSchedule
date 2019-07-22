using System;
using System.Drawing;
using System.Windows.Forms;
using GanttChart;

namespace OpSchedule.Views
{
    public partial class NavGanttChart : UserControl
    {
        public NavGanttChart()
        {
            InitializeComponent();
        }

        private void ButtonLeft_Click(object sender, EventArgs e)
        {
            OnBeginNavigate?.Invoke();

            DateTime ganttStart = ganttChart.StartDate;
            DateTime ganttEnd = ganttChart.EndDate;
            ganttChart.StartDate = ganttStart.AddDays(-7);
            ganttChart.EndDate = ganttEnd.AddDays(-7);

            ganttChart.UpdateView();
        }

        private void ButtonRight_Click(object sender, EventArgs e)
        {
            OnBeginNavigate?.Invoke();

            DateTime ganttStart = ganttChart.StartDate;
            DateTime ganttEnd = ganttChart.EndDate;
            ganttChart.StartDate = ganttStart.AddDays(7);
            ganttChart.EndDate = ganttEnd.AddDays(7);

            ganttChart.UpdateView();
        }

        private void ButtonGoTo_Click(object sender, EventArgs e)
        {
            OnBeginNavigate?.Invoke();

            DateTime targetMon = Common.GetMondayForWeek(dateTimePickerGoTo.Value);
            DateTime targetSat = Common.GetFridayForWeek(dateTimePickerGoTo.Value).AddDays(1);

            ganttChart.StartDate = targetMon;
            ganttChart.EndDate = targetSat;

            ganttChart.UpdateView();
        }

        public Chart GanttChart
        {
            get { return ganttChart; }
            set { ganttChart = value; }
        }

        public delegate void OnBeginNavigateDelegate();
        public event OnBeginNavigateDelegate OnBeginNavigate;
    }
}
