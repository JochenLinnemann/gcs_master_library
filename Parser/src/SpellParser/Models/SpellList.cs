using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SpellParser.Models
{
    [XmlRoot("spell_list")]
    public class SpellList
    {
        // @formatter:off — disable formatter after this line

        #region Properties

        [XmlIgnore]
        [JsonProperty("type")]
        public string Type { get; set; }

        [XmlAttribute("version")]
        [JsonProperty("version")]
        public string Version { get; set; }

        [XmlAttribute("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [XmlElement("ritual_magic_spell")]
        [JsonProperty("rows")]
        public List<RitualMagicSpell> Spells { get; set; }

        [XmlElement("splittermond_spell")]
        [JsonProperty("rows2")]
        public List<RitualMagicSpell> SplittermondSpells { get; set; }

        #endregion

        // @formatter:on — enable formatter after this line
    }
}