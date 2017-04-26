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

        public IEnumerable<service> FindservicesPerAgencyIdList(List<int> agencyList, string lang = "en", string rdate = null)
        {
            IEnumerable<service> services = null;
            if(rdate == null)
            {
                services = dbCtxt.services.Where(c => agencyList.Contains(c.agency_id ?? 0));
            }
            else
            {
                var releaseDate = DateTime.Parse(rdate);
                services = dbCtxt.services.Where(c => agencyList.Contains(c.agency_id ?? 0) && releaseDate >= c.created_date);
            }
            
            return services;
         }
    }
}