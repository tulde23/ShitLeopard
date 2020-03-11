using System.Linq;
using Newtonsoft.Json.Linq;

namespace ShitLeopard.Common.Models
{
    public class ParsedSentence
    {
        public bool IsQuestion { get; }

        public JArray Tokens { get; }

        public ParsedSentence()
        {
        }

        internal ParsedSentence(object payload)

        {
            var entity = payload as JObject;
            var arrayOfSentences = entity.SelectToken("sentences") as JArray;
            var sentenceToken = arrayOfSentences.FirstOrDefault();
            Tokens = sentenceToken.SelectToken("tokens") as JArray;
            IsQuestion = Tokens.OfType<JObject>().Any(x => x.Property("pos").Value.ToString().StartsWith("W"));
        }

        public static ParsedSentence Build(object model)
        {
            return new ParsedSentence(model);
        }
    }
}