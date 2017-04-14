using MobileNewsModel.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace NYSOFA_api.AdminAPI
{
    public class SubCategoryController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/SubCategory
        public IQueryable<sub_category> Getsub_category()
        {
            return db.sub_category;
        }

        // GET: api/SubCategory/5
        [ResponseType(typeof(sub_category))]
        public IHttpActionResult Getsub_category(int id)
        {
            sub_category sub_category = db.sub_category.Find(id);
            if (sub_category == null)
            {
                return NotFound();
            }

            return Ok(sub_category);
        }

        // PUT: api/SubCategory/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putsub_category(int id, sub_category sub_category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sub_category.sub_category_id)
            {
                return BadRequest();
            }

            db.Entry(sub_category).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sub_categoryExists(id))
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

        // POST: api/SubCategory
        [ResponseType(typeof(sub_category))]
        public IHttpActionResult Postsub_category(sub_category sub_category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.sub_category.Add(sub_category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sub_category.sub_category_id }, sub_category);
        }

        // DELETE: api/SubCategory/5
        [ResponseType(typeof(sub_category))]
        public IHttpActionResult Deletesub_category(int id)
        {
            sub_category sub_category = db.sub_category.Find(id);
            if (sub_category == null)
            {
                return NotFound();
            }

            db.sub_category.Remove(sub_category);
            db.SaveChanges();

            return Ok(sub_category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool sub_categoryExists(int id)
        {
            return db.sub_category.Count(e => e.sub_category_id == id) > 0;
        }
    }
}