using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using PeanutButter.INIFile;

namespace SpellParser
{
    public static class Translations
    {
        #region Static Fields

        private const string TranslatorApiKeyResourceName = "SpellParser.translator-api-key.txt";

        private static readonly Lazy<Assembly> Assembly = new Lazy<Assembly>(() => typeof(Translations).Assembly);

        private static readonly Lazy<string> TranslatorApiKey = new Lazy<string>(() =>
            new StreamReader(Assembly.Value.GetManifestResourceStream(TranslatorApiKeyResourceName) ?? Stream.Null)
                .ReadToEnd());

        #endregion

        #region Properties

        public static Dictionary<string, string> EnToDe { get; } = new Dictionary<string, string>();

        #endregion

        #region Methods

        public static void LoadTranslations(in Stream stream)
        {
            var translation = new INIFile();
            translation.Parse(new StreamReader(stream).ReadToEnd());

            foreach (var (key, value) in translation[""])
                EnToDe.Add(key, value);
        }

        public static void SaveTranslations(in string filePath)
        {
            var translation = new INIFile();
            translation.AddSection("");

            foreach (var (key, value) in EnToDe)
                translation[""].Add(key, value);

            translation.Persist(filePath);
        }

        public static string Translate(in string text)
        {
            if (EnToDe.ContainsKey(text))
                return EnToDe[text];

            var request =
                WebRequest.Create(
                    "https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&from=en&to=de");
            request.Method = "POST";

            request.Headers.Add("Ocp-Apim-Subscription-Key", TranslatorApiKey.Value);
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

        #endregion
    }
}