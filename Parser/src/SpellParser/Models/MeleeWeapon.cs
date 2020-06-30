using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpellParser.Models
{
    public class MeleeWeapon
    {
		// @formatter:off — disable formatter after this line

        #region Properties

        [XmlElement("damage")]
        public Damage Damage {get;set;}

		[XmlElement("strength")]
        public string Strength { get; set; }

		[XmlElement("usage")]
        public string Usage { get; set; }

		[XmlElement("reach")]
        public string Reach { get; set; }

		[XmlElement("parry")]
        public string Parry { get; set; }

		[XmlElement("block")]
        public string Block { get; set; }

        [XmlElement("default")]
        public List<Default> Defaults { get; set; }

        #endregion

        // @formatter:on — enable formatter after this line
    }
}