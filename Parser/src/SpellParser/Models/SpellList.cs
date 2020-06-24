using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpellParser.Models
{
    [XmlRoot("spell_list")]
    public class SpellList
    {
        #region Properties

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("ritual_magic_spell")]
        public List<RitualMagicSpell> Spells { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        #endregion
    }
}