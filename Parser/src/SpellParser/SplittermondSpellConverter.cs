using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpellParser.Models;

namespace SpellParser
{
    public class SplittermondSpellConverter : JsonConverter
    {
        #region Methods

        public static void ConvertSpellList(SpellList spellList)
        {
            var convertedSpells = new List<RitualMagicSpell>();
            foreach (var spell in spellList.Spells)
            {
                if (spell is SplittermondSpell)
                {
                    convertedSpells.Add(new SplittermondSpell(spell));
                    continue;
                }

                var colleges = spell.College
                    .Replace("Technological/", "Technological+")
                    .Replace("Meta/Linking", "Meta+Linking").Split("/")
                    .Select(c => c.Trim()
                        .Replace("Technological+", "Technological/")
                        .Replace("Meta+Linking", "Meta/Linking"));

                foreach (var college in colleges)
                    convertedSpells.Add(new SplittermondSpell(spell)
                    {
                        Name = $"{spell.Name} ({GetBaseSkill(college)})",
                        NameDe = $"{Translations.Translate(spell.Name)} ({GetBaseSkill(college)})",
                        College = college,
                        BaseSkill = GetBaseSkill(college),
                        Points = (1 + (int) (spell.PrereqCount / 2f + 0.6)).ToString()
                    });
            }

            spellList.Spells = convertedSpells;

            string GetBaseSkill(in string college)
            {
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
                        throw new ArgumentOutOfRangeException(nameof(college), $"College '{college}' is not known.");
                }
            }
        }

        #endregion

        #region Overrides of JsonConverter

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
            JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            RitualMagicSpell spell = null;
            switch (obj.Property("type")?.Value.Value<string>())
            {
                case RitualMagicSpell.RitualMagicSpellType:
                    spell = new RitualMagicSpell();
                    break;
                case SplittermondSpell.SplittermondSpellType:
                    spell = new SplittermondSpell();
                    break;
            }

            if (spell != null)
                serializer.Populate(obj.CreateReader(), spell);

            return spell;
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return typeof(RitualMagicSpell) == objectType || typeof(SplittermondSpell) == objectType;
        }

        #endregion
    }
}