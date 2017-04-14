using MobileNewsModel.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace NYSOFA_api.AdminAPI
{
    public class CategoryLocationController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/CategoryLocation
        public IQueryable<category_location> Getcategory_location()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.category_location;
        }

        // GET: api/CategoryLocation/5
        [ResponseType(typeof(category_location))]
        public IHttpActionResult Getcategory_location(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            category_location category_location = db.category_location.Find(id);
            if (category_location == null)
            {
                return NotFound();
            }

            return Ok(category_location);
        }


        // GET: api/CategoryLocation/5
        [ResponseType(typeof(category_location))]
        public IHttpActionResult Categories(int categoryId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            category_location category_location = db.category_location.Find(categoryId);
            if (category_location == null)
            {
                return NotFound();
            }

            return Ok(category_location);
        }

        // PUT: api/CategoryLocation/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcategory_location(int id, category_location category_location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category_location.category_location_id)
            {
                return BadRequest();
            }

            db.Entry(category_location).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!category_locationExists(id))
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

        // POST: api/CategoryLocation
        [ResponseType(typeof(category_location))]
        public IHttpActionResult Postcategory_location(category_location category_location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.category_location.Add(category_location);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category_location.category_location_id }, category_location);
        }

        // DELETE: api/CategoryLocation/5
        [ResponseType(typeof(category_location))]
        public IHttpActionResult Deletecategory_location(int id)
        {
            category_location category_location = db.category_location.Find(id);
            if (category_location == null)
            {
                return NotFound();
            }

            db.category_location.Remove(category_location);
            db.SaveChanges();

            return Ok(category_location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool category_locationExists(int id)
        {
            return db.category_location.Count(e => e.category_location_id == id) > 0;
        }
    }
}