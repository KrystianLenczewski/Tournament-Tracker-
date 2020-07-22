﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        //Order our list randomly of teams
        //Check if it is big enough - if not, add in byes
        //2*2*2*2=2^4 
        //Create our first round of matchups 
        //Create every round after that -8 matchups -4matchups -2matchups -1matchup
        public static void CreateRounds(TournamentModel model)
        {
            List <TeamModel> randomizedTeams = RandomizeTeamModel(model.EnteredTeams);
            int rounds = FindNumberOfRounds(randomizedTeams.Count);
            int byes = NumberOfByes(rounds, randomizedTeams.Count);

            model.Rounds.Add(CreateFirstRound(byes, randomizedTeams));
            CreateOtherRounds(model, rounds);
        }

        public static void CreateOtherRounds(TournamentModel model,int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currentRound = new List<MatchupModel>();
            MatchupModel currentMatchup = new MatchupModel();


            while(round<=rounds)
            {
                foreach (MatchupModel match in previousRound)
                {
                    currentMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = match });

                    if(currentMatchup.Entries.Count>1)
                    {
                        currentMatchup.MatchupRound = round;
                        currentRound.Add(currentMatchup);
                        currentMatchup = new MatchupModel();
                    }
                }

                model.Rounds.Add(currentRound);
                previousRound = currentRound;

                round += 1;
                currentRound = new List<MatchupModel>();



            }
        }
        private static List<MatchupModel>CreateFirstRound(int byes,List<TeamModel>teams)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel current = new MatchupModel();

            foreach (TeamModel team in teams)
            {
                current.Entries.Add(new MatchupEntryModel { TeamCompeting = team });

                if(byes>0 || current.Entries.Count>1)
                {
                    current.MatchupRound = 1;
                    output.Add(current);
                    current = new MatchupModel();

                    if(byes>0)
                    {
                        byes -= 1;
                    }
                }
            }
            return output;
        }

        private static int NumberOfByes(int rounds,int numberOfTeams)
        {
            int output = 0;
            int totalTeams = 1;

            for (int i = 1; i <= rounds; i++)
            {
                totalTeams *= 2;
            }


            output = totalTeams - numberOfTeams;
            return output;

        }

        private static int FindNumberOfRounds(int teamCount)
        {
            int output = 1;
            int val = 2;

            while(val<teamCount)
            {
                output += 1;
                val *= 2;
            }

            return output;

        }
        private static List<TeamModel>RandomizeTeamModel(List<TeamModel>teams)
        {
            return teams.OrderBy(a => Guid.NewGuid()).ToList();
        }



    }
}
