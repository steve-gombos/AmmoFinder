using System.Collections.Generic;

namespace AmmoFinder.Parsers.Models
{
    public class SearchCriteria
    {
        public string Name { get; set; }
        public List<string> SearchIndicators { get; set; }
    }
}
