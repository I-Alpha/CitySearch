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

        static CityFinder _cityfinder { get;  set; }
        //Before testing begins csv dataset of 3million+ records is loaded from FakeRepository



        [TestClass]
        public class QsheetTestCases
        {
            [ClassInitialize()]
            public static void checkQSData(TestContext context)
            {

                Assert.IsNotNull(_cityfinder);
                var _dataEx1 = FakeRepository.dataEx1;
                var _dataEx2 = FakeRepository.dataEx2;
                var _dataEx3 = FakeRepository.dataEx3;
                Assert.IsInstanceOfType(_dataEx1, new List<string>().GetType());
                Assert.IsInstanceOfType(_dataEx2, new List<string>().GetType());
                Assert.IsInstanceOfType(_dataEx3, new List<string>().GetType());
                CollectionAssert.AllItemsAreNotNull(_dataEx1);
                CollectionAssert.AllItemsAreNotNull(_dataEx2);
                CollectionAssert.AllItemsAreNotNull(_dataEx3);

            }


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




            //Test Methods using csv dataset end here
            [TestMethod()]
            [TestCategory("QSheetUseCases")]
            public void TestCaseEx1_cities()
            {

                var data = FakeRepository.dataEx1;
                CityFinder.Dataset = data;
                ICollection<string> expectedCities = new List<string>() { "BANGUI", "BANGKOK", "BANGALORE" };
                var results = _cityfinder.Search("BANG");
                var actualCities = results.NextCities;

                /*if (expectedLetters == actualLetters || expectedCities==actualCities){

                    Assert.IsFalse(false);
                };*/

                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());

            }


            [TestMethod()]
            [TestCategory("QSheetUseCases")]
            public void TestCaseEx1_letters()
            {

                var data = FakeRepository.dataEx1;
                CityFinder.Dataset = data;
                ICollection<string> expectedLetters = new List<string>() { "U", "K", "A" };
                var results = _cityfinder.Search("BANG");
                var actualLetters = results.NextLetters;

                /*if (expectedLetters == actualLetters || expectedCities==actualCities){

                    Assert.IsFalse(false);                };*/


                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());
            }

            [TestMethod()]
            [TestCategory("QSheetUseCases")]
            public void TestCaseEx2_cities()
            {

                var data = FakeRepository.dataEx2;
                CityFinder.Dataset = data;



                ICollection<string> expectedCities = new List<string>() { "LA PAZ", "LA PLATA", "LAGOS" };

                var results = _cityfinder.Search("LA");

                var actualCities = results.NextCities;
                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());
            }


            [TestMethod()]
            [TestCategory("QSheetUseCases")]
            public void TestCaseEx2_letters()
            {

                var data = FakeRepository.dataEx2;
                CityFinder.Dataset = data;


                ICollection<string> expectedLetters = new List<string>() { "G", " " };
                var results = _cityfinder.Search("LA");
                var actualLetters = results.NextLetters;
                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());


            }

            [TestMethod()]
            [TestCategory("QSheetUseCases")]
            public void TestCaseEx3_cities()
            {

                var data = FakeRepository.dataEx3;
                CityFinder.Dataset = data;


                ICollection<string> expectedCities = new List<string>() { };
                var results = _cityfinder.Search("ZE");

                var actualCities = results.NextCities;

                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());
            }


            [TestMethod()]
            [TestCategory("QSheetUseCases")]
            public void TestCaseEx3_letters()
            {

                var data = FakeRepository.dataEx3;
                CityFinder.Dataset = data;

                ICollection<string> expectedLetters = new List<string>() { };
                var results = _cityfinder.Search("ZE");
                var actualLetters = results.NextLetters;
                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());
            }

            [TestMethod()]
            [TestCategory("QSheetUseCases")]
            public void TestCaseConsecutiveInput()
            {

                var data = FakeRepository.dataEx1;
                CityFinder.Dataset = data;

                //cities
                ICollection<string> expectedCities = new List<string>() { "BANGUI", "BANGKOK", "BANGALORE" };
                var results = _cityfinder.Search("BANG");
                var actualCities = results.NextCities;
                //test cities
                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());

                //letters
                ICollection<string> expectedLetters = new List<string>() { "U", "K", "A" };
                var actualLetters = results.NextLetters;
                //test letters
                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());


                //new call
                results = _cityfinder.Search("BAN");
                actualCities = results.NextCities;
                actualLetters = results.NextLetters;
                expectedCities = new List<string>() { "BANGUI", "BANGKOK", "BANGALORE", "BADUNG" };
                expectedLetters = new List<string>() { "G", "G", "G", "U" };
                //test after new input
                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());
                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());


                //newcall spaced added
                results = _cityfinder.Search("BAN ");
                actualCities = results.NextCities;
                actualLetters = results.NextLetters;
                expectedCities = new List<string>() { };
                expectedLetters = new List<string>() { };
                //test after new input
                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());
                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());

            }
            [TestMethod()]
            [TestCategory("QSheetUseCases")]
            public void TestCaseAwkwardCharsInput()
            {

                var data = FakeRepository.dataEx1;
                CityFinder.Dataset = data;

                //cities
                ICollection<string> expectedCities = new List<string>() { "BANGUI", "BANGKOK", "BANGALORE" };
                var results = _cityfinder.Search("1");
                var actualCities = results.NextCities;
                //test cities
                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());

                //letters
                ICollection<string> expectedLetters = new List<string>() { "U", "K", "A" };
                var actualLetters = results.NextLetters;
                //test letters
                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());


                //newcall spaced added
                results = _cityfinder.Search(" ");
                actualCities = results.NextCities;
                actualLetters = results.NextLetters;
                expectedCities = new List<string>() { };
                expectedLetters = new List<string>() { };
                //test after new input
                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());
                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());

                results = _cityfinder.Search("/");
                actualCities = results.NextCities;
                actualLetters = results.NextLetters;
                expectedCities = new List<string>() { };
                expectedLetters = new List<string>() { };
                //test after new input
                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());
                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());

                results = _cityfinder.Search("*");
                actualCities = results.NextCities;
                actualLetters = results.NextLetters;
                expectedCities = new List<string>() { };
                expectedLetters = new List<string>() { };
                //test after new input
                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());
                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());

                results = _cityfinder.Search("");
                actualCities = results.NextCities;
                actualLetters = results.NextLetters;
                expectedCities = new List<string>() { };
                expectedLetters = new List<string>() { };
                //test after new input
                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());
                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());

                results = _cityfinder.Search("\\");
                actualCities = results.NextCities;
                actualLetters = results.NextLetters;
                expectedCities = new List<string>() { };
                expectedLetters = new List<string>() { };
                //test after new input
                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());
                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());

                results = _cityfinder.Search(@"\");
                actualCities = results.NextCities;
                actualLetters = results.NextLetters;
                expectedCities = new List<string>() { };
                expectedLetters = new List<string>() { };
                //test after new input
                Assert.AreEqual(expectedCities.ToString(), actualCities.ToString());
                Assert.AreEqual(expectedLetters.ToString(), actualLetters.ToString());



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
              CollectionAssert.AreNotEquivalent(new List<string>(),Csvdataset);
            }
             
            [ClassCleanup()]
            public static void CleanStaticVariables() {
                Csvdataset = null;
            }         
        

            [TestMethod]
            [TestCategory("CsvDataset")]
            public void TestWith3milQuery1()
            {

                CityFinder.Dataset = Csvdataset;
              

                var results = _cityfinder.Search(" ");
                ICollection<string> expectedVal = new List<string>() { };

                Assert.AreEqual(expectedVal.ToString(), results.NextCities.ToString());
                Assert.AreEqual(expectedVal.ToString(), results.NextLetters.ToString());


            }

            [TestMethod()]
            [TestCategory("CsvDataset")]
            public void TestWith3milEmptyQuery()
            {
                CityFinder.Dataset = Csvdataset;
               
                var results = _cityfinder.Search("");
                ICollection<string> expectedVal = new List<string>() { };
                Assert.AreEqual(expectedVal.ToString(), results.NextCities.ToString());
                Assert.AreEqual(expectedVal.ToString(), results.NextLetters.ToString());
            }
        }
    }
}