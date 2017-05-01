using MobileNewsModel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MobileNewsBusinessLogic.Admin
{
    public class AgencyFactory
    {
        private ITS_MobileNewsEntities db = null;
        private ApplicationAgencyFactory aaf = null;
        private AgencyFactory()
        {

        }

        public AgencyFactory(ITS_MobileNewsEntities dbCtxt)
        {
            db = dbCtxt;
        }

        public IEnumerable<agency> Getagencies(List<int> agency_id_list, string rdate = null)
        {
            
            IQueryable<agency> data;

            //DateTime releaseDate = rdate ?? DateTime.MinValue;
            if (rdate == null)// if no rdate was passed in return all undeleted records
            {
                data = db.agencies.Where(c => agency_id_list.Contains(c.agency_id) && c.logical_delete_date == null); // && c.logical_delete_date == null
            }
            else // if rdate was passed in return all records on or after rdate
            {
                var releaseDate = DateTime.Parse(rdate);
                data = db.agencies.Where(c => agency_id_list.Contains(c.agency_id) && c.modified_date >= releaseDate);
            }


            return data;
        }

        public agency getAgency(int id)
        {
            agency a = db.agencies.Find(id);
            if(a.agency_id < 0)
            { a = null; }
            return a;
        }
    }
}
