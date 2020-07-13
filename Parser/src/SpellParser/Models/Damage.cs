using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SpellParser.Models
{
    public class Damage
    {
        // @formatter:off — disable formatter after this line

        #region Properties

        [XmlAttribute("type")]
        [JsonProperty("type")]
        public string Type { get; set; }

        [XmlAttribute("st")]
        [JsonProperty("st")]
        public string ST { get; set; }

        [XmlAttribute("base")]
        [JsonProperty("base")]
        public string Base { get; set; }

        [XmlAttribute("armor_divisor")]
        [JsonProperty("armor_divisor")]
        public string ArmorDivisor { get; set; }

        [XmlAttribute("fragmentation")]
        [JsonProperty("fragmentation")]
        public string Fragmentation { get; set; }

        [XmlAttribute("fragmentation_armor_divisor")]
        [JsonProperty("fragmentation_armor_divisor")]
        public string FragmentationArmorDivisor { get; set; }

        [XmlAttribute("fragmentation_type")]
        [JsonProperty("fragmentation_type")]
        public string FragmentationType { get; set; }

        [XmlAttribute("modifier_per_die")]
        [JsonProperty("modifier_per_die")]
        public string ModifierPerDie { get; set; }

        #endregion

        // @formatter:on — enable formatter after this line
    }
}