using GanttChart;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace OpSchedule
{
    [Serializable]
    public class Shift : TimeBlock
    {
        public Shift() : base()
        {

        }

        public Shift(string title) : base(title)
        {
            Notes = "";
        }

        public Shift(string title, DateTime startTime, DateTime endTime) : base(title, startTime, endTime)
        {
            Notes = "";
        }

        public string Notes { get; set; }

        [XmlIgnore]
        public bool AlertedUser { get; set; } //If we have alerted the user about this shift upcoming

        public override bool Equals(object obj)
        {
            if (obj is Shift)
            {
                Shift shift = obj as Shift;

                if (shift.Text != this.Text ||
                    !shift.StartTime.Equals(this.StartTime) ||
                    !shift.EndTime.Equals(this.EndTime) ||
                    !shift.Color.Equals(this.Color) ||
                    shift.Notes != this.Notes)
                    return false;

                return true;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Text} ({StartTime.ToString("ddd M/d h:mm")} - {EndTime.ToString("h:mm")})";
        }

        public XmlElement Serialize(ref XmlDocument document)
        {
            XmlElement shiftElement = document.CreateElement("Shift");

            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                if (prop.GetCustomAttributes(true).Any(a => a is XmlIgnoreAttribute))
                    continue;

                XmlElement element = document.CreateElement(prop.Name);
                if (prop.PropertyType == typeof(Color))
                    element.InnerText = (prop.GetValue(this) as Color?).Value.ToArgb().ToString();
                else
                    element.InnerText = prop.GetValue(this).ToString();

                shiftElement.AppendChild(element);
            }

            return shiftElement;
        }

        public static Shift Deserialize(XmlElement shiftElement)
        {
            Shift result = new Shift();

            foreach (XmlNode propertyNode in shiftElement.ChildNodes)
            {
                PropertyInfo targetProp = result.GetType().GetProperties().FirstOrDefault(p => p.Name == propertyNode.Name);
                if (targetProp != null)
                {
                    if (targetProp.PropertyType == typeof(Color))
                        targetProp.SetValue(result, Color.FromArgb(Convert.ToInt32(propertyNode.InnerText)));
                    else
                        targetProp.SetValue(result, Convert.ChangeType(propertyNode.InnerText, targetProp.PropertyType));
                }
            }

            return result;
        }

        #region Override For Ignore
        [XmlIgnore]
        public override bool IsVisible { get => base.IsVisible; set => base.IsVisible = value; }

        [XmlIgnore]
        public override Rectangle Rect { get => base.Rect; set => base.Rect = value; }
        #endregion Override For Ignore
    }
}
