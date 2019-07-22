using OpSchedule.Utilities;
using System.Drawing;
using System.Xml.Serialization;

namespace OpSchedule.Objects
{
    public class Filter
    {
        public Filter(string name)
        {
            Name = name;
        }

        public Filter()
        {

        }

        public string Name { get; set; }
        public bool InvertFilter { get; set; }
        public string TextContains { get; set; }
        public string NoteContains { get; set; }

        [XmlElement(Type=typeof(XmlColor))]
        public Color? Color { get; set; }

        //---------BELOW SHOULD NOT BE ACCESSIBLE TO USERS---------
        public bool OnlyCinci { get; set; }
        public bool OnlySeattle { get; set; }
    }
}
