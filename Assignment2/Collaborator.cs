using System;
using System.Collections.Generic;
using System.Collections.Immutable;


/*
    Brief: A class that stores details of the Collaborator
            
    Author: Terence Lee Xin Jin
    Date Created: 7 August 2021
 */
namespace Assignment2
{
    public class Collaborator: ISingleLocationInvolved
    {
        private string collaboratorID = "-1";
        private string firstName = "<Not Yet Set>";
        private string lastName = "<Not Yet Set>";

        private IGPSLocation gpsLocation;

        private int hoursAvailableEachWeek;
        private HashSet<SupportActivity> listOfSupportActivities
                            = new HashSet<SupportActivity>();



        public enum SupportActivity
        {
            GroceryShopping, GroupActivityFacilitation, MealPreparation, 
            MedicationAssistance, ShoweringAssistance, Transportation
        }


        /*
            Brief: Creates an instance of Collaborator
         */
        public Collaborator()
        { 
        
        }



        /*
            Brief: Creates an instance of Collaborator

            Arguments:
                [1] collaboratorIDVal - the collaborator id of the
                                    collaborator
                [2] firstNameVal - first name of the collaborator
                [3] lastNameVal - last name of the collaborator
         */
        public Collaborator(string collaboratorIDVal, string firstNameVal,
                            string lastNameVal)
        {
            this.CollaboratorID = collaboratorIDVal;
            this.FirstName = firstNameVal;
            this.LastName = lastNameVal;
        }



        /*
            Brief: Sets and gets the collaborator id of the collaborator

            Exception:
                [1] Throws a ArgumentNullException if a null value is used
                to set the collaborator id
                
                [2] Throws a ArgumentException if a empty/whitespace string 
                    is used to set the collaborator id

            Return:
                if this property has not been, returns the default
                value of the string "-1"
         */
        public string CollaboratorID
        {
            set
            {
                this.collaboratorID = ValidateAndTrimString(value,
                                                "CollaboratorID");
            }

            get { return this.collaboratorID; }
        }




        /*
            Brief: Sets and gets the first name of the collaborator

            Exception:
                [1] Throws a ArgumentNullException if a null value is used
                to set the first name
                
                [2] Throws a ArgumentException if a empty/whitespace string 
                    is used to set the first name

            Return:
                If this property has not been set, returns the default
                value of "<Not Yet Set>"
         */
        public string FirstName
        {
            set
            {
                this.firstName = ValidateAndTrimString(value, "FirstName");
            }

            get { return this.firstName; }
        }




        /*
            Brief: Sets and gets the last name of the collaborator

            Exception:
                [1] Throws a ArgumentNullException if a null value is used
                to set the last name
                
                [2] Throws a ArgumentException if a empty/whitespace string 
                    is used to set the last name

            Return:
                If this property has not been set, returns the default
                value of "<Not Yet Set>"
         */
        public string LastName
        {
            set 
            {
                this.lastName = ValidateAndTrimString(value, "LastName");
            }

            get { return this.lastName; }
        }



        /*
            Brief: This a a helper method to do a simple   
                string validation, and also trim the string

            Argument:
                [1]stringVal - the string to be validated and
                        trimmed
                [2] propertyName - the property name of the
                        string 'stringVal'

            Exception:
                [1] ArgumentNullException - if the argument
                        'stringVal' is null
                [2] ArgumentException - if the argument 'stringVal'
                        is an empty/ whitespace-only string

            Return:
                A trimmed version of the string 'stringVal'
         */
        private string ValidateAndTrimString(string stringVal, 
                                             string propertyName)
        {
            if (stringVal == null)
            {
                throw new ArgumentNullException(
                    propertyName + " cannot be null");
            }


            stringVal = stringVal.Trim();


            if (stringVal == "")
            {
                throw new ArgumentException(
                    propertyName + " cannot be an empty string");
            }


            return stringVal;
        }




        /*
            Brief: Sets and gets the GPS location of the collaborator

            Throws ArgumentNullException if a null value is used to
                set this property
         */
        public IGPSLocation GPSLocation
        {
            set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException("GPSLocation cannot be null");

                }

                this.gpsLocation = value;
            }


            get { return this.gpsLocation; }
        }





        /*
            Brief: Sets and gets the number of hours the collaborator
                    is available each week

            Note: There are only 168 hours in a week, so number of
                hours cannot exceed 168

            Throws ArgumentOutOfRangeException if a value less than 1
                or a value greater than 168 is used to set the value of
                this property
         */
        public int HoursAvailableEachWeek
        {
            set
            {
                if (value <= 0 || value > 168)
                {
                    throw new ArgumentOutOfRangeException("Argument has " +
                        "to be between 0 (exclusive) and 168 (inclusive");
                }
                else
                {
                    this.hoursAvailableEachWeek = value;
                }
            }


            get { return this.hoursAvailableEachWeek; }
        }



        /*
            Brief: Adds a support activity that this collaborator is
                    providiing

            Argument:
                [1] activity - the support activity that this collaborator
                        is providing, but has not been added to this
                        collaborator instance yet

            Return:
                bool - true if successfully added, false if the
                    activity has already been added
         */
        public bool AddSupportActivity(SupportActivity activity)
        {
            return this.listOfSupportActivities.Add(activity);
        }




        /*
            Brief: Removes a support activity that the collaborator is
                    providing

            Argument:
                [1] activity - the support activity that this collaborator
                        is currently providing, that is to be removed

            Return:
                bool - true if the support activity is successfully
                    removed, false if the argument 'activity' is
                    not a support activity that the collaborator is
                    currently providing
         */
        public bool RemoveSupportActivity(SupportActivity activity)
        {
            return this.listOfSupportActivities.Remove(activity);
        }



        /*
            Brief: Removes all the support activities that this 
                collaborator is providing

            Post-condition:
                If the operation is successful, the list of support
                activities that this collaborator is providing will
                be empty
         */
        public void RemoveAllSupportActivities()
        {
            this.listOfSupportActivities.Clear();
        }



        /*
            Brief: Checks if the collaborator is providing the
                particular support activity

            Argument:
                activity - the support activity to be checked

            Return:
                bool - false if argument 'activity' is not part of the
                    collaborator's list of provided support activities.
                    Otherwise, return true.
         */
        public bool IsProvidingSupportActivity(SupportActivity activity)
        {
            return this.listOfSupportActivities.Contains(activity);
        }




        /*
            Brief: Returns the number of support activities that
                this collaborator is providing
         */
        public int GetNumberOfSupportActivities()
        {
            return this.listOfSupportActivities.Count;
        }




        /*
            Brief: Returns a list of support activities that are 
                this collaborator is providing
         */
        public ImmutableList<SupportActivity> 
                                    GetListOfAllSupportActivities()
        {
            return this.listOfSupportActivities.ToImmutableList();
        }



        
    }
}
