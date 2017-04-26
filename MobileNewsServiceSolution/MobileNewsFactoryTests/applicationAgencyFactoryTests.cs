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
    public class applicationAgencyFactoryTests
    {
        private ApplicationAgencyFactory factory;

        private void testFactorySetup()
        {
            List<application_agency> dataList = new List<application_agency>
            {
                new application_agency { application_agency_id = 1, application_id = 1, agency_id = 1, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                new application_agency { application_agency_id = 2, application_id = 2, agency_id = 1, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                new application_agency { application_agency_id = 3, application_id = 2, agency_id = 2, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = new DateTime(2017,1,17,19,20,10)}
            
    };
            IQueryable<application_agency> data = dataList.AsQueryable();
            var mockSet = new Mock<DbSet<application_agency>>();
            mockSet.As<IQueryable<application_agency>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<application_agency>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<application_agency>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<application_agency>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ITS_MobileNewsEntities>();
            mockContext.Setup(c => c.application_agency).Returns(mockSet.Object);
            factory = new ApplicationAgencyFactory(mockContext.Object);
        }

        //TESTS

        [TestMethod]
        public void GetAllApplicationsAgencyIdsReturnsIntList()
        {
            testFactorySetup();
            var result = factory.GetAllApplicationsAgencyIds(2);
            Assert.IsInstanceOfType(result, typeof(List<int>));
            //var response = actionResult as OkNegotiatedContentResult<IEnumerable<newsViewModel>>;
            Assert.AreEqual(2, result.Count);

        }
    }
}
