using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

/*
    Brief: A Window that prompts from the user to 
        enter the details of the tweet he/she wishes
        to post
            
    Author: Terence Lee Xin Jin
    Date Created: 7 Auguest 2021
 */
namespace Assignment2
{
    
    public partial class PromptTweetDetailsWindow : Window
    {
        private TwitterClient twitterClient;

        private GPSLocation gpsLocation;

        private OpenFileDialog openFileDialog = new OpenFileDialog();



        /*
           Brief: Display a form that allows user to enter the
                details of the tweet to be published


            Note: You still need to call ".ShowDialog()" to make the
                form window visible after calling this constructor

           Post-condition:
                If all the user input entered is valid

                [1] the 'IsSuccessful' property will be set to true

                [3] the property 'PublishedTweet' will
                    contain all the details of the published tweet
        */
        public PromptTweetDetailsWindow(TwitterClient twitterClientVal,
                                        GPSLocation gpsLocationVal)
        {
            InitializeComponent();
            SetOpenFileDialogFilter();

            this.twitterClient = twitterClientVal;
            this.gpsLocation = gpsLocationVal;

            gpsLocationTextBox.Text = gpsLocationVal.ToString();
        }



        /*
           Brief: A read-only property that specifies the operation
                 of publishing a tweet is successful

           Return:
                true if successful, false if otherwise
        */
        public bool IsSuccessful
        {
            private set;
            get;
        }



        /*
           Brief: A read-only property that contains the
                instance of the published tweet if the tweet
                is successfully published to twitter

                Otherwise, returns null if failed to
                    publish to twitter
        */
        public ISocialMediaPost PublishedTweet
        {
            private set;
            get;
        }




        /*
          Brief: Attempts to publish the tweet based on the details the
                    user entered
       

         Postcondition:
            if successful, the 'IsSuccessful' property will return true,
                and the 'PublishedTweet' property will contain an instance
                of the published tweet'

            Otherwise, the 'IsSuccessful' property will return false,
                and the 'PublishedTweet' property will return null.

       */
        private async Task PublishTweet()
        {
            string tweetText = messageTextBox.Text.Trim();

            string imagePath = null;


            bool inputsAreValid = ValidateInputs();

            //Check whether the inputs are valid
            // if not, return
            if (!inputsAreValid)
            {
                return;
            }


            //note that the radio button returns a nullable bool
            if (yesIncludeImageRadioBtn.IsChecked == true)
            {
                imagePath = openFileDialog.FileName;
            }
            

            try
            {
                this.PublishedTweet = 
                    await twitterClient.PublishPostAsync(
                        tweetText, this.gpsLocation, imagePath);

                MessageBox.Show("Tweet successfully published!");


                this.IsSuccessful = true;

            }
            catch (Exception)
            {
                MessageBox.Show("Failed to publish the tweet. It may have failed " +
                    "due to Twitter server is down");
            }
            
           

        }


        /*
          Brief: Sets the open file dialog filter for
                to limit the type of files that can be selected

          Note:
            Due to the limitations of the TwitterClient, only
             .png files can be used in a tweet
       */
        private void SetOpenFileDialogFilter()
        {
            
            openFileDialog.Filter = "Image files(*.png)|" +
                "*.png";

        }




        /*
            Brief:
                When the user selects "No" for the "noIncludeImageRadioBtn",
                hides the image selector option from the user.
         */
        private void noIncludeImageRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            attachImagePanel.Visibility = Visibility.Collapsed;
        }



        /*
            Brief:
                When the user selects "Yes" for the "yesIncludeImageRadioBtn",
                reveals (make visible) the image selector option to the user.
         */
        private void yesIncludeImageRadioBtn_Click(object sender, RoutedEventArgs e)
        {
            attachImagePanel.Visibility = Visibility.Visible;
        }


        /*
           Brief: When the user clicks on the button, allows the user
                to select an image file. Also displays the path of the 
                selected image file on the textbox
        */
        private void selectImageBtn_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Due to a limitation of the application, " +
                "only PNG files can be selected");

            this.openFileDialog.ShowDialog();

            selectedImagePathTextBox.Text = openFileDialog.FileName;

        }


        /*
           Brief: Event handler for the post tweet button

           When the post tweet event button is clicked, will attempt
            to extract the user entered input

           Post-condition:
                If all the user input entered is valid

                [1] the 'IsSuccessful' property will be set to true

                [3] the 'PublishedTweet' property will contain the
                    details of the published tweet
        */
        private async void publishTweetBtn_Click(object sender, RoutedEventArgs e)
        {
            //temporarily display the entire window
            this.IsEnabled = false;

            await PublishTweet();

            //reenable the window
            this.IsEnabled = true;

            if (this.IsSuccessful)
            {
                this.Close();
            }
        }


        /*
           Brief: An event handler for the cancel button. Closes
                the window if the cancel button is clicked
        */
        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private bool ValidateInputs()
        {
            bool inputsAreValid;

            string tweetText = this.messageTextBox.Text.Trim();

            bool userIntendsToIncudeImage =
                    (yesIncludeImageRadioBtn.IsChecked == true);

            
            string imagePath = this.selectedImagePathTextBox.Text;

            bool fileExists = File.Exists(imagePath); ;




            if (tweetText == "")
            {
                MessageBox.Show("You did not enter any tweet text");

                inputsAreValid = false;

            }
            else if (userIntendsToIncudeImage && !fileExists)
            {
                MessageBox.Show("You selected 'Yes' for the " +
                    "'Include Image' option but the image is " +
                    "not selected/ does not exists");

                inputsAreValid = false;
            }
            else
            {
                inputsAreValid = true;
            }


            return inputsAreValid;
        }
    }
}
