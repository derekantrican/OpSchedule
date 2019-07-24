using GanttChart;
using OpSchedule.Objects;
using OpSchedule.Utilities;
using OpSchedule.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using static OpSchedule.Utilities.Enums;

namespace OpSchedule
{
    public static class Common
    {
        public static string ApplicationName = "OpSchedule";
        public static string ApplicationVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
        public static string UserSettings = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
        public static string LogPath = Path.Combine(UserSettings, "Log Files");

        public static bool HasVPNConnection = true;

        public static Dictionary<string, TimeZoneInfo> TZDict = new Dictionary<string, TimeZoneInfo>()
        {
            { "EST", TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")},
            { "CST", TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")},
            { "MST", TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time")},
            { "PST", TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")}
        };

        private static List<Holiday> holidays = null;
        public static List<Holiday> Holidays
        {
            get
            {
                if (holidays == null)
                    holidays = Serializers.DeserializeHolidays(5);

                return holidays;
            }
            set
            {
                holidays = value;
            }
        }

        /// <summary>
        /// Schedule Data WITHOUT pending changes
        /// </summary>
        public static List<Person> CurrentData = new List<Person>();
        /// <summary>
        /// Pending person (not shift) changes
        /// </summary>
        public static List<Tuple<Person, Edit>> PendingPersonChanges = new List<Tuple<Person, Edit>>();
        /// <summary>
        /// Pending shift changes
        /// </summary>
        public static List<Tuple<Shift, Person, Edit>> PendingShiftChanges = new List<Tuple<Shift, Person, Edit>>();
        /// <summary>
        /// Schedule data WITH pending changes
        /// </summary>
        /// <param name="withPending"></param>
        /// <returns></returns>
        public static List<Person> AllData(bool withPending = true)
        {
            List<Person> results = CurrentData.ToList();

            if (!withPending)
                return results;

            foreach (Tuple<Person, Edit> personChange in PendingPersonChanges)
            {
                Person matchingPerson = results.Find(p => p.Username == personChange.Item1.Username);
                if (personChange.Item2 == Edit.Add)
                    results.Add(personChange.Item1);
                else if (personChange.Item2 == Edit.Delete)
                    results.Remove(personChange.Item1);
                else if (personChange.Item2 == Edit.Overwrite)
                {
                    Person newPerson = personChange.Item1;
                    newPerson.TimeBlocks.AddRange(matchingPerson.GetShifts());
                    int index = results.IndexOf(matchingPerson);
                    results.Remove(matchingPerson);
                    results.Insert(index, newPerson);
                }
                else if (personChange.Item2 == Edit.Merge)
                {
                    if (matchingPerson != null)
                        matchingPerson.TimeBlocks.AddRange(personChange.Item1.GetShifts());
                    else
                        results.Add(personChange.Item1);
                }
            }

            foreach (Tuple<Shift, Person, Edit> shiftChange in PendingShiftChanges)
            {
                Person matchingPerson = results.Find(p => p.Username == shiftChange.Item2.Username);
                Shift matchingShift = matchingPerson.GetShifts().Find(p => p.StartTime == shiftChange.Item1.StartTime && 
                                                                           p.EndTime == shiftChange.Item1.EndTime);
                if (shiftChange.Item3 == Edit.Add)
                    matchingPerson.TimeBlocks.Add(shiftChange.Item1);
                else if (shiftChange.Item3 == Edit.Delete)
                    matchingPerson.TimeBlocks.Remove(matchingShift);
                else if (shiftChange.Item3 == Edit.Overwrite)
                {
                    matchingPerson.TimeBlocks.Remove(matchingShift);
                    matchingPerson.TimeBlocks.Add(shiftChange.Item1);
                }
            }

            return results;
        }
        /// <summary>
        /// Common.AllData but typed for the GanttChart
        /// </summary>
        /// <param name="withPending"></param>
        /// <returns></returns>
        public static List<Row> GanttData(bool withPending = true)
        {
            return AllData(withPending).Cast<Row>().ToList();
        }

        public static List<Color> CurrentlyUsedColors { get; set; }

        public static void CheckRequiredDirectoriesExist()
        {
            if (!Directory.Exists(UserSettings))
                Directory.CreateDirectory(UserSettings);

            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);

            if (!Directory.Exists(Serializers.DataPath) ||
                (!File.Exists(Serializers.SchedulePath) &&
                !File.Exists(Serializers.HolidayPath)))
                HasVPNConnection = false;
        }

        public static void DeleteOldVersions()
        {
            try
            {
                string progLoc = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\") + 1);
                File.Delete(Path.Combine(progLoc, $"{Common.ApplicationName}-old.exe"));
            }
            catch { }
        }

        public static void CheckForUpdate(bool showCurrentVersionMessage = false)
        {
            string updateFile = @"\\sigmatek.net\Media\Web\info.sigmatek.net\OpSchedule\Update.txt";
            string updateText = "";
            if (File.Exists(updateFile))
                updateText = File.ReadAllText(updateFile);

            List<string> updateInfo = new List<string>(updateText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
            double latestVersion = updateInfo.Where(p => p.IndexOf("Current") >= 0).FirstOrDefault() != null ? Convert.ToDouble(updateInfo.Where(p => p.IndexOf("Current") >= 0).FirstOrDefault().Split(':')[1]) : double.NaN;
            string updateURL = "";
            if (updateInfo.Where(p => p.IndexOf("Location") >= 0).FirstOrDefault() != null)
            {
                updateURL = updateInfo.Where(p => p.IndexOf("Location") >= 0).FirstOrDefault();
                updateURL = updateURL.Substring(updateURL.IndexOf(':') + 1);
            }

            string thisVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            if (latestVersion > Convert.ToDouble(thisVersion))
            {
                DialogResult res = MessageBox.Show($"A new version is available!\n\nThe current version is {latestVersion} and you are running {thisVersion}" +
                                    "\n\nDo you want to update to the new version?", "New Update Available!", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                    try
                    {
                        Updater updater = new Updater(updateURL);
                        updater.ShowDialog();
                    }
                    catch
                    {
                        res = MessageBox.Show("There was a problem getting the new update. Would you like to be redirected to https://github.com/derekantrican/OpSchedule/releases to download it manually?", 
                                        "Update Error", MessageBoxButtons.YesNo);

                        if (res == DialogResult.Yes)
                            Process.Start("https://github.com/derekantrican/OpSchedule/releases");
                    }
                }
            }
            else if (showCurrentVersionMessage)
                MessageBox.Show($"Congrats, you have the most current version! You are running version {thisVersion}", "Most Current Version", MessageBoxButtons.OK);
        }

        #region Log
        public static string LogFilePath { get; set; }
        public static string LogContents { get; set; }

        public static void Log(string itemToLog, string callerMethodName = null, bool writeToStatusStrip = false)
        {
            if (callerMethodName == null)
            {
                StackTrace stackTrace = new StackTrace();
                MethodBase method = stackTrace.GetFrame(1).GetMethod();
                callerMethodName = method.Name;
            }

            DateTime date = DateTime.Now;
            LogContents += $"[{callerMethodName} {date}] {itemToLog}\n";

            if (!File.Exists(LogFilePath))
                File.Create(LogFilePath).Close();

            if (File.ReadAllText(LogFilePath) != LogContents)
                File.WriteAllText(LogFilePath, LogContents);

            //Rather than appending text, we'll just check to see if it's different and write everything
            //(There shouldn't ever be so much text that this is a real performance hog and it also has
            //the advantage of taking care of the situation where a user turns on this setting midway
            //through a session of using the program)

            if (writeToStatusStrip)
                UpdateStatusStrip?.Invoke(itemToLog, false, false);
        }

        public static void LogError(string itemToLog, string callerMethodName = null, bool writeToStatusStrip = false)
        {
            if (callerMethodName == null)
            {
                StackTrace stackTrace = new StackTrace();
                MethodBase method = stackTrace.GetFrame(1).GetMethod();
                callerMethodName = method.Name;
            }

            Log(itemToLog, callerMethodName, false); //writeToStatusStrip: false because we're using different parameters and will do it below

            if (writeToStatusStrip)
                UpdateStatusStrip?.Invoke(itemToLog, true, true);
        }

        public delegate void UpdateStatusStripDelegate(string text, bool error = false, bool overwriteErrors = true);
        public static UpdateStatusStripDelegate UpdateStatusStrip;
        #endregion Log

        public static DateTime GetMondayForWeek(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Monday)
            {
                if (date.DayOfWeek == DayOfWeek.Sunday)
                    date = date.AddDays(1);
                else
                    date = date.AddDays(-1);
            }

            return date.Date;
        }

        public static DateTime GetFridayForWeek(DateTime date)
        {
            while (date.DayOfWeek != DayOfWeek.Friday)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                    date = date.AddDays(-1);
                else
                    date = date.AddDays(1);
            }

            return date.Date;
        }

        public static bool IsDataSame(List<Person> currentList, List<Person> newList)
        {
            List<Person> currentPersonsNotInNew = new List<Person>();
            foreach (Person person in currentList)
            {
                if (newList.Find(p => p.Equals(person)) == null)
                    currentPersonsNotInNew.Add(person);
            }

            List<Person> newPersonsNotInCurrent = new List<Person>();
            foreach (Person person in newList)
            {
                if (currentList.Find(p => p.Equals(person)) == null)
                    newPersonsNotInCurrent.Add(person);
            }

            return currentList.Count == newList.Count &&
                   !currentPersonsNotInNew.Any() &&
                   !newPersonsNotInCurrent.Any();
        }

        public static void DumpException(Exception ex)
        {
            string exceptionString = "";
            exceptionString += $"[{DateTime.Now}] EXCEPTION TYPE: {ex?.GetType()}\n\n";
            exceptionString += $"[{DateTime.Now}] EXCEPTION MESSAGE: {ex?.Message}\n\n";
            exceptionString += $"[{DateTime.Now}] INNER EXCEPTION: {ex?.InnerException}\n\n";
            exceptionString += $"[{DateTime.Now}] STACK TRACE: {ex?.StackTrace}\n\n";
            File.AppendAllText(Path.Combine(UserSettings, "CRASHREPORT (" + DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss") + ").log"), exceptionString);

            MessageBox.Show("There was an unhandled exception. Please contact the developer and relay this information: \n\nMessage: " + ex?.Message);
        }
    }
}
