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

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private List<Person> availableTeamMembers = GlobalConfig.Connections.GetPerson_All();
        private List<Person> selectedTeamMembers = new List<Person>();
        public CreateTeamForm()
        {
            InitializeComponent();
            //CreateSampleData();
            WireUpLists();
        }
      

        private void WireUpLists()
        {
            selectTeamMemberDropDown.DataSource = null;
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";

           
        }
      
        private void headerLabel_Click(object sender, EventArgs e)
        {

        }

        private void headerLabel_Click_1(object sender, EventArgs e)
        {

        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void firstNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void teamOneScoreText_TextChanged(object sender, EventArgs e)
        {

        }

        private void CreateTeamForm_Load(object sender, EventArgs e)
        {

        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if(ValidateForm())
            {
                Person person = new Person();
                person.FirstName = firstNameValue.Text;
                person.LastName = lastNameValue.Text;
                person.EmailAddress = emailValue.Text;
                person.CellPhoneNumber = cellPhoneValue.Text;
                person= GlobalConfig.Connections.CreatePerson(person);
                selectedTeamMembers.Add(person);

                WireUpLists();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellPhoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("Error, You have to fill all fields");
            }
        }
        private bool ValidateForm()
        {
            if(firstNameValue.Text.Length==0)
            {
                return false;
            }
            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }
            if (emailValue.Text.Length == 0)
            {
                return false;
            }
            if (cellPhoneValue.Text.Length == 0)
            {
                return false;
            }

            //add validation to the form
            return true;
        }

        private void addMemberButton_Click(object sender, EventArgs e)
        {
            Person p = (Person)selectTeamMemberDropDown.SelectedItem;
            if (p != null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpLists();
            }
        }

        private void removeSelectedPlayer_Click(object sender, EventArgs e)
        {
            Person p = (Person)teamMembersListBox.SelectedItem;

            if (p!=null) { selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists();
            }
            

        }

        private void createTeamButton_Click(object sender, EventArgs e)
        {

        }
    }
}
