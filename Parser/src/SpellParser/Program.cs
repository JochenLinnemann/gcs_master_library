using System.Globalization;
using System.IO;
using CommandLine;
using CsvHelper;

namespace SpellParser
{
    public static class Program
    {
        #region Static Fields

        private const string CustomSpellsResourceName = "SpellParser.data.Custom Spells.json";
        private const string SpellsResourceName = "SpellParser.data.Ritual Magic Spells.spl";
        private const string SpellTranslationResourceName = "SpellParser.data.SpellTranslation.ini";

        #endregion

        #region Methods

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ProgramOptions>(args).WithParsed(options =>
            {
                const bool isJsonBased = true;

                var assembly = typeof(Program).Assembly;
                var customSpellsResourceStream =
                    assembly.GetManifestResourceStream(CustomSpellsResourceName) ?? Stream.Null;
                var spellsResourceStream = assembly.GetManifestResourceStream(SpellsResourceName) ?? Stream.Null;

                Translations.LoadTranslations(assembly.GetManifestResourceStream(SpellTranslationResourceName));

                var spellList = isJsonBased
                    ? SpellListHandler.LoadSpellListJson(spellsResourceStream, customSpellsResourceStream)
                    : SpellListHandler.LoadSpellListXml(spellsResourceStream);

                SplittermondSpellConverter.ConvertSpellList(spellList);

                if (!string.IsNullOrWhiteSpace(options.SaveTranslations))
                    Translations.SaveTranslations(options.SaveTranslations);

                if (!string.IsNullOrWhiteSpace(options.SaveAdaption))
                {
                    using var streamWriter = new StreamWriter(options.SaveAdaption);
                    using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                    csvWriter.Configuration.Delimiter = ";";
                    csvWriter.WriteRecords(spellList.Spells);
                    csvWriter.Flush();
                }

                if (isJsonBased)
                    SpellListHandler.SaveSpellListJson(new StreamWriter(options.Output), spellList);
                else
                    SpellListHandler.SaveSpellListXml(new StreamWriter(options.Output), spellList);
            });
        }

        #endregion
    }
}