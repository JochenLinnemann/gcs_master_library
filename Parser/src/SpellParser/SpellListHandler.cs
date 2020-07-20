using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SpellParser.Models;

namespace SpellParser
{
    public static class SpellListHandler
    {
        #region Methods

        public static SpellList LoadSpellListJson(Stream spellListStream, Stream additionalSpellListStream)
        {
            var streamReader = new StreamReader(spellListStream);
            var spellList = JsonConvert.DeserializeObject<SpellList>(streamReader.ReadToEnd());

            var additionalStreamReader = new StreamReader(additionalSpellListStream);
            var additionalSpellList = JsonConvert.DeserializeObject<SpellList>(additionalStreamReader.ReadToEnd(),
                new SplittermondSpellConverter());

            spellList.Spells.AddRange(additionalSpellList.Spells);

            return spellList;
        }

        public static SpellList LoadSpellListXml(Stream spellListStream)
        {
            var xmlSerializer = new XmlSerializer(typeof(SpellList));
            var spellList = xmlSerializer.Deserialize(spellListStream) as SpellList ?? new SpellList();
            return spellList;
        }

        public static void SaveSpellListJson(TextWriter textWriter, SpellList spellList)
        {
            var json = JsonConvert.SerializeObject(spellList, Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            textWriter.Write(json);
            textWriter.Flush();
        }

        public static void SaveSpellListXml(TextWriter textWriter, SpellList spellList)
        {
            var xmlSerializer = new XmlSerializer(typeof(SpellList));
            xmlSerializer.Serialize(textWriter, spellList);
        }

        #endregion
    }
}