using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTournamentForm : Form,IPriceRequester, ITeamRequester
    {
        List<TeamModel> availableTeams = GlobalConfig.Connections.GetTeam_ALL();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();


        public CreateTournamentForm()
        {
            InitializeComponent();
            WireUpLists();
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
           
          
            TeamModel teamModel = (TeamModel)tournamentTeamsListBox.SelectedItem;
            if(teamModel!=null)
            {
                selectedTeams.Remove(teamModel);
                availableTeams.Add(teamModel);
                WireUpLists();
            }
        }
        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";


        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;
            if (t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpLists();
            }
        }

        private void createPriceButton_Click(object sender, EventArgs e)
        {
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();


        }

        public void PrizeComplete(PrizeModel model)
        {
            selectedPrizes.Add(model);
            WireUpLists();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm createTeamForm = new CreateTeamForm(this);
            createTeamForm.Show();

        }

        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel prizeModel = (PrizeModel)prizesListBox.SelectedItem;

            if(prizeModel!=null)
            {
                selectedPrizes.Remove(prizeModel);
                WireUpLists();

            }    
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);

            if(!feeAcceptable)
            {
                MessageBox.Show("Error, You have to enter valid Entry Fee","Invalid Fee",
                    MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            //create tournament model
            TournamentModel tournamentModel = new TournamentModel();
            tournamentModel.TournamentName = tournamentNameValue.Text;
            tournamentModel.EntryFee = fee;

            tournamentModel.Prizes = selectedPrizes;
            tournamentModel.EnteredTeams = selectedTeams;

            //TODO-Wire our matchups
            //Order our list randomly of teams
            //Check if it is big enough - if not, add in byes
            //2*2*2*2=2^4 
            //Create our first round of matchups 
            //Create every round after that -8 matchups -4matchups -2matchups -1matchup

            TournamentLogic.CreateRounds(tournamentModel);

            GlobalConfig.Connections.CreateTournament(tournamentModel);



            //Create tournament entry 

        }
    }
}
