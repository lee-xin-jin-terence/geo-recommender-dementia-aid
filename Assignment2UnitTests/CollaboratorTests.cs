using Assignment2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Assignment2UnitTests
{

    [TestClass]
    public class CollaboratorTests
    {
        /*
            Brief:
                Test whether the constructor successfully creates an
                instance of Collaborator with the default values of
                various properties correctly set
         */
        [TestMethod]
        public void CollaboratorConstructorTest_ValidNoArgument()
        {
            Collaborator collaborator = new Collaborator();

            Assert.AreEqual(collaborator.CollaboratorID, "-1");
            Assert.AreEqual(collaborator.FirstName, "<Not Yet Set>");
            Assert.AreEqual(collaborator.LastName, "<Not Yet Set>");
        }



        /*
            Brief:
                Test whether the constructor successfully sets the
                value of various properties when valid argument values
                are used
         */
        [TestMethod]
        public void CollaboratorConstructorTest_ValidArguments()
        {
            Collaborator collaborator = new Collaborator(
                "ID1", "John", "Smith");


            Assert.AreEqual(collaborator.CollaboratorID, "ID1");
            Assert.AreEqual(collaborator.FirstName, "John");
            Assert.AreEqual(collaborator.LastName, "Smith");
        }


        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentNullException if a null value is used
                as argument value to set the CollaboratorID 
                
         */
        [TestMethod]
        public void CollaboratorConstructorTest_InvalidNullCollaboratorID()
        {
            Collaborator collaborator;
            string id = null;
            string firstName = "John";
            string lastName = "Smith";

            Assert.ThrowsException<ArgumentNullException>(
               ()=> collaborator = new Collaborator(
                        id, firstName, lastName));
        }


        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentException if an empty string value is used
                as argument value to set the CollaboratorID 
                
         */
        [TestMethod]
        public void CollaboratorConstructorTest_InvalidEmptyStringCollaboratorID()
        {
            Collaborator collaborator;
            string id = "";
            string firstName = "John";
            string lastName = "Smith";

            Assert.ThrowsException<ArgumentException>(
               () => collaborator = new Collaborator(
                        id, firstName, lastName));
        }


        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentException if a whitespace-only string value 
                is used as argument value to set the CollaboratorID  
                
         */
        [TestMethod]
        public void CollaboratorConstructorTest_InvalidWhitespaceStringCollaboratorID()
        {
            Collaborator collaborator;
            string id = "     ";
            string firstName = "John";
            string lastName = "Smith";

            Assert.ThrowsException<ArgumentException>(
               () => collaborator = new Collaborator(
                        id, firstName, lastName));
        }



        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentNullException if a null value is used
                as argument value to set the FirstName  
                
         */
        [TestMethod]
        public void CollaboratorConstructorTest_InvalidNullFirstName()
        {
            Collaborator collaborator;
            string id = "ID1";
            string firstName = null;
            string lastName = "Smith";

            Assert.ThrowsException<ArgumentNullException>(
               () => collaborator = new Collaborator(
                        id, firstName, lastName));
        }



        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentException if an empty string value is used
                as argument value to set the FirstName 
                
         */
        [TestMethod]
        public void CollaboratorConstructorTest_InvalidEmptyStringFirstName()
        {
            Collaborator collaborator;
            string id = "ID1";
            string firstName = "";
            string lastName = "Smith";

            Assert.ThrowsException<ArgumentException>(
               () => collaborator = new Collaborator(
                        id, firstName, lastName));
        }



        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentException if a whitespace-only string value 
                is used as argument value to set the FirstName  
                
         */
        [TestMethod]
        public void CollaboratorConstructorTest_InvalidWhitespaceStringFirstName()
        {
            Collaborator collaborator;
            string id = "ID1";
            string firstName = "     ";
            string lastName = "Smith";

            Assert.ThrowsException<ArgumentException>(
               () => collaborator = new Collaborator(
                        id, firstName, lastName));
        }




        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentNullException if a null value is used
                as argument value to set the LastName  
                
         */
        [TestMethod]
        public void CollaboratorConstructorTest_InvalidNullLastName()
        {
            Collaborator collaborator;
            string id = "ID1";
            string firstName = "John";
            string lastName = null;

            Assert.ThrowsException<ArgumentNullException>(
               () => collaborator = new Collaborator(
                        id, firstName, lastName));
        }



        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentException if an empty string value is used
                as argument value to set the LastName 
                
         */
        [TestMethod]
        public void CollaboratorConstructorTest_InvalidEmptyStringLastName()
        {
            Collaborator collaborator;
            string id = "ID1";
            string firstName = "John";
            string lastName = "";

            Assert.ThrowsException<ArgumentException>(
               () => collaborator = new Collaborator(
                        id, firstName, lastName));
        }



        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentException if a whitespace-only string is used
                as argument value to set the LastName 
                
         */
        [TestMethod]
        public void CollaboratorConstructorTest_InvalidWhitespaceStringLastName()
        {
            Collaborator collaborator;
            string id = "ID1";
            string firstName = "John";
            string lastName = "    ";

            Assert.ThrowsException<ArgumentException>(
               () => collaborator = new Collaborator(
                        id, firstName, lastName));
        }



        /*
            Brief:
                Test whether the CollaboratorID property successfully
                sets a valid string for its property value
                
         */
        [TestMethod]
        public void CollaboratorIDTest_ValidString()
        {
            string id = "ID1";

            Collaborator collaborator = new Collaborator();
            collaborator.CollaboratorID = id;

            Assert.AreEqual(collaborator.CollaboratorID, "ID1");
        }




        /*
            Brief:
                Test whether the CollaboratorID property successfully
                throw an ArgumentNullException if a null string value 
                is used to set the property value
                
         */
        [TestMethod]
        public void CollaboratorIDTest_InvalidNullString()
        {
           
            Collaborator collaborator = new Collaborator();

            Assert.ThrowsException<ArgumentNullException>(
                ()=>collaborator.CollaboratorID = null);
        }



        /*
            Brief:
                Test whether the CollaboratorID property successfully
                 throw an ArgumentException if an empty string value 
                is used to set the property value
                
         */
        [TestMethod]
        public void CollaboratorIDTest_InvalidEmptyString()
        {

            Collaborator collaborator = new Collaborator();

            Assert.ThrowsException<ArgumentException>(
                () => collaborator.CollaboratorID = "");
        }




        /*
            Brief:
                Test whether the CollaboratorID property successfully
                 throw an ArgumentException if a whitespace-only string 
                is used to set the property value
                
         */
        [TestMethod]
        public void CollaboratorIDTest_InvalidWhitespaceString()
        {

            Collaborator collaborator = new Collaborator();

            Assert.ThrowsException<ArgumentException>(
                () => collaborator.CollaboratorID = "    ");
        }



        /*
            Brief:
                Test whether the FirstName property successfully
                sets a valid string for its property value
                
         */
        [TestMethod]
        public void FirstNameTest_ValidString()
        {
            string firstName = "John";

            Collaborator collaborator = new Collaborator();
            collaborator.FirstName = firstName;

            Assert.AreEqual(collaborator.FirstName, "John");
        }



        /*
            Brief:
                Test whether the FirstName property successfully
                throw an ArgumentNullException if a null string value 
                is used to set the property value
                
         */
        [TestMethod]
        public void FirstNameTest_InvalidNullString()
        {

            Collaborator collaborator = new Collaborator();
            

            Assert.ThrowsException<ArgumentNullException>(
                ()=> collaborator.FirstName = null);

        }



        /*
            Brief:
                Test whether the FirstName property successfully
                 throw an ArgumentException if an empty string value 
                is used to set the property value
                
         */
        [TestMethod]
        public void FirstNameTest_InvalidEmptyString()
        {

            Collaborator collaborator = new Collaborator();


            Assert.ThrowsException<ArgumentException>(
                () => collaborator.FirstName = "");

        }



        /*
            Brief:
                Test whether the FirstName property successfully
                 throw an ArgumentException if a whitespace-only string 
                is used to set the property value
                
         */
        [TestMethod]
        public void FirstNameTest_InvalidWhitespaceString()
        {

            Collaborator collaborator = new Collaborator();


            Assert.ThrowsException<ArgumentException>(
                () => collaborator.FirstName = "     ");

        }



        /*
            Brief:
                Test whether the LastName property successfully
                sets a valid string for its property value
                
         */
        [TestMethod]
        public void LastNameTest_ValidString()
        {
            string lastName = "Smith";

            Collaborator collaborator = new Collaborator();
            collaborator.LastName = lastName;

            Assert.AreEqual(collaborator.LastName, "Smith");
        }



        /*
            Brief:
                Test whether the LastName property successfully
                throw an ArgumentNullException if a null string value 
                is used to set the property value        
         */
        [TestMethod]
        public void LastNameTest_InvalidNullString()
        {

            Collaborator collaborator = new Collaborator();


            Assert.ThrowsException<ArgumentNullException>(
                () => collaborator.LastName = null);

        }



        /*
            Brief:
                Test whether the LastName property successfully
                 throw an ArgumentException if an empty string value 
                is used to set the property value
                
         */
        [TestMethod]
        public void LastNameTest_InvalidEmptyString()
        {

            Collaborator collaborator = new Collaborator();


            Assert.ThrowsException<ArgumentException>(
                () => collaborator.LastName = "");

        }



        /*
            Brief:
                Test whether the LastName property successfully
                throw an ArgumentException if an whitespace-only
                string is used to set the property value
                
         */
        [TestMethod]
        public void LastNameTest_InvalidWhitespaceString()
        {

            Collaborator collaborator = new Collaborator();


            Assert.ThrowsException<ArgumentException>(
                () => collaborator.LastName = "     ");

        }



        /*
            Brief:
                Test whether the GPSLocation property successfully
                sets a valid location 
                
         */
        [TestMethod]
        public void GPSLocationTest_ValidArgument()
        {
            GPSLocation location = new GPSLocation(15.5, 25.5);

            Collaborator collaborator = new Collaborator();

            collaborator.GPSLocation = location;


            Assert.AreEqual(collaborator.GPSLocation,
                            new GPSLocation(15.5, 25.5));
        }



        /*
            Brief:
                Test whether the GPSLocation property successfully
                throw an ArgumentNullException if a null reference 
                is used to set the property value        
         */
        [TestMethod]
        public void GPSLocationTest_InvalidNullArgument()
        {
            Collaborator collaborator = new Collaborator();


            Assert.ThrowsException<ArgumentNullException>(
                ()=>collaborator.GPSLocation = null);
        }



        /*
            Brief:
                Test whether the HoursAvailableEachWeek property 
                successfully sets a valid value      
         */
        [TestMethod]
        public void HoursAvailableEachWeekTest_ValidArgument()
        {
            Collaborator collaborator = new Collaborator();

            collaborator.HoursAvailableEachWeek = 5;

            Assert.AreEqual(collaborator.HoursAvailableEachWeek,
                            5);
        }



        /*
            Brief:
                Test whether the HoursAvailableEachWeek property 
                successfully throw an ArgumentOutOfRangeException if
                non-positive interger is used to set the property value   
         */
        [TestMethod]
        public void HoursAvailableEachWeekTest_InvalidNonPositiveArgument()
        {
            Collaborator collaborator = new Collaborator();

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                ()=>collaborator.HoursAvailableEachWeek = 0);
        }



        /*
            Brief:
                Test whether the HoursAvailableEachWeek property 
                successfully throw an ArgumentOutOfRangeException if
                an invalid positive interger is used to set the 
                property value   

            Note: There are only 168 hours in a week, so cannot
                exceed 168
         */
        [TestMethod]
        public void HoursAvailableEachWeekTest_InvalidPositiveArgument()
        {
            Collaborator collaborator = new Collaborator();

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => collaborator.HoursAvailableEachWeek = 500);
        }



        /*
            Brief:
                Test whether the AddSupportActivity method allows user
                to successfully add a support activity
         */
        [TestMethod]
        public void AddSupportActivityTest_ValidSupportActivity()
        {
            Collaborator collaborator = new Collaborator();

            bool returnVal = collaborator.AddSupportActivity(
                Collaborator.SupportActivity.GroceryShopping);

            Assert.IsTrue(returnVal);
        }




        /*
            Brief:
                Test whether the AddSupportActivity method returns false
                if the user attempts to add a support acitivity that 
                was already previously added       
         */
        [TestMethod]
        public void AddSupportActivityTest_InvalidRepeatedSupportActivity()
        {
            Collaborator collaborator = new Collaborator();

            collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.GroceryShopping);

            bool returnVal = collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.GroceryShopping);

            Assert.IsFalse(returnVal);
        }



        /*
            Brief:
                Test whether the IsProvidingSupportActivity test 
                returns true for an existing support activity
        */
        [TestMethod]
        public void IsProvidingSupportActivityTest_IsProvidingSupportActivity()
        {
            Collaborator collaborator = new Collaborator();

            collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.GroceryShopping);

            bool returnVal = collaborator.IsProvidingSupportActivity(
                    Collaborator.SupportActivity.GroceryShopping);

            Assert.IsTrue(returnVal);
        }


        /*
            Brief:
                Test whether the IsProvidingSupportActivity test 
                returns true for a support activity that the 
                collaborator is not providiing
        */
        [TestMethod]
        public void IsProvidingSupportActivityTest_IsNotProvidingSupportActivity()
        {
            Collaborator collaborator = new Collaborator();

            bool returnVal = collaborator.IsProvidingSupportActivity(
                    Collaborator.SupportActivity.MealPreparation);

            Assert.IsFalse(returnVal);
        }



        /*
            Brief:
                Test whether the RemoveSupportActivity method
                allows the removal of an existing support activity
                that the collaborator is providing
         */
        [TestMethod]
        public void RemoveSupportActivityTest_ValidRemoval()
        {
            Collaborator collaborator = new Collaborator();

            collaborator.AddSupportActivity(
                Collaborator.SupportActivity.Transportation);


            bool returnVal = collaborator.RemoveSupportActivity(
                Collaborator.SupportActivity.Transportation);


            Assert.IsTrue(returnVal);
        }



        /*
            Brief:
                Test whether the RemoveSupportActivity method
                returns false if the user attempt to remove a 
                support activity that the collaborator is not
                providing
         */
        [TestMethod]
        public void RemoveSupportActivityTest_InvalidRemoval()
        {
            Collaborator collaborator = new Collaborator();


            bool returnVal = collaborator.RemoveSupportActivity(
                Collaborator.SupportActivity.GroceryShopping);


            Assert.IsFalse(returnVal);
        }



        /*
            Brief:
                Test whether the RemoveAllSupportActivities 
                method successfully allows the removal of all
                existing support activities
         */
        [TestMethod]
        public void RemoveAllSupportActivitiesTest()
        {
            Collaborator collaborator = new Collaborator();

            collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.GroceryShopping);

            collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.Transportation);

            //boolean checks
            bool isProvidingGroceryShopping =
                collaborator.IsProvidingSupportActivity(
                    Collaborator.SupportActivity.GroceryShopping);


            bool isProvidingTransportation =
                collaborator.IsProvidingSupportActivity(
                    Collaborator.SupportActivity.Transportation);


            Assert.IsTrue(isProvidingGroceryShopping);

            Assert.IsTrue(isProvidingTransportation);
        }


        /*
            Brief:
                Test whether the GetNumberOfSupportActivities method
                successfully returns the default value, which is zero
         */
        [TestMethod]
        public void GetNumberOfSupportActivitiesTest_DefaultValue()
        {
            Collaborator collaborator = new Collaborator();

            Assert.AreEqual(collaborator.GetNumberOfSupportActivities(),
                            0);
        }


        /*
            Brief:
                Test whether the GetNumberOfSupportActivities method
                successfully returns the correct number of support
                activities that the collaborator is providing, after
                some activities have been added
         */
        [TestMethod]
        public void GetNumberOfSupportActivitiesTest_AddedSomeActivities()
        {
            Collaborator collaborator = new Collaborator();

            collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.GroceryShopping);

            collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.Transportation);

            collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.MealPreparation);

            Assert.AreEqual(collaborator.GetNumberOfSupportActivities(),
                            3);
        }



        /*
            Brief:
                Test whether the GetListOfSupportActivities method
                successfully allows the client get a list of all
                support activities providing by the collaborator
         */
        [TestMethod]
        public void GetListOfSupportActivitiesTest()
        {

            Collaborator collaborator = new Collaborator();

            collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.GroceryShopping);

            collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.Transportation);

            collaborator.AddSupportActivity(
                    Collaborator.SupportActivity.MealPreparation);

            var listOfSupportActivities = 
                collaborator.GetListOfAllSupportActivities();


            Assert.AreEqual(listOfSupportActivities.Count, 3);

            Assert.IsTrue(
                listOfSupportActivities.IndexOf(
               Collaborator.SupportActivity.GroceryShopping) != -1);


            Assert.IsTrue(
                listOfSupportActivities.IndexOf(
               Collaborator.SupportActivity.Transportation) != -1);

            Assert.IsTrue(
                listOfSupportActivities.IndexOf(
               Collaborator.SupportActivity.MealPreparation) != -1);
        }
    }
}
