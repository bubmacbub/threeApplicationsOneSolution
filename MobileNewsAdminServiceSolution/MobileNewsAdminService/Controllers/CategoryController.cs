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
    public class CategoryController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/Category
        public IQueryable<category> Getcategories()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.categories;
        }

        
        // GET: api/Category/5
        [ResponseType(typeof(category))]
        public IHttpActionResult Getcategory(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            category category = db.categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpGet]
        public IEnumerable<category> AllNewsCategories(int agencyId)
        {
            //Find all the distinct categories that are in the category_news table
            db.Configuration.LazyLoadingEnabled = false;
            CategoryFactory catFactory = new CategoryFactory(db);
            return catFactory.FindNewsCategories(agencyId);
        }

        [HttpGet]
        public IEnumerable<category> ForNews(int newsId)
        {
            //TODO: look up all the categories associated with the given news id
            db.Configuration.LazyLoadingEnabled = false;
            CategoryFactory catFactory = new CategoryFactory(db);
            return catFactory.FindAgencyCategories(newsId);
        }

        [HttpGet]
        public IEnumerable<category> Agency(int agencyId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            CategoryFactory catFactory = new CategoryFactory(db);
            return catFactory.FindAgencyCategories(agencyId);
        }
        // PUT: api/Category/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcategory(int id, category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.category_id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!categoryExists(id))
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

        // POST: api/Category
        [ResponseType(typeof(category))]
        public IHttpActionResult Postcategory(category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.category_id }, category);
        }

        // DELETE: api/Category/5
        [ResponseType(typeof(category))]
        public IHttpActionResult Deletecategory(int id)
        {
            category category = db.categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            db.categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool categoryExists(int id)
        {
            return db.categories.Count(e => e.category_id == id) > 0;
        }
    }
}