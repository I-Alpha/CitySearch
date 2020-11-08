using CitySearch.Interfaces;
using CitySearch.SampleData;
using CitySearch.Trie;
using CitySearch.Brute;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CitySearch
{

    public class ManualTest
    {
        //This is manual input test code that uses the large sample database 
        ICityFinder  _cityfinder ;
        static List<string> Csvdataset = FakeRepository.ParseCsvData;
        //Before testing begins csv dataset of 3million+ records is loaded from FakeRepository


        public ManualTest()
        {  
            
            CityFinderBrute.Dataset = Csvdataset;
            _cityfinder = new CityFinderBrute();
            ManualTestMethod();
        }


        public void ManualTestMethod()
        {
            
            string searchInput = null;
            Console.WriteLine("\n\n---Data Loaded!---\n\n");

            Console.WriteLine("\n\nPlease Begin typing Search String : ");
            //print after every key input 

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey();
                //Aggregate key input
                //backspace keypressed
                if (keyinfo.Key.ToString() == "Backspace")
                    searchInput = searchInput.Remove(searchInput.Length - 1);
                else
                    searchInput += keyinfo.Key.ToString();

                Console.WriteLine("\n\n       | " + keyinfo.Key + " | was pressed");
                Console.WriteLine("\n Currinput : " + searchInput + "\n Showing First 10 results:");

                var results = _cityfinder.Search(searchInput);
                Console.WriteLine("\n\ncities:");
                int i = 1;
                foreach (var city in results.NextCities)
                {
                    if (i > 10)
                        break;

                    Console.WriteLine(city);
                    //   break; // get first city
                    i++;
                }
                var item = results.NextLetters;
                Console.WriteLine("\n\n Next Letters: [{0}]", string.Join(" ", item.ToArray()));

            }
            while (keyinfo.Key != ConsoleKey.X);
        }
    }
}
