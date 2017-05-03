using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileNewsService.Controllers;
using MobileNewsService.Models;
using MobileNewsModel.Entities;
using System.Data.Entity;
using System.Web.Http.Results;
using System.Web.Http;

namespace MobileNewsServiceControllersTests
{
    [TestClass]
    public class servicesControllerTests
    {

        private servicesController controller;

        private void testControllerSetup()
        {
            // make a fake db context using moq and the following data:
            List<service> dataList = new List<service>
            {
                new service { service_id = 1, agency_id = 1, title = "first service", service_content = "service content", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null, language =  new language { language_id = 1, language_short = "en", language_name = "english", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null}},
                new service { service_id = 2, agency_id = 1, title = "second service", service_content = "service content", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null, language =  new language { language_id = 1, language_short = "en", language_name = "english", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null}},
                new service { service_id = 3, agency_id = 1, title = "deleted service", service_content = "service content", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = new DateTime(2017,1,17,19,20,10), language =  new language { language_id = 1, language_short = "en", language_name = "english", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null}}
            };
            IQueryable<service> data = dataList.AsQueryable();

            var mockSet = new Mock<DbSet<service>>();
            mockSet.As<IQueryable<service>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<service>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<service>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<service>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<int>()))
                         .Returns<object[]>(ids => data.FirstOrDefault(y => (int)typeof(service).GetProperty("service_id").GetValue(y, null) == (int)ids[0]));
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
            mockContext.Setup(c => c.services).Returns(mockSet.Object);
            mockContext.Setup(c => c.application_agency).Returns(mockAaSet.Object);
            controller = new servicesController(mockContext.Object);
        }

        //TESTS

        [TestMethod]
        public void GetservicesReturnsAllUndeletedRecords()
        {
            // If NO date is passed in to FindservicesPerAgencyIdList the app is initializing, 
            // do not send deleted records

            testControllerSetup();
            var actionResult = controller.Getservices(3);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<serviceViewModel>>));
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<serviceViewModel>>;
            Assert.AreEqual(2, response.Content.Count());
        }

        [TestMethod]
        public void GetservicesDoesNotReturnOldRecords()
        {
            // If a date is passed in to FindNewsPerAgencyList the app is updating, 
            // send all recent records, including deleted ones. 
            // The app needs to know about recent deletions. 
            // Here the date is now so all records are old.

            testControllerSetup();
            var currentDate = DateTime.Now;
            var dateString = currentDate.ToShortDateString();
            var actionResult = controller.Getservices(3, dateString);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<serviceViewModel>>));
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<serviceViewModel>>;
            Assert.AreEqual(0, response.Content.Count());
        }

        [TestMethod]
        public void GetservicesDoesReturnRecentRecords()
        {
            // If a date is passed in to FindNewsPerAgencyList the app is updating, 
            // send all recent records, including deleted ones. 
            // The app needs to know about recent deletions. 
            // Here the date is old so all records are recent.

            testControllerSetup();
            var cutoffDate = new DateTime(2016, 1, 17, 19, 10, 10);
            var dateString = cutoffDate.ToShortDateString();
            var actionResult = controller.Getservices(3, dateString);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<serviceViewModel>>));
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<serviceViewModel>>;
            Assert.AreEqual(3, response.Content.Count());
        }

        [TestMethod]
        public void detailsReturnsOneRecord_Test()
        {
            testControllerSetup();
            var actionResult = controller.Details(1);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<serviceViewModel>));
        }

        [TestMethod()]
        public void detailsDoesNotReturnDeletedRecord_Test()
        {
            testControllerSetup();
            var actionResult = controller.Details(3);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

    }
}
