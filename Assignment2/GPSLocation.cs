using System;


/*
    Brief: A simple class that stores the gps location. Stores latitude
            and longitude

    Author: Terence Lee Xin Jin
    Date Created: 23/6/2021


    Extract Interface Refactoring: 
                The interface IGPSLocation was extracted from this
                 class GPSLocation. This class then implemented
                 the interface IGPSLocation
*/

namespace Assignment2
{
    public class GPSLocation: IGPSLocation
    {

        private double longitude = 0;
        private double latitude = 0;



        /*
            Brief: Creates an instance of GPSLocation 

         */
        public GPSLocation()
        { 
        
        }




        /*
            Brief: Creates an instance of GPSLocation 

            Argument:
                [1] latitudeVal - the value of the latitude to be set
                [2] longitudeVal - the value of the longitude to be set

            Exceptions:
                throws ArgumentOutOfRange exception if values are not valid.
                Refer to the "Latitude" and "Longitude" properties for more
                details
         */

        public GPSLocation(double latitudeVal, double longitudeVal)
        {
            Latitude = latitudeVal;
            Longitude = longitudeVal;
            
        }



        /*
            Brief: Sets and gets the latitude of the GPS Location.
                   Valid values are from -90 (inclusive) to 90 (inclusive)

            Exception:
                Thorws an ArgumentOutOfRange exception if attempt to set
                a value outside the valid range
                
        
            Return:
                If the latitude has not been previously set, the getter returns the
                default value of 0
         */
        public double Latitude
        {
            set
            {
                if (value < -90 || value > 90)
                {
                    throw new ArgumentOutOfRangeException(
                            "Latitude must be between -90 (inclusive) and " +
                            "90 (inclusive)");
                }

                latitude = value;
            }


            get
            {
                return latitude;
            }
        }




        /*
            Brief: Sets and gets the longitude of the GPS Location.
                   Valid values are from -180 (inclusive) to 180 (inclusive)

            Exception:
                Thorws an ArgumentOutOfRange exception if attempt to set
                a value outside the valid range

            Return:
                If the longitude has not been previously set, the getter returns the
                default value of 0
                
         */
        public double Longitude
        {
            set
            {
                if (value < -180 || value > 180)
                {
                    throw new ArgumentOutOfRangeException(
                        "Longitude must be between -180 (inclusive) and " +
                        "180 (inclusive)");
                }

                longitude = value; 
            }
            

            get 
            {
                return longitude;
            }
        }


        public override bool Equals(object obj)
        {
            

            GPSLocation otherLocation = obj as GPSLocation;


            if (otherLocation == null)
            {
                return false;
            }

            return this.Latitude == otherLocation.Latitude
                                &&
                   this.Longitude == otherLocation.Longitude;
        }


        /*
            Brief: Returns the latitude and longitude of the gps location 
                separated by a comma, with latitude and longitude values
                being in 4 decimal places
                
         */
        public override string ToString()
        {
            return string.Format("{0:N4}, {1:N4}", latitude, longitude); ;
        }
    }
}
