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
    public class agencyFactoryTests
    {
        private AgencyFactory factory;
        private void testFactorySetup()
        {
            // make a fake db context using moq and the following data:
            List<agency> dataList = new List<agency>
            {
                new agency { agency_id = 1, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                new agency { agency_id = 2, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                new agency { agency_id = 3, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = new DateTime(2017,1,17,19,20,10)}
            };
            IQueryable<agency> data = dataList.AsQueryable();

            var mockSet = new Mock<DbSet<agency>>();
            mockSet.As<IQueryable<agency>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<agency>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<agency>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<agency>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ITS_MobileNewsEntities>();
            mockContext.Setup(c => c.agencies).Returns(mockSet.Object);
            factory = new AgencyFactory(mockContext.Object);
        }

        //TESTS

        [TestMethod]
        public void GetagenciesReturnsAllUndeletedRecords()
        {
            // if date is NOT passed filter out deleted records
            testFactorySetup();
            List<int> agencies = new List<int>() { 1, 2, 3 };
            var result = factory.Getagencies(agencies);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<agency>));
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetagenciesReturnsAllRecentRecords()
        {
            // if date is passed in DO NOT filter out deleted records
            testFactorySetup();
            List<int> agencies = new List<int>() { 1, 2, 3 };
            var result = factory.Getagencies(agencies, "2017-01-01");
            Assert.IsInstanceOfType(result, typeof(IEnumerable<agency>));
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetagenciesDoesNotReturnOldRecords()
        {
            // if date is passed in DO NOT filter out deleted records
            testFactorySetup();
            List<int> agencies = new List<int>() { 1, 2, 3 };
            var currentDate = DateTime.Now;
            var dateString = currentDate.ToShortDateString();
            var result = factory.Getagencies(agencies, dateString);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<agency>));
            Assert.AreEqual(0, result.Count());
        }
    }
}
