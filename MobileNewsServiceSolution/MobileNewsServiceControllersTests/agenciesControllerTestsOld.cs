using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileNewsServices.Controllers;
using MobileNewsModel.Entities;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using MobileNewsServices.Models;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Results;
using System.Threading.Tasks;
namespace MobileNewsServiceControllersTests
{
    [TestClass]
    public class agenciesControllerTests
    {
        private agenciesController controller;
        private void testControllerSetup()
        {
            // make a fake db context using moq and the following data:

            List<agency> agencyList = new List<agency>
            {
                new agency { agency_id = 1, agency_name = "An Agency", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                new agency { agency_id = 2, agency_name = "second agency", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                new agency { agency_id = 3, agency_name = "deleted agency", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = new DateTime(2017,1,17,19,20,10)}
            };
            IQueryable<agency> agencyData = agencyList.AsQueryable();
            var mockSet = new Mock<DbSet<agency>>();
            mockSet.As<IQueryable<agency>>().Setup(m => m.Provider).Returns(agencyData.Provider);
            mockSet.As<IQueryable<agency>>().Setup(m => m.Expression).Returns(agencyData.Expression);
            mockSet.As<IQueryable<agency>>().Setup(m => m.ElementType).Returns(agencyData.ElementType);
            mockSet.As<IQueryable<agency>>().Setup(m => m.GetEnumerator()).Returns(agencyData.GetEnumerator());

            mockSet.Setup(m => m.Find(It.IsAny<int>()))
                         .Returns<object[]>(ids => agencyData.FirstOrDefault(y => (int)typeof(agency).GetProperty("agency_id").GetValue(y, null) == (int)ids[0]));
            mockSet.Setup(b => b.FindAsync(It.IsAny<object[]>()))
                         .Returns<object[]>(ids => agencyData.FirstOrDefaultAsync(b => b.agency_id == (int)ids[0]));
            /*
             mockDbSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns<object[]>(x => (sourceList as List<MySecondSet>).FirstOrDefault(y => y.MySecondSetKey == (Guid)x[0]) as T);
             */
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
            /*
                        List<language> languageList = new List<language>
                        {
                            new language { language_id = 1, language_short = "en", language_name = "english", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                            new language { language_id = 2, language_short = "es", language_name = "spanish", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                            new language { language_id = 3, language_short = "fr", language_name = "french", created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = new DateTime(2017,1,17,19,20,10)}
                        };
                        IQueryable<language> languageData = languageList.AsQueryable();
                        var mockLangSet = new Mock<DbSet<language>>();
                        mockLangSet.As<IQueryable<language>>().Setup(m => m.Provider).Returns(languageData.Provider);
                        mockLangSet.As<IQueryable<language>>().Setup(m => m.Expression).Returns(languageData.Expression);
                        mockLangSet.As<IQueryable<language>>().Setup(m => m.ElementType).Returns(languageData.ElementType);
                        mockLangSet.As<IQueryable<language>>().Setup(m => m.GetEnumerator()).Returns(languageData.GetEnumerator());
            */
            var mockContext = new Mock<ITS_MobileNewsEntities>();
            mockContext.Setup(c => c.agencies).Returns(mockSet.Object);
            mockContext.Setup(c => c.application_agency).Returns(mockAaSet.Object);
            //mockContext.Setup(c => c.languages).Returns(mockLangSet.Object);
            controller = new agenciesController(mockContext.Object);
        }

        [TestMethod]
        public void GetagenciesReturnsAllRecordsForAppId_Test()
        {
            testControllerSetup();
            var actionResult = controller.Getagencies(3);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<agencyViewModel>>));
            var response = actionResult as OkNegotiatedContentResult<IEnumerable<agencyViewModel>>;
            Assert.AreEqual(2, response.Content.Count());
        }

        [TestMethod]
        public async Task GetagencyReturnsCorrectRecord_Test()
        {
            testControllerSetup();
            var actionResult = await controller.Getagency(1);
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<agency>));
            var response = actionResult as OkNegotiatedContentResult<agency>;
            Assert.AreEqual(1, response.Content.agency_id);
        }
// This test is failing because we decided to return deleted records so they could be synced.
/*
        [TestMethod]
        public void GetagencyDoesNotReturnDeletedRecord_Test()
        {
            testControllerSetup();
            var actionResult = controller.Getagency(3);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

        }
*/
    }
}
