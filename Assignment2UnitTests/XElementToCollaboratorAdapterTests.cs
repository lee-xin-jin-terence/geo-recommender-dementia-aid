using Assignment2;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;

namespace Assignment2UnitTests
{
    

    [TestClass]
    public class XElementToCollaboratorAdapterTests
    {

        /*
            Brief:
                Test whether the Constructor throws an exception if a null
                XElement reference was used
    */
        [TestMethod]
        public void ConvertXElementToCollaboratorTest_InvalidNullXElement()
        {

            XNamespace ns = "http://www.xyz.org/lifelogevents";


            XElementToCollaboratorAdapter adapter;

            Assert.ThrowsException<ArgumentNullException>(
                () => adapter = new XElementToCollaboratorAdapter(ns, null));

        }


        [TestMethod]
        public void ConvertXElementToCollaboratorTest_ValidXElement()
        {
            XNamespace ns = "http://www.xyz.org/lifelogevents";

            XDocument doc = XDocument.Load("../../../XML_Fragment.xml");

            XElement collaboratorElement = doc.Descendants(ns + "Collaborator").First();

            XElementToCollaboratorAdapter adapter =
                    new XElementToCollaboratorAdapter(ns, collaboratorElement);

            Collaborator collaborator = 
                        adapter.ConvertXElementToCollaborator();

            Assert.AreEqual(collaborator.CollaboratorID, "ID5");
            Assert.AreEqual(collaborator.FirstName, "Jane");
            Assert.AreEqual(collaborator.LastName, "Smith");
            Assert.AreEqual(collaborator.GPSLocation,
                                new GPSLocation(15.5, 25.5));
            Assert.AreEqual(collaborator.HoursAvailableEachWeek, 20);
            

            Assert.IsTrue(
                collaborator.IsProvidingSupportActivity
                    (Collaborator.SupportActivity.MealPreparation));

            Assert.IsTrue(
                collaborator.IsProvidingSupportActivity
                    (Collaborator.SupportActivity.Transportation));

            Assert.IsTrue(
                collaborator.IsProvidingSupportActivity
                    (Collaborator.SupportActivity.ShoweringAssistance));
        }
    }
}
