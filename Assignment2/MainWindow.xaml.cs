
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GMap.NET.MapProviders;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using System.Xml.Linq;


/*
    Brief: The main Window of the program. Displays the map canvas
            
    Author: Terence Lee Xin Jin
    Date Created: 7 August 2021
 */
namespace Assignment2
{

    public partial class MainWindow : Window
    {

        private XDocument collaboratorXMLDocument;

        private TwitterClient twitterClient;

        private List<Collaborator> listOfCollaborators = new List<Collaborator>();

        private List <ISocialMediaPost> listOfTweets = new List<ISocialMediaPost>();


        //store the 'mapping' of map markers to tweets/collaborators
        private Dictionary<GMapMarker, ISingleLocationInvolved> mapMarkerDictionary
                = new Dictionary<GMapMarker, ISingleLocationInvolved>();


        private RoutedEventHandler previousAddCollaboratorBtnHandler;

        private RoutedEventHandler previousPublishNewTweetBtnHandler;

        private GMapMarker currentPositionMarker;


        /*
            Brief: Display the main application form that contains a
                    map canvas
         */
        public MainWindow()
        {
            InitializeComponent();

            InitializeTwitterClient();

            LoadCollaboratorXMLFile();

            AddAllCollaboratorsFromXMLToList();

            DisplayAllCollaboratorsFromListToMap();

            RetrieveTweetsFromTwitterAndDisplayOnMap();

        }



        /*
            Brief: Initialize the keys to allow the application to access Twitter
         */
        private void InitializeTwitterClient()
        {
            const string consumerKey = "[redacted]";

            const string consumerKeySecret = "[redacted]";

            const string accessToken = "[redacted]";

            const string accessTokenSecret = "[redacted]";
            

            this.twitterClient = new TwitterClient(consumerKey, consumerKeySecret,
                                                   accessToken, accessTokenSecret);
        }



        /*
           Brief: Initialize the map so that it displays the map with 
            Singapore as the center     

           Note: 
            GMap on WPF is buggy, does not allow certain functionalities
                [1] Zooming in/out using the mouse wheel
                [2] Does not allowing setting zoom level to a 'double' value,
                    only imprecise 'integer' values are allowed

            Third-party Code: 
                From GMap.NET.Core, GMap.NET.WinPresentation
                Line 106 to 127
        */
        private void InitializeMapSettings(object sender, RoutedEventArgs e)
        {
            map.MapProvider = GMapProviders.GoogleMap;

            GMapProviders.GoogleMap.ApiKey = "[redacted]";


            //The lat and longt represent the coodinates for the center of
            //singapore
            double lat = 1.360563951933723;
            double longt = 103.81556721884013;
            map.Position = new PointLatLng(lat, longt);


         
            //the zooming in WPF WPF is buggy, does not work
            // have to set the values of both on .MinZoom and .Zoom
            // in order for the map to be properly zoomed
            map.MinZoom = 12;
            map.Zoom = 12;

            //Hide the default red-cross on the map
            map.ShowCenter = false;
            map.DragButton = MouseButton.Left;

        }


        /*
           Brief: Loads the XML file containing details of the collaborators and store the
                loaded xml file in the instance variable 'collaboratorXMLDocument'

            Return:
                bool - true if successfully in loading the xml file represented by 
                        the constant XML_FILE_PATH , or false if failed to load
                        the xml file 

            Post-condition:
                if success, the 'collaboratorXMLDocument' instance variable will contain 
                the entire xml document, otherwise the variable 'collaboratorXMLDocument'
                will be null
        */
        private void LoadCollaboratorXMLFile()
        {


            const string XML_FILE_NAME = "../../../CollaboratorRecords.xml";

            try
            {
                this.collaboratorXMLDocument = XDocument.Load(XML_FILE_NAME);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load the file " + XML_FILE_NAME + 
                    " . This program will now terminate.");

                this.Close();
            }
            
        }



        /*
           Brief: Adds events stored in the xml document to the dictionary
                'listOfCollaborators' (instance variable)

           Post-condition:
               All valid collaborators from the xml file will be stored in the   
               list 'listOfCollaborators' (instace variable). Any collaborator data 
               from the xml file with  incomplete information will not be stored 
               in the list 'listOfCollaborators' 
        */
        private void AddAllCollaboratorsFromXMLToList()
        {

            XNamespace ns = "http://www.xyz.org/collaborator-records";

            IEnumerable<XElement> listOfCollaboratorElements =
                            collaboratorXMLDocument.Descendants(ns + "Collaborator");

            Collaborator collaborator;

            XElementToCollaboratorAdapter adapter;

            foreach (XElement collaboratorElement in listOfCollaboratorElements)
            {
                adapter = new XElementToCollaboratorAdapter(ns, collaboratorElement);

                collaborator = adapter.ConvertXElementToCollaborator();


                if (collaborator != null)
                {
                    listOfCollaborators.Add(collaborator);
                }
            }
        }



        /*
          Brief: Display all the collaborators found in the list 
            'listOfCollaborators' (instance variable) onto the map

          Post-condition:
               all the collaborators will be displayed on the map with
                the collaborator icons on the screen
       */
        private void DisplayAllCollaboratorsFromListToMap()
        {
            foreach (Collaborator currentCollaborator in this.listOfCollaborators)
            {
                PlotSingleMarkerOnMap(currentCollaborator);
            }
        }



        /*
           Brief: Retrieve a list of tweets from Twitter (tweets associated
                with the Twitter account from the instance variable 'twitterClient').
                These tweets will be displayed on the map with the Twitter icons

           Post-condition:
               [1] All valid tweets ret will be stored in the list 'listOfTweets'. 
               Any tweet from Twitter that does not contain location inforation 
               will not be stored in the list 'listOfTweets' (instance variable)
            
               [2] All tweets will be represented on the map with the Tweet icons
        */
        private async void RetrieveTweetsFromTwitterAndDisplayOnMap()
        {

            try
            {
                this.listOfTweets =
                    await this.twitterClient.GetListOfRecentPostsAsync();


                foreach (var singleTwitterTweet in this.listOfTweets)
                {

                    if (singleTwitterTweet.GPSLocation != null)
                    {

                        PlotSingleMarkerOnMap(singleTwitterTweet);
                    }
                    
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load tweets from Twitter. This " +
                    "program will now terminate");
                this.Close();
            }

            
        }




        /*
           Brief: When the user clicks on a position on the map, the right panel 
                will be made visible. The user will be able to add a new collaborator
                or post a tweet to the map location where he/she clicked

            Note: As GMap does not support the "Click" event, I have to use the
                  alternative MouseLeftButtonDown event

            Third-party Code: 
                From GMap.NET.Core, GMap.NET.WinPresentation
                Line 288 to 289
        */
        private void map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            System.Windows.Point mousePoint = e.GetPosition(map);
           
            //The WPF version of GMap does not access screen coordinates in
            // 'double' data type, so I am forced to type-cast it to integer
            PointLatLng mapPosition = map.FromLocalToLatLng((int)mousePoint.X,
                                                            (int)mousePoint.Y);

            GPSLocation gpsLocation = ConvertPointLatLngToGPSLocation(mapPosition);

            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                menuButtonsGrid.Visibility = Visibility.Visible;

           
                DisplayClickedCoordinates(mapPosition);

                DisplayCurrentLocationIcon(mapPosition);

                SetUpPublishNewTweetHandler(gpsLocation);

                SetUpAddNewCollaboratorHandler(gpsLocation);

            }

        }


        /*
          Brief: Convert a PointLatLng location to a GPSLocation
               instance

          Arguments:
                location - the location of interest

       */
        private static GPSLocation ConvertPointLatLngToGPSLocation(PointLatLng point)
        {

            return new GPSLocation(point.Lat, point.Lng);
        }


        /*
           Brief: Display on the right panel the coordinates that the user just clicked  
        */
        private void DisplayClickedCoordinates(PointLatLng position)
        {
            coordinatesClickedTextBox.Text = string.Format("{0:N4}, {1:N4}", position.Lat, position.Lng);
        }



        /*
           Brief: Set up the event handler to call the publish new tweet window.
                Also, deletes the previous publish new tweet handler.

            Note: It is necessary to delete the previous event handler as C# stores
                a list of event handlers for a particular event. C# does not support
                a .Clear() method to clear all existing event handlers
        */
        private void SetUpPublishNewTweetHandler(GPSLocation location)
        {
            if (this.previousPublishNewTweetBtnHandler != null)
            {
                publishNewTweetBtn.Click -= this.previousPublishNewTweetBtnHandler;
            }


            this.previousPublishNewTweetBtnHandler = (sender, e) => PublishNewTweet(location);

            publishNewTweetBtn.Click += this.previousPublishNewTweetBtnHandler;
        }



        /*
           Brief: Displays an PromptTweetDetails window that
                prompts the user of details of the new
                tweet to publish to twitter.

            Post-condition:
                If the user successfully enter all the collaborator 
                details, the new tweet will be
                (a) Published to Twitter
                (a) Stored in the dictionary 'tweetDictionary'
            
        */
        private void PublishNewTweet(GPSLocation location)
        {

            PromptTweetDetailsWindow promptWindow =
                new PromptTweetDetailsWindow(this.twitterClient, location);


            promptWindow.ShowDialog();

            ISocialMediaPost publishedTweet;


            if (promptWindow.IsSuccessful)
            {
                publishedTweet = promptWindow.PublishedTweet;


                listOfTweets.Add(publishedTweet);

                PlotSingleMarkerOnMap(publishedTweet);

            }
        }





        /*
           Brief: Set up the event handler to call the add new collaborator window.
                Also, deletes the previous add new event handler.

            Note: It is necessary to delete the previous event handler as
                C# stores a list of event handlers for a particular event.
                C# does not support a .Clear() method to clear all existing
                event handlers
        */
        private void SetUpAddNewCollaboratorHandler(GPSLocation location)
        {
            if (this.previousAddCollaboratorBtnHandler != null)
            {
                addCollaboratorBtn.Click -= this.previousAddCollaboratorBtnHandler;
            }


            this.previousAddCollaboratorBtnHandler = (sender, e) => AddNewCollaborator(location);

            addCollaboratorBtn.Click += this.previousAddCollaboratorBtnHandler;
        }



        /*
           Brief: Displays an PromptCollaboratorDetails window that
                prompts the user of details of the new
                collaborator to add.

            Post-condition:
                If the user successfully enter all the collaborator 
                details, the new collaborator will be
                (a) Stored in the dictionary 'collaboratorDictionary'
                (b) Saved to the in-memory XML document 'collaboratorXMLDocument'
                (c) Save the changes to the external XML file
            
        */
        private void AddNewCollaborator(GPSLocation location)
        {
            Collaborator newCollaborator;

            string newCollaboratorID = GenerateNewCollaboratorID();

            PromptCollaboratorDetailsWindow promptWindow =
                        new PromptCollaboratorDetailsWindow(newCollaboratorID, location);


            promptWindow.ShowDialog();

       
            if (promptWindow.IsSuccessful)
            {
                newCollaborator = promptWindow.Collaborator;

                listOfCollaborators.Add(newCollaborator);

                PlotSingleMarkerOnMap(newCollaborator);
     
                AddNewCollaboratorToXMLDocument(newCollaborator);

                SaveXMLDocumentToFile();
            }
        }



        /*
           Brief: Generate an event id for a new event to be added

            Return:
                string - an event id in the format of "ID{somenumber}"
        */
        private string GenerateNewCollaboratorID()
        {
            int numOfExistingEvents = listOfCollaborators.Count;


            return "ID" + (numOfExistingEvents + 1);
        }




        /*
          Brief: Plot a single marker on the map. This marker
                may be representing a collaborator or a
                Tweet

          Argument:
            objectOfInterest - the object to be plotted on the map



          Post-conditions:
            [1] The map will be plotted with the marker(s) of the
                associated object (collaborator or Tweet) 
            [2] The instance variable 'markerDictionary' will be
                updated where it stores the map marker to its associated
                object instance ('objectOfInterest')

            [3] Each map marker will be set with an click event handler that
                allows user to view the collaborator or tweet by clicking
                on the map marker
            

            Third-party Code: 
                From GMap.NET.Core, GMap.NET.WinPresentation
                Line 510 to 548
       */
        public void PlotSingleMarkerOnMap(ISingleLocationInvolved objectOfInterest)
        {
            PointLatLng singleMapPoint;
            GMapMarker singleMapPointMarker = null;
            Image mapPointMarkerIcon = null;

            Collaborator collaboratorObj = null;
            TwitterPost tweetPostObj = null;


            singleMapPoint =
                    ConvertGPSLocationToPointLatLng(objectOfInterest.GPSLocation);

            
            if (objectOfInterest is TwitterPost)
            {
                //tweet event
                tweetPostObj = objectOfInterest as TwitterPost;
                mapPointMarkerIcon = GetMapMarkerIcon("TWITTER");
                mapPointMarkerIcon.MouseLeftButtonDown += 
                        (sender, e) => DisplayTweetPostDetails(tweetPostObj);

            }
            else
            {
                //is a collaborator
                collaboratorObj = objectOfInterest as Collaborator;
                mapPointMarkerIcon = GetMapMarkerIcon("COLLABORATOR");
                mapPointMarkerIcon.MouseLeftButtonDown += 
                        (sender, e) => DisplayCollaboratorDetails(collaboratorObj);
            }


            //Plots a single marker on the map
            singleMapPointMarker = new GMapMarker(singleMapPoint);

            singleMapPointMarker.Shape = mapPointMarkerIcon;

            mapMarkerDictionary.Add(singleMapPointMarker, objectOfInterest);

            map.Markers.Add(singleMapPointMarker);
        }



        /*
           Brief: Converts a GPSLocation to a GMap.PointLatLng

           Arguments:
                location - the location of interest

           Return:
               the PointLatLng equivalent of the GPSLocation argument
                
        */
        private static PointLatLng ConvertGPSLocationToPointLatLng(IGPSLocation location)
        {

            return new PointLatLng(location.Latitude, location.Longitude);
        }


        


        /*
            Brief: Return a map marker icon based on requested type

            Argument:
                [1] iconType - a string with three acceptable values:
                    [a] "TWITTER" - for a twitter map marker icon
                    [b] "COLLABORATOR" - for a collaborator map marker icon
                    [c] "CURRENT-LOCATION" - an icon representing current location
        */
        private static Image GetMapMarkerIcon(string iconType)
        {
            Image markerIcon = new Image();

            markerIcon.Height = 40;
            markerIcon.Width = 40;


            switch (iconType)
            {
                case "TWITTER":
                    markerIcon.Source = new BitmapImage(
                        new Uri("pack://application:,,,/Icons/Twitter-Icon.png"));
                    break;


                case "COLLABORATOR":
                    markerIcon.Source = new BitmapImage(
                        new Uri("pack://application:,,,/Icons/Collaborator-Icon.png"));
                    break;

                case "CURRENT-LOCATION":
                    markerIcon.Source = new BitmapImage(
                        new Uri("pack://application:,,,/Icons/Current-Location-Icon.png"));
                    break;
            }



            return markerIcon;
        }



        /*
            Brief: Display a form that shows the details of the tweet post

            Argument:
                [1] tweetPost - the tweet to be displayed
         */
        private void DisplayTweetPostDetails(TwitterPost tweetPost)
        {
            DisplayTweetDetailsWindow form = new DisplayTweetDetailsWindow(tweetPost);

            form.ShowDialog();
        }


        /*
            Brief: Display a form that shows the details of the collaborator

            Argument:
                [1] collaborator - the collaborator of interest
         */
        private void DisplayCollaboratorDetails(Collaborator collaborator)
        {
            DisplayCollaboratorDetailsWindow window =
                        new DisplayCollaboratorDetailsWindow(collaborator);

            window.ShowDialog();
        }



        /*
           Brief: Adds an collaborator element to the xml document (in-memory xml document)

           Arguments:
                newCollaborator - a new collaborator to be added to the in-memory xml document

           Post-condition:
                the in-memory xml document will be appended with a new collaborator
                
        */
        private void AddNewCollaboratorToXMLDocument(Collaborator newCollaborator)
        {

            XNamespace parentElementNamespace = "http://www.w3.org/2001/12/soap-envelope";
            XNamespace eventElementNamespace = "http://www.xyz.org/collaborator-records";


            CollaboratorToXElementAdapter adapter = new CollaboratorToXElementAdapter(
                eventElementNamespace, newCollaborator);


            XElement newCollaboratorElement = adapter.ConvertCollaboratorToXElement();



            XElement currentLastCollaboratorElement;

            XElement parentElement;

            try
            {
                //works if the xml file has at least one event element,
                // but throws an exception if there are zero event elements in the xml file
                currentLastCollaboratorElement =
                   this.collaboratorXMLDocument.Descendants(eventElementNamespace + "Collaborator").Last();

                currentLastCollaboratorElement.AddAfterSelf(newCollaboratorElement);

            }
            catch (Exception)
            {
                //to handle cases when the xml document has zero collaborators
                parentElement = this.collaboratorXMLDocument.Descendants(
                                    parentElementNamespace + "Body").First();


                parentElement.Add(newCollaboratorElement);
            }
        }


        /*
           Brief: Saves the changes made to xml document to a file

           Post-condition:
                If saving is successfully, a success message will be
                displayed. If failure, error message will be displayed
        */
        private void SaveXMLDocumentToFile()
        {

            const string XML_FILE_PATH = "../../../CollaboratorRecords.xml";

            string errorString;

            try
            {
                this.collaboratorXMLDocument.Save(XML_FILE_PATH);

                MessageBox.Show("Changes successfully saved to " +
                    "the file " + XML_FILE_PATH);
            }
            catch (Exception e)
            {

                errorString = string.Format("Failed to save changes "
                    + "to the file {0}.{1}Detailed Error: {2}",
                    XML_FILE_PATH, Environment.NewLine, e.Message);


                MessageBox.Show(errorString);
            }
        }



        /*
            Brief:
                Display an icon on the map representing where the user is currently

                The previous existing icon representing where the user was previously
                will be removed, if there is any

            Argument:
                [1] currentLocationPoint - the coordinates representing where the
                        user is currently

            Third-party Code: 
                From GMap.NET.Core, GMap.NET.WinPresentation
                Line 750 to 763
         */
        private void DisplayCurrentLocationIcon(PointLatLng currentLocationPoint)
        {

            if (this.currentPositionMarker != null)
            {
                //removes the previous current location icon
                map.Markers.Remove(this.currentPositionMarker);
            }



            this.currentPositionMarker = new GMapMarker(currentLocationPoint);

            this.currentPositionMarker.Shape = GetMapMarkerIcon("CURRENT-LOCATION");
  

            map.Markers.Add(this.currentPositionMarker);

        }



       

        
    }
}
