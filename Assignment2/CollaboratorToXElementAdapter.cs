using System;
using System.Collections.Immutable;
using System.Xml.Linq;


/*
    Brief: A utility class that converts a Collaborator instance 
            into a XElement instance
            
    Author: Terence Lee Xin Jin
    Date Created: 7 August 2021
 */
namespace Assignment2
{
    public class CollaboratorToXElementAdapter
    {
        private Collaborator collaboratorObject;
        private XNamespace xmlNamespace;


        /*
          Brief: Creates an instance of CollaboratorToXElementAdapter

          Arguments:
            [1] xmlns - the namespace of the XElement obj to be created/
                                built
            [2] collaboratorObjVal - the collaborator instance to be
                                converted to an XElement. 


           Exception:
                [1] Throws an ArgumentNullException if the argument
                    'collaboratorObjVal' is null
       */
        public CollaboratorToXElementAdapter(XNamespace xmlns, 
                                             Collaborator collaboratorObjVal)
        {

            this.xmlNamespace = xmlns;

            if (collaboratorObjVal == null)
            {
                throw new ArgumentNullException("collaboratorObj cannot be null");
            }

            this.collaboratorObject = collaboratorObjVal;

        }



        /*
           Brief: Converts the collaborator instance, set in the constructor,
                into a XElement instance, where all the data in that 
                collaborator instance is stored in the XElement instance 


            Pre-conditions:
                [1] The 'Namespace' and the 'EventObject' properties should
                    first be set before calling this method
        */
        public XElement ConvertCollaboratorToXElement()
        {

            XElement collaboratorElement = new XElement(xmlNamespace + "Collaborator");

            XElement collaboratorIDElement = GetCollaboratorIDElement();

            XElement firstNameElement = GetFirstNameElement();

            XElement lastNameElement = GetLastNameElement();

            XElement locationElement = GetLocationElement();

            XElement hoursAvailableElement = GetHoursAvailableElement();


            XElement supportActivityListElement = GetSupportActivityListElement();


            collaboratorElement.Add(collaboratorIDElement);

            collaboratorElement.Add(firstNameElement);

            collaboratorElement.Add(lastNameElement);

            collaboratorElement.Add(locationElement);

            collaboratorElement.Add(hoursAvailableElement);

            collaboratorElement.Add(supportActivityListElement);


            return collaboratorElement;
        }



        /*
           Brief: Return XElement "collaborator-id" containing the 
                    collaborator id associated with the collaborator. 
                    It is a simple XElement with no descendant elements

            Pre-conditions:
                [1] The instance variable 'collaboratorObject' should already 
                    set the value of the property 'CollaboratorID'
        */
        private XElement GetCollaboratorIDElement()
        {
            XElement collaboratorIDElement = new XElement(xmlNamespace + "collaborator-id");

            collaboratorIDElement.Value = collaboratorObject.CollaboratorID;

            return collaboratorIDElement;
        }




        /*
           Brief: Return XElement "first-name" containing the 
                    first name associated with the collaborator. 
                    It is a simple XElement with no descendant elements

            Pre-conditions:
                [1] The instance variable 'collaboratorObject' should already 
                    set the value of the property 'FirstName'
        */
        private XElement GetFirstNameElement()
        {
            XElement firstNameElement = new XElement(xmlNamespace + "first-name");

            firstNameElement.Value = collaboratorObject.FirstName;

            return firstNameElement;
        }



        /*
           Brief: Return XElement "last-name" containing the 
                    last name associated with the collaborator. 
                    It is a simple XElement with no descendant elements

            Pre-conditions:
                [1] The instance variable 'collaboratorObject' should already 
                    set the value of the property 'LastName'
        */
        private XElement GetLastNameElement()
        {
            XElement lastNameElement = new XElement(xmlNamespace + "last-name");

            lastNameElement.Value = collaboratorObject.LastName;

            return lastNameElement;
        }



        /*
           Brief: Return XElement "hours-available-each-week" containing 
                    the number of hours the collaborator is available 
                    each week


            Pre-conditions:
                [1] The instance variable 'collaboratorObject' 
                    should already set the value of the property 
                    'HoursAvailableEachWeek'
        */
        private XElement GetHoursAvailableElement()
        {
            XElement hoursAvailableElement = new XElement(xmlNamespace + "hours-available-each-week");

            hoursAvailableElement.Value = collaboratorObject.HoursAvailableEachWeek.ToString();

            return hoursAvailableElement;
        }


        /*
           Brief: Return XElement "location" containing the location
                    associated with the collaborator. 


            Pre-conditions:
                [1] The instance variable 'collaboratorObject' 
                    should already set the value of the property 
                    'GPSLocation'
        */
        private XElement GetLocationElement()
        {

            XElement locationElement = new XElement(xmlNamespace + "location");


            double latitudeVal = this.collaboratorObject.GPSLocation.Latitude;
            double longitudeVal = this.collaboratorObject.GPSLocation.Longitude;

            XElement latitudeElement = new XElement(xmlNamespace + "lat");
            latitudeElement.Value = latitudeVal.ToString();


            XElement longitudeElement = new XElement(xmlNamespace + "long");
            longitudeElement.Value = longitudeVal.ToString();


            locationElement.Add(latitudeElement);
            locationElement.Add(longitudeElement);


            return locationElement;
        }




        /*
           Brief: Return XElement "support-activity-list" containing all the
                    support activities provided by the collaborator


            Pre-conditions:
                [1] The instance variable 'collaboratorObject' 
                    should already contain the list of support activites
                    provided by the collaborator      
        */
        private XElement GetSupportActivityListElement()
        {
            XElement supportActivityListElement = new XElement(xmlNamespace + "support-activity-list");


            XElement supportActivityElement;

            ImmutableList<Collaborator.SupportActivity> listOfSupportActivities =
                                    collaboratorObject.GetListOfAllSupportActivities();

            foreach (var supportActivty in listOfSupportActivities)
            {
                supportActivityElement = new XElement(xmlNamespace + "support-activity");

                supportActivityElement.Value = supportActivty.ToString();

                supportActivityListElement.Add(supportActivityElement);
            }


            return supportActivityListElement;
        }
    }
}
