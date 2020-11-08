using Microsoft.VisualStudio.TestTools.UnitTesting;
using CitySearch.Interfaces;
using CitySearch.Brute;
using CitySearch.Trie;
using CitySearch;
using System.Collections.Generic;

namespace CS_MSUnitTests.Tests.IntefaceTest
{
    [TestClass]
    public class TestICityFinder
    {
        static ICityFinder cityFinder = null;

        [TestMethod()]
        public void testInterface()
        {
            cityFinder = new CityFinderBrute();
            Assert.IsNotNull(cityFinder);

            cityFinder = new CityFinderTrie();
            Assert.IsNotNull(cityFinder);
        }
    }

    [TestClass]
    public class TestICityResults
    {
        static ICityResult cityResult = null;

        [TestMethod()]
        public void testInterface()
        {
            cityResult = new CityResult();
            Assert.IsNotNull(cityResult);
            cityResult = new HashSetCityResult();
            Assert.IsNotNull(cityResult);

        }
        public class HashSetCityResult : ICityResult
        {
            public HashSetCityResult()
            {
                NextCities = new HashSet<string>();
                NextLetters = new HashSet<string>();
            }
            public ICollection<string> NextLetters { get; set; }
            public ICollection<string> NextCities { get; set; }
        }
    }
}