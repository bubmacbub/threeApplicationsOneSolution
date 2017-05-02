using log4net;
using MobileNewsModel.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MobileNewsBusinessLogic.Admin
{
    public class LocationFactory
    {
        protected static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ITS_MobileNewsEntities dbCtxt;

        /// <summary>
        /// Private so nobody makes a location factory with a db context
        /// </summary>
        private LocationFactory()
        {

        }

        /// <summary>
        /// Injecting the db context into this factory is the only way it should be used.
        /// </summary>
        /// <param name="db"></param>
        public LocationFactory(ITS_MobileNewsEntities db)
        {
            dbCtxt = db;
        }

        /// <summary>
        /// Find locations that are in the list of categories.  
        /// Might need to add logic to limit categories to only the ones the user's agency is tied to but going to go on a trust relationship at this time.
        /// something like this SQL:
        /// select l.location_name , cl.category_id from dbo.location l , dbo.category_location cl where cl.category_id in (1,2) and l.location_id = cl.location_id
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        public IEnumerable<location> FindLocations(List<int> categories)
        {
            IEnumerable<location> locations = null;
            //Iqueryable<location> locQuery = from loc in dbCtxt.locations where loc.category_location = (from cl in dbCtxt.category_location where cl.category_id == categories select cl) select loc;
            //var k = objlist.Where(u => strset.Contains(u.somecol)).ToList();
            IQueryable<category_location> catLocs = dbCtxt.category_location.Where(c => categories.Contains(c.category_id));
            locations = from l in dbCtxt.locations
                           join cl in dbCtxt.category_location on l.location_id equals cl.location_id
                           where categories.Contains(cl.category_id)
                           select l;

            /*
             * from c in svcContext.ContactSet
                    join a in svcContext.AccountSet
                    on c.ContactId equals a.PrimaryContactId.Id
                    where a.Name.Contains("Contoso")
                    where c.LastName.Contains("Smith")
                    select new
                    {
                     account_name = a.Name,
                     contact_name = c.LastName
                    };
                    */
            return locations;
        }

        public IEnumerable<location> FindLocationsByAgencyList(List<int> agencies, string rdate = null)
        {
            IEnumerable<location> locations = null;
            if (rdate == null)
            {
                // If no date is passed in the app is initializing, do not send deleted records
                locations = dbCtxt.locations.Where(c => agencies.Contains(c.agency_id ?? 0) && c.logical_delete_date == null);
            }
            else
            {
                // If a date is passed in the app is updating, send all recent records, including deleted ones. 
                // The app needs to know about recent deletions
                var releaseDate = System.DateTime.Parse(rdate);
                locations = dbCtxt.locations.Where(c => agencies.Contains(c.agency_id ?? 0) && c.modified_date >= releaseDate);
            }

            return locations;
        }

    }
}
 
 