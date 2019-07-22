using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpSchedule.Views
{
    [DefaultEvent("MouseClick")]
    public partial class RibbonButton : UserControl
    {
        public RibbonButton()
        {
            InitializeComponent();
            InitEvents();

            AdjustVertically();
        }

        public RibbonButton(Image buttonImage, string buttonText)
        {
            InitializeComponent();
            InitEvents();

            pictureBox.Image = buttonImage;
            labelText.Text = buttonText;

            AdjustVertically();
        }

        private void InitEvents()
        {
            pictureBox.MouseClick += (sender, args) => this.OnMouseClick(args);
            labelText.MouseClick += (sender, args) => this.OnMouseClick(args);
        }

        private void AdjustVertically()
        {
            string labelDisplayedText = GetLabelDisplayText(labelText);
            int textHeight = TextRenderer.MeasureText(labelDisplayedText, this.Font).Height;
            int diff = labelText.Height - textHeight;
            labelText.Height = textHeight;

            labelText.Top += diff;
            pictureBox.Height += diff;
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

        public Image Image
        {
            get { return pictureBox.Image; }
            set { pictureBox.Image = value; }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return labelText.Text; }
            set
            {
                labelText.Text = value;
                AdjustVertically();
            }
        }
    }
}
