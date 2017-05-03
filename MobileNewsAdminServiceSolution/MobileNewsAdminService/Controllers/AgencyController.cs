using MobileNewsModel.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NYSOFA_api.AdminAPI
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class AgencyController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/Agency
        public IQueryable<agency> Getagencies()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.agencies;
        }

        // GET: api/Agency/5
        [ResponseType(typeof(agency))]
        public IHttpActionResult Getagency(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            agency agency = db.agencies.Find(id);
            if (agency == null)
            {
                return NotFound();
            }

            return Ok(agency);
        }

        // PUT: api/Agency/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putagency(int id, agency agency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agency.agency_id)
            {
                return BadRequest();
            }

            db.Entry(agency).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!agencyExists(id))
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

        // POST: api/Agency
        [ResponseType(typeof(agency))]
        public IHttpActionResult Postagency(agency agency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.agencies.Add(agency);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = agency.agency_id }, agency);
        }

        // DELETE: api/Agency/5
        [ResponseType(typeof(agency))]
        public IHttpActionResult Deleteagency(int id)
        {
            agency agency = db.agencies.Find(id);
            if (agency == null)
            {
                return NotFound();
            }

            db.agencies.Remove(agency);
            db.SaveChanges();

            return Ok(agency);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool agencyExists(int id)
        {
            return db.agencies.Count(e => e.agency_id == id) > 0;
        }
    }
}