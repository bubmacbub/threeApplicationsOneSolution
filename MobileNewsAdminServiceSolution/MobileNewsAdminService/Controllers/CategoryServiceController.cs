using MobileNewsBusinessLogic.Admin;
using MobileNewsModel.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NYSOFA_api.AdminAPI
{
    [EnableCors(origins: "http://localhost:57080", headers: "*", methods: "*", SupportsCredentials = true)]
    public class CategoryServiceController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/CategoryService
        public IQueryable<category_service> Getcategory_service()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.category_service;
        }

        // GET: api/CategoryService/5
        [ResponseType(typeof(category_service))]
        public IHttpActionResult Getcategory_service(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            category_service category_service = db.category_service.Find(id);
            if (category_service == null)
            {
                return NotFound();
            }

            return Ok(category_service);
        }

        /// <summary>
        /// Get the category service object for a particular service, which is just a foreign key
        /// </summary>
        /// <param name="agencyId"></param>
        /// <param name="serviceId"></param>
        /// <returns>list of category service objects</returns>
        [HttpGet]
        public IEnumerable<category> CategoriesForService(int agencyId, int serviceId)
        {
            System.Diagnostics.Debug.WriteLine("CategoriesForService webapi call");
            System.Diagnostics.Debug.WriteLine(User.Identity.Name);
            System.Diagnostics.Debug.WriteLine(RequestContext.Principal.Identity.Name);
            IEnumerable<category> cats;
            db.Configuration.LazyLoadingEnabled = false;
            CategoryFactory factory = new CategoryFactory(db);
            cats = factory.FindCategoriesForService(serviceId);
            return cats;
        }

        // PUT: api/CategoryService/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcategory_service(int id, category_service category_service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category_service.category_service_id)
            {
                return BadRequest();
            }

            db.Entry(category_service).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!category_serviceExists(id))
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

        // POST: api/CategoryService
        [ResponseType(typeof(category_service))]
        public IHttpActionResult Postcategory_service(category_service category_service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.category_service.Add(category_service);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category_service.category_service_id }, category_service);
        }

        // DELETE: api/CategoryService/5
        [ResponseType(typeof(category_service))]
        public IHttpActionResult Deletecategory_service(int id)
        {
            category_service category_service = db.category_service.Find(id);
            if (category_service == null)
            {
                return NotFound();
            }

            db.category_service.Remove(category_service);
            db.SaveChanges();

            return Ok(category_service);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool category_serviceExists(int id)
        {
            return db.category_service.Count(e => e.category_service_id == id) > 0;
        }
    }
}