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
    public class category_serviceController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        // GET: api/category_service
        /*
        public IQueryable<category_service> Getcategory_service()
        {
            return db.category_service;
        }
        */

        //// NOTE: category_service does not reference agency so you will get ALL sub_categories regardless of agency or application
        //public IEnumerable<category_serviceViewModel> Getcategory_service(string lang = "en", DateTime? rdate = null)
        //{
        //    IQueryable<category_service> data;
        //    DateTime releaseDate = rdate ?? DateTime.MinValue;
        //    if (releaseDate == DateTime.MinValue)// if no rdate was passed in return all undeleted records
        //    {
        //        data = db.category_service.Where(c => c.logical_delete_date == null);
        //    }
        //    else // if rdate was passed in return all records on or after rdate
        //    {
        //        data = db.category_service.Where(c => c.modified_date >= rdate && c.logical_delete_date == null);
        //    }

        //    List<category_serviceViewModel> listOfCatServs = new List<category_serviceViewModel>();

        //    foreach (var cat_servs in data)
        //    {
        //        category_serviceViewModel catServs = new category_serviceViewModel();
        //        catServs.category_service_id = cat_servs.category_service_id;
        //        catServs.service_id = cat_servs.service_id;
        //        //catServs.sub_category_id = cat_servs.sub_category_id;
        //        catServs.category_id = cat_servs.category_id;
        //        catServs.created_date = cat_servs.created_date;
        //        catServs.modified_date = cat_servs.modified_date;
        //        catServs.logical_delete_date = cat_servs.logical_delete_date;
        //        listOfCatServs.Add(catServs);
        //    }
        //    IEnumerable<category_serviceViewModel> categoryServs = listOfCatServs;
        //    return categoryServs;
        //}

        //// GET: api/category_service/5
        //[ResponseType(typeof(category_service))]
        //public IHttpActionResult Getcategory_service(int id)
        //{
        //    category_service category_service = db.category_service.Find(id);
        //    if (category_service == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(category_service);
        //}

        //// PUT: api/category_service/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Putcategory_service(int id, category_service category_service)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != category_service.category_service_id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(category_service).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!category_serviceExists(id))
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

        //// POST: api/category_service
        //[ResponseType(typeof(category_service))]
        //public IHttpActionResult Postcategory_service(category_service category_service)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.category_service.Add(category_service);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = category_service.category_service_id }, category_service);
        //}

        //// DELETE: api/category_service/5
        //[ResponseType(typeof(category_service))]
        //public IHttpActionResult Deletecategory_service(int id)
        //{
        //    category_service category_service = db.category_service.Find(id);
        //    if (category_service == null)
        //    {
        //        return NotFound();
        //    }

        //    db.category_service.Remove(category_service);
        //    db.SaveChanges();

        //    return Ok(category_service);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool category_serviceExists(int id)
        //{
        //    return db.category_service.Count(e => e.category_service_id == id) > 0;
        //}
    }
}