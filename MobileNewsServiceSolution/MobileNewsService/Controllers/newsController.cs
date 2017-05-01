using MobileNewsBusinessLogic.Admin;
using MobileNewsModel.Entities;
using MobileNewsService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace MobileNewsService.Controllers
{
    public class newsController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();
        public newsController() { }

        public newsController(ITS_MobileNewsEntities context)
        {
            db = context;
        }


        // GET: api/news?aid=1
        [ResponseType(typeof(IEnumerable<newsViewModel>))]
        public IHttpActionResult Getnews(int aid, string lang = "en", string rdate = null)
        {
            // aid now refers to APP_ID get list of agencies for app
            ApplicationAgencyFactory ApplicationAgencyFactory = new ApplicationAgencyFactory(db);
            List<int> agency_id_list = ApplicationAgencyFactory.GetAllApplicationsAgencyIds(aid);

            NewsFactory newsfactory = new NewsFactory(db);
            IEnumerable<news> newsQryResult = newsfactory.FindNewsPerAgencyList(agency_id_list, rdate);
            
            if (rdate == null)// if no rdate was passed in return all undeleted records
            {
                newsQryResult = db.news.Where(c => agency_id_list.Contains(c.agency_id)).OrderByDescending(c => c.publish_date);
            }
            else // if rdate was passed in return all records on or after rdate
            {
                DateTime releaseDate = DateTime.Parse(rdate);
                newsQryResult = db.news.Where(c => agency_id_list.Contains(c.agency_id) && c.modified_date >= releaseDate).OrderByDescending(c => c.publish_date);
            }
            if (newsQryResult == null) { return NotFound(); }

            var agency = aid;
            
            //newsQryResult = db.news;
            if(newsQryResult == null) { return NotFound(); }
            List<newsViewModel> listOfNews = new List<newsViewModel>();

            foreach (var article in newsQryResult)
            {
                newsViewModel News = new newsViewModel();
                News.news_id = article.news_id;
                News.agency_id = article.agency_id;
                //News.language_id = article.language_id;
                //News.archive_flag = article.archive_flag;
                News.news_title = article.news_title;
                //News.news_content = article.news_content;
                //News.news_image = article.news_image;
                News.publish_date = article.publish_date;
                News.created_date = article.created_date;
                News.modified_date = article.modified_date;
                News.logical_delete_date = article.logical_delete_date;
                listOfNews.Add(News);
            }
            IEnumerable<newsViewModel> newsArticles = listOfNews;

            return Ok(newsArticles);
        }

        // GET: api/news/5
        [ResponseType(typeof(news))]
        public IHttpActionResult Details(int id)
        {
            news news = db.news.Find(id);
            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }

        // PUT: api/news/5
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

        // POST: api/news
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

        // DELETE: api/news/5
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