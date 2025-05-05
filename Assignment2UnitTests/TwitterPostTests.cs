using Assignment2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Assignment2UnitTests
{
    [TestClass]
    public class TwitterPostTests
    {
        /*
            Brief:
                Test whether the constructor successfully creates an
                instance of TwitterPost with the various default
                values set to correct values
         */
        [TestMethod]
        public void TwitterPostConstructorTest_ValidNoArgument()
        {
            TwitterPost twitterPost = new TwitterPost();

            Assert.AreEqual(twitterPost.PostID, "-1");
            Assert.AreEqual(twitterPost.MessageText, "<Not Yet Set>");
            Assert.AreEqual(twitterPost.ImageURL, null);
            Assert.AreEqual(twitterPost.GPSLocation, null);
        }


        /*
            Brief:
                Test whether the constructor successfully sets the
                post id and message text property of TwitterPost 
                correctly when valid arguments are used 
                
         */
        [TestMethod]
        public void TwitterPostConstructorTest_ValidArguments()
        {
            TwitterPost twitterPost = new TwitterPost("ID6" ,
                                            "Hello World");

            Assert.AreEqual(twitterPost.PostID, "ID6");
            Assert.AreEqual(twitterPost.MessageText, "Hello World");   
        }


        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentNullException if a null value is used
                as argument value to set the PostID 
                
         */
        [TestMethod]
        public void TwitterPostConstructorTest_InvalidNullPostID()
        {
            TwitterPost twitterPost;

            Assert.ThrowsException<ArgumentNullException>(
              ()=> twitterPost = new TwitterPost(null, "Hello World"));

        }



        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentException if an empty string value is used
                as argument value to set the PostID 
                
         */
        [TestMethod]
        public void TwitterPostConstructorTest_InvalidEmptyStringPostID()
        {
            TwitterPost twitterPost;

            Assert.ThrowsException<ArgumentException>(
              () => twitterPost = new TwitterPost("", "Hello World"));

        }




        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentException if a whitespace string value is used
                as argument value to set the PostID 
                
         */
        [TestMethod]
        public void TwitterPostConstructorTest_InvalidWhitespacePostID()
        {
            TwitterPost twitterPost;

            Assert.ThrowsException<ArgumentException>(
              () => twitterPost = new TwitterPost("      ", "Hello World"));

        }



        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentNullException if a null value is used
                as argument value to set the MessageText 
                
         */
        [TestMethod]
        public void TwitterPostConstructorTest_InvalidNullMessageText()
        {
            TwitterPost twitterPost;

            Assert.ThrowsException<ArgumentNullException>(
                () => twitterPost = new TwitterPost("ID6", null));

        }



        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentException if an empty string value is used
                as argument value to set the MessageText 
                
         */
        [TestMethod]
        public void TwitterPostConstructorTest_InvalidEmptyStringMessageText()
        {
            TwitterPost twitterPost;

            Assert.ThrowsException<ArgumentException>(
              () => twitterPost = new TwitterPost("ID6", ""));

        }



        /*
            Brief:
                Test whether the constructor successfully throw an 
                ArgumentException if a whitespace string value is used
                as argument value to set the MessageText 
                
         */
        [TestMethod]
        public void TwitterPostConstructorTest_InvalidWhitespaceStringMessageText()
        {
            TwitterPost twitterPost;

            Assert.ThrowsException<ArgumentException>(
              () => twitterPost = new TwitterPost("ID6", "      "));

        }




        /*
            Brief:
                Test whether the PostID property successfully
                sets a valid string for its property value
                
         */
        [TestMethod]
        public void PostIDTest_ValidString()
        {
            string postID = "ID4";

            TwitterPost twitterPost = new TwitterPost();
            twitterPost.PostID = postID;


            Assert.AreEqual(twitterPost.PostID, "ID4");
        }



        /*
            Brief:
                Test whether the PostID property successfully
                throw an ArgumentNullException if a null string value 
                is used to set the property value
                
         */
        [TestMethod]
        public void PostIDTest_InvalidNullString()
        {
          
            TwitterPost twitterPost = new TwitterPost();

            Assert.ThrowsException<ArgumentNullException>(
                ()=>twitterPost.PostID = null);
        }




        /*
            Brief:
                Test whether the PostID property successfully
                 throw an ArgumentException if an empty string value 
                is used to set the property value
                
         */
        [TestMethod]
        public void PostIDTest_InvalidEmptyString()
        {

            TwitterPost twitterPost = new TwitterPost();

            Assert.ThrowsException<ArgumentException>(
                () => twitterPost.PostID = "");
        }


        /*
            Brief:
                Test whether the PostID property successfully
                throw an ArgumentException if a whitespace-only
                string is used to set the property value
                
         */
        [TestMethod]
        public void PostIDTest_InvalidWhitespaceString()
        {

            TwitterPost twitterPost = new TwitterPost();

            Assert.ThrowsException<ArgumentException>(
                () => twitterPost.PostID = "    ");
        }



        /*
            Brief:
                Test whether the MessageText property successfully
                sets a valid string for its property value
                
         */
        [TestMethod]
        public void MessageTextTest_ValidString()
        {
            string messageText = "Hello";

            TwitterPost twitterPost = new TwitterPost();
            twitterPost.MessageText = messageText;


            Assert.AreEqual(twitterPost.MessageText, messageText);
        }



        /*
            Brief:
                Test whether the MessageText property successfully
                throw an ArgumentNullException if a null string value 
                is used to set the property value
                
         */
        [TestMethod]
        public void MessageTextTest_InvalidNullString()
        {

            TwitterPost twitterPost = new TwitterPost();

            Assert.ThrowsException<ArgumentNullException>(
                () => twitterPost.MessageText = null);
        }



        /*
            Brief:
                Test whether the PostID property successfully
                 throw an ArgumentException if an empty string value 
                is used to set the property value
                
         */
        [TestMethod]
        public void MessageTextTest_InvalidEmptyString()
        {

            TwitterPost twitterPost = new TwitterPost();

            Assert.ThrowsException<ArgumentException>(
                () => twitterPost.MessageText = "");
        }


        /*
            Brief:
                Test whether the MessageText property successfully
                 throw an ArgumentException if a whitespace string
                is used to set the property value
                
         */
        [TestMethod]
        public void MessageTextTest_InvalidWhitespaceString()
        {

            TwitterPost twitterPost = new TwitterPost();

            Assert.ThrowsException<ArgumentException>(
                () => twitterPost.MessageText = "    ");
        }



        /*
            Brief:
                Test whether the ImageURL property successfully
                sets a valid string for its property value

            Note: There is no test for invalid null arguments,
                as null value is a valid argument
                
         */
        [TestMethod]
        public void ImageURLTest_ValidArgument()
        {
            string imageURL = "www.yahoo.com/someImage.png";

            TwitterPost twitterPost = new TwitterPost();
            twitterPost.ImageURL = imageURL;


            Assert.AreEqual(twitterPost.ImageURL,
                            "www.yahoo.com/someImage.png");
        }



        /*
            Brief:
                Test whether the DateTime property successfully
                sets a valid DatTime for its property value

            Note: There is no test for invalid null arguments,
                as null value is a valid argument
                
         */
        [TestMethod]
        public void DateTimeTest_ValidArgument()
        {
            DateTime dateTime = new DateTime(2015, 12, 25);

            TwitterPost twitterPost = new TwitterPost();
            twitterPost.DateTime = dateTime;


            Assert.AreEqual(twitterPost.DateTime,
                            new DateTime(2015, 12, 25));
        }



        /*
            Brief:
                Test whether the GPSLocation property successfully
                sets a valid GPSLocation for its property value           
         */
        [TestMethod]
        public void GPSLocationTest_ValidArgument()
        {
            TwitterPost twitterPost = new TwitterPost();

            twitterPost.GPSLocation = new GPSLocation(15.5, 25.5);


            Assert.AreEqual(twitterPost.GPSLocation,
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
            TwitterPost twitterPost = new TwitterPost();

            Assert.ThrowsException<ArgumentNullException>(
                ()=>twitterPost.GPSLocation = null);
        }
    }
}
