
using System.Windows;

/*
    File Name: DisplayTweetDetailsWindow.cs
    Author: Terence Lee Xin Jin
    Date: 31 July 2021

    Purpose: Display all the details of an existing tweet in
            a new window
 */

namespace Assignment2
{
    
    public partial class DisplayTweetDetailsWindow : Window
    {
        private TwitterPost tweetPost;

        public DisplayTweetDetailsWindow(TwitterPost tweetPostVal)
        {
            InitializeComponent();

            this.tweetPost = tweetPostVal;

            SetViewImageHandler();
            DisplayTweetPostDetails();
        }

        /*
            Brief: Displays the various details of the tweet
         */
        private void DisplayTweetPostDetails()
        {

            this.tweetIDTextBox.Text = this.tweetPost.PostID.ToString();

            this.tweetTextTextBox.Text = this.tweetPost.MessageText;

            this.dateTimeTextBox.Text = this.tweetPost.DateTime.ToString();

            this.gpsLocationTextBox.Text = this.tweetPost.GPSLocation.ToString();

        }

        /*
            Brief: If the tweet contains a URL for an image, display the "View Image" button.
                   Also, set the click event handler for that button such that it displays the
                    particular image associated with that tweet when it is clicked
         */
        private void SetViewImageHandler()
        {
            if (tweetPost.ImageURL != null)
            {
                viewImageGrid.Visibility = Visibility.Visible;

                this.viewImageBtn.Click += (sender, e) => DisplayTweetImage(tweetPost.ImageURL);
            }
        }


        /*
            Brief: Display the image associated with the tweet
            
            Arguments:
                tweetImageURL - the url of the tweet image
         */
        private void DisplayTweetImage(string tweetImageURL)
        {
            ImageViewerWindow viewer = new ImageViewerWindow(tweetImageURL);

            viewer.ShowDialog();
        }



        

     
    }
}
