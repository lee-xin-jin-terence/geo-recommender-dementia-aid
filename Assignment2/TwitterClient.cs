using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi.Models;
using System.IO;
using Tweetinvi.Models.Entities;
using Tweetinvi.Parameters;

/*
    Brief: A class that allows users to post/retrieve tweets
            to/from the Twitter server
            
    Author: Terence Lee Xin Jin
    Date Created: 3 August 2021


    Extract Interface Refactoring:
        The interface ISocialMediaClient is extracted from this class,
        and this class now implements ISocialMediaClient.
        
        This is to help ensure that if other social media clients like
        Facebook are added in the future, they would share the same
        interface as this class by implementing ISocialMediaClient.
 */
namespace Assignment2
{
    public class TwitterClient : ISocialMediaClient
    {

        private Tweetinvi.TwitterClient userClient;



        /*
            Brief: Create an instance of Twitter client

            Argument:
                [1] consumerKeyVal - the consumer key
                [2] consumerKeySecretVal - the consumer key secret
                [3] accessTokenVal - the access token
                [4] accessTokenSecretVal - the access token secret

            Exception:
                [1] Throws ArgumentNullException if any of the arguments is null
                [2] Throws ArgumentException if any of the arguments is an 
                    empty string or whitespace-only string


            Note: This constructor does not check whether any of the keys are
                    invalid, but merely does a simple check that the keys are
                    not null or empty strings.

                    If the keys are invalid keys, the methods 
                    'GetListOfRecentTweetsAsync' and 'PostTweetAsync' will throw
                    FailedOperationException
         */
        public TwitterClient(string consumerKeyVal,
                            string consumerKeySecretVal,
                            string accessTokenVal,
                            string accessTokenSecretVal)
        {
            ValidateAndSetUserClient(consumerKeyVal, consumerKeySecretVal,
                                        accessTokenVal, accessTokenSecretVal);


        }


        /*
            Brief:
                Conduct a simple check that all the various keys are valid
                (not null and not empty string) before instantiating the private
                instance variable 'userClient'

            Exception:
                [1] Throws ArgumentNullException if any of the arguments is null
                [2] Throws ArgumentException if any of the arguments is an 
                    empty string or whitespace-only string

            Post-condition:
                If all of the arguments are valid, then the instance variable
                'userClient' will be instantiated
         */
        private void ValidateAndSetUserClient(string consumerKeyVal,
                                              string consumerKeySecretVal,
                                              string accessTokenVal,
                                              string accessTokenSecretVal)
        {
            consumerKeyVal = ValidateAndTrimString(consumerKeyVal, "consumerKeyVal");
            consumerKeySecretVal = ValidateAndTrimString(consumerKeySecretVal, "consumerKeySecretVal");
            accessTokenVal = ValidateAndTrimString(accessTokenVal, "accessTokenVal");
            accessTokenSecretVal = ValidateAndTrimString(accessTokenSecretVal, "accessTokenSecretVal");


            this.userClient = new Tweetinvi.TwitterClient(consumerKeyVal, consumerKeySecretVal,
                                                          accessTokenVal, accessTokenSecretVal);

        }



        /*
            Brief: Does a simple validation check to make sure that the string
                argument is not null/ empty string

            Arguments:
                [1] stringVal - the string to be checked
                [2] argumentName - the name of the string variable 'stringVal'
                                    in the calling method

            Exceptions:
                [1] throws ArgumentNullException if 'stringVal' is null
                [2] throws ArgumentException if 'stringVal' is an empty/
                        whitespace-only string
         */
        private string ValidateAndTrimString(string stringVal, string argumentName)
        {
            if (stringVal == null)
            {
                throw new ArgumentNullException(argumentName + " cannot be null!");
            }

            stringVal = stringVal.Trim();


            if (stringVal == "")
            {
                throw new ArgumentException(argumentName +
                            " cannot be an empty/whitespace-only string");
            }


            return stringVal;
        }




        /*
            Brief: Post a tweet to Twitter server

            Arguments:
                [1] tweetTextVal - the tweet message (compulsory)
                [2] locationVal - the location of the tweet (compulsory)
                [3] imageFilaPathVal - the image path of the image to be posted alongside
                            the tweet (optional)
                        
            Exceptions:
                [1] Throws ArgumentNulLException if the arguments 'tweetTextVal' or
                        'locationVal' is null
                [2] Throws ArgumentException if the arguments 'tweetTextVal' is an empty 
                        string/ whitespace-only string
                [3] Throws FailedOperationException if fails to post the tweet 


            Return:
                ITwitterTweet - an object instance containing all the details of the tweet
                        posted to the Twitter server

            Refactoring:
                Extract Method Refactoring: 
                        extracted some helper methods out of this long method to
                        shorten this code and make this code more readable


            Third-party Code: 
                From TweetinviAPI
                Line 185 to 207
         */
        public async Task<ISocialMediaPost> PublishPostAsync(string tweetTextVal,
                                                        IGPSLocation locationVal,
                                                        string imageFilePathVal = null)
        {
            //throws ArgumentNulLException and ArgumentException if invalid value
            tweetTextVal = ValidateAndTrimString(tweetTextVal, "tweetTextVal");


            if (locationVal == null)
            {
                throw new ArgumentException("locationVal cannot be null");
            }



            PublishTweetParameters tweetParam = new PublishTweetParameters();

            tweetParam.Text = tweetTextVal;
            tweetParam.Coordinates = ConvertGPSLocationToCoordinates(locationVal);



            if (imageFilePathVal != null)
            {
                //throws FailedOperationException
                long mediaID = await UploadImageAndGetMediaIDAsync(imageFilePathVal);

                tweetParam.MediaIds.Add(mediaID);
            }



            ITweet publishedTweet;

            try
            {
                //this method call throws multiple exceptions
                publishedTweet = await userClient.Tweets.PublishTweetAsync(tweetParam);

                if (publishedTweet == null)
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                throw new FailedOperationException("Failed to publish tweet");
            }



            return ConvertITweetToISocialMediaPost(publishedTweet);

        }




        /*
            Brief: Uploads an image to twitter and obtain the corresponding media ID
                    of the uploaded image

            Argument:
                [1] imagePath - the path of the image to be uploaded

            Return:
                long - returns the media ID of the image if it is successfully uploaded.
            

            Exception:
                Throws FailedOperationException if if fails to upload the image to
                    Twitter server to obtain the media ID

            Note: To post an image to Twitter, it must first be uploaded to Twitter
                  first. Only after uploading it, include the image's media ID in
                  the tweet for the image to appear in the tweet.

            Refactoring:
                Extract Method Refactoring: 
                        This method was extracted out of the long method
                        PublishPostAsync.


            Third-party Code: 
                From TweetinviAPI
                Line 262 to 279
         */
        private async Task<long> UploadImageAndGetMediaIDAsync(string imagePath)
        {

            byte[] imageByteArray = null;
            IMedia uploadedImage;
            long mediaID = -1;



            try
            {
                //this call throws a few possible exceptions
                imageByteArray = File.ReadAllBytes(imagePath);


                //this call throws a few possible exceptions
                uploadedImage = await this.userClient.Upload.UploadTweetImageAsync(imageByteArray);


                if (uploadedImage != null)
                {
                    mediaID = uploadedImage.Id.Value;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                throw new FailedOperationException("Failed to upload image '" + imagePath +
                                                    "' to Twitter");
            }


            return mediaID;
        }




        /*
            Brief: Get a list of tweets posted by the user himself/herself

            Exceptions:
                [1] FailedOperationException - failed to retrieve the list of
                        tweets from the Twitter server

            Third-party Code: 
                From TweetinviAPI
                Line 314 to 318
         */
        public async Task<List<ISocialMediaPost>> GetListOfRecentPostsAsync()
        {

            ITweet[] arrayOfTweets;

            try
            {
                arrayOfTweets = await userClient.Timelines.GetHomeTimelineAsync();
            }
            catch
            {
                throw new FailedOperationException("Failed to retrieve tweets from Twitter");
            }


            ISocialMediaPost currentTweetPost;
            List<ISocialMediaPost> listOfTweetPosts = new List<ISocialMediaPost>();


            foreach (ITweet tweet in arrayOfTweets)
            {
                currentTweetPost = ConvertITweetToISocialMediaPost(tweet);


                listOfTweetPosts.Add(currentTweetPost);
            }

            return listOfTweetPosts;
        }



        /*
            Brief: Convert an ITweet (defined by Tweetinvi) received into a ISocialMediaPost

            Argument:
                [1] tweet - an ITweet instance to be converted to ISocialMediaPost
         

            Refactoring:
                Extract Method Refactoring: 
                        This method was extracted out of the long method
                        PublishPostAsync.


            Third-party Code: 
                From TweetinviAPI
                Line 363 to 370
         */
        private static ISocialMediaPost ConvertITweetToISocialMediaPost(ITweet tweet)
        {

            ISocialMediaPost twitterTweet = new TwitterPost()
            {
                PostID = tweet.IdStr,
                MessageText = tweet.FullText,
                DateTime = tweet.CreatedAt.LocalDateTime,
                GPSLocation = ConvertCoordinatesToGPSLocationIfExists(tweet.Coordinates),
                ImageURL = RetrieveImageURLIfExists(tweet.Media)
            };


            return twitterTweet;
        }




        /*
            Brief: Converts a IGPSLocation instance into ICoordinates instance

            Arguments:
                    [1] gpsLocation - a IGPSLocation instance

            
            Pre-condition: The argument 'gpsLocation' cannot be null

            Refactoring:
                Extract Method Refactoring: 
                        This method was extracted out of the long method
                        PublishPostAsync.

            Third-party Code: 
                From TweetinviAPI
                Line 399 to 400
         */
        private static Coordinates ConvertGPSLocationToCoordinates(IGPSLocation gpsLocation)
        {
            return new Coordinates(gpsLocation.Latitude,
                                   gpsLocation.Longitude);

        }


        /*
            Brief: Converts a ICoordinate instance into GPSLocation instance

            Arguments:
                    [1] coordinates - a ICoordinate instance

            Return: equivalent GPSLocation instance is the argument 'coordinates' is not
                null, or null if the argument 'coordinates' is null

         */
        private static GPSLocation ConvertCoordinatesToGPSLocationIfExists(ICoordinates coordinates)
        {
            GPSLocation locationToReturn = null;

            if (coordinates != null)
            {
                locationToReturn = new GPSLocation()
                {
                    Latitude = coordinates.Latitude,
                    Longitude = coordinates.Longitude
                };
            }

            return locationToReturn;
        }




        /*
            Brief: Retrieves the first url of the image associated with the tweet
                 Otherwise, returns null if no url is found
        
            Third-party Code: 
                From TweetinviAPI
                Line 446 to 453
            
         */
        private static string RetrieveImageURLIfExists(List<IMediaEntity> listOfMediaEntities)
        {

            foreach (IMediaEntity entity in listOfMediaEntities)
            {

                if (entity.MediaType == "photo")
                {
                    return entity.MediaURLHttps;
                }
            }


            return null;
        }






    }
}
