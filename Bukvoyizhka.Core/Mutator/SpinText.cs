using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bukvoyizhka.Core.Configs;
using Bukvoyizhka.Core.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Bukvoyizhka.Core.Mutator
{
    public class SpinText : ISpinner
    {

        private string? _text;

        private MutatorsConfig _config;

        public SpinText(MutatorsConfig config)
        {
            _config = config;
        }

        

        public string Mutate(string text)
        {
            if (text is null)
                return string.Empty;

            _text = text;

            MatchCollection matches = MakeMatches(_config.SpinTextSearchRegexPattern);

            Spin(matches);

            ReplacePattern(_config.SpinTextReplaceRegexPattern);

            return _text;
        }

        private MatchCollection MakeMatches(string pattern) => Regex.Matches(_text!, pattern, RegexOptions.Multiline);

        private void Spin(MatchCollection matches)
        {
            var random = new Random();

            for (int i = 0; i < matches.Count; i++)
            {
                var variants = matches[i].Value.Split('|');

                _text = _text?.Replace(matches[i].Value, variants[random.Next(0, variants.Length - 1)]);
            }

        }

        private void ReplacePattern(string pattern) => _text = _text?.Replace("{ST{", "").Replace("}ST}", "");
    }
}
