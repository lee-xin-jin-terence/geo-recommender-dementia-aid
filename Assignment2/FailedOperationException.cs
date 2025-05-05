using System;

/*
    Brief: A application exception that represents a failed
            operation

    Author: Terence Lee Xin Jin
    Date Created: 23/6/2021

*/
namespace Assignment2
{
    public class FailedOperationException: ApplicationException
    {

        public FailedOperationException()
        { 
        
        }


        public FailedOperationException(string message)
            :base(message)
        { 
            
        }



        public FailedOperationException(string message,
                                        Exception inner)
            :base(message, inner)
        { 
        
        }
    }
}
