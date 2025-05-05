
/*
    Brief: An interface for classes that stores GPS locations

    Author: Terence Lee Xin Jin
    Date Created: 7 August 2021


    Extract Interface Refactoring: 
        This interface was extracted from the class GPSLocation
 */
namespace Assignment2
{
    public interface IGPSLocation
    {
        public double Latitude
        {
            set; get;
        }


        public double Longitude
        {
            set; get;
        }
    }
}
