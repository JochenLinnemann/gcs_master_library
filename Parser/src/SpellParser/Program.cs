using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using CsvHelper;
using Newtonsoft.Json;
using PeanutButter.INIFile;
using SpellParser.Models;

namespace SpellParser
{
    public static class Program
    {
        #region Static Fields

        private const string SpellsResourceName = "SpellParser.data.Ritual Magic Spells.spl";
        private const string SpellTranslationResourceName = "SpellParser.data.SpellTranslation.ini";
        private const string TranslatorApiKeyResourceName = "SpellParser.translator-api-key.txt";

        private static readonly Dictionary<string, string> EnToDe = new Dictionary<string, string>();

        private static string _translatorApiKey;

        #endregion

        #region Methods

        private static SpellList LoadSpellListJson(Stream spellsResouceStream)
        {
            var textReader = new StreamReader(spellsResouceStream);
            var spellList = JsonConvert.DeserializeObject<SpellList>(textReader.ReadToEnd());
            return spellList;
        }

        private static SpellList LoadSpellListXml(Stream spellsResouceStream)
        {
            var xmlSerializer = new XmlSerializer(typeof(SpellList));
            var spellList = xmlSerializer.Deserialize(spellsResouceStream) as SpellList ?? new SpellList();
            return spellList;
        }

        private static void LoadTranslations(in Stream stream)
        {
            var translation = new INIFile();
            translation.Parse(new StreamReader(stream).ReadToEnd());

            foreach (var (key, value) in translation[""])
                EnToDe.Add(key, value);
        }

        private static void Main(string[] args)
        {
            const bool isV2 = true;

            var assembly = typeof(Program).Assembly;
            var spellsResouceStream = assembly.GetManifestResourceStream(SpellsResourceName) ?? Stream.Null;

            LoadTranslations(assembly.GetManifestResourceStream(SpellTranslationResourceName));

            var spellList = isV2 ? LoadSpellListJson(spellsResouceStream) : LoadSpellListXml(spellsResouceStream);

            _translatorApiKey =
                new StreamReader(assembly.GetManifestResourceStream(TranslatorApiKeyResourceName) ?? Stream.Null)
                    .ReadToEnd();

            spellList.SplittermondSpells = SeparateByCollege(spellList, true, true, true).ToList();
            spellList.Spells.Clear();

            SaveTranslations("C:\\Temp\\SpellTranslation.ini");

            using var streamWriter = new StreamWriter("C:\\Temp\\Ritual Magic Spells-Adapted.csv");
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Configuration.Delimiter = ";";
            csvWriter.WriteRecords(spellList.Spells);

            csvWriter.Flush();

            if (isV2)
                SaveSpellListJson(new StreamWriter("C:\\Temp\\Splittermond-Zaubersprüche.spl"), spellList);
            else
                SaveSpellListXml(new StreamWriter("C:\\Temp\\Splittermond-Zaubersprüche.spl"), spellList);
        }

        private static void SaveSpellListJson(TextWriter textWriter, SpellList spellList)
        {
            spellList.Spells = spellList.SplittermondSpells;
            spellList.SplittermondSpells = null;

            var json = JsonConvert.SerializeObject(spellList, Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            textWriter.Write(json);
            textWriter.Flush();
        }

        private static void SaveSpellListXml(TextWriter textWriter, SpellList spellList)
        {
            var xmlSerializer = new XmlSerializer(typeof(SpellList));
            xmlSerializer.Serialize(textWriter, spellList);
        }

        private static void SaveTranslations(in string filePath)
        {
            var translation = new INIFile();
            translation.AddSection("");

            foreach (var (key, value) in EnToDe)
                translation[""].Add(key, value);

            translation.Persist(filePath);
        }

        private static IEnumerable<RitualMagicSpell> SeparateByCollege(SpellList spellList,
            bool useMagicSchoolsOfSplittermond, bool translateToGerman, bool calculateSpellLevel)
        {
            foreach (var spell in spellList.Spells)
            {
                var colleges = spell.College
                    .Replace("Technological/", "Technological+")
                    .Replace("Meta/Linking", "Meta+Linking").Split("/")
                    .Select(c => c.Trim()
                        .Replace("Technological+", "Technological/")
                        .Replace("Meta+Linking", "Meta/Linking"));

                foreach (var college in colleges)
                    yield return new RitualMagicSpell(spell)
                    {
                        Type = GetSpellType(spell.Type),
                        Name = $"{spell.Name} ({GetBaseSkill(college)})",
                        NameDe = $"{Translate(spell.Name)} ({GetBaseSkill(college)})",
                        College = college,
                        BaseSkill = GetBaseSkill(college),
                        Points = GetSpellLevel(spell.PrereqCount).ToString()
                    };
            }

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
                if (!translateToGerman)
                    return text;

                if (EnToDe.ContainsKey(text))
                    return EnToDe[text];

                var request =
                    WebRequest.Create(
                        "https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&from=en&to=de");
                request.Method = "POST";

                request.Headers.Add("Ocp-Apim-Subscription-Key", _translatorApiKey);
                request.Headers.Add("Ocp-Apim-Subscription-Region", "westeurope");
                request.ContentType = "application/json; charset=utf-8";

                var writer = new StreamWriter(request.GetRequestStream());
                writer.Write(JsonConvert.SerializeObject(new[] { new { Text = text } }));
                writer.Flush();

                using var response = request.GetResponse();
                var resultData = JsonConvert.DeserializeAnonymousType(
                    new StreamReader(response.GetResponseStream() ?? Stream.Null).ReadToEnd(), new[]
                    {
                        new
                        {
                            translations = new[]
                            {
                                new { text = "" }
                            }
                        }
                    });

                return EnToDe[text] = resultData?[0].translations?[0].text;
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