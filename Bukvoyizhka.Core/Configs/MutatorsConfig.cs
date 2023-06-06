using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bukvoyizhka.Core.Configs
{
    public class MutatorsConfig
    {
        public string SpinTextSearchRegexPattern { get; private set; }
        public string SpinTextReplaceRegexPattern { get; private set; }
        public string SpinLettersSearchRegexPattern { get; private set; }
        public string SpinLettersReplaceRegexPattern { get; private set; }


        public List<string> LookAlikeCharsList { get; private set; }

        private int _mutationChance;
        /// <summary>
        /// Range 1-10. 1 point means 10%
        /// </summary>
        public int MutationChance
        {
            get { return _mutationChance; }
            set { if (value >= 0 && value <= 10) _mutationChance = value; }
        }

        /// <summary>
        /// Create config with defaults settings
        /// </summary>
        public MutatorsConfig()
        {
            SpinTextSearchRegexPattern = @"\{ST\{([^{}]+)\}ST\}";
            SpinTextReplaceRegexPattern = @"\{ST\{|\}ST\}";

            SpinLettersSearchRegexPattern = @"\{SL\{.*\}SL\}";
            SpinLettersReplaceRegexPattern = @"\{SL\{|\}SL\}";

            LookAlikeCharsList = new List<string>()
            {
                "aаạąäàáą",
                "cсƈċ",
                "dԁɗ",
                "eеẹėéè",
                "gġ",
                "hһ",
                "iіíï",
                "jјʝ",
                "kκ",
                "lӏḷ",
                "nո",
                "oоοօȯọỏơóòö",
                "pр",
                "qզ",
                "sʂ",
                "uυսüúù",
                "vνѵ",
                "xхҳ",
                "yуý",
                "zʐż"
            };

            MutationChance = 2;
        }


    }
}
