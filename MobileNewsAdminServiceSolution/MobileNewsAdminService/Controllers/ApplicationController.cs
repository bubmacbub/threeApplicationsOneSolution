using MobileNewsModel.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace NYSOFA_api.AdminAPI
{
    public class ApplicationController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/Application
        public IQueryable<application> Getapplications()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.applications;
        }

        // GET: api/Application/5
        [ResponseType(typeof(application))]
        public IHttpActionResult Getapplication(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            application application = db.applications.Find(id);
            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        // PUT: api/Application/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putapplication(int id, application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != application.application_id)
            {
                return BadRequest();
            }

            db.Entry(application).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!applicationExists(id))
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

        // POST: api/Application
        [ResponseType(typeof(application))]
        public IHttpActionResult Postapplication(application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.applications.Add(application);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = application.application_id }, application);
        }

        // DELETE: api/Application/5
        [ResponseType(typeof(application))]
        public IHttpActionResult Deleteapplication(int id)
        {
            application application = db.applications.Find(id);
            if (application == null)
            {
                return NotFound();
            }

            db.applications.Remove(application);
            db.SaveChanges();

            return Ok(application);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool applicationExists(int id)
        {
            return db.applications.Count(e => e.application_id == id) > 0;
        }
    }
}