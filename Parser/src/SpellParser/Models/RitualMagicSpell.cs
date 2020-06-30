using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpellParser.Models
{
    public class RitualMagicSpell
    {
        #region Constructors

        public RitualMagicSpell()
        {
        }

        public RitualMagicSpell(RitualMagicSpell spell)
        {
            Name = spell.Name;
            NameDe = spell.NameDe;
            Difficulty = spell.Difficulty;
            College = spell.College;
            PowerSource = spell.PowerSource;
            SpellClass = spell.SpellClass;
            CastingCost = spell.CastingCost;
            MaintenanceCost = spell.MaintenanceCost;
            CastingTime = spell.CastingTime;
            Duration = spell.Duration;
            Points = spell.Points;
            Reference = spell.Reference;
            BaseSkill = spell.BaseSkill;
            PrereqCount = spell.PrereqCount;
            Categories = new List<string>(spell.Categories);
            MeleeWeapons = new List<MeleeWeapon>(spell.MeleeWeapons);
            RangedWeapons = new List<RangedWeapon>(spell.RangedWeapons);
            Version = spell.Version;
        }

        #endregion

        // @formatter:off — disable formatter after this line

        #region Properties

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("name_de")]
        public string NameDe { get; set; }

        [XmlElement("difficulty")]
        public string Difficulty { get; set; }

        [XmlElement("college")]
        public string College { get; set; }

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
    
        [XmlElement("duration")]
        public string Duration { get; set; }

        [XmlElement("points")]
        public string Points { get; set; }

        [XmlElement("reference")]
        public string Reference { get; set; }
     
        [XmlElement("base_skill")]
        public string BaseSkill { get; set; }
      
        [XmlElement("prereq_count")]
        public int PrereqCount { get; set; }

        [XmlArray("categories")]
        [XmlArrayItem("category")]
        public List<string> Categories { get; set; }

        [XmlElement("melee_weapon")]
        public List<MeleeWeapon> MeleeWeapons { get; set; }

        [XmlElement("ranged_weapon")]
        public List<RangedWeapon> RangedWeapons { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

         #endregion

        // @formatter:on — enable formatter after this line
    }
}