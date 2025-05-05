using System.Collections.Generic;
using System.Threading.Tasks;


/*
    Brief: An interface for classes that have interacts with
            social media sites 

    Author: Terence Lee Xin Jin
    Date Created: 7 August 2021

    Extract Interface Refactoring:
        This interface was extracted from TwitterClient, so
        that in the future, if there is any new social media clients
        (e.g. Facebook) that is to be added, these new client classes
        can simply implement this interface.

        Allow for allow social media clients to share a common 
        interface.
 */
namespace Assignment2
{
    public interface ISocialMediaClient
    {
        Task<List<ISocialMediaPost>> GetListOfRecentPostsAsync();

        Task<ISocialMediaPost> PublishPostAsync(string messageTextVal,
                IGPSLocation locationVal, string imageFilePathVal = null);
    }
}