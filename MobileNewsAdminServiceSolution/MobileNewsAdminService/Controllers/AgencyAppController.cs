using MobileNewsModel.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace NYSOFA_api.AdminAPI
{

    public class AgencyAppController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/AgencyApp
        public IQueryable<application_agency> Getapplication_agency()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.application_agency;
        }

        // GET: api/AgencyApp/5
        [ResponseType(typeof(application_agency))]
        public IHttpActionResult Getapplication_agency(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            application_agency application_agency = db.application_agency.Find(id);
            if (application_agency == null)
            {
                return NotFound();
            }

            return Ok(application_agency);
        }

        // PUT: api/AgencyApp/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putapplication_agency(int id, application_agency application_agency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != application_agency.application_agency_id)
            {
                return BadRequest();
            }

            db.Entry(application_agency).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!application_agencyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AgencyApp
        [ResponseType(typeof(application_agency))]
        public IHttpActionResult Postapplication_agency(application_agency application_agency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.application_agency.Add(application_agency);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = application_agency.application_agency_id }, application_agency);
        }

        // DELETE: api/AgencyApp/5
        [ResponseType(typeof(application_agency))]
        public IHttpActionResult Deleteapplication_agency(int id)
        {
            application_agency application_agency = db.application_agency.Find(id);
            if (application_agency == null)
            {
                return NotFound();
            }

            db.application_agency.Remove(application_agency);
            db.SaveChanges();

            return Ok(application_agency);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool application_agencyExists(int id)
        {
            return db.application_agency.Count(e => e.application_agency_id == id) > 0;
        }
    }
}