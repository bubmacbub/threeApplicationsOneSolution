using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileNewsService.Controllers;
using MobileNewsService.Models;
using System.Collections.Generic;
using MobileNewsModel.Entities;
using Moq;
using System.Linq;
using System.Data.Entity;
using System.Web.Http.Results;


namespace MobileNewsServiceControllersTests
{
    [TestClass]
    public class locationControllerTests
    {
        private locationsController controller;

        private void testControllerSetup()
        {
            // make a fake db context using moq and the following data:
            List<location> locationList = new List<location>
            {
                new location { location_id = 1, agency_id = 1, address = "first service", city = "service content", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null, language =  new language { language_id = 1, language_short = "en", language_name = "english", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null}},
                new location { location_id = 2, agency_id = 1, address = "second service", city = "service content", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null, language = new language { language_id = 1, language_short = "en", language_name = "english", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null}},
                new location { location_id = 3, agency_id = 1, address = "deleted service", city = "service content", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = new DateTime(2017,1,17,19,20,10), language =  new language { language_id = 1, language_short = "en", language_name = "english", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null}}
            };
            IQueryable<location> locationData = locationList.AsQueryable();
            var mockLocSet = new Mock<DbSet<location>>();
            mockLocSet.As<IQueryable<location>>().Setup(m => m.Provider).Returns(locationData.Provider);
            mockLocSet.As<IQueryable<location>>().Setup(m => m.Expression).Returns(locationData.Expression);
            mockLocSet.As<IQueryable<location>>().Setup(m => m.ElementType).Returns(locationData.ElementType);
            mockLocSet.As<IQueryable<location>>().Setup(m => m.GetEnumerator()).Returns(locationData.GetEnumerator());

            List<application_agency> aaList = new List<application_agency>
            {
                new application_agency { application_agency_id = 1, agency_id = 1, application_id = 2, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                new application_agency { application_agency_id = 2, agency_id = 1, application_id = 3, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                new application_agency { application_agency_id = 3, agency_id = 2, application_id = 3, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null}
            };
            IQueryable<application_agency> aaData = aaList.AsQueryable();
            var mockAaSet = new Mock<DbSet<application_agency>>();
            mockAaSet.As<IQueryable<application_agency>>().Setup(m => m.Provider).Returns(aaData.Provider);
            mockAaSet.As<IQueryable<application_agency>>().Setup(m => m.Expression).Returns(aaData.Expression);
            mockAaSet.As<IQueryable<application_agency>>().Setup(m => m.ElementType).Returns(aaData.ElementType);
            mockAaSet.As<IQueryable<application_agency>>().Setup(m => m.GetEnumerator()).Returns(aaData.GetEnumerator());
 
            var mockContext = new Mock<ITS_MobileNewsEntities>();
            mockContext.Setup(c => c.locations).Returns(mockLocSet.Object);
            mockContext.Setup(c => c.application_agency).Returns(mockAaSet.Object);
            controller = new locationsController(mockContext.Object);
        }

        // TESTS

        [TestMethod]
        public void GetlocationsReturnsAllRecords()
        {
            // If NO date is passed in to FindservicesPerAgencyIdList the app is initializing, 
            // do not send deleted records

            testControllerSetup();
            var actionResult = controller.Getlocations(3);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<locationViewModel>>));
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<locationViewModel>>;
            Assert.AreEqual(3, response.Content.Count());

        }

        [TestMethod]
        public void GetlocationsDoesNotReturnOldRecords()
        {
            // If a date is passed in to Getlocations the app is updating, 
            // send all recent records, including deleted ones. 
            // The app needs to know about recent deletions. 
            // Here the date is now so all records are old.

            testControllerSetup();
            var currentDate = DateTime.Now;
            var dateString = currentDate.ToShortDateString();
            var actionResult = controller.Getlocations(3, dateString);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<locationViewModel>>));
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<locationViewModel>>;
            Assert.AreEqual(0, response.Content.Count());
        }

        [TestMethod]
        public void GetlocationsDoesReturnRecentRecords()
        {
            // If a date is passed in to Getlocations the app is updating, 
            // send all recent records, including deleted ones. 
            // The app needs to know about recent deletions. 
            // Here the date is old so all records are recent.

            testControllerSetup();
            var cutoffDate = new DateTime(2016, 1, 17, 19, 10, 10);
            var dateString = cutoffDate.ToShortDateString();
            var actionResult = controller.Getlocations(3, dateString);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<locationViewModel>>));
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<locationViewModel>>;
            Assert.AreEqual(3, response.Content.Count());
        }
    }
}
