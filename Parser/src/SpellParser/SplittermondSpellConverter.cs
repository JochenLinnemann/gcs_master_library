using System.Collections.Generic;
using System.Linq;
using SpellParser.Models;

namespace SpellParser
{
    public static class SplittermondSpellConverter
    {
        #region Methods

        public static void ConvertSpellList(SpellList spellList,
            bool useMagicSchoolsOfSplittermond = true, bool translateToGerman = true, bool calculateSpellLevel = true)
        {
            var convertedSpells = new List<RitualMagicSpell>();
            foreach (var spell in spellList.Spells)
            {
                var colleges = spell.College
                    .Replace("Technological/", "Technological+")
                    .Replace("Meta/Linking", "Meta+Linking").Split("/")
                    .Select(c => c.Trim()
                        .Replace("Technological+", "Technological/")
                        .Replace("Meta+Linking", "Meta/Linking"));

                foreach (var college in colleges)
                    convertedSpells.Add(new RitualMagicSpell(spell)
                    {
                        Type = GetSpellType(spell.Type),
                        Name = $"{spell.Name} ({GetBaseSkill(college)})",
                        NameDe = $"{Translate(spell.Name)} ({GetBaseSkill(college)})",
                        College = college,
                        BaseSkill = GetBaseSkill(college),
                        Points = GetSpellLevel(spell.PrereqCount).ToString()
                    });
            }

            spellList.Spells = convertedSpells;

            string GetBaseSkill(in string college)
            {
                if (!useMagicSchoolsOfSplittermond)
                    return
                        $"Path of {college.Replace("Technological", "Technology").Replace("Meta", "Meta-Spells").Replace("Armor Enchantment", "Enchantment")}";

                switch (college)
                {
                    case "Air":
                        return "Windmagie";
                    case "Animal":
                        return "Tiermagie";
                    case "Body Control":
                        return "Verwandlungsmagie";
                    case "Communication & Empathy":
                        return "Kommunikationsmagie";
                    case "Earth":
                    case "Technological/Metal":
                        return "Felsmagie";
                    case "Fire":
                        return "Feuermagie";
                    case "Food":
                        return "Nahrungsmagie";
                    case "Healing":
                        return "Heilungsmagie";
                    case "Illusion & Creation":
                        return "Illusionsmagie";
                    case "Knowledge":
                        return "Erkenntnismagie";
                    case "Light & Darkness":
                        return "Licht- & Schattenmagie";
                    case "Making & Breaking":
                        return "Objektmagie";
                    case "Meta":
                    case "Meta/Linking":
                        return "Metamagie";
                    case "Mind Control":
                        return "Beherrschungsmagie";
                    case "Movement":
                        return "Bewegungsmagie";
                    case "Necromancy":
                        return "Todesmagie";
                    case "Plant":
                        return "Pflanzenmagie";
                    case "Protection":
                        return "Schutzmagie";
                    case "Sound":
                        return "Geräuschmagie";
                    case "Water":
                        return "Wassermagie";
                    case "Weather":
                        return "Wettermagie";

                    case "Gate":
                        return "Portalmagie";

                    case "Enchantment":
                    case "Armor Enchantment":
                        return "Artefaktmagie";

                    case "Technological":
                    case "Technological/Energy":
                    case "Technological/Machine":
                    case "Technological/Plastic":
                    case "Technological/Radiation":
                        return "Technomagie";

                    default:
                        return college;
                }
            }

            string Translate(in string text)
            {
                return translateToGerman ? Translations.Translate(text) : text;
            }

            int GetSpellLevel(in int spellPrereqCount)
            {
                return calculateSpellLevel
                    ? 1 + (int) (spellPrereqCount / 2f + 0.6)
                    : 0;
            }

            string GetSpellType(in string spellType)
            {
                return calculateSpellLevel
                    ? "splittermond_spell"
                    : spellType;
            }
        }

        #endregion
    }
}