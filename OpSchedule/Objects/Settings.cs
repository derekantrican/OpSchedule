using OpSchedule.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OpSchedule
{
    public class Settings
    {
        private static string settingsPath = Path.Combine(Common.UserSettings, "Settings.xml");
        public static Settings Instance = new Settings();

        #region Settings
        [XmlIgnore]
        public TimeZoneInfo TimeZone { get; set; }
        public string TimeZoneString
        {
            get { return TimeZone.ToSerializedString(); }
            set { TimeZone = TimeZoneInfo.FromSerializedString(value); }
        }
        public bool AutoAdjustTZ { get; set; }
        public bool NotifyBeforeShift { get; set; }
        public int NotifyMinBeforeShift { get; set; }
        public bool NotifyScheduleChange { get; set; }
        public int SelectedFilterIndex { get; set; }
        public List<Filter> CustomFilters { get; set; }
        public Point ProgramStartupLocation { get; set; }
        public Size ProgramStartupSize { get; set; }
        public WindowState ProgramStartupState { get; set; }

        private static Settings GetDefaultValues()
        {
            Settings defaultSettings = new Settings();

            defaultSettings.TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            defaultSettings.AutoAdjustTZ = true;
            defaultSettings.NotifyBeforeShift = true;
            defaultSettings.NotifyMinBeforeShift = 5;
            defaultSettings.NotifyScheduleChange = true;
            defaultSettings.SelectedFilterIndex = 0;
            defaultSettings.CustomFilters = new List<Filter>();
            defaultSettings.ProgramStartupLocation = new Point(0, 0);
            defaultSettings.ProgramStartupSize = new Size(1066, 509);
            defaultSettings.ProgramStartupState = WindowState.Normal;

            return defaultSettings;
        }
        #endregion Settings

        #region Save Settings
        public static void SaveSettings()
        {
            TextWriter writer = new StreamWriter(settingsPath);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
            xmlSerializer.Serialize(writer, Instance);
            writer.Close();
        }
        #endregion Save Settings

        #region Read Settings
        public static void ReadSettings()
        {
            try
            {
                XDocument document = XDocument.Load(settingsPath);
                using (FileStream fileStream = new FileStream(settingsPath, FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
                    Instance = (Settings)xmlSerializer.Deserialize(fileStream);
                }

                foreach (PropertyInfo prop in typeof(Settings).GetProperties())
                {
                    if (prop.GetValue(Instance) == null) //Check to see if any of the properties are null
                        prop.SetValue(Instance, prop.GetValue(GetDefaultValues())); //Replace that value with the default value
                }
            }
            catch
            {
                Instance = GetDefaultValues();
            }
        }
        #endregion Read Settings
    }
}
