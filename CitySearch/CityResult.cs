using CitySearch.Interfaces;
using System.Collections.Generic;

namespace CitySearch
{
    public class CityResult : ICityResult
    {
        public CityResult()
        {
            NextCities = new List<string>();
            NextLetters = new List<string>();        
        }

 

        public ICollection<string> NextLetters { get; set; }
        public ICollection<string> NextCities { get; set; }
    }
}
