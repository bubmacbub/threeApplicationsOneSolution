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
    public class newsFactoryTests
    {
        private NewsFactory factory;

        private void testFactorySetup()
        {
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
            var mockContext = new Mock<ITS_MobileNewsEntities>();
            mockContext.Setup(c => c.news).Returns(mockSet.Object);
            factory = new NewsFactory(mockContext.Object);
        }

        //TESTS

        [TestMethod]
        public void FindNewsPerAgencyListFindsTwoArticles()
        {
            testFactorySetup();
            List<int> agencies = new List<int>() { 1, 2 };
            var result = factory.FindNewsPerAgencyList(agencies);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<news>));
        }
    }
}
