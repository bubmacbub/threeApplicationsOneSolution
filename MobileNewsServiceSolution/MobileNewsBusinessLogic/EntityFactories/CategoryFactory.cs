using log4net;
using MobileNewsModel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MobileNewsBusinessLogic.Admin
{

    public class CategoryFactory
    {
        protected static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ITS_MobileNewsEntities dbCtxt;
        /// <summary>
        /// 
        /// The proper way of creating this class is to inject it with your db context
        /// </summary>
        /// <param name="db"></param>
        public CategoryFactory(ITS_MobileNewsEntities db)
        {
            dbCtxt = db;
        }

        private CategoryFactory()
        {

        }

        public IEnumerable<category> FindNewsCategories(int agencyId)
        {
            //not sure if we are going to be making more agencies so no real check on this compared to the user that is logged in
            //if so then there would be an association with what a user can do with an agency or agencies
            dbCtxt.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            IQueryable<int> catNews = dbCtxt.category_news.Select(c => c.category_id).Distinct();
            IEnumerable<category> cats = null;
            cats = from c in dbCtxt.categories
                   where catNews.Contains(c.category_id)
                   select c;
            return cats;
        }

        ///// select loc.agency_id, loc.location_id, cl.location_id, c.category_name, c.agency_id from dbo.location loc 
        ///// left join dbo.category_location cl on loc.location_id=cl.location_id
        ///// right join dbo.category c on cl.category_id = c.category_id
        ///// where c.category_id= 1;
        ///// 


        /// <summary>
        /// 
        /// Find all the categories for a given agency
        /// </summary>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        public IEnumerable<category_location> FindLocationCategories(int agencyId)
        {
            List<category> cats = new List<category>();
            IQueryable<category> allCats;
            IEnumerable<category_location> agencyLocationCategories;

            allCats = (from c in dbCtxt.categories where c.agency_id == agencyId select c);


            agencyLocationCategories = from cl in dbCtxt.category_location
                                       join cat in allCats
                                       on cl.category_id equals cat.category_id
                                       select cl;


            return (IEnumerable<category_location>)agencyLocationCategories;
        }

        public IEnumerable<category> FindCategoriesForService(int serviceId)
        {
            IEnumerable<category> cats = from c in dbCtxt.categories
                                         join cs in dbCtxt.category_service.Where(cs => cs.service_id == serviceId) on c.category_id equals cs.category_id
                                         select c;
            return cats;
        }

        public IEnumerable<category_location> FindLocationCategories(int agencyId, int categoryId)
        {
            IQueryable<category> allCats;
            IEnumerable<category_location> agencyLocationCategories;
            allCats = (from c in dbCtxt.categories where c.agency_id == agencyId select c);
            agencyLocationCategories = from cl in dbCtxt.category_location
                                       join cat in allCats
                                       on cl.category_id equals cat.category_id
                                       select cl;
            IEnumerable<category_location> specificCatLoc = agencyLocationCategories.Where(locCat => locCat.category_id == categoryId);

            return specificCatLoc;
        }

        public IEnumerable<category> FindAgencyCategories(int agencyId)
        {
            IEnumerable<category> cats = dbCtxt.categories;
            cats = cats.Where(c => c.agency_id == agencyId);
            return cats;
        }

        public IEnumerable<category> GetApplicationsCategories(int aid, string rdate = null)
        {
            // aid now refers to APP_ID get list of agencies for app
            DbRawSqlQuery<int> agencies = dbCtxt.Database.SqlQuery<int>("select distinct agency_id from application_agency where application_id = @p0", aid);
            List<int> agency_id_list = new List<int>();
            foreach (var agId in agencies)
            {
                agency_id_list.Add(agId);
            }
            IQueryable<category> data;

            if (rdate != null)
            {
                var releaseDate = DateTime.Parse(rdate);
                data = dbCtxt.categories.Where(c => agency_id_list.Contains(c.agency_id) && c.created_date > releaseDate);
            }
            else
            {
                data = dbCtxt.categories.Where(c => agency_id_list.Contains(c.agency_id));
            }
            return data;
        }

        public category Find(int id)
        {
            return dbCtxt.categories.Find(id);
        }
    }
}