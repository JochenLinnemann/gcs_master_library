using System.IO;
using System.Linq;
using System.Xml.Serialization;
using CsvHelper;
using CsvHelper.Configuration;
using SpellParser.Models;

namespace SpellParser
{
    public static class Program
    {
        #region Static Fields

        private const string SpellsResourceName = "SpellParser.data.Ritual Magic Spells.spl";

        #endregion

        #region Methods

        private static void Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;
            var spellsResouceStream = assembly.GetManifestResourceStream(SpellsResourceName) ?? Stream.Null;

            var xmlSerializer = new XmlSerializer(typeof(SpellList));
            var spellList = xmlSerializer.Deserialize(spellsResouceStream) as SpellList ?? new SpellList();

            var csvWriter = new CsvWriter(
                new StreamWriter("C:\\Temp\\Ritual Magic Spells-Adapted.csv"),
                new CsvConfiguration { Delimiter = ";" });

            csvWriter.WriteHeader(new { Name = "", College = "", PrereqCount = 0 }.GetType());

            foreach (var spell in spellList.Spells)
            foreach (var college in spell.College.Split('/').Select(c => c.Trim()))
                csvWriter.WriteRecord(new
                {
                    Name = spell.Name + " (" + college + ")", college, spell.PrereqCount
                });
        }

        #endregion
    }
}