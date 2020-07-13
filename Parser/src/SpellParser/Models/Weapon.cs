using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SpellParser.Models
{
    public class Weapon
    {
		// @formatter:off — disable formatter after this line

        #region Properties

        [XmlIgnore]
        [JsonProperty("type")]
        public string Type { get; set; }

		[XmlElement("strength")]
		[JsonProperty("strength")]
        public string Strength { get; set; }

        [XmlElement("damage")]
        [JsonProperty("damage")]
        public Damage Damage {get;set;}

		[XmlElement("usage")]
		[JsonProperty("usage")]
        public string Usage { get; set; }

		[XmlElement("reach")]
		[JsonProperty("reach")]
        public string Reach { get; set; }

		[XmlElement("parry")]
		[JsonProperty("parry")]
        public string Parry { get; set; }

		[XmlElement("block")]
		[JsonProperty("block")]
        public string Block { get; set; }

		[XmlElement("accuracy")]
		[JsonProperty("accuracy")]
        public string Accuracy { get; set; }

		[XmlElement("range")]
		[JsonProperty("range")]
        public string Range { get; set; }

		[XmlElement("rate_of_fire")]
		[JsonProperty("rate_of_fire")]
        public string RateOfFire { get; set; }

		[XmlElement("shots")]
		[JsonProperty("shots")]
        public string Shots { get; set; }

		[XmlElement("bulk")]
		[JsonProperty("bulk")]
        public string Bulk { get; set; }

		[XmlElement("recoil")]
		[JsonProperty("recoil")]
        public string Recoil { get; set; }

        [XmlElement("default")]
        [JsonProperty("defaults")]
        public List<Default> Defaults { get; set; }

        #endregion

        // @formatter:on — enable formatter after this line
    }
}