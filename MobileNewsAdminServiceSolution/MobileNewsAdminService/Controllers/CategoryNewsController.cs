using MobileNewsModel.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace NYSOFA_api.AdminAPI
{
    public class CategoryNewsController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/CategoryNews
        public IQueryable<category_news> Getcategory_news()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.category_news;
        }

        // GET: api/CategoryNews/5
        [ResponseType(typeof(category_news))]
        public IHttpActionResult Getcategory_news(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            category_news category_news = db.category_news.Find(id);
            if (category_news == null)
            {
                return NotFound();
            }

            return Ok(category_news);
        }

        // PUT: api/CategoryNews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcategory_news(int id, category_news category_news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category_news.category_news_id)
            {
                return BadRequest();
            }

            db.Entry(category_news).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!category_newsExists(id))
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

        // POST: api/CategoryNews
        [ResponseType(typeof(category_news))]
        public IHttpActionResult Postcategory_news(category_news category_news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.category_news.Add(category_news);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category_news.category_news_id }, category_news);
        }

        // DELETE: api/CategoryNews/5
        [ResponseType(typeof(category_news))]
        public IHttpActionResult Deletecategory_news(int id)
        {
            category_news category_news = db.category_news.Find(id);
            if (category_news == null)
            {
                return NotFound();
            }

            db.category_news.Remove(category_news);
            db.SaveChanges();

            return Ok(category_news);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool category_newsExists(int id)
        {
            return db.category_news.Count(e => e.category_news_id == id) > 0;
        }
    }
}