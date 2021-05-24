using System;
using System.Collections.Generic;
using GrandTextAdventure.Core.TextProcessing.Interfaces;

namespace GrandTextAdventure.Core.TextProcessing.Synonyms
{
    public class AdjectiveMapping : IAdjectiveMapping
    {
        private readonly Dictionary<string, string> _adjectiveMapping = new();

        public AdjectiveMapping()
        {
            // Appearance
            Add("bald");
            Add("beautiful");
            Add("chubby");
            Add("clean");
            Add("dazzling");
            Add("drab");
            Add("elegant");
            Add("fancy");
            Add("fit");
            Add("flabby");
            Add("glamorous");
            Add("gorgeous");
            Add("handsome");
            Add("magnificent");
            Add("muscular");
            Add("plain");
            Add("plump");
            Add("scruffy");
            Add("shapely");
            Add("skinny");
            Add("stock");
            Add("unkempt");
            Add("unsightly");

            // Positive Personality
            Add("agreeable");
            Add("ambitious");
            Add("brave");
            Add("calm");
            Add("delightful");
            Add("eager");
            Add("faithful");
            Add("gentle");
            Add("happy");
            Add("jolly");
            Add("kind");
            Add("lively");
            Add("nice");
            Add("obedient");
            Add("polite");
            Add("proud");
            Add("silly");
            Add("thankful");
            Add("victorious");
            Add("witty");
            Add("wonderful");
            Add("zealous");

            // Negative Personality
            Add("angry");
            Add("bewildered");
            Add("clumsy");
            Add("defeated");
            Add("embarrassed");
            Add("fierce");
            Add("grumpy");
            Add("helpless");
            Add("itchy");
            Add("jealous");
            Add("lazy");
            Add("mysterious");
            Add("nervous");
            Add("obnoxious");
            Add("panicky");
            Add("pitiful");
            Add("repulsive");
            Add("scary");
            Add("thoughtless");
            Add("uptight");
            Add("worried");

            // Size
            Add("big");
            Add("colossal");
            Add("fat");
            Add("gigantic");
            Add("great");
            Add("huge");
            Add("immense");
            Add("large");
            Add("little");
            Add("mammoth");
            Add("massive");
            Add("microscopic");
            Add("miniature");
            Add("petite");
            Add("puny");
            Add("scrawny");
            Add("short");
            Add("small");
            Add("tall");
            Add("teeny");
            Add("tiny");
        }

        public void Add(string adjective)
        {
            if (string.IsNullOrEmpty(adjective))
            {
                throw new ArgumentNullException(nameof(adjective));
            }

            _adjectiveMapping.Add(adjective, adjective);
        }

        public bool CheckAdjectiveExists(string adjective)
        {
            if (string.IsNullOrEmpty(adjective))
            {
                return false;
            }

            if (_adjectiveMapping.ContainsKey(adjective))
            {
                return true;
            }

            return false;
        }
    }
}
