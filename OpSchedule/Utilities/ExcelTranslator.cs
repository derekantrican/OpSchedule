using GanttChart;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OpSchedule.Utilities
{
    public class ExcelTranslator
    {
        private ExcelWorksheets worksheets;
        public ExcelTranslator()
        {

        }

        public void LoadFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            ExcelPackage pkg = new ExcelPackage(file);
            worksheets = pkg.Workbook.Worksheets;
        }

        public List<string> GetSheetNames()
        {
            List<string> wsNames = worksheets.Select(p => p.Name).ToList();

            return wsNames;
        }

        public List<Person> TranslateExcel(string targetWS)
        {
            ExcelWorksheet ws = worksheets.FirstOrDefault(p => p.Name == targetWS);
            List<Person> result = ParseData(ws);

            return result;
        }

        public void Close()
        {
            worksheets.Dispose();
        }

        private List<string> GetNames(ExcelWorksheet ws)
        {
            List<string> names = new List<string>();

            for (int i = 1; i <= ws.Dimension.End.Row; i++)
                names.Add(ws.Cells[i, 1].Text);

            names = names.Distinct().ToList();
            names.RemoveAll(p => 
            {
                if (p == "" ||
                    p == "Training" ||
                    p == "Travel" ||
                    p == "Logout" ||
                    p == "Demo" ||
                    p == "STAFF" ||
                    p == "Total Login Hours" ||
                    p == "AE Hours" ||
                    p == "0")
                    return true;
                else
                    return false;
            });

            return names; //We won't try to "filter" the names (eg only first name) or anything. Just take exactly as they are in Excel
        }

        private List<DateTime> PopulateDateTimeRange(DateTime mon, DateTime fri)
        {
            List<DateTime> result = new List<DateTime>();

            DateTime curDay = mon;
            while (curDay <= fri)
            {
                int curHour = 8;
                while (curHour <= 18)
                {
                    DateTime dateWithHour = curDay.AddHours(curHour);

                    result.Add(dateWithHour);

                    curHour++;
                }

                result.Add(DateTime.MinValue); //Add this for spacing between days in the schedule

                curDay = curDay.AddDays(1);
            }

            return result;
        }

        private List<Shift> GetShifts(List<DateTime> dateData, ExcelRange personRow)
        {
            List<int> dateTimeRange = new List<int>();
            List<Shift> result = new List<Shift>();

            Shift currentShift = new Shift("", DateTime.MinValue, DateTime.MinValue);
            string curCellText = "";

            int startRow = personRow.Start.Row;
            int startCol = personRow.Start.Column;
            int endCol = personRow.End.Column - 6;
            for (int i = startCol; i <= endCol; i++)
            {
                string cellText = personRow[startRow, i].Text;
                Color cellColor = Color.White;
                string cellColorRgb = personRow[startRow, i].Style.Fill.BackgroundColor.Rgb;
                if (cellColorRgb != null)
                {
                    System.Windows.Media.Color mediaColor = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#" + cellColorRgb);
                    var colorLookup = typeof(Color)
                                        .GetProperties(BindingFlags.Public | BindingFlags.Static)
                                        .Select(f => (Color)f.GetValue(null, null))
                                        .Where(c => c.IsNamedColor)
                                        .ToLookup(c => c.ToArgb());

                    int argb = (mediaColor.A << 24) | (mediaColor.R << 16) | (mediaColor.G << 8) | mediaColor.B;
                    cellColor = colorLookup[argb].FirstOrDefault();
                    if (cellColor.IsEmpty)
                        cellColor = Color.FromArgb(argb);
                }

                DateTime correspondingDateTime = dateData[i - startCol];
                if (currentShift.StartTime != DateTime.MinValue)
                {
                    if (correspondingDateTime != DateTime.MinValue)
                    {
                        if (cellText != curCellText)
                        {
                            result.Add(currentShift);

                            //Start the next shift
                            Shift parsedShift = ParseShift(cellText, cellColor, ""/*WILL NOT SUPPORT (too hard)*/);
                            if (parsedShift != null)
                            {
                                currentShift = parsedShift;
                                currentShift.StartTime = correspondingDateTime;
                                currentShift.EndTime = correspondingDateTime.AddHours(1);

                                curCellText = cellText;
                            }
                            else
                                currentShift = new Shift("", DateTime.MinValue, DateTime.MinValue); //Reset currentShift
                        }
                        else
                            currentShift.EndTime = correspondingDateTime.AddHours(1);
                    }
                    else
                    {
                        result.Add(currentShift);
                        currentShift = new Shift("", DateTime.MinValue, DateTime.MinValue); //Reset currentShift
                    }
                }
                else
                {
                    Shift parsedShift = ParseShift(cellText, cellColor, ""/*WILL NOT SUPPORT (too hard)*/);
                    if (parsedShift != null)
                    {
                        currentShift = parsedShift;
                        currentShift.StartTime = correspondingDateTime;
                        currentShift.EndTime = correspondingDateTime.AddHours(1);

                        curCellText = cellText;
                    }
                }
            }

            return result;
        }

        private Shift ParseShift(string cellText, Color cellColor, string notes)
        {
            Shift result = null;

            if (cellColor == Color.Yellow) //Travel
            {
                result = new Shift("Travel");
                result.Color = Color.Yellow;
                result.Notes = notes;

                return result;
            }
            else
            {
                switch (cellText)
                {
                    case "2": //Tier 2
                    case "1": //Tier 1
                    case "e":
                        result = new Shift(cellText);
                        result.Color = Color.YellowGreen;
                        return result;
                    case "x": //Operator
                        result = new Shift("X");
                        result.Color = Color.YellowGreen;
                        return result;
                    case "t": //Training
                        result = new Shift("Training");
                        result.Color = Color.Gray;
                        result.Notes = notes;
                        return result;
                    case "D": //Demo
                        result = new Shift("Demo");
                        result.Color = Color.LightSkyBlue;
                        return result;
                    case "p": //PTO
                        result = new Shift("PTO");
                        result.Color = Color.MediumPurple;
                        return result;
                    case "C": //Call
                        result = new Shift("C");
                        result.Color = Color.DarkOrange;
                        result.Notes = notes;
                        return result;
                    case "N": //Blockout
                        result = new Shift("");
                        result.Color = Color.Red;
                        result.Hatch = true;
                        result.Clickable = false;
                        return result;
                }
            }

            //If we get here, nothing was matched above
            if (!string.IsNullOrWhiteSpace(cellText) && cellText != "o" &&
                cellColor.Name != "ffbfbfbf")
            {
                result = new Shift(cellText);
                result.Color = cellColor;
            }

            return result;
        }

        private List<Person> ParseData(ExcelWorksheet ws)
        {
            List<Person> result = new List<Person>();
            List<string> names = GetNames(ws);

            DateTime mon = DateTime.MinValue;
            DateTime fri = DateTime.MinValue;
            for (int i = 1; i <= ws.Dimension.End.Row; i++)
            {

                string rowItem0 = ws.Cells[i, 1].Text;
                if (names.Contains(rowItem0) && 
                    mon != DateTime.MinValue && fri != DateTime.MinValue)
                {
                    string name = ws.Cells[i, 1].Text;
                    Person matchingPerson = result.Find(p => p.Text == name);
                    if (matchingPerson == null)
                    {
                        matchingPerson = new Person(name);
                        result.Add(matchingPerson);
                    }

                    matchingPerson.TimeBlocks.AddRange(GetShifts(PopulateDateTimeRange(mon, fri), ws.Cells[i, 3, i, ws.Dimension.End.Column]));
                }
                else if (rowItem0 == "Training")
                {
                    mon = DateTime.MinValue;
                    fri = DateTime.MinValue;

                    DateTime.TryParse(ws.Cells[i, 3].Text, out mon);
                    DateTime.TryParse(ws.Cells[i, 51].Text, out fri);
                }
            }

            //Set location of each person (based on blockouts)
            foreach (Person person in result)
            {
                //Get closest blockout to now
                Shift closestBlockout = person.GetShifts().Where(p => !p.Clickable).OrderBy(p => Math.Abs((p.StartTime - DateTime.Now).Ticks)).FirstOrDefault();
                if (closestBlockout == null || closestBlockout.StartTime.Hour == 5)
                    person.EmployeeLocation = Enums.EmployeeLocation.Cincinnati;
                else if (closestBlockout.StartTime.Hour == 8)
                    person.EmployeeLocation = Enums.EmployeeLocation.Seattle;
            }

            //Convert all shift times from EST to Settings.Instance.TimeZone
            DateTime now = DateTime.UtcNow;
            int diff = (Settings.Instance.TimeZone.GetUtcOffset(now) - TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").GetUtcOffset(now)).Hours;

            foreach (Person person in result)
                person.OffsetShifts(diff);

            return result;
        }
    }
}
