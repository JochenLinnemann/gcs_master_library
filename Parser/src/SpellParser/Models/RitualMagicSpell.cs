using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SpellParser.Models
{
    public class RitualMagicSpell
    {
        #region Static Fields

        internal const string RitualMagicSpellType = "ritual_magic_spell";

        #endregion

        #region Constructors

        public RitualMagicSpell()
        {
        }

        public RitualMagicSpell(RitualMagicSpell spell)
        {
            Type = spell.Type;
            Version = spell.Version;
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
            Categories = new List<string>(spell.Categories ?? new List<string>());
            Weapons = new List<Weapon>(spell.Weapons ?? new List<Weapon>());
            MeleeWeapons = new List<MeleeWeapon>(spell.MeleeWeapons ?? new List<MeleeWeapon>());
            RangedWeapons = new List<RangedWeapon>(spell.RangedWeapons ?? new List<RangedWeapon>());
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

        [XmlElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [XmlElement("name_de")]
        [JsonProperty("name_de")]
        public string NameDe { get; set; }

        [XmlElement("reference")]
        [JsonProperty("reference")]
        public string Reference { get; set; }
     
        [XmlElement("difficulty")]
        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }

        [XmlElement("college")]
        [JsonProperty("college")]
        public string College { get; set; }

        [XmlElement("power_source")]
        [JsonProperty("power_source")]
        public string PowerSource { get; set; }

        [XmlElement("spell_class")]
        [JsonProperty("spell_class")]
        public string SpellClass { get; set; }

        [XmlElement("casting_cost")]
        [JsonProperty("casting_cost")]
        public string CastingCost { get; set; }

        [XmlElement("maintenance_cost")]
        [JsonProperty("maintenance_cost")]
        public string MaintenanceCost { get; set; }

        [XmlElement("casting_time")]
        [JsonProperty("casting_time")]
        public string CastingTime { get; set; }
    
        [XmlElement("duration")]
        [JsonProperty("duration")]
        public string Duration { get; set; }

        [XmlElement("points")]
        [JsonProperty("points")]
        public string Points { get; set; }

        [XmlIgnore]
        [JsonProperty("weapons")]
        public List<Weapon> Weapons { get; set; }

        [XmlElement("base_skill")]
        [JsonProperty("base_skill")]
        public string BaseSkill { get; set; }
      
        [XmlElement("prereq_count")]
        [JsonProperty("prereq_count")]
        public int PrereqCount { get; set; }

        //
        // Ignore prereqs
        //

        [XmlArray("categories")]
        [XmlArrayItem("category")]
        [JsonProperty("categories")]
        public List<string> Categories { get; set; }

        [XmlElement("melee_weapon")]
        [JsonIgnore]
        public List<MeleeWeapon> MeleeWeapons { get; set; }

        [XmlElement("ranged_weapon")]
        [JsonIgnore]
        public List<RangedWeapon> RangedWeapons { get; set; }

        #endregion

        // @formatter:on — enable formatter after this line
    }
}