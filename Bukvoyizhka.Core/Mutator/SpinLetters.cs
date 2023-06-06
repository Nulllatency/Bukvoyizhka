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
    public class SpinLetters : ISpinner
    {
        private string? _text;

        private MutatorsConfig _config;
        public SpinLetters(MutatorsConfig config)
        {
            _config = config;
        }
        

        public string Mutate(string text)
        {
            if (text is null)
                return string.Empty;

            _text = text;

            MatchCollection matches = MakeMatches(_config.SpinLettersSearchRegexPattern);

            var stringsList = Spin(matches);

            ReplaceInText(matches, stringsList);

            return _text;
        }

        private MatchCollection MakeMatches(string pattern) => Regex.Matches(_text!, pattern, RegexOptions.Multiline);

        private List<string> Spin(MatchCollection matches)
        {
            var random = new Random();

            List<char[]> charsList = matches.Select(m => Regex.Replace(m.Value,_config.SpinLettersReplaceRegexPattern, "")
            .ToCharArray())
                .ToList();

            for (int i = 0; i < charsList.Count; i++)
            {
                for (int j = 0; j < charsList[i].Length; j++)
                    foreach (var alikeArray in _config.LookAlikeCharsList)
                    {
                        if (alikeArray.Length != 0)
                            if (alikeArray.Contains(charsList[i][j]))
                                charsList[i][j] = random.Next(1, 10) < _config.MutationChance ? alikeArray[random.Next(0, alikeArray.Length - 1)] : charsList[i][j];
                    }
            }

            return charsList.Select(chars => new string(chars)).ToList();
        }

        private void ReplaceInText(MatchCollection matches, List<string> stringsList)
        {
            for (int i = 0; i < matches.Count; i++)
                _text = _text?.Replace(matches[i].Value, stringsList[i]);
        }
    }
}
