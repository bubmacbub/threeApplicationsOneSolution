using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileNewsModel.Entities;
using MobileNewsBusinessLogic.Admin;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace MobileNewsFactoryTests
{
    [TestClass]
    public class servicesFactoryTests
    {
        private ServiceFactory factory;
        private void testFactorySetup()
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

            var mockContext = new Mock<ITS_MobileNewsEntities>();
            mockContext.Setup(c => c.services).Returns(mockSet.Object);
            factory = new ServiceFactory(mockContext.Object);
        }
        //TESTS

        [TestMethod]
        public void FindservicesPerAgencyIdListReturnsAllServices()
        {
            // If NO date is passed in to FindservicesPerAgencyIdList the app is initializing, 
            // do not send deleted records

            testFactorySetup();
            List<int> agencies = new List<int>() { 1, 2 };
            var result = factory.FindservicesPerAgencyIdList(agencies);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<service>));
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void FindservicesPerAgencyIdListDoesNotReturnOldRecords()
        {
            // If a date is passed in to FindNewsPerAgencyList the app is updating, 
            // send all recent records, including deleted ones. 
            // The app needs to know about recent deletions. 
            // Here the date is now so all records are old.

            testFactorySetup();
            List<int> agencies = new List<int>() { 1, 2 };
            var currentDate = DateTime.Now;
            var dateString = currentDate.ToShortDateString();
            var result = factory.FindservicesPerAgencyIdList(agencies, dateString);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<service>));
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void FindservicesPerAgencyIdListDoesReturnRecentRecords()
        {
            // If a date is passed in to FindNewsPerAgencyList the app is updating, 
            // send all recent records, including deleted ones. 
            // The app needs to know about recent deletions.
            // Here the date is old so all the records are recent.

            testFactorySetup();
            List<int> agencies = new List<int>() { 1, 2 };
            var cutoffDate = new DateTime(2016, 1, 17, 19, 10, 10);
            var dateString = cutoffDate.ToShortDateString();
            var result = factory.FindservicesPerAgencyIdList(agencies, dateString);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<service>));
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(result.ElementAt(0).title, "first service");
        }
    }
}
