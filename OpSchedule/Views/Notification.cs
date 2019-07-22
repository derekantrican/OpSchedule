using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpSchedule.Views
{
    public partial class Notification : Form
    {
        public static readonly List<Notification> OpenNotifications = new List<Notification>();
        public new Main ParentForm;
        private Timer lifeTimer = new Timer();
        public Notification(string notificationTitle, string notificationMessage, bool hideAfterDelay = true, bool withSound = true)
        {
            InitializeComponent();

            //Subscribe to all click events
            this.Click += NotificationClick;
            pictureBoxIcon.Click += NotificationClick;
            labelTitle.Click += NotificationClick;
            labelMessage.Click += NotificationClick;

            labelTitle.Text = notificationTitle;
            labelMessage.Text = notificationMessage;

            AdjustVertically();

            if (withSound)
            {
                string notificationSoundPath = @"C:\Windows\media\Windows Notify System Generic.wav";
                using (var player = new System.Media.SoundPlayer(notificationSoundPath))
                    player.Play();
            }

            lifeTimer.Interval = 5000; //Show the notification for 5 seconds
            lifeTimer.Tick += LifeTimer_Tick;

            if (hideAfterDelay)
                StartAutoHideTimer();
        }

        private void AdjustVertically()
        {
            string labelDisplayedText = GetLabelDisplayText(labelMessage);
            int textHeight = TextRenderer.MeasureText(labelDisplayedText, this.Font).Height;
            int diff = textHeight - labelMessage.Height;

            if (diff > 0)
            {
                this.Height += diff;
                labelMessage.Height += diff;
            }
        }

        private string GetLabelDisplayText(Label label)
        {
            List<string> labelWords = label.Text.Split(' ').ToList();
            List<string> labelLines = new List<string>();
            while (labelWords.Count > 0)
            {
                int lineWidth = 0;
                int wordIndex = 0;
                List<string> lineWords = new List<string>();
                while (lineWidth <= label.Width - label.Padding.Left - label.Padding.Right &&
                       wordIndex < labelWords.Count)
                {
                    lineWords.Add(labelWords[wordIndex]);
                    lineWidth = TextRenderer.MeasureText(string.Join(" ", lineWords), this.Font).Width;
                    wordIndex++;
                }

                if (lineWidth > label.Width - label.Padding.Left - label.Padding.Right)
                    lineWords.RemoveAt(lineWords.Count - 1); //We broke out of the while loop because the last word does not fit

                labelWords.RemoveAll(p => lineWords.Contains(p));
                labelLines.Add(string.Join(" ", lineWords));
            }

            return string.Join("\n", labelLines);
        }

        public void StartAutoHideTimer()
        {
            lifeTimer.Start();
        }

        private void LifeTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            // Display the form just above the system tray.
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width,
                                      Screen.PrimaryScreen.WorkingArea.Height - this.Height);

            // Move each open form upwards to make room for this one
            foreach (Notification openForm in OpenNotifications)
            {
                openForm.Top -= Height + (int)(Screen.PrimaryScreen.WorkingArea.Height * 0.01); //Padding of 1% of screen height between multiple notifications
            }

            OpenNotifications.Add(this);
        }

        private void NotificationClick(object sender, EventArgs e)
        {
            if (ParentForm != null)
                ParentForm.Restore();

            this.Close();
        }

        //Don't steal focus when showing
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }
    }
}
