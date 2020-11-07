
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;

namespace CitySearch.SampleData
{
    public class FakeRepository
    {
        private const string csvpath = @"..\..\..\SampleData\worldcitiespop.csv";
        public static List<string> dataEx1 = new List<string>() { "BANDUNG", "BANGUI", "BANGKOK", "BANGALORE" };
        public static List<string> dataEx2 = new List<string>() { "LA PAZ", "LA PLATA", "LAGOS", "LEEDS" };
        public static List<string> dataEx3 = new List<string>() { "ZARIA", "ZHUGHAI", "ZIBO" };
        public static List<string> ParseCsvData
        {
            get
            {
                //create new ICollection
                List<string> listOfCities = new List<string>();
                using (TextFieldParser csvParser = new TextFieldParser(csvpath))
                {
                    csvParser.CommentTokens = new string[] { "#" };
                    csvParser.SetDelimiters(new string[] { "," });
                    csvParser.HasFieldsEnclosedInQuotes = true;


                    //skip header row
                    csvParser.ReadFields();

                    while (!csvParser.EndOfData)
                    {
                        // Read current line fields, pointer moves to the next line.
                        string[] fields = csvParser.ReadFields();

                        //add each item in country field to list
                        listOfCities.Add(fields[0].ToUpper());
                    }
                }

                return listOfCities;

            }
        }
        public static string Csvpath => csvpath;
    }
}
