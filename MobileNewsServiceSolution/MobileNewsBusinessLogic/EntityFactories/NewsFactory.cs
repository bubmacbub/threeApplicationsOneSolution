using log4net;
using MobileNewsModel.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MobileNewsBusinessLogic.Admin
{
    public class NewsFactory
    {
        protected static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ITS_MobileNewsEntities dbCtxt;


        public NewsFactory(ITS_MobileNewsEntities db)
        {
            dbCtxt = db;
        }

        public IEnumerable<news> FindNewsPerCategory(int categoryID)
        {
            IEnumerable<news> news = null;
            news = (from c in dbCtxt.category_news join n in dbCtxt.news on c.news_id equals n.news_id where c.category_id == categoryID select n);
            return news;
         }
    }
}