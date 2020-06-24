using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpellParser.Models
{
    public class RitualMagicSpell
    {
        #region Properties

        [XmlElement("points")]
        public string Points { get; set; }

        [XmlElement("reference")]
        public string Reference { get; set; }

        [XmlElement("base_skill")]
        public string BaseSkill { get; set; }

        [XmlElement("power_source")]
        public string PowerSource { get; set; }

        [XmlElement("spell_class")]
        public string SpellClass { get; set; }

        [XmlElement("casting_cost")]
        public string CastingCost { get; set; }

        [XmlElement("maintenance_cost")]
        public string MaintenanceCost { get; set; }

        [XmlElement("casting_time")]
        public string CastingTime { get; set; }

        [XmlArray("categories")]
        [XmlArrayItem("category")]
        public List<string> Categories { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("difficulty")]
        public string Difficulty { get; set; }

        [XmlElement("college")]
        public string College { get; set; }

        [XmlElement("duration")]
        public string Duration { get; set; }

        [XmlElement("prereq_count")]
        public int PrereqCount { get; set; }

        [XmlElement("ranged_weapon")]
        public List<RangedWeapon> RangedWeapons { get; set; }

        [XmlElement("melee_weapon")]
        public List<MeleeWeapon> MeleedWeapons { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        #endregion
    }
}