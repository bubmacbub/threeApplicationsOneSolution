using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileNewsServices.Controllers;
using MobileNewsServices.Models;
using MobileNewsModel.Entities;
using System.Data.Entity;
using System.Web.Http.Results;
using System.Web.Http;
namespace MobileNewsServiceControllersTests
{
    /*
    [TestClass]
    public class categoriesControllerTests
    {
        private categoriesController controller;

        private void testControllerSetup()
        {
            List<category> dataList = new List<category>
            {
                new category { category_id = 1, agency_id = 1, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                new category { category_id = 2, agency_id = 1, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = null},
                new category { category_id = 3, agency_id = 1, created_date = new DateTime(2017,1,17,19,10,10), modified_date = new DateTime(2017,1,17,19,10,10), logical_delete_date = new DateTime(2017,1,17,19,20,10)}
            };
            IQueryable<application_agency> data = dataList.AsQueryable();
            var mockSet = new Mock<DbSet<application_agency>>();
            mockSet.As<IQueryable<application_agency>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<application_agency>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<application_agency>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<application_agency>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var mockContext = new Mock<ITS_MobileNewsEntities>();
            mockContext.Setup(c => c.application_agency).Returns(mockSet.Object);
            controller = new categoriesController(mockContext.Object);
        }


        [TestMethod]
        public void GetcategoriesReturnsAllRecordsTest()
        {
        }
    }
*/
}
