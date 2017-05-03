using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileNewsService.Controllers;
using MobileNewsService.Models;
using MobileNewsModel.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Http.Results;
using System.Web.Http;


namespace MobileNewsServiceControllersTests
{
    [TestClass]
    public class newsControllerTests
    {
        private newsController controller;

        private void testControllerSetup()
        {
            // make a fake db context using moq and the following data:
            List<news> dataList = new List<news>
                {
                    new news { news_id = 1, agency_id = 1, news_title = "first article", created_date = new DateTime(2017,1,17,19,10,10), publish_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                    new news { news_id = 2, agency_id = 1, news_title = "second article", created_date = new DateTime(2017,1,17,19,10,10), publish_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                    new news { news_id = 3, agency_id = 1, news_title = "deleted article", created_date = new DateTime(2017,1,17,19,10,10), publish_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = new DateTime(2017,1,17,19,20,10)}
                };
            IQueryable<news> data = dataList.AsQueryable();

            var mockSet = new Mock<DbSet<news>>();
            mockSet.As<IQueryable<news>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<news>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<news>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<news>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

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
            mockContext.Setup(c => c.news).Returns(mockSet.Object);
            mockContext.Setup(c => c.application_agency).Returns(mockAaSet.Object);
            controller = new newsController(mockContext.Object);
        }

        [TestMethod]
        public void GetnewsReturnsAllUndeletedRecords()
        {
            testControllerSetup();
            var actionResult = controller.Getnews(3);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<newsViewModel>>));
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<newsViewModel>>;
            Assert.AreEqual(2, response.Content.Count());
        }


        [TestMethod]
        public void GetnewsDoesNotReturnOldRecords()
        {
            // If a date is passed in to FindNewsPerAgencyList the app is updating, 
            // send all recent records, including deleted ones. 
            // The app needs to know about recent deletions. 
            // Here the date is now so all records are old.

            testControllerSetup();
            var currentDate = DateTime.Now;
            var dateString = currentDate.ToShortDateString();
            var actionResult = controller.Getnews(3, dateString);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<newsViewModel>>));
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<newsViewModel>>;
            Assert.AreEqual(0, response.Content.Count());
        }

        [TestMethod]
        public void GetnewsDoesReturnRecentRecords()
        {
            // If a date is passed in to FindNewsPerAgencyList the app is updating, 
            // send all recent records, including deleted ones. 
            // The app needs to know about recent deletions. 
            // Here the date is old so all records are recent.

            testControllerSetup();
            var cutoffDate = new DateTime(2016, 1, 17, 19, 10, 10);
            var dateString = cutoffDate.ToShortDateString();
            var actionResult = controller.Getnews(3, dateString);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<newsViewModel>>));
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<newsViewModel>>;
            Assert.AreEqual(3, response.Content.Count());
        }
    }
}

