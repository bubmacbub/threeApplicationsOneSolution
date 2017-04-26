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
            factory = new ServiceFactory(mockContext.Object);
        }
        //TESTS

        [TestMethod]
        public void FindservicesPerAgencyIdListReturnsAllServices()
        {
            testFactorySetup();
            List<int> agencies = new List<int>() { 1, 2 };
            var result = factory.FindservicesPerAgencyIdList(agencies);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<service>));
            Assert.AreEqual(3, result.Count());
        }
    }
}
