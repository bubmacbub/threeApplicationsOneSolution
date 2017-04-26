using log4net;
using MobileNewsModel.Entities;
using System;
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

        public IEnumerable<news> FindNewsPerAgencyList(List<int> agency_id_list, string rdate = null)
        {
            IEnumerable<news> news;
                if(rdate == null)
            {
                //news = dbCtxt.news.Where(c => agency_id_list.Contains(c.agency_id)).OrderByDescending(c => c.publish_date);
                news = dbCtxt.news;
            }
            else
            {
                var releaseDate = DateTime.Parse(rdate);
                news = dbCtxt.news;
                //news = dbCtxt.news.Where(c => agency_id_list.Contains(c.agency_id) && c.modified_date >= releaseDate).OrderByDescending(c => c.publish_date);
            }
            
            return news;
        }
    }
}