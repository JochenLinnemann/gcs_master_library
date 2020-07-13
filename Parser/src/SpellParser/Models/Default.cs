using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SpellParser.Models
{
    public class Default
    {
        // @formatter:off — disable formatter after this line

        #region Properties

        [XmlAttribute("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        [XmlElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlElement("specialization")]
        [JsonProperty("specialization")]
        public string Specialization { get; set; }

        [XmlElement("modifier")]
        [JsonProperty("modifier")]
        public string Modifier { get; set; }

        [XmlElement("level")]
        [JsonProperty("level")]
        public string Level{ get; set; }

        [XmlElement("adjusted_level")]
        [JsonProperty("adjusted_level")]
        public string AdjustedLevel { get; set; }

        [XmlElement("points")]
        [JsonProperty("points")]
        public string Points { get; set; }

        #endregion

        // @formatter:on — enable formatter after this line
    }
}