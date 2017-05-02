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
    public class locationFactoryTests
    {
        private LocationFactory factory;
        private void testFactorySetup()
        {
            // make a fake db context using moq and the following data:
            List<location> dataList = new List<location>
            {
                new location { location_id = 1, agency_id = 1, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                new location { location_id = 2, agency_id = 1, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                new location { location_id = 3, agency_id = 2, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = new DateTime(2017,1,17,19,20,10)}
            };
            IQueryable<location> data = dataList.AsQueryable();

            var mockSet = new Mock<DbSet<location>>();
            mockSet.As<IQueryable<location>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<location>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<location>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<location>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

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
            mockContext.Setup(c => c.locations).Returns(mockSet.Object);
            mockContext.Setup(c => c.application_agency).Returns(mockAaSet.Object);
            factory = new LocationFactory(mockContext.Object);
        }

        // TESTS
/*
        [TestMethod]
        public void FindLocationsByAgencyListReturnsAllRecords()
        {
            testFactorySetup();
            List<int> agencyList = new List<int> { 1, 2 };
            var result = factory.FindLocationsByAgencyList(agencyList);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<location>));
            Assert.AreEqual(3, result.Count());
        }
*/
        [TestMethod]
        public void FindLocationsByAgencyListReturnsAllUndeletedRecords()
        {
            // If no date is passed in to FindLocationsByAgencyList the app is initializing, 
            // do not send deleted records

            testFactorySetup();
            List<int> agencyList = new List<int> { 1, 2 };
            //var oldDate = new DateTime(2017, 1, 1, 1, 1, 1);
            //var dateString = oldDate.ToShortDateString();
            //Console.WriteLine(dateString);
            var result = factory.FindLocationsByAgencyList(agencyList); // , dateString
            Assert.IsInstanceOfType(result, typeof(IEnumerable<location>));
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void FindLocationsByAgencyListDoesNotReturnOldRecords()
        {
            // If a date is passed in to FindLocationsByAgencyList the app is updating, 
            // send all recent records, including deleted ones. 
            // The app needs to know about recent deletions

            testFactorySetup();
            List<int> agencyList = new List<int> { 1, 2 };
            var currentDate = DateTime.Now;
            var dateString = currentDate.ToShortDateString();
            var result = factory.FindLocationsByAgencyList(agencyList, dateString);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<location>));
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void FindLocationsByAgencyListDoesReturnRecentRecords()
        {
            // If a date is passed in to FindLocationsByAgencyList the app is updating, 
            // send all recent records, including deleted ones. 
            // The app needs to know about recent deletions.
            // Here the date is old so all the records are recent.

            testFactorySetup();
            List<int> agencies = new List<int>() { 1, 2 };
            var cutoffDate = new DateTime(2016, 1, 17, 19, 10, 10);
            var dateString = cutoffDate.ToShortDateString();
            var result = factory.FindLocationsByAgencyList(agencies, dateString);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<location>));
            Assert.AreEqual(3, result.Count());
        }

    }
}
