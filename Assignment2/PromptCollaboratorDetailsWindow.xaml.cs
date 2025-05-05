using System;
using System.Windows;

/*
    Brief: A Window that prompts from the user the details of
            collaborator
            
    Author: Terence Lee Xin Jin
    Date Created: 7 August 2021
 */
namespace Assignment2
{
    
    public partial class PromptCollaboratorDetailsWindow : Window
    {

        private Collaborator collaborator;


        public PromptCollaboratorDetailsWindow(string collaboratorID,
                                               GPSLocation location)
        {
            InitializeComponent();

            this.collaboratorIDTextBox.Text = collaboratorID;

            this.collaborator = new Collaborator();
            this.collaborator.CollaboratorID = collaboratorID;
            this.collaborator.GPSLocation = location;
        }



        /*
           Brief: A read-only property that specifies the operation
                 of obtaining the Collaborator detaills is
                successful

           Return:
                true if successful, false if otherwise
        */
        public bool IsSuccessful
        {
            private set;
            get;
        }



        /*
           Brief: A read-only property that contains the
                instance of the Collaborator if all the details
                of the collaborator is correctly entered by
                the user

           Return:
                true if successful, false if otherwise
        */
        public Collaborator Collaborator
        {
            private set;
            get;
        }





        /*
          Brief: Attempts to retrieve the details of the collaborator
                that the user has entered. 
       
  
         Postcondition:
            If all the details of valid, then the details of the 
                collaborator will be stored in the property
                'Collaborator', and the property 'IsSuccessful'
                will return true

            Otherwise, the property 'Collaborator' will return
                null, and the 'IsSuccessful' property will return
                false
       */
        private void ValidateFormDetails()
        {


            this.IsSuccessful = GetFirstNameEntered();

            if (!this.IsSuccessful)
            {
                return;
            }


            this.IsSuccessful = GetLastNameEntered();

            if (!this.IsSuccessful)
            {
                return;
            }


            this.IsSuccessful =  GetHoursAvailableEntered();

            if (!this.IsSuccessful)
            {
                return;
            }

            this.IsSuccessful = GetListOfSupportActivitiesSelected();


            if (this.IsSuccessful)
            {
                this.Collaborator = this.collaborator;
               
                this.Close();
            }
        }



        /*
          Brief: Attempts to set the first name of the collaborator

       
          Return:
            [1] bool -true if setting of first name of the collorator
                    succeeds, false if otherwise

          Post-condition:
            [1] if the setting of first name of the collaborator succeeds,
                the instance variable 'collaborator' will contain
                the first name of the collaborator

            [2] if the setting of the first name fails, a message box
                will display the error message
       */
        private bool GetFirstNameEntered()
        {

            string firstName = firstNameTextBox.Text.Trim();

            try
            {
                collaborator.FirstName = firstName;
                return true;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("You did not enter first name");
                return false;
            }

        }




        /*
          Brief: Attempts to set the last name of the collaborator

       
          Return:
            [1] bool -true if setting of last name of the collorator
                    succeeds, false if otherwise

          Post-condition:
            [1] if the setting of last name of the collaborator succeeds,
                the instance variable 'collaborator' will contain
                the last name of the collaborator

            [2] if the setting of the last name fails, a message box
                will display the error message
       */
        private bool GetLastNameEntered()
        {
            string lastName = lastNameTextBox.Text.Trim();

            try
            {
                collaborator.LastName = lastName;
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("You did not enter last name");
                return false;
            }
        }




        /*
          Brief: Attempts to set the number of hours available
                of the collaborator

       
          Return:
            [1] bool -true if setting of number of hours available
                    of the collorator succeeds, false if otherwise

          Post-condition:
            [1] if the setting of the number of hours available
                of the collaborator succeeds, the instance variable 'collaborator' will contain
                the number of hours available of the collaborator

            [2] if the setting of the number of hours available
                    fails, a message box will display the error 
                    message
       */
        private bool GetHoursAvailableEntered()
        {
            string hoursAvailableString = 
                    hoursAvailableTextBox.Text.Trim();

            int hoursAvailable;


            if (hoursAvailableString == "")
            {
                MessageBox.Show("You did not enter an integer for " +
                    "'No. of Hours Available Each Week'");

                return false;
            }



            try
            {
                hoursAvailable = Convert.ToInt32(hoursAvailableString);
                collaborator.HoursAvailableEachWeek = hoursAvailable;

                return true;
            }
            catch (Exception) 
            {
                //format exception, overflow exception, argumentOutOfRangeException
                MessageBox.Show("'Hours Available Each Week' must be an integer " +
                        "between 1 (inclusive) and 168 (inclusive) ");

                return false;
            }
          
        }



        /*
          Brief: Attempts to retrieve and store the list of
                support activities that the collaborator is able
                to provide

       
          Return:
            [1] bool -true the user has selected at least one
                    support activity, and false if otherwise

          Post-condition:
            [1] All the selected support activities will be
                    stored in the instance variable 'collaborator'

            [2] if the user did not select at least one support
                activity, a message box will display the error 
                    message
       */
        private bool GetListOfSupportActivitiesSelected()
        {
            // as IsChecked return a nullable bool, have to set the
            // boolean flag like this

            bool groceryShoppingIsSelected = (groceryShoppingCheckBox.IsChecked == true);

            bool grpActivityFacilitationIsSelected = (grpActivityFacilitationCheckBox.IsChecked == true);

            bool mealPreparationIsSelected = (mealPreparationCheckBox.IsChecked == true);

            bool medicationAssistanceIsSelected = (medicationAssistanceCheckBox.IsChecked == true);

            bool showeringAssistanceIsSelected = (showeringAssistanceCheckBox.IsChecked == true);

            bool transportationAssistanceIsSelected = (transportationCheckBox.IsChecked == true);



            if (groceryShoppingIsSelected)
            {
                collaborator.AddSupportActivity(Collaborator.SupportActivity.GroceryShopping);
            }


            if (grpActivityFacilitationIsSelected)
            {
                collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.GroupActivityFacilitation);
            }


            if (mealPreparationIsSelected)
            {
                collaborator.AddSupportActivity(Collaborator.SupportActivity.MealPreparation);
            }


            if (medicationAssistanceIsSelected)
            {
                collaborator.AddSupportActivity(Collaborator.SupportActivity.MedicationAssistance);
            }


            if (showeringAssistanceIsSelected)
            {
                collaborator.AddSupportActivity(Collaborator.SupportActivity.ShoweringAssistance);
            }


            if (transportationAssistanceIsSelected)
            {
                collaborator.AddSupportActivity(Collaborator.SupportActivity.Transportation);
            }




            if (collaborator.GetNumberOfSupportActivities() == 0)
            {
                MessageBox.Show("You must select at least one support activity");
                return false;
            }
            else
            {
                return true;
            }
        }


        /*
            Brief: An event handler that closes the window when the
                cancel button is clicked
         */
        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        /*
            Brief: When the add collaborator button is clicked , attempt to
                add the collaborator
         */
        private void addCollaboratorBtn_Click(object sender, RoutedEventArgs e)
        {
            ValidateFormDetails();
        }
    }
}
