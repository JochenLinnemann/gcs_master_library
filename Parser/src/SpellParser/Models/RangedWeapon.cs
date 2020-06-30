using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpellParser.Models
{
    public class RangedWeapon
    {
		// @formatter:off — disable formatter after this line

        #region Properties

        [XmlElement("damage")]
        public Damage Damage {get;set;}

		[XmlElement("strength")]
        public string Strength { get; set; }

		[XmlElement("usage")]
        public string Usage { get; set; }

		[XmlElement("accuracy")]
        public string Accuracy { get; set; }

		[XmlElement("range")]
        public string Range { get; set; }

		[XmlElement("rate_of_fire")]
        public string RateOfFire { get; set; }

		[XmlElement("shots")]
        public string Shots { get; set; }

		[XmlElement("bulk")]
        public string Bulk { get; set; }

		[XmlElement("recoil")]
        public string Recoil { get; set; }

        [XmlElement("default")]
        public List<Default> Defaults { get; set; }

        #endregion

        // @formatter:on — enable formatter after this line
    }
}