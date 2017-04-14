using MobileNewsModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileNewsBusinessLogic.Admin
{
    public class ApplicationAgencyFactory
    {
        private ITS_MobileNewsEntities db;

        private ApplicationAgencyFactory()
        {

        }

        public ApplicationAgencyFactory(ITS_MobileNewsEntities dbCtxt)
        {
            db = dbCtxt;
        }

        public application_agency Find(int id)
        {
            application_agency application_agency = db.application_agency.Where(c => c.application_agency_id == id && c.logical_delete_date == null).FirstOrDefault();

            return application_agency;
        }


        // GET: api/application_agency
        public IEnumerable<application_agency> GetApplicationAgency(int aid, string lang = "en", string rdate = null)
        {
            IQueryable<application_agency> data;
            //DateTime releaseDate = rdate ?? DateTime.MinValue;
            if (rdate == null)// if no rdate was passed in return all undeleted records
            {
                data = db.application_agency.Where(c => c.application_id == aid && c.logical_delete_date == null);
            }
            else // if rdate was passed in return all records on or after rdate
            {
                var releaseDate = DateTime.Parse(rdate);
                data = db.application_agency.Where(c => c.application_id == aid && c.modified_date >= releaseDate && c.logical_delete_date == null);
            }

            return data;
        }

        public List<int> GetAllApplicationsAgencyIds(int aid)
        {
            List<int> agencyList = new List<int>();
            IQueryable<application_agency> Agencies = db.application_agency.Where(c => c.application_id == aid && c.logical_delete_date == null);
            foreach (var agencyItem in Agencies)
            {
                if (!agencyList.Contains(agencyItem.agency_id))
                {
                    agencyList.Add(agencyItem.agency_id);
                }
            }
            return agencyList;
        }

        public bool ApplicationAgencyExists(int id)
        {
            return db.application_agency.Count(e => e.application_agency_id == id) > 0;
        }
    }
}
