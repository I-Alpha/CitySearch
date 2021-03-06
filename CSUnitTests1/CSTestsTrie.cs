﻿using CitySearch.Interfaces;
using CitySearch.SampleData;
using CitySearch.Trie;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CS_MSUnitTests.Tests.Trie
{

    [TestClass()]
    public class CSTestsTrie
    //These are basic unit tests.  Seven test methods.

    //methods TestWith3mil- loads performs search tests using sample data of 3million+ records

    //methods TestCase- are arranged as follows:
    //3 usecases (taken from question sheet) with two test methods(Nextletters and NextCities)
    //Datasets in fake repository:
    //TestCases-
    //Ex 1 : { "BANDUNG", "BANGUI", "BANGKOK", "BANGALORE" };
    //Ex 2 : { "LA PAZ", "LA PLATA", "LAGOS", "LEEDS" };
    //Ex 3 : { "ZARIA", "ZHUGHAI", "ZIBO" };
    {

        static ICityFinder _cityfinder { get; set; }
        static List<string> AltString = "}!@#&()–[{}]:;',/?*'`".Select(x => x.ToString()).ToList();


        [TestClass()]
        public class QsheetTestCases
        {

            static List<string> _dataEx1;// { "BANGUI", "BANGKOK", "BANGALORE" };
            static List<string> _dataEx2;// {"LA PAZ", "LA PLATA", "LAGOS"}
            static List<string> _dataEx3;  // { "ZARIA", "ZHUGHAI", "ZIBO" };


            [ClassInitialize()]
            public static void checkFakeRespostorySData(TestContext context)
            {
                _cityfinder = new CityFinderTrie();
                Assert.IsNotNull(_cityfinder);
                _dataEx1 = FakeRepository.dataEx1; // { "BANGUI", "BANGKOK", "BANGALORE" };
                _dataEx2 = FakeRepository.dataEx2; // {"LA PAZ", "LA PLATA", "LAGOS"}
                _dataEx3 = FakeRepository.dataEx3;  // { "ZARIA", "ZHUGHAI", "ZIBO" };
                Assert.IsInstanceOfType(_dataEx1, new List<string>().GetType());
                Assert.IsInstanceOfType(_dataEx2, new List<string>().GetType());
                Assert.IsInstanceOfType(_dataEx3, new List<string>().GetType());
                CollectionAssert.AllItemsAreNotNull(_dataEx1);
                CollectionAssert.AllItemsAreNotNull(_dataEx2);
                CollectionAssert.AllItemsAreNotNull(_dataEx3);
            }


            [TestInitialize()]
            public void createCobject()
            {
                _cityfinder = new CityFinderTrie();


            }

            [TestCleanup()]
            public void cleanStaticVariables()
            {
                _cityfinder = new CityFinderTrie();
            }



            [DataTestMethod]
            [DynamicData(nameof(GetLettersTestData_A), DynamicDataSourceType.Property)]
            public void TestCase1Letters(string _input, List<string> _expectedLetters)
            {
                CityFinderTrie.Dataset = _dataEx1;
                var results = _cityfinder.Search(_input);
                var actualLetters = results.NextLetters;
                Assert.AreEqual(_expectedLetters.ToString(), actualLetters.ToString());
            }




            [DataTestMethod]
            [DynamicData(nameof(GetCitiesTestData_A), DynamicDataSourceType.Property)]
            public void TestCase1Cities(string _input, List<string> _expectedCities)
            {
                CityFinderTrie.Dataset = _dataEx1;
                var results = _cityfinder.Search(_input);
                var actualCities = results.NextCities;
                Assert.AreEqual(_expectedCities.ToString(), actualCities.ToString());
            }

            public static IEnumerable<object[]> GetCitiesTestData_A
            {
                get
                {
                    yield return new object[] { "LA", new List<string>() { "LA PAZ", "LA PLATA", "LAGOS" } };
                    yield return new object[] { "LA ", new List<string>() { "LA PAZ", "LA PLATA" } };
                    yield return new object[] { "LA P", new List<string>() { "LA PAZ", "LA PLATA" } };
                    yield return new object[] { "LAG", new List<string>() { "LAGOS" } };
                    yield return new object[] { "LAGO", new List<string>() { "LAGOS" } };                   
                    yield return new object[] { " ", new List<string>() { } };
                }
            }

            public static IEnumerable<object[]> GetLettersTestData_A
            {
                get
                {
                    yield return new object[] { "LA", new List<string>() { "G", " ", " " } };
                    yield return new object[] { "LA ", new List<string>() { "P", "P", "G" } };
                    yield return new object[] { "LA P", new List<string>() { "A", "A" } };
                    yield return new object[] { "LAGO", new List<string>() { "S" } };
                    yield return new object[] { "", new List<string>() { } };
                    yield return new object[] { " ", new List<string>() { } };
                }
            }

            [DataTestMethod]
            [DynamicData(nameof(GetLettersTestData_B), DynamicDataSourceType.Property)]
            public void TestCase2Letters(string _input, List<string> _expectedLetters)
            {
                CityFinderTrie.Dataset = _dataEx2;
                var results = _cityfinder.Search(_input);
                var actualLetters = results.NextLetters;
                Assert.AreEqual(_expectedLetters.ToString(), actualLetters.ToString());
            }


            [DataTestMethod]
            [DynamicData(nameof(GetCitiesTestData_B), DynamicDataSourceType.Property)]
            public void TestCase2Cities(string _input, List<string> _expectedCities)
            {
                CityFinderTrie.Dataset = _dataEx2;
                var results = _cityfinder.Search(_input);
                var actualCities = results.NextCities;
                Assert.AreEqual(_expectedCities.ToString(), actualCities.ToString());
            }

            public static IEnumerable<object[]> GetCitiesTestData_B
            {
                get
                {
                    yield return new object[] { "BAN", new List<string>() { "BANGUI", "BANGKOK", "BANGALORE", "BADUNG" } };
                    yield return new object[] { "BAN ", new List<string>() { } };
                    yield return new object[] { "BANG", new List<string>() { "BANGUI", "BANGKOK", "BANGALORE" } };
                    yield return new object[] { "", new List<string>() { } };
                }
            }

            public static IEnumerable<object[]> GetLettersTestData_B
            {
                get
                {
                    yield return new object[] { "BAN", new List<string>() { "G", "G", "G", "U" } };
                    yield return new object[] { "BAN ", new List<string>() { } };
                    yield return new object[] { "BANG", new List<string>() { "U", "K", "A" } };
                    yield return new object[] { "BANGK", new List<string>() { "O" } };
                }
            }



            [DataTestMethod]
            [DynamicData(nameof(GetLettersTestData_C), DynamicDataSourceType.Property)]
            public void TestCase3Letters(string _input, List<string> _expectedLetters)
            {
                CityFinderTrie.Dataset = _dataEx3;
                var results = _cityfinder.Search(_input);
                var actualLetters = results.NextLetters;
                Assert.AreEqual(_expectedLetters.ToString(), actualLetters.ToString());
            }


            [DataTestMethod]
            [DynamicData(nameof(GetCitiesTestData_C), DynamicDataSourceType.Property)]
            public void TestCase3Cities(string _input, List<string> _expectedCities)
            {
                CityFinderTrie.Dataset = _dataEx3;
                var results = _cityfinder.Search(_input);
                var actualCities = results.NextCities;
                Assert.AreEqual(_expectedCities.ToString(), actualCities.ToString());
            }


            public static IEnumerable<object[]> GetCitiesTestData_C
            {
                get
                {// { "ZARIA", "ZHUGHAI", "ZIBO" };
                    yield return new object[] { "Z", new List<string>() { "ZARIA", "ZHUGHAI", "ZIBO" } };
                    yield return new object[] { "ZA", new List<string>() { "ZARIA" } };
                    yield return new object[] { "Z", new List<string>() { "ZARIA", "ZHUGHAI", "ZIBO" } };
                    yield return new object[] { "ZE", new List<string>() { } };
                    yield return new object[] { " Z", new List<string>() { } };
                }
            }

            public static IEnumerable<object[]> GetLettersTestData_C
            {
                get
                {
                    yield return new object[] { "Z", new List<string>() { "A", "H", "I" } };
                    yield return new object[] { "ZA", new List<string>() { "R" } };
                    yield return new object[] { "Z", new List<string>() { "A", "H", "I" } };
                    yield return new object[] { "ZE", new List<string>() { } };
                    yield return new object[] { " Z", new List<string>() { } };
                }
            }

            [TestMethod]
            public void AltKeyTest_Letters()
            {

                foreach (var chunk in new[] { _dataEx1, _dataEx2, _dataEx3 })
                {

                    CityFinderTrie.Dataset = chunk;

                    foreach (var item in AltString)
                    {

                        var results = _cityfinder.Search(item);
                        Assert.AreEqual(new List<string>() { }.ToString(), results.NextLetters.ToString());
                    }

                }
            }

            [TestMethod]
            public void AltKeyTest_Cities()
            {

                foreach (var chunk in new[] { _dataEx1, _dataEx2, _dataEx3 })
                {

                    CityFinderTrie.Dataset = chunk;
                    foreach (var item in AltString)
                    {
                        var results = _cityfinder.Search(item);
                        Assert.AreEqual(new List<string>() { }.ToString(), results.NextCities.ToString());
                    }
                }
            }


        }
        //Test Methods using csv dataset begin here

        [TestClass]
        public class LargeDatasetTests
        {
            //load data from worldcitiespop.csv
            static List<string> Csvdataset = FakeRepository.ParseCsvData;

            [TestInitialize()]
            public void createCFobject()
            {
                _cityfinder = new CityFinderTrie();

            }

            [TestCleanup()]
            public void cleanStaticVariables()
            {
                _cityfinder = new CityFinderTrie();
            }

            [ClassInitialize]
            public static void LoadDataset(TestContext context)
            {
                Assert.IsNotNull(Csvdataset);
                CollectionAssert.AllItemsAreNotNull(Csvdataset);
                CollectionAssert.AreNotEquivalent(new List<string>(), Csvdataset);
                CityFinderTrie.Dataset = FakeRepository.ParseCsvData;
            }

            [ClassCleanup()]
            public static void CleanStaticVariables()
            {
                Csvdataset = null;
                CityFinderTrie.Dataset = null;
            }

            [TestMethod]
            public void AltKeyTest_Letters()
            {

                foreach (var item in AltString)
                {
                    var results = _cityfinder.Search(item);
                    Assert.AreEqual(new List<string>() { }.ToString(), results.NextLetters.ToString());
                }
            }

            [TestMethod]
            public void AltKeyTest_Cities()
            {

                foreach (var item in AltString)
                {
                    var results = _cityfinder.Search(item);
                    Assert.AreEqual(new List<string>() { }.ToString(), results.NextCities.ToString());
                }
            }
        }


    }
}


