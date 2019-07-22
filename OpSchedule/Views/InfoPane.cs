using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OpSchedule
{
    public partial class InfoPane : Form
    {
        Timer expirationTimer;
        public static InfoPane OpenInfoPane;
        public InfoPane(Shift shift, Person relatedPerson)
        {
            InitializeComponent();

            labelText.Text += $"{shift.Text}";
            labelText.Text += $"\n\n{relatedPerson.Text}";
            labelText.Text += $"\n\n{shift.StartTime.ToString("M/d %htt")}";
            labelText.Text += $"\n\n{shift.EndTime.ToString("M/d %htt")}";

            if (!string.IsNullOrEmpty(shift.Notes))
                labelText.Text += $"\n\n{shift.Notes}";

            ResizePane();
            StartExpirationTimer();

            OpenInfoPane = this;
        }

        public void ResizePane()
        {
            List<string> resultLines = new List<string>();
            foreach (string line in labelText.Text.Split('\n'))
            {
                int linePixels = TextRenderer.MeasureText(line, this.Font).Width;
                if (linePixels > this.Width)
                    resultLines.AddRange(WordWrap.Wrap(line, 50).Split('\n'));
                else
                    resultLines.Add(line);
            }

            string resultString = string.Join("\n", resultLines);
            labelText.Text = resultString;
            this.Size = TextRenderer.MeasureText(resultString, this.Font);
        }

        public void StartExpirationTimer()
        {
            expirationTimer = new Timer() { Interval = 3000 };
            expirationTimer.Tick += Timer_Tick;
            expirationTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //If the cursor is over the form, don't close the form
            Point adjCursorLocation = new Point(Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y);
            if (!this.ClientRectangle.Contains(adjCursorLocation))
                this.Close();
            else
                expirationTimer.Interval = 1500;
        }
    }
}
