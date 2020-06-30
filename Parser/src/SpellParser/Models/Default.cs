using System.Xml.Serialization;

namespace SpellParser.Models
{
    public class Default
    {
        // @formatter:off — disable formatter after this line

        #region Properties

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("specialization")]
        public string Specialization { get; set; }

        [XmlElement("modifier")]
        public string Modifier { get; set; }

        [XmlElement("level")]
        public string Level{ get; set; }

        [XmlElement("adjusted_level")]
        public string AdjustedLevel { get; set; }

        [XmlElement("points")]
        public string Points { get; set; }

        #endregion

        // @formatter:on — enable formatter after this line
    }
}