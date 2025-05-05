
using System.Text;
using System.Windows;
using System.Linq;


/*
    File Name: DisplayCollaboratorDetailsWindow.cs
    Author: Terence Lee Xin Jin
    Date: 31 July 2021

    Purpose: Display all the details of an existing collaborator in
            a new window
 */

namespace Assignment2
{
    
    public partial class DisplayCollaboratorDetailsWindow : Window
    {
        private Collaborator collaborator;


        public DisplayCollaboratorDetailsWindow(Collaborator collaboratorVal)
        {
            InitializeComponent();

            this.collaborator = collaboratorVal;

            DisplayCollaboratorDetails();
        }


        /*
            Brief: Display the details of the collaborator
         */
        private void DisplayCollaboratorDetails()
        {
            collaboratorIDTextBox.Text = collaborator.CollaboratorID;

            firstNameTextBox.Text = collaborator.FirstName;

            lastNameTextBox.Text = collaborator.LastName;

            gpsLocationTextBox.Text = collaborator.GPSLocation.ToString();

            hoursAvailableTextBox.Text = collaborator.HoursAvailableEachWeek.ToString();

            DisplaySupportActivities();

        }


        /*
            Brief: Display the various support activities of the collaborator
                    in a textbox
         */
        private void DisplaySupportActivities()
        {
            StringBuilder stringBuilder = new StringBuilder("");

            string currentSupportActivityString;

            var listOfSupportActivties = collaborator.GetListOfAllSupportActivities();


            foreach(Collaborator.SupportActivity activity in listOfSupportActivties)
            {
                currentSupportActivityString = GetSupportActivityString(activity);

                stringBuilder.Append(currentSupportActivityString);


                if (activity != listOfSupportActivties.Last())
                {
                    stringBuilder.Append(", ");
                }
            }


            string stringToDisplay = stringBuilder.ToString();

            supportActivitiesTextBox.Text = stringToDisplay;

        }


        /*
            Brief:
                Returns a more readable string version of the
                    enum SupportActivity, where the string
                    contains spaces between words
         */
        private string GetSupportActivityString(Collaborator.SupportActivity activity)
        {
            string stringToReturn = null;


            switch (activity)
            {
                case Collaborator.SupportActivity.GroceryShopping:
                    stringToReturn = "Grocery Shopping";
                    break;


                case Collaborator.SupportActivity.GroupActivityFacilitation:
                    stringToReturn = "Group Activity Facilitation";
                    break;


                case Collaborator.SupportActivity.MealPreparation:
                    stringToReturn = "Meal Preparation";
                    break;


                case Collaborator.SupportActivity.MedicationAssistance:
                    stringToReturn = "Medication Assistance";
                    break;


                case Collaborator.SupportActivity.ShoweringAssistance:
                    stringToReturn = "Showering Assistance";
                    break;


                case Collaborator.SupportActivity.Transportation:
                    stringToReturn = "Transportation";
                    break;
            }

            return stringToReturn;
        }
    }
}
