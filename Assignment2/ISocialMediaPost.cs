using System;


/*
    Brief: An interface for classes that have stores details
            of social media posts

    Author: Terence Lee Xin Jin
    Date Created: 7 August 2021
 */
namespace Assignment2
{
    public interface ISocialMediaPost: ISingleLocationInvolved
    {
        string PostID { get; set; }

        string MessageText { get; set; }

        string ImageURL { get; set; }

        DateTime DateTime { get; set; }
        
    }
}