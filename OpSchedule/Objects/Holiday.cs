using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpSchedule.Objects
{
    public class Holiday
    {
        public Holiday()
        {

        }

        public static List<Holiday> GetDefaultHolidays()
        {
            List<Holiday> holidays = new List<Holiday>();

            holidays.Add(new Holiday() { Name = "New Year's Day", Active = true });
            //holidays.Add(new Holiday() { Name = "New Year's Eve", Active = true }); //SigmaTEK doesn't get New Year's Eve off
            holidays.Add(new Holiday() { Name = "Thanksgiving Day", Active = true });
            holidays.Add(new Holiday() { Name = "Black Friday", Active = true });
            holidays.Add(new Holiday() { Name = "Christmas Day", Active = true });
            holidays.Add(new Holiday() { Name = "Christmas Eve", Active = true });
            holidays.Add(new Holiday() { Name = "Fourth of July", Active = true });
            holidays.Add(new Holiday() { Name = "Labor Day", Active = true });
            holidays.Add(new Holiday() { Name = "Memorial Day", Active = true });

            return holidays;
        }

        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? OverrideDate { get; set; }
        [XmlIgnore]
        public DateTime HolidayDate
        {
            get
            {
                if (OverrideDate.HasValue)
                    return OverrideDate.Value.Date;
                else
                {
                    DateTime holidayThisYear = CalcHolidayDate(DateTime.Today.Year);
                    if (holidayThisYear < DateTime.Today.AddDays(-30)) //"Margin" of 30 days
                        return CalcHolidayDate(DateTime.Today.Year + 1);
                    else
                        return holidayThisYear;
                }
            }
        }

        public DateTime CalcHolidayDate(int year)
        {
            if (OverrideDate.HasValue)
                return OverrideDate.Value.Date;

            string filteredName = Regex.Replace(Name.ToLower(), "((?![a-z0-9]).)", "");
            if (filteredName == "newyearsday")
                return new DateTime(year, 1, 1);
            else if (filteredName == "newyearseve")
                return new DateTime(year, 12, 31);
            else if (filteredName == "thanksgivingday")
            {
                var thanksgiving = (from day in Enumerable.Range(1, 30)
                                    where new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                    select day).ElementAt(3);
                return new DateTime(year, 11, thanksgiving);
            }
            else if (filteredName == "dayafterthanksgiving" ||
                     filteredName == "blackfriday")
            {
                var thanksgiving = (from day in Enumerable.Range(1, 30)
                                    where new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                    select day).ElementAt(3);
                return new DateTime(year, 11, thanksgiving + 1);
            }
            else if (filteredName == "christmasday")
                return new DateTime(year, 12, 25);
            else if (filteredName == "christmaseve")
                return new DateTime(year, 12, 24);
            else if (filteredName == "fourthofjuly" ||
                     filteredName == "july4th" ||
                     filteredName == "independenceday")
            {
                return new DateTime(year, 7, 4);
            }
            else if (filteredName == "laborday")
            {
                DateTime laborDay = new DateTime(year, 9, 1);
                DayOfWeek dayOfWeek = laborDay.DayOfWeek;
                while (dayOfWeek != DayOfWeek.Monday)
                {
                    laborDay = laborDay.AddDays(1);
                    dayOfWeek = laborDay.DayOfWeek;
                }

                return laborDay;
            }
            else if (filteredName == "memorialday")
            {
                DateTime memorialDay = new DateTime(year, 5, 31);
                DayOfWeek dayOfWeek = memorialDay.DayOfWeek;
                while (dayOfWeek != DayOfWeek.Monday)
                {
                    memorialDay = memorialDay.AddDays(-1);
                    dayOfWeek = memorialDay.DayOfWeek;
                }

                return memorialDay;
            }

            return new DateTime();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
