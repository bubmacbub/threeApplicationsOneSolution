using log4net;
using MobileNewsModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileNewsBusinessLogic.Admin
{
    public class ServiceFactory
    {
        protected static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private ITS_MobileNewsEntities dbCtxt;


        public ServiceFactory(ITS_MobileNewsEntities db)
        {
            dbCtxt = db;
        }

        public IEnumerable<service> FindservicesPerAgencyIdList(List<int> agencyList, string rdate = null, string lang = "en")
        {
            IEnumerable<service> services = null;
            if(rdate == null)
            {
                // If no date is passed in the app is initializing, do not send deleted records

                services = dbCtxt.services.Where(c => agencyList.Contains(c.agency_id ?? 0) && c.logical_delete_date == null);
            }
            else
            {
                // If a date is passed in the app is updating, send all recent records, including deleted ones. 
                // The app needs to know about recent deletions

                var releaseDate = DateTime.Parse(rdate);
                services = dbCtxt.services.Where(c => agencyList.Contains(c.agency_id ?? 0) && c.created_date >= releaseDate);
            }
            
            return services;
         }
    }
}