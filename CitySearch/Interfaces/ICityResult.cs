using System;
using System.Collections.Generic;
using System.Text;

namespace CitySearch.Interfaces
{
    public interface ICityResult
    {
        ICollection<string> NextLetters { get; set; }
        ICollection<string> NextCities { get; set; }
    }
}
