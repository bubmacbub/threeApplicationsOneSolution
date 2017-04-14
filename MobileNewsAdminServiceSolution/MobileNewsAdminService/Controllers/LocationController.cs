using MobileNewsBusinessLogic.Admin;
using MobileNewsModel.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace NYSOFA_api.AdminAPI
{
    public class LocationController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/Location
        public IQueryable<location> Getlocations()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.locations;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="categoryId"></param>
        /// <returns>List of locations according to the category</returns>
        [HttpGet]
        public IEnumerable<category_location> Categories(int agencyId, int categoryId)
        {
            IEnumerable<category_location> cats;
            db.Configuration.LazyLoadingEnabled = false;
            CategoryFactory factory = new CategoryFactory(db);
            cats = factory.FindLocationCategories(agencyId,categoryId);
            return cats;
        }

        /// <summary>
        /// The user's agency they are workng with.  The list of categories they want to filter the locations.  
        /// Category list comes from the multiselect requested via the onChange event
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="categories"></param>
        /// <returns>A list of locations that are filtered according to the category location association</returns>
        [HttpGet]
        public IEnumerable<location> FilterLocation(int agencyId, [FromUri] List<int> categories)
        {
            IEnumerable<location> locations=null;
            db.Configuration.LazyLoadingEnabled = false;
            LocationFactory lf = new LocationFactory(db);
            locations = lf.FindLocations(categories);
            return locations;
        }
        // GET: api/Location/5
        [ResponseType(typeof(location))]
        public IHttpActionResult Getlocation(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            location location = db.locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/Location/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlocation(int id, location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.location_id)
            {
                return BadRequest();
            }

            db.Entry(location).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!locationExists(id))
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

        // POST: api/Location
        [ResponseType(typeof(location))]
        public IHttpActionResult Postlocation(location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.locations.Add(location);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = location.location_id }, location);
        }

        // DELETE: api/Location/5
        [ResponseType(typeof(location))]
        public IHttpActionResult Deletelocation(int id)
        {
            location location = db.locations.Find(id);
            if (location == null)
            {
                return NotFound();
            }

            db.locations.Remove(location);
            db.SaveChanges();

            return Ok(location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool locationExists(int id)
        {
            return db.locations.Count(e => e.location_id == id) > 0;
        }
    }
}