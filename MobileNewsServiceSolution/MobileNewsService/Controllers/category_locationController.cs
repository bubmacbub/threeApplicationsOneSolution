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
    public class category_locationController : ApiController
    {
        private ITS_MobileNewsEntities db = new ITS_MobileNewsEntities();

        //// GET: api/category_location
        //// NOTE: category_location does not reference agency so you will get ALL sub_categories regardless of agency or application
        //public IEnumerable<category_locationViewModel> Getcategory_location(string lang = "en", string rdate = null)
        //{
        //    IQueryable<category_location> data;
            
        //    if (rdate == null)// if no rdate was passed in return all undeleted records
        //    {
        //        data = db.category_location.Where(c => c.logical_delete_date == null);
        //    }
        //    else // if rdate was passed in return all records on or after rdate
        //    {
        //        var releaseDate = DateTime.Parse(rdate);
        //        data = db.category_location.Where(c => c.modified_date >= releaseDate && c.logical_delete_date == null);
        //    }

        //    List<category_locationViewModel> listOfCatLocs = new List<category_locationViewModel>();

        //    foreach (var cat_locs in data)
        //    {
        //        category_locationViewModel catLocs = new category_locationViewModel();
        //        catLocs.category_location_id = cat_locs.category_location_id;
        //        catLocs.location_id = cat_locs.location_id;
        //        catLocs.category_id = cat_locs.category_id;
        //        //catLocs.sub_category_id = cat_locs.sub_category_id;
        //        catLocs.created_date = cat_locs.created_date;
        //        catLocs.modified_date = cat_locs.modified_date;
        //        catLocs.logical_delete_date = cat_locs.logical_delete_date;
        //        listOfCatLocs.Add(catLocs);
        //    }
        //    IEnumerable<category_locationViewModel> categoryLocs = listOfCatLocs;
        //    return categoryLocs;
        //}
        ///*
        //public IQueryable<category_location> Getcategory_location()
        //{
        //    return db.category_location;
        //}
        //*/

        //// GET: api/category_location/5
        //[ResponseType(typeof(category_location))]
        //public IHttpActionResult Getcategory_location(int id)
        //{
        //    category_location category_location = db.category_location.Find(id);
        //    if (category_location == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(category_location);
        //}

        //// PUT: api/category_location/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult Putcategory_location(int id, category_location category_location)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != category_location.category_location_id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(category_location).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!category_locationExists(id))
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

        //// POST: api/category_location
        //[ResponseType(typeof(category_location))]
        //public IHttpActionResult Postcategory_location(category_location category_location)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.category_location.Add(category_location);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = category_location.category_location_id }, category_location);
        //}

        //// DELETE: api/category_location/5
        //[ResponseType(typeof(category_location))]
        //public IHttpActionResult Deletecategory_location(int id)
        //{
        //    category_location category_location = db.category_location.Find(id);
        //    if (category_location == null)
        //    {
        //        return NotFound();
        //    }

        //    db.category_location.Remove(category_location);
        //    db.SaveChanges();

        //    return Ok(category_location);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool category_locationExists(int id)
        //{
        //    return db.category_location.Count(e => e.category_location_id == id) > 0;
        //}
    }
}