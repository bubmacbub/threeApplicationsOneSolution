using MobileNewsBusinessLogic.Admin;
using MobileNewsModel.Entities;
using MobileNewsServices.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace MobileNewsServices.Controllers
{
    public class application_agencyController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        public application_agencyController() { }

        public application_agencyController(ITS_MobileNewsEntities context)
        {
            db = context;
        }

        // GET: api/application_agency
        public IEnumerable<application_agencyViewModel> Getapplication_agency(int aid, string lang = "en", string rdate = null)
        {
            IEnumerable<application_agency> data;
            ApplicationAgencyFactory aaFactory = new ApplicationAgencyFactory(db);
            data = aaFactory.GetApplicationAgency(aid, lang, rdate);
            List<application_agencyViewModel> listOfAppAgencies = new List<application_agencyViewModel>();

            foreach (var appAgency in data)
            {
                application_agencyViewModel app_agency = new application_agencyViewModel();
                app_agency.application_agency_id = appAgency.application_agency_id;
                app_agency.application_id = appAgency.application_id;
                app_agency.agency_id = appAgency.agency_id;
                app_agency.created_date = appAgency.created_date;
                app_agency.modified_date = appAgency.modified_date;
                app_agency.logical_delete_date = appAgency.logical_delete_date;
                listOfAppAgencies.Add(app_agency);
            }
            IEnumerable<application_agencyViewModel> AppAgencies = listOfAppAgencies;
            return AppAgencies;
        }
        [HttpGet]
        public List<int> AgencyIdList(int aid)
        {
            ApplicationAgencyFactory aaFactory = new ApplicationAgencyFactory(db);
            return aaFactory.GetAllApplicationsAgencyIds(aid);
        }

        /*
        public IQueryable<application_agency> Getapplication_agency()
        {
            return db.application_agency;
        }
        */
        // GET: api/application_agency/Details/5
        [HttpGet]
        [ResponseType(typeof(application_agencyViewModel))]
        public IHttpActionResult Details(int id)
        {
            ApplicationAgencyFactory aaFactory = new ApplicationAgencyFactory(db);
            application_agency application_agency = aaFactory.Find(id);
            if (application_agency != null)
            {
                application_agencyViewModel aaViewModel = new application_agencyViewModel();
                aaViewModel.application_agency_id = application_agency.application_agency_id;
                aaViewModel.application_id = application_agency.application_id;
                aaViewModel.agency_id = application_agency.agency_id;
                aaViewModel.created_date = application_agency.created_date;
                aaViewModel.modified_date = application_agency.modified_date;
                aaViewModel.logical_delete_date = application_agency.logical_delete_date;

                return Ok(aaViewModel);
            }
            else
            {
                return NotFound();
            }    
        }

        //// GET: api/application_agency/5
        //[ResponseType(typeof(application_agency))]
        //public IHttpActionResult Getapplication_agency(int id)
        //{
        //    application_agency application_agency = db.application_agency.Find(id);
        //    if (application_agency == null)
        //    {
        //        //return NotFound();
        //        return Ok(application_agency);
        //    }

        //    return Ok(application_agency);
        //}

        //// PUT: api/application_agency/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Putapplication_agency(int id, application_agency application_agency)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != application_agency.application_agency_id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(application_agency).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!application_agencyExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/application_agency
        //[ResponseType(typeof(application_agency))]
        //public IHttpActionResult Postapplication_agency(application_agency application_agency)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.application_agency.Add(application_agency);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = application_agency.application_agency_id }, application_agency);
        //}

        //// DELETE: api/application_agency/5
        //[ResponseType(typeof(application_agency))]
        //public IHttpActionResult Deleteapplication_agency(int id)
        //{
        //    application_agency application_agency = db.application_agency.Find(id);
        //    if (application_agency == null)
        //    {
        //        return NotFound();
        //    }

        //    db.application_agency.Remove(application_agency);
        //    db.SaveChanges();

        //    return Ok(application_agency);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool application_agencyExists(int id)
        {
            bool returnValue = false;
            ApplicationAgencyFactory aaFactory = new ApplicationAgencyFactory(db);
            returnValue = aaFactory.ApplicationAgencyExists(id);
            
            return returnValue;
        }
    }
}