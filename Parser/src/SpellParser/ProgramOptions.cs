using CommandLine;

namespace SpellParser
{
    public class ProgramOptions
    {
        #region Properties

        /// <summary>
        ///     e.g. "C:\\Temp\\Splittermond-Zaubersprüche.spl"
        /// </summary>
        [Option('o', "output", Required = true,
            HelpText = "Path of the XML/JSON file to write the converted spell library output to.")]
        public string Output { get; set; }

        /// <summary>
        ///     e.g. "C:\\Temp\\Ritual Magic Spells-Adapted.csv"
        /// </summary>
        [Option("save-adaption", HelpText = "Path of the CSV file to save spell adaptions to.")]
        public string SaveAdaption { get; set; }

        /// <summary>
        ///     e.g. "C:\\Temp\\SpellTranslation.ini"
        /// </summary>
        [Option("save-translations", HelpText = "Path of the INI file to save spell translations to.")]
        public string SaveTranslations { get; set; }

        #endregion
    }
}