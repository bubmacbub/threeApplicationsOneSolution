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
    public class ServiceController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/Service
        public IQueryable<service> Getservices()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.services;
        }

        // GET: api/Service/5
        [ResponseType(typeof(service))]
        public IHttpActionResult Getservice(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            service service = db.services.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // PUT: api/Service/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putservice(int id, service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != service.service_id)
            {
                return BadRequest();
            }

            db.Entry(service).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!serviceExists(id))
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

        // POST: api/Service
        [ResponseType(typeof(service))]
        public IHttpActionResult Postservice(service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.services.Add(service);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = service.service_id }, service);
        }

        // DELETE: api/Service/5
        [ResponseType(typeof(service))]
        public IHttpActionResult Deleteservice(int id)
        {
            service service = db.services.Find(id);
            if (service == null)
            {
                return NotFound();
            }

            db.services.Remove(service);
            db.SaveChanges();

            return Ok(service);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool serviceExists(int id)
        {
            return db.services.Count(e => e.service_id == id) > 0;
        }
    }
}