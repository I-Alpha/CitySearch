using Microsoft.VisualStudio.TestTools.UnitTesting;
using CitySearch.SampleData;
using System.Collections.Generic;
using CitySearch;

namespace CS_MSUnitTests.Tests
{

    [TestClass()]
    public class CSTests
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

        static CityFinder _cityfinder { get; set; }
        //Before testing begins csv dataset of 3million+ records is loaded from FakeRepository



        [TestClass]
        public class QsheetTestCases
        {

            static List<string> _dataEx1;// { "BANGUI", "BANGKOK", "BANGALORE" };
            static List<string> _dataEx2;// {"LA PAZ", "LA PLATA", "LAGOS"}
            static List<string> _dataEx3;  // { "ZARIA", "ZHUGHAI", "ZIBO" };

            [ClassInitialize()]
            public static void checkFakeRespostorySData(TestContext context)
            {
                _cityfinder = new CityFinder();
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
                _cityfinder = new CityFinder();


            }

            [TestCleanup()]
            public void cleanStaticVariables()
            {
                _cityfinder = new CityFinder();
            }



            [DataTestMethod]
            [DynamicData(nameof(GetLettersTestData_A), DynamicDataSourceType.Property)]
            public void TestCase1Letters(string _input, List<string> _expectedLetters)
            {
                CityFinder.Dataset = _dataEx1;
                var results = _cityfinder.Search(_input);
                var actualLetters = results.NextLetters;
                Assert.AreEqual(_expectedLetters.ToString(), actualLetters.ToString());
            }


            [DataTestMethod]
            [DynamicData(nameof(GetCitiesTestData_A), DynamicDataSourceType.Property)]
            public void TestCase1Cities(string _input, List<string> _expectedCities)
            {
                CityFinder.Dataset = _dataEx1;
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
                    yield return new object[] { "", new List<string>() { } };
                    yield return new object[] { " ", new List<string>() { } };
                }
            }






            [DataTestMethod]
            [TestCategory("QSheetUseCases")]
            [DynamicData(nameof(GetLettersTestData_B), DynamicDataSourceType.Property)]
            public void TestCase2Letters(string _input, List<string> _expectedLetters)
            {
                CityFinder.Dataset = _dataEx2;
                var results = _cityfinder.Search(_input);
                var actualLetters = results.NextLetters;
                Assert.AreEqual(_expectedLetters.ToString(), actualLetters.ToString());
            }


            [DataTestMethod]
            [DynamicData(nameof(GetCitiesTestData_B), DynamicDataSourceType.Property)]
            public void TestCase2Cities(string _input, List<string> _expectedCities)
            {
                CityFinder.Dataset = _dataEx2;
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
                CityFinder.Dataset = _dataEx3;
                var results = _cityfinder.Search(_input);
                var actualLetters = results.NextLetters;
                Assert.AreEqual(_expectedLetters.ToString(), actualLetters.ToString());
            }


            [DataTestMethod]
            [DynamicData(nameof(GetCitiesTestData_C), DynamicDataSourceType.Property)]
            public void TestCase3Cities(string _input, List<string> _expectedCities)
            {
                CityFinder.Dataset = _dataEx3;
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

            [DataTestMethod]
            [DynamicData(nameof(GetAltTestData), DynamicDataSourceType.Property)]
            public void AltKeyTest_Letters(string _input, List<string> _expected_Letters)
            {
                var results = _cityfinder.Search(_input);
                Assert.AreEqual(_expected_Letters.ToString(), results.NextLetters.ToString());
            }

            [DataTestMethod]
            [DynamicData(nameof(GetAltTestData), DynamicDataSourceType.Property)]
            public void AltKeyTest_Cities(string _input, List<string> _expectedCities)
            {
                var results = _cityfinder.Search(_input);
                Assert.AreEqual(_expectedCities.ToString(), results.NextCities.ToString());
            }

            public static IEnumerable<object[]> GetAltTestData
            {
                get
                {
                    yield return new object[] { "/", new List<string>() { } };
                    yield return new object[] { "?", new List<string>() { } };
                    yield return new object[] { "#", new List<string>() { } };
                    yield return new object[] { ";", new List<string>() { } };
                    yield return new object[] { " ", new List<string>() { } };
                    yield return new object[] { "-", new List<string>() { } };
                    yield return new object[] { "+", new List<string>() { } };
                    yield return new object[] { "1", new List<string>() { } };
                    yield return new object[] { "!", new List<string>() { } };
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
                _cityfinder = new CityFinder();

            }

            [TestCleanup()]
            public void cleanStaticVariables()
            {
                _cityfinder = new CityFinder();
            }

            [ClassInitialize]
            public static void LoadDataset(TestContext context)
            {
                Assert.IsNotNull(Csvdataset);
                CollectionAssert.AllItemsAreNotNull(Csvdataset);
                CollectionAssert.AreNotEquivalent(new List<string>(), Csvdataset);
                CityFinder.Dataset = FakeRepository.ParseCsvData;
            }

            [ClassCleanup()]
            public static void CleanStaticVariables()
            {
                Csvdataset = null;
                CityFinder.Dataset = null;
            }


            [DataTestMethod]
            [DynamicData(nameof(GetAltTestData), DynamicDataSourceType.Property)]
            public void AltKeyTest_Letters(string _input, List<string> _expected_Letters)
            {
                var results = _cityfinder.Search(_input);
                Assert.AreEqual(_expected_Letters.ToString(), results.NextLetters.ToString());
            }

            [DataTestMethod]
            [DynamicData(nameof(GetAltTestData), DynamicDataSourceType.Property)]
            public void AltKeyTest_Cities(string _input, List<string> _expectedCities)
            {
                var results = _cityfinder.Search(_input);
                Assert.AreEqual(_expectedCities.ToString(), results.NextCities.ToString());
            }

            public static IEnumerable<object[]> GetAltTestData
            {
                get
                {
                    yield return new object[] { "/", new List<string>() { } };
                    yield return new object[] { "?", new List<string>() { } };
                    yield return new object[] { "#", new List<string>() { } };
                    yield return new object[] { ";", new List<string>() { } };
                    yield return new object[] { " ", new List<string>() { } };
                    yield return new object[] { "-", new List<string>() { } };
                    yield return new object[] { "+", new List<string>() { } };
                    yield return new object[] { "1", new List<string>() { } };
                    yield return new object[] { "!", new List<string>() { } };
                }
            }


        }
    }
}


