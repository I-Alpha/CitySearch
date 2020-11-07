using CitySearch.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CitySearch.Brute
{

    public class CityFinder : ICityFinder
    {

        private static ICollection<string> dataset = new List<string>();

        public static ICollection<string> Dataset
        {
            get { return dataset; }
            //converts items in collection to uppercase and store
            set { dataset = value.Select(v => v.ToUpper()).ToList(); }
        }


        public ICityResult Search(string searchString = null)
        {
            //create  Cityresul  object
            CityResult cityresults = new CityResult();
            if (searchString != null && searchString != "" && searchString != " ")
            {
                //convert curr search string to UpperCase
                searchString = searchString.ToUpper();
                var searchResults = dataset.Where(c => c.StartsWith(searchString)).OrderByDescending(r => r).ToList();

                if (searchResults != null)
                {
                    //get matched items

                    cityresults.NextCities = searchResults;

                    if (searchResults.Count > 1)
                    {
                        //get subsequent char of each matched superstring
                        cityresults.NextLetters = searchResults.Select(x => x[searchString.Length].ToString()).ToList(); // get first letter after length of substring of every char;
                    }
                }
            }
            return cityresults;
        }
    }
}