using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;


/*
    Brief: A utility adapter class that converts XElement instance 
            into a Collaborator instance
            
    Author: Terence Lee Xin Jin
    Date Created: 7 August 2021
 */
namespace Assignment2
{
    public class XElementToCollaboratorAdapter
    {
        private XElement xElementObject;
        private XNamespace xmlNamespace;



        /*
           Brief: Creates an instance of XElementToCollaboratorAdapter

           Argument:
                [1] xmlNamespace - used to set the XElementObject property
                [2] obj - used to set the property XElementObject

           Exceptions:
                [1] Throws ArgumentNullException if the argument 'xElementObj'
                        is null
        */
        public XElementToCollaboratorAdapter(XNamespace xmlNamespace, XElement xElementObj)
        {
            this.xmlNamespace = xmlNamespace;

            if (xElementObj == null)
            {
                throw new ArgumentNullException("xElementObj cannot be null");
            }

            xElementObject = xElementObj;
        }




        /*
           Brief: Create a Collaborator instance, and populates
                the Collaborator instance with data based on the 
                'xElementObj' argument set in the constructor


            Return:
                if the XElementObject property contains valid data, then
                returns a Collaborator instance

                if the XElementObject property contains invalid data, then
                this method returns null

        */
        public Collaborator ConvertXElementToCollaborator()
        {

            Collaborator collaborator = new Collaborator();


            try
            {
                collaborator.CollaboratorID = RetrieveCollaboratorID();
                collaborator.FirstName = RetrieveFirstName();
                collaborator.LastName = RetrieveLastName();
                collaborator.GPSLocation = RetrieveGPSLocation();
                collaborator.HoursAvailableEachWeek = RetrieveHoursAvailableEachWeek();
                RetrieveListOfSupportActivities(collaborator);

            }
            catch (Exception)
            {
                collaborator = null;
            }


            return collaborator;
        }





        /*
          Brief: Returns the collaborator id from the instance
                variable xElementObject

          Assumption:
                [1] the instance variable xElementObject must 
                    contain an element called "collaborator-id"

          Return:
            a string that contains the collaborator id of the
                collaborator

          Exception:
            throws a InvalidOperationException if the instance 
                variable xElementObject' does not contain a 
                'collaborator-id' element
       */
        private string RetrieveCollaboratorID()
        {
            return xElementObject.Descendants(this.xmlNamespace
                    + "collaborator-id").First().Value;
        }



        /*
          Brief: Returns the first name from the instance
                variable xElementObject

          Assumption:
                [1] the instance variable xElementObject must 
                    contain an element called "first-name"

          Return:
            a string that contains the first name of the
                collaborator

          Exception:
            throws a InvalidOperationException if the instance
                variable xElementObject' does not contain a 
                'first-name' element
       */
        private string RetrieveFirstName()
        {

            return xElementObject.Descendants(this.xmlNamespace
                    + "first-name").First().Value;
        }



        /*
          Brief: Returns the last name from the instance
                variable xElementObject

          Assumption:
                [1] the instance variable xElementObject must 
                    contain an element called "last-name"

          Return:
            a string that contains the last name of the
                collaborator

          Exception:
            throws a InvalidOperationException if the instance
                variable xElementObject' does not contain a 
                'last-name' element
       */
        private string RetrieveLastName()
        {

            return xElementObject.Descendants(this.xmlNamespace
                    + "last-name").First().Value;
        }





        /*
          Brief: Returns the number of hours available each week from
                the instance variable xElementObject

          Assumption:
                [1] the instance variable xElementObject must contain 
                    an element called "hours-available-each-week"

          Return:
            an integer that represents the number of hours that the
                collaborator is available each week

          Exception:
            throws a InvalidOperationException if the instance variable 
                xElementObject' does not contain a 
                'hours-available-each-week' element
       */
        private int RetrieveHoursAvailableEachWeek()
        {
            string hoursAvailableInString =  xElementObject.Descendants(this.xmlNamespace
                            + "hours-available-each-week").First().Value;


            return Convert.ToInt32(hoursAvailableInString);
        }




        /*
          Brief: Returns the location of the collaborator from the 
                instance variable xElementObject

        
          Assumption:
                [1] the instance variable xElementObject must contain 
                        two elements called "lat" and "long"

          Return:
                a GPSLocation instance containing the location (latitude and
                longitude) of the collaborator

          Exception:
                [1] throws a InvalidOperationException if propery 'XElementObject'
                    does not contain the "lat' and "long" elements

                [2] throws a FormatException if the "lat" and "long" elements
                    contain values that are not a number in valid form

                [3] throws a OverflowException if the "lat" and "long" elements
                    contain values that represent a number that is less than
                    or greater than the acceptable range of a 'double' data type
       */
        private GPSLocation RetrieveGPSLocation()
        {

            string latitudeString = xElementObject.Descendants(this.xmlNamespace
                    + "lat").First().Value;

            string longitude = xElementObject.Descendants(this.xmlNamespace
                    + "long").First().Value; ;



            GPSLocation currentLocation = new GPSLocation
            {
                Latitude = Convert.ToDouble(latitudeString),
                Longitude = Convert.ToDouble(longitude)
            };

            return currentLocation;

        }




        /*
          Brief: Stores a list of support activities within the Collaborator
                instance

          Arguments:
                [1] collaborator - the collaborator instance to store
                        the list of support activities

          Exception:
                [1] throws a InvalidOperationException if propery 'XElementObject'
                    does not contain the "support-activity" elements
                [2] throws Exception if any of the support activity values
                    does not belong to an acceptable value


          Return:
                This is a return by reference method. Stores the list of event
                support activities within the 'collaborator' argument
       */
        private void RetrieveListOfSupportActivities(Collaborator collaborator)
        {

            IEnumerable<XElement> listOfSupportActivities = 
                xElementObject.Descendants(this.xmlNamespace + "support-activity");


            Collaborator.SupportActivity supportActivity;
            string supportActivityString;

            foreach (XElement supportActivityElement in listOfSupportActivities)
            {
                supportActivityString = supportActivityElement.Value;

                supportActivity = GetSupportActivityFromString(supportActivityString);

                collaborator.AddSupportActivity(supportActivity);
            }
            
        }


        /*
            Brief: Returns a SupportActivity based on the string argument

            Argument: 
                [1] the argument 'supportActivityString' must contain one
                    of the following values:
                    (a) "GroceryShopping"
                    (b) "GroupActivityFacilitation"
                    (c) "MealPreparation"
                    (d) "MedicationAssistance"
                    (e) "ShoweringAssistance"
                    (f) "Transportation"

            Otherwise, throw Exception if the argument 
                does not contain any of the above stated values
         */
        private Collaborator.SupportActivity GetSupportActivityFromString(string supportActivityString)
        {

            Collaborator.SupportActivity supportActivity =
                        Collaborator.SupportActivity.GroceryShopping;


            switch (supportActivityString)
            {
                case "GroceryShopping":
                    supportActivity = Collaborator.SupportActivity.GroceryShopping;
                    break;

                case "GroupActivityFacilitation":
                    supportActivity = Collaborator.SupportActivity.GroupActivityFacilitation;
                    break;

                case "MealPreparation":
                    supportActivity = Collaborator.SupportActivity.MealPreparation;
                    break;

                case "MedicationAssistance":
                    supportActivity = Collaborator.SupportActivity.MedicationAssistance;
                    break;

                case "ShoweringAssistance":
                    supportActivity = Collaborator.SupportActivity.ShoweringAssistance;
                    break;


                case "Transportation":
                    supportActivity = Collaborator.SupportActivity.Transportation;
                    break;

                default:
                    throw new Exception("No such support activity called '"+ 
                                            supportActivityString + "'");
            }


            return supportActivity;
        }

    }


    
}
