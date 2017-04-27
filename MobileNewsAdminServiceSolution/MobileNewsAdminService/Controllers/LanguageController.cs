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
    [EnableCors(origins: "http://localhost:61182", headers: "*", methods: "*", SupportsCredentials = true)]
    public class LanguageController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/Language
        public IQueryable<language> Getlanguages()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return db.languages;
        }

        // GET: api/Language/5
        [ResponseType(typeof(language))]
        public IHttpActionResult Getlanguage(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            language language = db.languages.Find(id);
            if (language == null)
            {
                return NotFound();
            }

            return Ok(language);
        }

        // PUT: api/Language/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlanguage(int id, language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != language.language_id)
            {
                return BadRequest();
            }

            db.Entry(language).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!languageExists(id))
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

        // POST: api/Language
        [ResponseType(typeof(language))]
        public IHttpActionResult Postlanguage(language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.languages.Add(language);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = language.language_id }, language);
        }

        // DELETE: api/Language/5
        [ResponseType(typeof(language))]
        public IHttpActionResult Deletelanguage(int id)
        {
            language language = db.languages.Find(id);
            if (language == null)
            {
                return NotFound();
            }

            db.languages.Remove(language);
            db.SaveChanges();

            return Ok(language);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool languageExists(int id)
        {
            return db.languages.Count(e => e.language_id == id) > 0;
        }
    }
}