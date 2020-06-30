using System.Xml.Serialization;

namespace SpellParser.Models
{
    public class Damage
    {
        // @formatter:off — disable formatter after this line

        #region Properties

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("st")]
        public string ST { get; set; }

        [XmlAttribute("base")]
        public string Base { get; set; }

        [XmlAttribute("armor_divisor")]
        public string ArmorDivisor { get; set; }

        [XmlAttribute("fragmentation")]
        public string Fragmentation { get; set; }

        [XmlAttribute("fragmentation_armor_divisor")]
        public string FragmentationArmorDivisor { get; set; }

        [XmlAttribute("fragmentation_type")]
        public string FragmentationType { get; set; }

        [XmlAttribute("modifier_per_die")]
        public string ModifierPerDie { get; set; }

        #endregion

        // @formatter:on — enable formatter after this line
    }
}