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
    public class MobileNewsController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/MobileNews
        public IQueryable<news> Getnews()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.news;
        }

        // GET: api/MobileNews/5
        [ResponseType(typeof(news))]
        public IHttpActionResult Getnews(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            news news = db.news.Find(id);
            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }

        [HttpGet]
        public IEnumerable<news> FindNewsForCategory(int categoryID)
        {
            IEnumerable<news> news = null;
            db.Configuration.LazyLoadingEnabled = false;
            NewsFactory newsFactory = new NewsFactory(db);
            news = newsFactory.FindNewsPerCategory(categoryID);
            return news;
        }

        // PUT: api/MobileNews/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putnews(int id, news news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != news.news_id)
            {
                return BadRequest();
            }

            db.Entry(news).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!newsExists(id))
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

        // POST: api/MobileNews
        [ResponseType(typeof(news))]
        public IHttpActionResult Postnews(news news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.news.Add(news);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = news.news_id }, news);
        }

        // DELETE: api/MobileNews/5
        [ResponseType(typeof(news))]
        public IHttpActionResult Deletenews(int id)
        {
            news news = db.news.Find(id);
            if (news == null)
            {
                return NotFound();
            }

            db.news.Remove(news);
            db.SaveChanges();

            return Ok(news);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool newsExists(int id)
        {
            return db.news.Count(e => e.news_id == id) > 0;
        }
    }
}