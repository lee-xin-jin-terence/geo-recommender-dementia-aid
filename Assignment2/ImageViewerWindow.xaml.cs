using System;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Media.Imaging;

/*
    Brief: A Window that downloads and displays image
            from the Internet
            
    Author: Terence Lee Xin Jin
    Date Created: 7 August 2021
 */
namespace Assignment2
{
    
    public partial class ImageViewerWindow : Window
    {

        /*
          Brief: Displays the photo in a new form

          Note: You still need to call ".ShowDialog()" to make the
                form window visible after calling this constructor

          Argument:
            [1] imageURL - the URL of the image to the downloaded and
                    displayed from the Internet
       */
        public ImageViewerWindow(string imageURL)
        {
            InitializeComponent();

            DownloadAndDisplayImage(imageURL);
        }




        /*
         Brief: Displays the image on the window. If the image URL is
               not correct, then an error message will be displayed

         Argument:
           [1] imageURL - the URL of the image to the downloaded and
                    displayed from the Internet
      */
        private async void DownloadAndDisplayImage(string imageURL)
        {

            byte[] imageDataInBytes;

          
            BitmapImage imageSource = new BitmapImage();

            
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(imageURL);
                    imageDataInBytes = await response.Content.ReadAsByteArrayAsync();
                    MemoryStream stream = new MemoryStream(imageDataInBytes);
                    BitmapImage image = new BitmapImage();

                    using (var memoryStream = new MemoryStream(imageDataInBytes))
                    {
                        memoryStream.Position = 0;

                        image.BeginInit();
                        image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = memoryStream;
                        image.EndInit();         
                    }

                    image.Freeze();
                    imageContainer.Source = image;
                }
                catch (Exception e)
                {
                    MessageBox.Show("An Error has occured: " + e.Message);

                    this.Close();
                }

            }
            

        }
    }
}
