
/*
    Brief: An interface for classes that have a single location

    Author: Terence Lee Xin Jin
    Date Created: 7 August 2021
 */
namespace Assignment2
{
    public interface ISingleLocationInvolved
    {

        public IGPSLocation GPSLocation
        {
            set; get;
        }
    }
}
