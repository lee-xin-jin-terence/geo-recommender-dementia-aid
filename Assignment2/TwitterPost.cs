using System;

/*
    Brief: A class that stores details for a Twitter post
            
    Author: Terence Lee Xin Jin
    Date Created: 2 August 2021
     
 */
namespace Assignment2
{
    public class TwitterPost :  ISocialMediaPost
    {
        private string postID = "-1";

        private string messageText = "<Not Yet Set>";

        private IGPSLocation gpsLocation;



        /*
            Brief: Creates an instance of TwitterEvent

         */
        public TwitterPost()
        {

        }




        /*
            Brief: Creates an instance of TwitterEvent

            Arguments:
                [1] postIDVal - the ID of the tweet
                [3] messageTextVal - the associated message text of the
                                tweet
         */
        public TwitterPost(string postIDVal, string messageTextVal)
        {
            this.PostID = postIDVal;

            this.MessageText = messageTextVal;
        }




        /*
            Brief: Sets and gets the post id of the tweet

            Exception:
                [1] ArgumentNullException - when a null value is used to
                        set the message text

                [2] ArgumentException - when a empty string/whitespace-only
                        string is used to set the message text

            Return:
                Getter returns the string "-1" if property has 
                not been set
         */
        public string PostID
        {
            set
            {
                this.postID = ValidateAndTrimString(value, "PostID");
            }

            get { return this.postID; }
        }



        /*
            Brief: Sets and gets the Message text

            Exception:
                [1] ArgumentNullException - when a null value is used to
                        set the message text

                [2] ArgumentException - when a empty string/whitespace-only
                        string is used to set the message text

            Return:
                Getter returns the string "<Not Yet Set>" if 
                property has not been set
         */
        public string MessageText
        {
            set
            {
            
                this.messageText = ValidateAndTrimString(value, "MessageText");
            }

            get { return this.messageText; }
        }



        /*
            Brief: A helper method to help validate and trim string
                    values from method arguments

            Argument:
                [1] stringVal - the string to be validated and trimmed
                [2] propertyName - the name of the property
            
            Exception:
                [1] ArgumentNullException - when the argument 'stringVal'
                        has a null value

                [2] ArgumentException - when the argument 'stringVal' is 
                        an empty string/whitespace-only

            Return:
                Returns the trimmed version of the argument 'stringVal'
         */
        private string ValidateAndTrimString(string stringVal, string propertyName)
        {

            if (stringVal == null)
            {
                throw new ArgumentNullException(propertyName + " cannot be null");
            }


            stringVal = stringVal.Trim();

            if (stringVal == "")
            {
                throw new ArgumentException(propertyName + " cannot be empty string");
            }


            return stringVal;
        }




        /*
            Brief: Sets and gets the associated URL of the image associated
                    with the tweet

            Note: As not all images have images, it is ok to set this value
                    to null

            Return:
                Getter returns the value null if property has not been set
         */
        public string ImageURL
        {
            set; get;
        }



        /*
            Brief: Sets and gets the date time of the social media event

         */
        public DateTime DateTime
        {
            //datetime is non-nullable, so no null checking
            set; get;
        }



        /*
            Brief: Sets and gets the gps location of the social media event

            Exception:
                [1] ArgumentNullException - when a null value is used to
                        set the gps location


            Extract Interface Refactoring:
                This class now uses IGPSLocation as data type instead
                of the original GPSLocation after the use of 
                extract interface refactoring
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




    }
}
