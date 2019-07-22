using GanttChart;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using static OpSchedule.Utilities.Enums;

namespace OpSchedule
{
    [Serializable]
    public class Person : Row
    {
        public Person() : base()
        {
            EmployeeLocation = EmployeeLocation.Cincinnati;
        }

        public Person(string name) : base(name)
        {
            EmployeeLocation = EmployeeLocation.Cincinnati;
        }

        public string Username { get; set; }
        [XmlIgnore]
        public string SkypeIdentity
        {
            get { return $"{Username}@sigmatek.net"; }
        }

        [XmlIgnore]
        public string Email
        {
            get { return $"{Username}@sigmanest.com"; }
        }

        public List<Shift> GetShifts()
        {
            return this.TimeBlocks.Cast<Shift>().ToList();
        }

        public List<Shift> GetShiftsForWeek(DateTime dayInWeek)
        {
            DateTime monday = Common.GetMondayForWeek(dayInWeek);
            DateTime saturday = Common.GetFridayForWeek(dayInWeek).AddDays(1);

            return GetShifts().Where(s =>
            {
                return s.StartTime >= monday && s.StartTime < saturday &&
                       s.EndTime >= monday && s.EndTime < saturday;
            }).ToList();
        }

        public List<Shift> GetShiftsForDay(DateTime day)
        {
            return GetShifts().Where(s =>
            {
                return s.StartTime.Date == day.Date && s.EndTime.Date == day.Date;
            }).ToList();
        }

        public EmployeeLocation EmployeeLocation { get; set; }
        public Permissions Permissions { get; set; }

        public void AddESTBlockouts(DateTime firstDay, DateTime lastDay)
        {
            DateTime curDay = firstDay;
            while (curDay <= lastDay)
            {
                AddESTBlockouts(curDay);
                curDay = curDay.AddDays(1);
            }
        }

        public void AddESTBlockouts(DateTime date)
        {
            Shift blockOut = new Shift("", date.AddHours(17), date.AddHours(19)) { Color = Color.Red, Hatch = true, Clickable = false };
            this.TimeBlocks.Add(blockOut);
        }

        public void AddPSTBlockouts(DateTime firstDay, DateTime lastDay)
        {
            DateTime curDay = firstDay;
            while (curDay <= lastDay)
            {
                AddPSTBlockouts(curDay);
                curDay = curDay.AddDays(1);
            }
        }

        public void AddPSTBlockouts(DateTime date)
        {
            Shift blockOut = new Shift("", date.AddHours(8), date.AddHours(10)) { Color = Color.Red, Hatch = true, Clickable = false };
            this.TimeBlocks.Add(blockOut);
        }

        public void OffsetShifts(int offset)
        {
            foreach (Shift shift in TimeBlocks)
            {
                shift.StartTime = shift.StartTime.AddHours(offset);
                shift.EndTime = shift.EndTime.AddHours(offset);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Person)
            {
                Person person = obj as Person;

                if (person.Text != this.Text ||
                    person.EmployeeLocation != this.EmployeeLocation ||
                    person.Username != this.Username)
                    return false;

                List<Shift> thisShifts = this.TimeBlocks.Cast<Shift>().ToList();
                List<Shift> otherShifts = person.TimeBlocks.Cast<Shift>().ToList();

                thisShifts.RemoveAll(p => !p.Clickable);
                otherShifts.RemoveAll(p => !p.Clickable);

                List<Shift> thisShiftsNotInOther = new List<Shift>();
                foreach (Shift shift in thisShifts)
                {
                    if (otherShifts.Find(p => p.Equals(shift)) == null)
                        thisShiftsNotInOther.Add(shift);
                }

                List<Shift> otherShiftsNotInThis = new List<Shift>();
                foreach (Shift shift in otherShifts)
                {
                    if (thisShifts.Find(p => p.Equals(shift)) == null)
                        otherShiftsNotInThis.Add(shift);
                }

                if (thisShifts.Count != otherShifts.Count ||
                    thisShiftsNotInOther.Any() ||
                    otherShiftsNotInThis.Any())
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
            return this.Text;
        }

        public Person Clone(bool withShifts)
        {
            Person p = new Person(this.Text);
            p.Username = this.Username;
            p.EmployeeLocation = this.EmployeeLocation;
            p.Permissions = this.Permissions;

            if (withShifts)
            {
                foreach (Shift shift in this.GetShifts())
                    p.TimeBlocks.Add(shift);
            }

            return p;
        }

        public XmlElement Serialize(ref XmlDocument document)
        {
            XmlElement personElement = document.CreateElement("Person");

            foreach (PropertyInfo prop in this.GetType().GetProperties())
            {
                if (prop.GetCustomAttributes(true).Any(a => a is XmlIgnoreAttribute))
                    continue;

                XmlElement element = document.CreateElement(prop.Name);
                element.InnerText = prop.GetValue(this).ToString();
                personElement.AppendChild(element);
            }

            return personElement;
        }

        public static Person Deserialize(XmlElement personElement)
        {
            Person result = new Person();

            foreach (XmlNode propertyNode in personElement.ChildNodes)
            {
                PropertyInfo targetProp = result.GetType().GetProperties().FirstOrDefault(p => p.Name == propertyNode.Name);
                if (targetProp != null)
                {
                    if (targetProp.PropertyType.IsEnum)
                        targetProp.SetValue(result, Enum.Parse(targetProp.PropertyType, propertyNode.InnerText));
                    else
                        targetProp.SetValue(result, propertyNode.InnerText);
                }
            }

            return result;
        }

        #region Override For Ignore
        [XmlIgnore]
        public override bool IsVisible { get => base.IsVisible; set => base.IsVisible = value; }

        [XmlIgnore]
        public override List<TimeBlock> TimeBlocks { get => base.TimeBlocks; set => base.TimeBlocks = value; }

        [XmlIgnore]
        public override Rectangle IconRect { get => base.IconRect; set => base.IconRect = value; }

        [XmlIgnore]
        public override Rectangle Rect { get => base.Rect; set => base.Rect = value; }

        [XmlIgnore]
        public override Image Icon { get => base.Icon; set => base.Icon = value; }
        #endregion Override For Ignore
    }
}
