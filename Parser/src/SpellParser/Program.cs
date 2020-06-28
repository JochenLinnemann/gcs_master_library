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

        private static readonly Dictionary<string, string> _enToDe = new Dictionary<string, string>();

        private static string _translatorApiKey;

        #endregion

        #region Methods

        private static void LoadTranslations(Stream stream)
        {
            var translation = new INIFile();
            translation.Parse(new StreamReader(stream).ReadToEnd());

            foreach (var (key, value) in translation[""])
                _enToDe.Add(key, value);
        }

        private static void Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;
            var spellsResouceStream = assembly.GetManifestResourceStream(SpellsResourceName) ?? Stream.Null;

            LoadTranslations(assembly.GetManifestResourceStream(SpellTranslationResourceName));

            var xmlSerializer = new XmlSerializer(typeof(SpellList));
            var spellList = xmlSerializer.Deserialize(spellsResouceStream) as SpellList ?? new SpellList();

            _translatorApiKey =
                new StreamReader(assembly.GetManifestResourceStream(TranslatorApiKeyResourceName) ?? Stream.Null)
                    .ReadToEnd();

            spellList.Spells = SeparateByCollege(spellList, true, true).ToList();

            SaveTranslations("C:\\Temp\\SpellTranslation.ini");

            using var streamWriter = new StreamWriter("C:\\Temp\\Ritual Magic Spells-Adapted.csv");
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Configuration.Delimiter = ";";
            csvWriter.WriteRecords(spellList.Spells);

            csvWriter.Flush();
        }

        private static void SaveTranslations(string filePath)
        {
            var translation = new INIFile();
            translation.AddSection("");

            foreach (var (key, value) in _enToDe)
                translation[""].Add(key, value);

            translation.Persist(filePath);
        }

        private static IEnumerable<RitualMagicSpell> SeparateByCollege(SpellList spellList,
            bool useMagicSchoolsOfSplittermond, bool translateToGerman)
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
                        Name = $"{spell.Name} ({GetBaseSkill(college)})",
                        NameDe = $"{Translate(spell.Name)} ({GetBaseSkill(college)})",
                        College = college,
                        BaseSkill = GetBaseSkill(college)
                    };
            }

            string GetBaseSkill(string college)
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
                        return "Stärkungsmagie";
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

            string Translate(string text)
            {
                if (!translateToGerman)
                    return text;

                if (_enToDe.ContainsKey(text))
                    return _enToDe[text];

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

                return _enToDe[text] = resultData?[0].translations?[0].text;
            }
        }

        #endregion
    }
}