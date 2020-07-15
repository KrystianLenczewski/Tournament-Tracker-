using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModel.csv";

        //Dapper
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //Load the text file
            //Convert the text to List<PrizeModel>
            //Find the max ID
            //Add the new record with the new ID(max+1)
            //Convert the prizes to List<string>
            //Save the List<string> to the text file 
            List<PrizeModel>prizes= PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            int currentId = 1;
            if(prizes.Count>0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1; 
            }

            model.Id = currentId;

            prizes.Add(model);
            prizes.SaveToPrizeFile(PrizesFile);

            return model;


        }

      
    }
}
