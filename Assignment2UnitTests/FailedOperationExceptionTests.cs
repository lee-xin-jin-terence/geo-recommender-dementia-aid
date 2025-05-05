
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment2;
using System;

namespace Assignment2UnitTests
{
    [TestClass]
    public class FailedOperationExceptionTests
    {
        /*
            Brief: Test that the constructor (without
                any arguments) is working correctly
         */
        [TestMethod]
        public void FailedOperationExceptionConstructorTest_ValidNoArgument()
        {
            Assert.ThrowsException<FailedOperationException>(
                ()=>throw new FailedOperationException()  );
        }


        /*
            Brief: Test that the constructor (with one argument) 
            is working correctly, where it is able to initialize
            the message property
         */
        [TestMethod]
        public void FailedOperationExceptionConstructorTest_ValidSingleArgument()
        {
            FailedOperationException exception =
                    new FailedOperationException("Some Failure Message");

            Assert.AreEqual(exception.Message, "Some Failure Message");

            Assert.ThrowsException<FailedOperationException>(
                () => throw exception);

        }


        /*
            Brief: Test that the constructor (with one argument) 
            is working correctly, where it is able to initialize
            the message property and the inner exception property
         */
        [TestMethod]
        public void FailedOperationExceptionConstructorTest_ValidTwoArguments()
        {
            Exception innerException = new Exception();

            FailedOperationException outerException =
                        new FailedOperationException("Some Failure Message",
                                                        innerException) ;


            Assert.AreEqual(outerException.Message, "Some Failure Message");
            Assert.AreEqual(outerException.InnerException, innerException);

            Assert.ThrowsException<FailedOperationException>(
                ()=>throw outerException);
        
        }
    }
}
