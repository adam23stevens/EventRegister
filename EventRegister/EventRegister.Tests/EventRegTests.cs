using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EventRegister.Business.RepoContract;
using EventRegister.Business.Sql;
using System.Collections.Generic;
using EventRegister.Business.Service;
using EventRegister.Business.RepoContract.Model;

namespace EventRegister.Tests
{
    [TestClass]
    public class EventRegTests
    {
        [TestMethod]
        public void IsAlreadyRegistered()
        {
            //Arrange
            Mock<IEventRegRepo> mock = new Mock<IEventRegRepo>();
            mock.Setup(m => m.GetAllEvents()).Returns(new List<Event>
            {
                new Event
                {
                     Name = "Testing Market",
                     UkStartTime = new DateTime(2017, 5, 1, 12,30,0),
                     UkEndTime = new DateTime(2017, 5, 1, 18, 0, 0),
                     RegisteredEmails = new List<string> { "adam23stevens@gmail.com" },
                     Location = new Location
                     {
                          City = "London",
                          Country = new Country
                          {
                              Name = "United Kingdom",
                              CountryCode = "UK"
                          },
                          LocationTimezone = new LocationTimezone
                          {
                              Id = "UK",
                              Name = "GMT Standard Time"
                          }
                     }
                }
            });

            RegisterReq req = new RegisterReq
            {
                EventName = "Testing Market",
                Country = "UK",
                ArrivalTime = new DateTime(2017, 5, 1, 11, 0, 0),
                RegistrationDate = new DateTime(2017, 04, 30, 23, 18, 0),
                FirstName = "Adam",
                LastName = "Stevens",
                EmailAddress = "adam23stevens@gmail.com"

            };
        
            //Act
            EventRegService service = new EventRegService(mock.Object);
            service.ValidateRegister(ref req);

            //Assert
            Assert.IsTrue(req.IsError);
            Assert.IsTrue(req.Error.Length > 0);
        }
    }
}
