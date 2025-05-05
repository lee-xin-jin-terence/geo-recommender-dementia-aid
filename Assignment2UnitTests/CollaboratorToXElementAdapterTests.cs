using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment2;
using System.Xml.Linq;
using System;
using System.Linq;

namespace Assignment2UnitTests
{
    [TestClass]
    public class CollaboratorToXElementAdapterTests
    {
        /*
           Brief:
               Test whether the constructor method successfully
                throw an ArgumentNullException if a null reference was used
        */
        [TestMethod]
        public void ConvertCollaboratorToXElementTest_InvalidNullEvent()
        {
            XNamespace ns = "http://www.xyz.org/lifelogevents";

            Collaborator collaborator = null;

            CollaboratorToXElementAdapter adapter;

            Assert.ThrowsException<ArgumentNullException>(
                () => adapter = new CollaboratorToXElementAdapter(ns, collaborator));
        }


        /*
            Brief:
                Test whether the ConvertCollaboratorToXElement method can
                successfully convert a Collaborator to a XElement correctly  
        
                Compares the converted XElement with an expected output
         */
        [TestMethod]
        public void ConvertEventToXElementTest_ValidTwitterEvent()
        {
            XNamespace ns = "http://www.xyz.org/lifelogevents";

            XDocument doc = XDocument.Load("../../../XML_Fragment.xml");

            XElement collaboratorElement = doc.Descendants(ns + "Collaborator").First();

            //-----------------------------------------------

            Collaborator collaborator = new Collaborator();

            collaborator.CollaboratorID = "ID5";
            collaborator.FirstName = "Jane";
            collaborator.LastName = "Smith";
            collaborator.GPSLocation = new GPSLocation(15.5, 25.5);

            collaborator.HoursAvailableEachWeek = 20;

            collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.MealPreparation);

            collaborator.AddSupportActivity(
                Collaborator.SupportActivity.Transportation);

            collaborator.AddSupportActivity(
                Collaborator.SupportActivity.ShoweringAssistance);


            CollaboratorToXElementAdapter adapter =
                new CollaboratorToXElementAdapter(ns, collaborator);

            XElement generatedXElement =
                        adapter.ConvertCollaboratorToXElement();


            Assert.IsTrue(XNode.DeepEquals(generatedXElement,
                                        collaboratorElement));

        }
    }

    
}
