using OpSchedule.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace OpSchedule.Utilities
{
    public static class Serializers
    {
        public static string DataPath = @"\\sigmatek.net\Media\Web\info.sigmatek.net\OpSchedule\";
        public static string SchedulePath = Path.Combine(DataPath, "Schedule.xml");
        public static string HolidayPath = Path.Combine(DataPath, "Holidays.xml");

        public static List<Person> DeserializeSchedule(int retries = 1)
        {
            List<Person> result = null;

            int tries = 0;
            while (result == null && tries <= retries)
            {
                try
                {
                    result = DeserializeSchedule(SchedulePath);
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException ||
                        ex is DirectoryNotFoundException)
                    {
                        MessageBox.Show("Could not access data path - are you connected to the VPN?");
                    }
                    else if (ex is IOException)
                    {
                        //File is in use
                    }
                    else
                        throw;
                }

                tries++;
                Thread.Sleep(500); //Wait 500ms before trying again
            }

            return result == null ? new List<Person>() : result;
        }

        public static List<Person> DeserializeSchedule(TimeZoneInfo timeZone, int retries = 1)
        {
            List<Person> schedule = DeserializeSchedule(retries);
            if (schedule == null || schedule.Count == 0)
                return null;

            DateTime now = DateTime.UtcNow;
            int diff = (timeZone.GetUtcOffset(now) - TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time").GetUtcOffset(now)).Hours;

            foreach (Person person in schedule)
                person.OffsetShifts(diff);

            return schedule;
        }

        private static List<Person> DeserializeSchedule(string path)
        {
            List<Person> result = new List<Person>();
            if (!File.Exists(path))
                return result;

            XmlDocument document = new XmlDocument();
            document.Load(path);

            XmlNode rootNode = document.GetElementsByTagName("Schedule")[0];
            foreach (XmlNode personNode in rootNode.ChildNodes)
            {
                Person person = Person.Deserialize(personNode as XmlElement);
                foreach (XmlNode shiftNode in personNode.SelectNodes(".//Shift"))
                    person.TimeBlocks.Add(Shift.Deserialize(shiftNode as XmlElement));

                result.Add(person);
            }

            return result;
        }

        private static object _scheduleLock = new object();
        public static void SerializeSchedule(List<Person> data)
        {
            lock (_scheduleLock) //Lock the file so that no one attempts to read/write it while we're writing to it
            {
                try
                {
                    SerializeSchedule(data, SchedulePath);
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException ||
                        ex is DirectoryNotFoundException)
                    {
                        MessageBox.Show("Could not access data path - are you connected to the VPN?");
                    }
                    else if (ex is IOException)
                    {
                        //File is in use
                        MessageBox.Show("The file is in use. Try again later.");
                    }
                    else
                        throw;
                }
            }
        }

        private static void SerializeSchedule(List<Person> data, string path)
        {
            XmlDocument document = new XmlDocument();
            XmlNode rootNode = document.AppendChild(document.CreateNode(XmlNodeType.Element, "Schedule", null));
            XmlDeclaration xmlDecl = document.CreateXmlDeclaration("1.0", "utf-8", null);
            document.InsertBefore(xmlDecl, rootNode);

            foreach (Person person in data)
            {
                XmlElement personElement = person.Serialize(ref document);

                XmlElement shiftsElement = document.CreateElement("Shifts");
                foreach (Shift shift in person.GetShifts())
                {
                    XmlElement shiftElement = shift.Serialize(ref document);
                    shiftsElement.AppendChild(shiftElement);
                }

                personElement.AppendChild(shiftsElement);
                rootNode.AppendChild(personElement);
            }

            document.Save(path);
        }

        public static List<Holiday> DeserializeHolidays(int retries = 1)
        {
            List<Holiday> result = null;

            int tries = 0;
            while (result == null && tries <= retries)
            {
                try
                {
                    result = new List<Holiday>();
                    using (FileStream fileStream = new FileStream(HolidayPath, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Holiday>));
                        result = (List<Holiday>)xmlSerializer.Deserialize(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    if (ex is IOException)
                    {
                        //File is in use
                    }
                    else
                        throw;
                }

                tries++;
                Thread.Sleep(500); //Wait 500ms before trying again
            }

            return result;
        }

        private static object _holidayLock = new object();
        public static void SerializeHolidays(List<Holiday> data)
        {
            lock (_scheduleLock) //Lock the file so that no one attempts to read/write it while we're writing to it
            {
                try
                {
                    TextWriter writer = new StreamWriter(HolidayPath);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Holiday>));
                    xmlSerializer.Serialize(writer, data);
                    writer.Close();
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException ||
                        ex is DirectoryNotFoundException)
                    {
                        MessageBox.Show("Could not access data path - are you connected to the VPN?");
                    }
                    else if (ex is IOException)
                    {
                        //File is in use
                        MessageBox.Show("The file is in use. Try again later.");
                    }
                    else
                        throw;
                }
            }
        }
    }
}
