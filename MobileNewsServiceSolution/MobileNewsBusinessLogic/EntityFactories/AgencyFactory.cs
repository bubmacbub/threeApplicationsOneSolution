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
        private AgencyFactory()
        {

        }

        public AgencyFactory(ITS_MobileNewsEntities dbCtxt)
        {
            db = dbCtxt;
        }

        public IEnumerable<agency> Getagencies(int aid, string rdate = null)
        {
            // aid now refers to APP_ID get list of agencies for app
            DbRawSqlQuery<int> agIdList = db.Database.SqlQuery<int>("select distinct agency_id from application_agency where application_id = @p0", aid);
            List<int> agency_id_list = new List<int>();
            foreach (var agId in agIdList)
            {
                agency_id_list.Add(agId);
            }



            IQueryable<agency> data;

            //DateTime releaseDate = rdate ?? DateTime.MinValue;
            if (rdate == null)// if no rdate was passed in return all undeleted records
            {
                data = db.agencies.Where(c => agency_id_list.Contains(c.agency_id) && c.logical_delete_date == null);
            }
            else // if rdate was passed in return all records on or after rdate
            {
                var releaseDate = DateTime.Parse(rdate);
                data = db.agencies.Where(c => agency_id_list.Contains(c.agency_id) && c.modified_date >= releaseDate && c.logical_delete_date == null);
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
