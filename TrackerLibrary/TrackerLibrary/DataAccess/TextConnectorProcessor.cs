using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName)//extension method 
        {
            //C:\data\TournamentTracker\PrizeModels.csv
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
        }
        public static List<string>LoadFile(this string file)
        {
            if(!File.Exists(file))//not exist
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }
        public static List<PrizeModel>ConvertToPrizeModels(this List<string>lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                PrizeModel p = new PrizeModel();
                p.Id =int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);
                output.Add(p);
            }
            return output;

        }
        public static List<Person> ConvertToPerson(this List<string>lines)
        {
            List<Person> output = new List<Person>();
            foreach (string item in lines)
            {
                string[] cols = item.Split(',');

                Person p = new Person();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.CellPhoneNumber = cols[4];

                output.Add(p);


            }
            return output;

        }

        public static void SaveToPeopleFile(this List<Person>models,string fileName)
        {
            List<string> lines = new List<string>();

            foreach (Person item in models)
            {
                lines.Add($"{item.Id},{item.FirstName},{item.LastName},{item.EmailAddress},{item.CellPhoneNumber}");

            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToPrizeFile(this List<PrizeModel>models,string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel item in models)
            {
                lines.Add($"{item.Id},{item.PlaceNumber},{item.PlaceName},{item.PrizeAmount},{item.PrizePercentage}");
                
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
    }
}
