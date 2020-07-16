using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SpellParser.Models
{
    [XmlRoot("spell_list")]
    public class SpellList
    {
        #region Constructors

        public SpellList()
        {
            Type = "spell_list";
        }

        #endregion

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

        [XmlElement("ritual_magic_spell", typeof(RitualMagicSpell))]
        [XmlElement("splittermond_spell", typeof(SplittermondSpell))]
        [JsonProperty("rows")]
        public List<RitualMagicSpell> Spells { get; set; }

        #endregion

        // @formatter:on — enable formatter after this line
    }
}