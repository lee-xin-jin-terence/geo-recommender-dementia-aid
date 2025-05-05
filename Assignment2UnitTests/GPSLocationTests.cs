using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Assignment2;

namespace Assignment2UnitTests
{
    [TestClass]
    public class GPSLocationTests
    {

        /*
            Brief:
                Test whether the constructor successfully creates an
                instance of GPSLocation with the default value of the
                Latitude and Longitude correctly set
         */
        [TestMethod]
        public void GPSLocationConstructorTest_NoArgument()
        {
            GPSLocation location = new GPSLocation();
            
            Assert.AreEqual(location.Latitude, 0);
            Assert.AreEqual(location.Longitude, 0);
        }

        /*
            Brief:
                Test whether the constructor successfully sets the
                Latitude and Longitude property values
                when valid argument values are used
         */
        [TestMethod]
        public void GPSLocationConstructorTest_ValidArguments()
        {
            GPSLocation location = new GPSLocation(55,95);

            Assert.AreEqual(location.Latitude, 55);
            Assert.AreEqual(location.Longitude, 95);
        }

        /*
            Brief:
                Test whether the constructor successfully throw
                ArgumentOutOfRangeException when an invalid positive
                Latitude value exceeding 90 is used as argument in 
                the constructor

            *Note: Latitude cannot be more than 90
         */
        [TestMethod]
        public void GPSLocationConstructorTest_InvalidPositiveLatitudeArgument()
        {

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => { GPSLocation location = new GPSLocation(95, 55); });

        }

        /*
            Brief:
                Test whether the constructor successfully throw
                ArgumentOutOfRangeException when an invalid negative
                Latitude value below -90 is used as argument in 
                the constructor

            *Note: Latitude cannot be less than -90
         */
        [TestMethod]
        public void GPSLocationConstructorTest_InvalidNegativeLatitudeArgument()
        {
            
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                ()=> { GPSLocation location = new GPSLocation(-95, 55); });

        }


        /*
            Brief:
                Test whether the constructor successfully throw
                ArgumentOutOfRangeException when an invalid positive
                Longitude value exceeding 180 is used as argument in 
                the constructor

            *Note: Longitude cannot be more than 180
         */
        [TestMethod]
        public void GPSLocationConstructorTest_InvalidPositiveLongitudeArgument()
        {

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => { GPSLocation location = new GPSLocation(55, 195); });

        }

        /*
            Brief:
                Test whether the constructor successfully throw
                ArgumentOutOfRangeException when an invalid negative
                Longitude value below -180 is used as argument in 
                the constructor

            *Note: Longitude cannot be less than -180
         */
        [TestMethod]
        public void GPSLocationConstructorTest_InvalidNegativeLongitudeArgument()
        {

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => { GPSLocation location = new GPSLocation(55, -195); });

        }


        /*
            Brief:
                Test whether the Latitude property successfully
                sets a valid positive latitude value

            Note: Valid latitude value is between 90 (inclusive)
                and -90 (inclusive)
                
         */
        [TestMethod]
        public void LatitudeTest_SetValidPositive()
        {
            GPSLocation location = new GPSLocation();

            location.Latitude = 55;

            Assert.AreEqual(55, location.Latitude);
        }


        /*
            Brief:
                Test whether the Latitude property successfully
                sets a valid negative latitude value

            Note: Valid latitude value is between 90 (inclusive)
                and -90 (inclusive)
                
         */
        [TestMethod]
        public void LatitudeTest_SetValidNegative()
        {
            GPSLocation location = new GPSLocation();

            location.Latitude = -55;

            Assert.AreEqual(-55, location.Latitude);
        }

        /*
            Brief:
                Test whether the Latitude property throws a 
                ArgumentOutOfRangeException when an invalid
                positive latitude value is used to set the property

            Note: Valid latitude value is between 90 (inclusive)
                and -90 (inclusive)
                
         */
        [TestMethod]
        public void LatitudeTest_SetInvalidPositive()
        {
            GPSLocation location = new GPSLocation();

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => location.Latitude = 95);
        }

        /*
            Brief:
                Test whether the Latitude property throws a 
                ArgumentOutOfRangeException when an invalid
                negative latitude value is used to set the property

            Note: Valid latitude value is between 90 (inclusive)
                and -90 (inclusive)
                
         */
        [TestMethod]
        public void LatitudeTest_SetInvalidNegative()
        {
            GPSLocation location = new GPSLocation();

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                ()=>location.Latitude = -95);
        }



        /*
            Brief:
                Test whether the Longitude property successfully
                sets a valid positive longitude value

            Note: Valid longitude value is between 180 (inclusive)
                and 180 (inclusive)
                
         */
        [TestMethod]
        public void LongitudeTest_SetValidPositive()
        {
            GPSLocation location = new GPSLocation();

            location.Longitude = 55;

            Assert.AreEqual(55, location.Longitude);
        }


        /*
            Brief:
                Test whether the Longitude property successfully
                sets a valid negative longitude value

            Note: Valid longitude value is between 180 (inclusive)
                and 180 (inclusive)
                
         */
        [TestMethod]
        public void LongitudeTest_SetValidNegative()
        {
            GPSLocation location = new GPSLocation();

            location.Longitude = -55;

            Assert.AreEqual(-55, location.Longitude);
        }


        /*
            Brief:
                Test whether the Longitude property throws a 
                ArgumentOutOfRangeException when an invalid
                positive longitude value is used to set the property

            Note: Valid latitude value is between 180 (inclusive)
                and -180 (inclusive)
                
         */
        [TestMethod]
        public void LongitudeTest_SetInvalidPositive()
        {
            GPSLocation location = new GPSLocation();

 
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => location.Longitude = 195);
        }

        /*
            Brief:
                Test whether the Longitude property throws a 
                ArgumentOutOfRangeException when an invalid
                negative longitude value is used to set the property

            Note: Valid latitude value is between 180 (inclusive)
                and -180 (inclusive)
                
         */
        [TestMethod]
        public void LongitudeTest_SetInvalidNegative()
        {
            GPSLocation location = new GPSLocation();


            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => location.Longitude = -195);
        }



        /*
            Brief:
                Test whether the Equals method correctly return
                true if two locations with the same latitude and
                longitude are compared
                
         */
        [TestMethod]
        public void EqualsTest_ValidEqualArgument()
        {
            GPSLocation locationA = new GPSLocation(1.5, 2.5);

            GPSLocation locationB = new GPSLocation(1.5, 2.5);

            Assert.IsTrue(locationA.Equals(locationB));
        }


        /*
            Brief:
                Test whether the Equals method correctly return
                false if two locations with the different latitude and
                longitude are compared   
         */
        [TestMethod]
        public void EqualsTest_ValidNotEqualArgument1()
        {
            GPSLocation locationA = new GPSLocation(55.5, 11.5);

            GPSLocation locationB = new GPSLocation(1.5, 2.5);

            Assert.IsFalse(locationA.Equals(locationB));
        }



        /*
            Brief:
                Test whether the Equals method correctly return
                false if two locations, where one of them is null,
                is compared
         */
        [TestMethod]
        public void EqualsTest_ValidNotEqualArgument2()
        {
            GPSLocation locationA = new GPSLocation(55.5, 11.5);

            GPSLocation locationB = null;

            Assert.IsFalse(locationA.Equals(locationB));
        }



        /*
            Brief:
                Test whether the ToString method correctly returns
                a string with its latitude and longitude formattef
                to 4 decimal places
         */
        [TestMethod]
        public void ToStringTest()
        {
            GPSLocation location = new GPSLocation(55.55,66.66);


            Assert.AreEqual("55.5500, 66.6600", location.ToString());
        }
    }
}