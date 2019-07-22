using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpSchedule.Views
{
    [DefaultEvent("MouseClick")]
    public partial class RibbonCounter : UserControl
    {
        public RibbonCounter()
        {
            InitializeComponent();
            InitEvents();

            AdjustVertically();
        }

        public RibbonCounter(string buttonText, int count)
        {
            InitializeComponent();
            InitEvents();

            labelText.Text = buttonText;
            labelCount.Text = count.ToString();

            AdjustVertically();
        }

        private void InitEvents()
        {
            labelCount.MouseClick += (sender, args) => this.OnMouseClick(args);
            labelText.MouseClick += (sender, args) => this.OnMouseClick(args);
        }

        private void AdjustCountSize()
        {
            string countDisplay = labelCount.Text;
            Font displayFont = this.Font;
            Size countSize = TextRenderer.MeasureText(countDisplay, displayFont);
            while (countSize.Width < labelCount.Width &&
                   countSize.Height < labelCount.Height)
            {
                displayFont = new Font(displayFont.Name, displayFont.Size + 2, FontStyle.Bold);
                countSize = TextRenderer.MeasureText(countDisplay, displayFont, new Size(0, 0), TextFormatFlags.NoPadding);
            }

            labelCount.Font = new Font(displayFont.Name, displayFont.Size - 2, FontStyle.Bold); //-2 because we exit after last iteration of while loop where we added 2

            if (labelCount.Width != countSize.Width + 5)
            {
                labelCount.Width = countSize.Width + 5;
                labelCount.Left = (this.Width - countSize.Width - 5) / 2;
            }
            Console.WriteLine("countSize width: " + countSize.Width);
            Console.WriteLine("labelCount width: " + labelCount.Width);

            Console.WriteLine("labelCount fontsize: " + labelCount.Font.Size);
            Console.WriteLine("displayFont size: " + displayFont.Size);
        }

        private void AdjustVertically()
        {
            string labelDisplayedText = GetLabelDisplayText();
            int textHeight = TextRenderer.MeasureText(labelDisplayedText, this.Font).Height;
            int diff = labelText.Height - textHeight;
            labelText.Height = textHeight;

            labelText.Top += diff;
            labelCount.Height += diff;

            AdjustCountSize();
        }

        private string GetLabelDisplayText()
        {
            List<string> labelWords = labelText.Text.Split(' ').ToList();
            List<string> labelLines = new List<string>();
            while (labelWords.Count > 0)
            {
                int lineWidth = 0;
                int wordIndex = 0;
                List<string> lineWords = new List<string>();
                while (lineWidth <= labelText.Width - labelText.Padding.Left - labelText.Padding.Right &&
                       wordIndex < labelWords.Count)
                {
                    lineWords.Add(labelWords[wordIndex]);
                    lineWidth = TextRenderer.MeasureText(string.Join(" ", lineWords), this.Font).Width;
                    wordIndex++;
                }

                if (lineWidth > labelText.Width - labelText.Padding.Left - labelText.Padding.Right)
                    lineWords.RemoveAt(lineWords.Count - 1); //We broke out of the while loop because the last word does not fit

                labelWords.RemoveAll(p => lineWords.Contains(p));
                labelLines.Add(string.Join(" ", lineWords));
            }

            return string.Join("\n", labelLines);
        }

        public int Count
        {
            get { return Convert.ToInt32(labelCount.Text); }
            set { labelCount.Text = value.ToString(); }
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

        private void LabelCount_Paint(object sender, PaintEventArgs e)
        {
            //labelCount.Font = GetAdjustedFont(e.Graphics, labelCount.Text, labelCount.Font, labelCount.ClientRectangle.Width, labelCount.ClientRectangle.Height, 100, 1, false);

        }

        public Font GetAdjustedFont(Graphics g, string graphicString, Font originalFont, int containerWidth, int containerHeight, int maxFontSize, int minFontSize, bool smallestOnFail)
        {
            Font testFont = null;
            // We utilize MeasureString which we get via a control instance           
            for (int adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize--)
            {
                testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);

                // Test the string with the new size
                SizeF adjustedSizeNew = g.MeasureString(graphicString, testFont);

                if (containerWidth > Convert.ToInt32(adjustedSizeNew.Width) && 
                    containerHeight > Convert.ToInt32(adjustedSizeNew.Height))
                {
                    // Good font, return it
                    return testFont;
                }
            }

            // If you get here there was no fontsize that worked
            // return minimumSize or original?
            if (smallestOnFail)
            {
                return testFont;
            }
            else
            {
                return originalFont;
            }
        }
    }
}
