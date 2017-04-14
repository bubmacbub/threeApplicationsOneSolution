using MobileNewsModel.Entities;
using MobileNewsServices.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace MobileNewsServices.Controllers
{
    public class category_newsController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        //// NOTE: category_news does not reference agency so you will get ALL sub_categories regardless of agency or application
        //public IEnumerable<category_newsViewModel> Getcategory_news(string lang = "en", DateTime? rdate = null)
        //{
        //    IQueryable<category_news> data;
        //    DateTime releaseDate = rdate ?? DateTime.MinValue;
        //    if (releaseDate == DateTime.MinValue)// if no rdate was passed in return all undeleted records
        //    {
        //        data = db.category_news.Where(c => c.logical_delete_date == null);
        //    }
        //    else // if rdate was passed in return all records on or after rdate
        //    {
        //        data = db.category_news.Where(c => c.modified_date >= rdate && c.logical_delete_date == null);
        //    }

        //    List<category_newsViewModel> listOfCatNews = new List<category_newsViewModel>();

        //    foreach (var cat_news in data)
        //    {
        //        category_newsViewModel catNews = new category_newsViewModel();
        //        catNews.category_news_id = cat_news.category_news_id;
        //        catNews.news_id = cat_news.news_id;
        //        //catNews.sub_category_id = cat_news.sub_category_id;
        //        catNews.category_id = cat_news.category_id;
        //        catNews.created_date = cat_news.created_date;
        //        catNews.modified_date = cat_news.modified_date;
        //        catNews.logical_delete_date = cat_news.logical_delete_date;
        //        listOfCatNews.Add(catNews);
        //    }
        //    IEnumerable<category_newsViewModel> categoryNews = listOfCatNews;
        //    return categoryNews;
        //}

        //// GET: api/category_news
        ///*
        //public IQueryable<category_news> Getcategory_news()
        //{
        //    return db.category_news;
        //}
        //*/
        //// GET: api/category_news/5
        //[ResponseType(typeof(category_news))]
        //public IHttpActionResult Getcategory_news(int id)
        //{
        //    category_news category_news = db.category_news.Find(id);
        //    if (category_news == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(category_news);
        //}

        //// PUT: api/category_news/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Putcategory_news(int id, category_news category_news)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != category_news.category_news_id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(category_news).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!category_newsExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/category_news
        //[ResponseType(typeof(category_news))]
        //public IHttpActionResult Postcategory_news(category_news category_news)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.category_news.Add(category_news);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = category_news.category_news_id }, category_news);
        //}

        //// DELETE: api/category_news/5
        //[ResponseType(typeof(category_news))]
        //public IHttpActionResult Deletecategory_news(int id)
        //{
        //    category_news category_news = db.category_news.Find(id);
        //    if (category_news == null)
        //    {
        //        return NotFound();
        //    }

        //    db.category_news.Remove(category_news);
        //    db.SaveChanges();

        //    return Ok(category_news);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool category_newsExists(int id)
        //{
        //    return db.category_news.Count(e => e.category_news_id == id) > 0;
        //}
    }
}